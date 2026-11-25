using System;
using System.Collections.Generic;
using System.Linq;
using BudgetBuddy.Data;
using BudgetBuddy.Models;

namespace BudgetBuddy.Services
{
    public class TransactionManager
    {
        private readonly DatabaseHelper _db;
        private readonly BudgetManager _budgetManager;

        public TransactionManager(DatabaseHelper db, BudgetManager budgetManager)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _budgetManager = budgetManager ?? throw new ArgumentNullException(nameof(budgetManager));
        }

        public List<Transaction> GetAllTransactions() => _db.GetAllTransactions();

        public List<Category> GetAllCategories() => _db.GetAllCategories();

        public AllowanceCycle GetCurrentCycle() => _db.GetCurrentCycle();

        public AllowanceCycle StartNewAllowanceCycle(DateTime startDate, DateTime nextAllowanceDate, decimal allowanceAmount, string notes = "")
        {
            decimal carryOver = 0m;
            var previousCycle = _db.GetCurrentCycle();
            if (previousCycle != null)
            {
                decimal prevBalance = ComputeCycleBalance(previousCycle);
                if (startDate.Date == DateTime.Today)
                {
                    decimal todayIncome = _db.GetAllTransactions()
                        .OfType<Income>()
                        .Where(i => i.Date.Date == startDate.Date)
                        .Sum(i => i.Amount);
                    decimal todayExpenses = ComputeExpensesForDate(startDate.Date);
                    prevBalance = prevBalance + todayExpenses - todayIncome;
                }
                carryOver = Math.Max(0m, prevBalance);
            }

            var newCycle = _db.AddCycle(new AllowanceCycle(0, startDate.Date, nextAllowanceDate.Date, allowanceAmount, carryOver));

            string incomeNote = string.IsNullOrWhiteSpace(notes)
                ? $"Allowance cycle ({startDate:MMM dd} - {nextAllowanceDate:MMM dd})"
                : notes.Trim();

            _db.AddTransaction(new Income(0, startDate.Date, allowanceAmount, "Allowance", incomeNote));
            return newCycle;
        }

        public Income AddIncome(DateTime date, decimal amount, string source, string notes)
        {
            var income = new Income(0, date, amount, source, notes);

            if (source.Equals("Allowance", StringComparison.OrdinalIgnoreCase))
            {
                var current = _db.GetCurrentCycle();
                DateTime nextDate = current != null && current.EndDate > date.Date
                    ? current.EndDate
                    : date.Date.AddDays(14);
                StartNewAllowanceCycle(date.Date, nextDate, amount, notes);
                return income;
            }

            return (Income)_db.AddTransaction(income);
        }

        public Expense AddExpense(DateTime date, decimal amount, int categoryId, string paymentMethod, string expenseKind, string notes)
        {
            var category = _db.GetCategoryById(categoryId);
            if (category == null)
                throw new ArgumentException("Selected expense category does not exist.");

            string normalizedKind = category.Name.Equals("Savings", StringComparison.OrdinalIgnoreCase)
                ? string.Empty
                : expenseKind;

            var expense = new Expense(0, date, amount, category.CategoryId, category.Name, paymentMethod, normalizedKind, notes);
            return (Expense)_db.AddTransaction(expense);
        }

        public List<string> CheckExpenseWarnings(Expense candidate, int? editingTransactionId = null)
        {
            var cycle = _db.GetCurrentCycle();
            decimal currentBalance = ComputeSafeSpendBalance(cycle);
            decimal todaysExpenses = ComputeDailyLimitExpensesForDate(DateTime.Today, editingTransactionId);
            return _budgetManager.EvaluateExpenseWarnings(candidate, currentBalance, todaysExpenses, cycle, editingTransactionId);
        }

        public void UpdateTransaction(Transaction updated)
        {
            if (updated == null) throw new ArgumentNullException(nameof(updated));

            var existing = _db.GetTransactionById(updated.Id);
            if (existing == null)
                throw new KeyNotFoundException("Transaction record could not be found.");

            var linkedDeposit = _db.GetDepositByTransactionId(updated.Id);
            if (linkedDeposit != null && updated is Expense updatedExp)
            {
                var goal = _db.GetSavingsGoalById(linkedDeposit.GoalId);
                if (goal != null)
                {
                    decimal diff = updatedExp.Amount - linkedDeposit.Amount;
                    if (diff >= 0) goal.Deposit(diff);
                    else goal.Withdraw(Math.Abs(diff));
                    _db.UpdateSavingsGoal(goal);
                }
                linkedDeposit.Amount = updatedExp.Amount;
                linkedDeposit.Date = updatedExp.Date;
            }

            _db.UpdateTransaction(updated);
        }

        public void DeleteTransaction(int transactionId)
        {
            var linkedDeposit = _db.GetDepositByTransactionId(transactionId);
            if (linkedDeposit != null)
            {
                var goal = _db.GetSavingsGoalById(linkedDeposit.GoalId);
                if (goal != null)
                {
                    goal.Withdraw(linkedDeposit.Amount);
                    _db.UpdateSavingsGoal(goal);
                }
                _db.DeleteSavingsDeposit(linkedDeposit.DepositId);
            }

            _db.DeleteTransaction(transactionId);
        }

        public SavingsDeposit RecordSavingsDeposit(int goalId, decimal amount, DateTime date, string paymentMethod, string notes, bool isExtraAllowance = false)
        {
            var goal = _db.GetSavingsGoalById(goalId);
            if (goal == null)
                throw new KeyNotFoundException("Savings goal not found.");

            var savingsCategory = _db.GetAllCategories()
                .FirstOrDefault(c => c.Name.Equals("Savings", StringComparison.OrdinalIgnoreCase));

            if (savingsCategory == null)
                savingsCategory = _db.AddCategory(new Category(0, "Savings", true));

            string txNotes = string.IsNullOrWhiteSpace(notes)
                ? $"Savings deposit: {goal.GoalName}"
                : $"Savings deposit ({goal.GoalName}): {notes.Trim()}";

            var expense = (Expense)_db.AddTransaction(
                new Expense(0, date, amount, savingsCategory.CategoryId, savingsCategory.Name, paymentMethod, string.Empty, txNotes));

            goal.Deposit(amount);
            _db.UpdateSavingsGoal(goal);

            return _db.AddSavingsDeposit(new SavingsDeposit(0, goal.GoalId, expense.Id, amount, date, notes, isExtraAllowance));
        }

        public void DeleteSavingsDeposit(int depositId, int goalId)
        {
            var deposits = _db.GetDepositsForGoal(goalId);
            var target = deposits.FirstOrDefault(d => d.DepositId == depositId);
            if (target == null) return;

            var goal = _db.GetSavingsGoalById(goalId);
            if (goal != null)
            {
                goal.Withdraw(target.Amount);
                _db.UpdateSavingsGoal(goal);
            }

            _db.DeleteSavingsDeposit(depositId);
            _db.DeleteTransaction(target.TransactionId);
        }

        public decimal ComputeProjectedCarryOver(AllowanceCycle cycle = null)
        {
            decimal currentBalance = ComputeCycleBalance(cycle);
            return Math.Round(Math.Max(0m, currentBalance), 2);
        }

        public decimal ComputeCycleTotalFunds(AllowanceCycle cycle = null)
        {
            cycle ??= _db.GetCurrentCycle();
            if (cycle == null) return 0m;

            decimal incomeInCycle = _db.GetAllTransactions()
                .OfType<Income>()
                .Where(i => i.Date >= cycle.StartDate && i.Date <= DateTime.Today)
                .Sum(i => i.Amount);

            return Math.Round(cycle.CarryOver + incomeInCycle, 2);
        }

        public decimal ComputeCycleBalance(AllowanceCycle cycle = null)
        {
            cycle ??= _db.GetCurrentCycle();
            if (cycle == null) return 0m;

            var allTx = _db.GetAllTransactions();
            decimal incomeInCycle = allTx
                .OfType<Income>()
                .Where(i => i.Date >= cycle.StartDate && i.Date <= DateTime.Today)
                .Sum(i => i.Amount);

            decimal expensesInCycle = allTx
                .OfType<Expense>()
                .Where(e => e.Date >= cycle.StartDate && e.Date <= DateTime.Today)
                .Sum(e => e.Amount);

            return Math.Round(cycle.CarryOver + incomeInCycle - expensesInCycle, 2);
        }

        public decimal ComputeExpensesForDate(DateTime date, int? excludeTransactionId = null)
        {
            return _db.GetAllTransactions()
                .OfType<Expense>()
                .Where(e => e.Date.Date == date.Date &&
                            (!excludeTransactionId.HasValue || e.Id != excludeTransactionId.Value))
                .Sum(e => e.Amount);
        }

        private HashSet<int> GetExtraAllowanceExpenseIds()
        {
            var ids = new HashSet<int>();
            foreach (var goal in _db.GetAllSavingsGoals())
                foreach (var deposit in _db.GetDepositsForGoal(goal.GoalId))
                    if (deposit.IsExtraAllowance && deposit.TransactionId > 0)
                        ids.Add(deposit.TransactionId);
            return ids;
        }

        public decimal ComputeExtraAllowanceInCycle(AllowanceCycle cycle = null)
        {
            cycle ??= _db.GetCurrentCycle();
            if (cycle == null) return 0m;

            var ids = GetExtraAllowanceExpenseIds();
            if (ids.Count == 0) return 0m;

            return _db.GetAllTransactions()
                .OfType<Expense>()
                .Where(e => ids.Contains(e.Id) && e.Date >= cycle.StartDate && e.Date <= DateTime.Today)
                .Sum(e => e.Amount);
        }

        public decimal ComputeSafeSpendBalance(AllowanceCycle cycle = null)
        {
            cycle ??= _db.GetCurrentCycle();
            return Math.Round(ComputeCycleBalance(cycle) + ComputeExtraAllowanceInCycle(cycle), 2);
        }

        public decimal ComputeDailyLimitExpensesForDate(DateTime date, int? excludeTransactionId = null)
        {
            var ids = GetExtraAllowanceExpenseIds();
            return _db.GetAllTransactions()
                .OfType<Expense>()
                .Where(e => e.Date.Date == date.Date &&
                            !ids.Contains(e.Id) &&
                            (!excludeTransactionId.HasValue || e.Id != excludeTransactionId.Value))
                .Sum(e => e.Amount);
        }

        public decimal ComputeTodaySafeDailyLimit()
        {
            var cycle = _db.GetCurrentCycle();
            if (cycle == null) return 0m;

            decimal cycleBalance = ComputeSafeSpendBalance(cycle);
            decimal todaysExpenses = ComputeDailyLimitExpensesForDate(DateTime.Today);
            return cycle.ComputeDailyLimit(cycleBalance, todaysExpenses, DateTime.Today);
        }

        public decimal ComputeRemainingSafeSpendToday()
        {
            var cycle = _db.GetCurrentCycle();
            if (cycle == null) return 0m;

            decimal cycleBalance = ComputeSafeSpendBalance(cycle);
            decimal todaysExpenses = ComputeDailyLimitExpensesForDate(DateTime.Today);
            return cycle.ComputeRemainingSafeSpendToday(cycleBalance, todaysExpenses, DateTime.Today);
        }

        public bool IsTodayOverDailyLimit()
        {
            var cycle = _db.GetCurrentCycle();
            if (cycle == null) return false;

            decimal safeDaily = ComputeTodaySafeDailyLimit();
            decimal todaysExpenses = ComputeDailyLimitExpensesForDate(DateTime.Today);
            return todaysExpenses > safeDaily;
        }

        public (decimal TotalIncome, decimal TotalExpenses, decimal NetBalance, decimal NeedsTotal, decimal WantsTotal, Dictionary<string, decimal> CategoryTotals)
            GetMonthlySummary(int month, int year)
        {
            var allTx = _db.GetAllTransactions();

            decimal totalIncome = allTx
                .OfType<Income>()
                .Where(i => i.Date.Month == month && i.Date.Year == year)
                .Sum(i => i.Amount);

            var monthExpenses = allTx
                .OfType<Expense>()
                .Where(e => e.Date.Month == month && e.Date.Year == year)
                .ToList();

            decimal totalExpenses = monthExpenses.Sum(e => e.Amount);

            var nonSavingsExpenses = monthExpenses
                .Where(e => !e.CategoryName.Equals("Savings", StringComparison.OrdinalIgnoreCase))
                .ToList();

            decimal needsTotal = nonSavingsExpenses
                .Where(e => e.ExpenseKind.Equals("Need", StringComparison.OrdinalIgnoreCase))
                .Sum(e => e.Amount);

            decimal wantsTotal = nonSavingsExpenses
                .Where(e => e.ExpenseKind.Equals("Want", StringComparison.OrdinalIgnoreCase))
                .Sum(e => e.Amount);

            var categoryTotals = monthExpenses
                .GroupBy(e => e.CategoryName)
                .ToDictionary(g => g.Key, g => g.Sum(x => x.Amount));

            return (totalIncome, totalExpenses, totalIncome - totalExpenses, needsTotal, wantsTotal, categoryTotals);
        }

        public List<Transaction> SearchTransactions(
            DateTime? fromDate,
            DateTime? toDate,
            int? categoryId,
            string transactionType,
            string expenseKind,
            string keyword)
        {
            IEnumerable<Transaction> query = _db.GetAllTransactions();

            if (fromDate.HasValue)
                query = query.Where(t => t.Date.Date >= fromDate.Value.Date);

            if (toDate.HasValue)
                query = query.Where(t => t.Date.Date <= toDate.Value.Date);

            if (!string.IsNullOrWhiteSpace(transactionType) && !transactionType.Equals("All", StringComparison.OrdinalIgnoreCase))
                query = query.Where(t => t.TransactionType.Equals(transactionType, StringComparison.OrdinalIgnoreCase));

            if (categoryId.HasValue && categoryId.Value > 0)
                query = query.Where(t => t is Expense e && e.CategoryId == categoryId.Value);

            if (!string.IsNullOrWhiteSpace(expenseKind) && !expenseKind.Equals("All", StringComparison.OrdinalIgnoreCase))
                query = query.Where(t => t is Expense e && e.ExpenseKind.Equals(expenseKind, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                string kw = keyword.Trim();
                query = query.Where(t =>
                    (t.Notes != null && t.Notes.IndexOf(kw, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (t is Income inc && inc.Source.IndexOf(kw, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (t is Expense exp && exp.CategoryName.IndexOf(kw, StringComparison.OrdinalIgnoreCase) >= 0));
            }

            return query.ToList();
        }
    }
}
