using System;
using System.Collections.Generic;
using System.Linq;
using BudgetBuddy.Interfaces;
using BudgetBuddy.Models;

namespace BudgetBuddy.Data
{
    public class InMemoryDataStore : IDataRepository
    {
        private readonly List<Category> _categories = new List<Category>();
        private readonly List<Transaction> _transactions = new List<Transaction>();
        private readonly List<Budget> _budgets = new List<Budget>();
        private readonly List<AllowanceCycle> _cycles = new List<AllowanceCycle>();
        private readonly List<SavingsGoal> _goals = new List<SavingsGoal>();
        private readonly List<SavingsDeposit> _deposits = new List<SavingsDeposit>();

        private int _nextCategoryId = 1;
        private int _nextTransactionId = 1;
        private int _nextBudgetId = 1;
        private int _nextCycleId = 1;
        private int _nextGoalId = 1;
        private int _nextDepositId = 1;

        public InMemoryDataStore()
        {
            SeedDefaultData();
        }

        public void ResetSampleData()
        {
            _categories.Clear();
            _transactions.Clear();
            _budgets.Clear();
            _cycles.Clear();
            _goals.Clear();
            _deposits.Clear();

            _nextCategoryId = 1;
            _nextTransactionId = 1;
            _nextBudgetId = 1;
            _nextCycleId = 1;
            _nextGoalId = 1;
            _nextDepositId = 1;

            SeedDefaultData();
        }

        private void SeedDefaultData()
        {
            string[] presetNames =
            {
                "Food and Canteen",
                "Fare and Transport",
                "School Supplies",
                "Projects and Printing",
                "Load and Data",
                "Tuition and Fees",
                "Rent or Dorm",
                "Entertainment",
                "Savings"
            };

            foreach (string name in presetNames)
            {
                _categories.Add(new Category(_nextCategoryId++, name, true));
            }

            DateTime today = DateTime.Today;
            DateTime cycleStart = today.AddDays(-5);
            DateTime nextAllowance = today.AddDays(9);

            _cycles.Add(new AllowanceCycle(_nextCycleId++, cycleStart, nextAllowance, 3500.00m, 250.00m));

            AddTransaction(new Income(0, cycleStart, 3500.00m, "Allowance", "Bi-weekly school allowance"));
            AddTransaction(new Income(0, today.AddDays(-3), 500.00m, "Part-time job", "Weekend tutoring session"));

            int foodId = GetCategoryIdByName("Food and Canteen");
            int fareId = GetCategoryIdByName("Fare and Transport");
            int printId = GetCategoryIdByName("Projects and Printing");
            int loadId = GetCategoryIdByName("Load and Data");
            int entId = GetCategoryIdByName("Entertainment");
            int suppliesId = GetCategoryIdByName("School Supplies");
            int savingsCatId = GetCategoryIdByName("Savings");

            AddTransaction(new Expense(0, cycleStart, 85.00m, foodId, "Food and Canteen", "Cash", "Need", "Canteen lunch and water"));
            AddTransaction(new Expense(0, cycleStart, 44.00m, fareId, "Fare and Transport", "Cash", "Need", "Jeepney and tricycle fare"));
            AddTransaction(new Expense(0, today.AddDays(-4), 120.00m, printId, "Projects and Printing", "E-wallet", "Need", "OOP chapter documentation printing"));
            AddTransaction(new Expense(0, today.AddDays(-3), 99.00m, loadId, "Load and Data", "E-wallet", "Need", "Weekly mobile data promo"));
            AddTransaction(new Expense(0, today.AddDays(-2), 145.00m, entId, "Entertainment", "E-wallet", "Want", "Milk tea with groupmates"));
            AddTransaction(new Expense(0, today.AddDays(-2), 95.00m, foodId, "Food and Canteen", "Cash", "Need", "Breakfast silog meal"));
            AddTransaction(new Expense(0, today.AddDays(-1), 65.00m, suppliesId, "School Supplies", "Cash", "Need", "Yellow pad and ballpens"));
            AddTransaction(new Expense(0, today.AddDays(-1), 44.00m, fareId, "Fare and Transport", "Cash", "Need", "Roundtrip campus commute"));
            AddTransaction(new Expense(0, today, 110.00m, foodId, "Food and Canteen", "Cash", "Need", "Lunch at student cafeteria"));

            int m = today.Month;
            int y = today.Year;
            SaveOrUpdateBudget(new Budget(0, foodId, "Food and Canteen", m, y, 1800.00m));
            SaveOrUpdateBudget(new Budget(0, fareId, "Fare and Transport", m, y, 800.00m));
            SaveOrUpdateBudget(new Budget(0, printId, "Projects and Printing", m, y, 500.00m));
            SaveOrUpdateBudget(new Budget(0, loadId, "Load and Data", m, y, 400.00m));
            SaveOrUpdateBudget(new Budget(0, entId, "Entertainment", m, y, 300.00m));

            var laptopGoal = AddSavingsGoal(new SavingsGoal(0, "Capstone Laptop Upgrade Fund", 15000.00m, 2500.00m, today.AddMonths(4)));
            var orgShirtGoal = AddSavingsGoal(new SavingsGoal(0, "CCS Department Org Shirt", 450.00m, 150.00m, today.AddDays(20)));

            var depositTx = AddTransaction(new Expense(0, today.AddDays(-2), 150.00m, savingsCatId, "Savings", "E-wallet", "", "Deposit to: " + orgShirtGoal.GoalName));
            AddSavingsDeposit(new SavingsDeposit(0, orgShirtGoal.GoalId, depositTx.Id, 150.00m, today.AddDays(-2), "Initial deposit"));
        }

        private int GetCategoryIdByName(string name)
        {
            var cat = _categories.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            return cat != null ? cat.CategoryId : 1;
        }

        public List<Category> GetAllCategories() =>
            _categories.Select(c => new Category(c.CategoryId, c.Name, c.IsPreset)).ToList();

        public Category GetCategoryById(int categoryId)
        {
            var c = _categories.FirstOrDefault(x => x.CategoryId == categoryId);
            return c == null ? null : new Category(c.CategoryId, c.Name, c.IsPreset);
        }

        public Category AddCategory(Category category)
        {
            if (_categories.Any(c => c.Name.Equals(category.Name, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException($"A category named '{category.Name}' already exists.");

            var created = new Category(_nextCategoryId++, category.Name, category.IsPreset);
            _categories.Add(created);
            return new Category(created.CategoryId, created.Name, created.IsPreset);
        }

        public void UpdateCategory(Category category)
        {
            var existing = _categories.FirstOrDefault(c => c.CategoryId == category.CategoryId);
            if (existing == null)
                throw new KeyNotFoundException("Category not found.");

            if (_categories.Any(c => c.CategoryId != category.CategoryId &&
                                     c.Name.Equals(category.Name, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException($"A category named '{category.Name}' already exists.");

            existing.Name = category.Name;

            foreach (var exp in _transactions.OfType<Expense>().Where(e => e.CategoryId == category.CategoryId))
                exp.CategoryName = category.Name;

            foreach (var b in _budgets.Where(b => b.CategoryId == category.CategoryId))
                b.CategoryName = category.Name;
        }

        public void DeleteCategory(int categoryId)
        {
            var existing = _categories.FirstOrDefault(c => c.CategoryId == categoryId);
            if (existing == null)
                throw new KeyNotFoundException("Category not found.");
            if (existing.IsPreset)
                throw new InvalidOperationException("Preset student categories cannot be deleted.");
            if (CountTransactionsByCategory(categoryId) > 0)
                throw new InvalidOperationException("Cannot delete a category that still has transactions. Move its records to another category first.");

            _budgets.RemoveAll(b => b.CategoryId == categoryId);
            _categories.Remove(existing);
        }

        public int CountTransactionsByCategory(int categoryId) =>
            _transactions.OfType<Expense>().Count(e => e.CategoryId == categoryId);

        public void ReassignTransactionCategory(int oldCategoryId, int newCategoryId)
        {
            var targetCat = _categories.FirstOrDefault(c => c.CategoryId == newCategoryId);
            if (targetCat == null)
                throw new KeyNotFoundException("Target category not found.");

            foreach (var exp in _transactions.OfType<Expense>().Where(e => e.CategoryId == oldCategoryId))
            {
                exp.CategoryId = targetCat.CategoryId;
                exp.CategoryName = targetCat.Name;
            }
        }

        public List<Transaction> GetAllTransactions() =>
            _transactions
                .OrderByDescending(t => t.Date)
                .ThenByDescending(t => t.Id)
                .Select(CloneTransaction)
                .ToList();

        public Transaction GetTransactionById(int transactionId)
        {
            var t = _transactions.FirstOrDefault(x => x.Id == transactionId);
            return t == null ? null : CloneTransaction(t);
        }

        public Transaction AddTransaction(Transaction transaction)
        {
            Transaction stored = CloneTransaction(transaction);
            stored.Id = _nextTransactionId++;
            if (stored is Expense exp)
            {
                var cat = _categories.FirstOrDefault(c => c.CategoryId == exp.CategoryId);
                if (cat != null) exp.CategoryName = cat.Name;
            }
            _transactions.Add(stored);
            return CloneTransaction(stored);
        }

        public void UpdateTransaction(Transaction transaction)
        {
            int index = _transactions.FindIndex(t => t.Id == transaction.Id);
            if (index < 0)
                throw new KeyNotFoundException("Transaction not found.");

            Transaction updated = CloneTransaction(transaction);
            if (updated is Expense exp)
            {
                var cat = _categories.FirstOrDefault(c => c.CategoryId == exp.CategoryId);
                if (cat != null) exp.CategoryName = cat.Name;
            }
            _transactions[index] = updated;
        }

        public void DeleteTransaction(int transactionId)
        {
            _transactions.RemoveAll(t => t.Id == transactionId);
        }

        private Transaction CloneTransaction(Transaction t)
        {
            if (t is Income inc)
                return new Income(inc.Id, inc.Date, inc.Amount, inc.Source, inc.Notes);
            if (t is Expense exp)
                return new Expense(exp.Id, exp.Date, exp.Amount, exp.CategoryId, exp.CategoryName, exp.PaymentMethod, exp.ExpenseKind, exp.Notes);
            return new Transaction(t.Id, t.Date, t.Amount, t.Notes);
        }

        public List<Budget> GetBudgetsForMonth(int month, int year) =>
            _budgets
                .Where(b => b.Month == month && b.Year == year)
                .Select(CloneBudget)
                .ToList();

        public Budget GetBudget(int categoryId, int month, int year)
        {
            var b = _budgets.FirstOrDefault(x => x.CategoryId == categoryId && x.Month == month && x.Year == year);
            return b == null ? null : CloneBudget(b);
        }

        public Budget SaveOrUpdateBudget(Budget budget)
        {
            var cat = _categories.FirstOrDefault(c => c.CategoryId == budget.CategoryId);
            string catName = cat != null ? cat.Name : budget.CategoryName;

            var existing = _budgets.FirstOrDefault(b =>
                b.CategoryId == budget.CategoryId && b.PeriodType == budget.PeriodType && b.PeriodStart.Date == budget.PeriodStart.Date && b.PeriodEnd.Date == budget.PeriodEnd.Date);

            if (existing != null)
            {
                existing.LimitAmount = budget.LimitAmount;
                existing.CategoryName = catName;
                existing.PeriodType = budget.PeriodType; existing.PeriodStart = budget.PeriodStart; existing.PeriodEnd = budget.PeriodEnd;
                return CloneBudget(existing);
            }

            var created = new Budget(_nextBudgetId++, budget.CategoryId, catName, budget.PeriodStart, budget.PeriodEnd, budget.PeriodType, budget.LimitAmount);
            _budgets.Add(created);
            return CloneBudget(created);
        }

        private static Budget CloneBudget(Budget b) => new Budget(b.BudgetId, b.CategoryId, b.CategoryName, b.PeriodStart, b.PeriodEnd, b.PeriodType, b.LimitAmount);

        public void DeleteBudget(int budgetId)
        {
            _budgets.RemoveAll(b => b.BudgetId == budgetId);
        }

        public List<AllowanceCycle> GetAllCycles() =>
            _cycles.Select(c => new AllowanceCycle(c.CycleId, c.StartDate, c.EndDate, c.AllowanceAmount, c.CarryOver)).ToList();

        public AllowanceCycle GetCurrentCycle()
        {
            var c = _cycles.OrderByDescending(x => x.CycleId).FirstOrDefault();
            return c == null ? null : new AllowanceCycle(c.CycleId, c.StartDate, c.EndDate, c.AllowanceAmount, c.CarryOver);
        }

        public AllowanceCycle AddCycle(AllowanceCycle cycle)
        {
            var created = new AllowanceCycle(_nextCycleId++, cycle.StartDate, cycle.EndDate, cycle.AllowanceAmount, cycle.CarryOver);
            _cycles.Add(created);
            return new AllowanceCycle(created.CycleId, created.StartDate, created.EndDate, created.AllowanceAmount, created.CarryOver);
        }

        public void UpdateCycle(AllowanceCycle cycle)
        {
            var existing = _cycles.FirstOrDefault(c => c.CycleId == cycle.CycleId);
            if (existing == null)
                throw new KeyNotFoundException("Allowance cycle not found.");
            existing.StartDate = cycle.StartDate;
            existing.EndDate = cycle.EndDate;
            existing.AllowanceAmount = cycle.AllowanceAmount;
            existing.CarryOver = cycle.CarryOver;
        }

        public List<SavingsGoal> GetAllSavingsGoals() =>
            _goals.Select(g => new SavingsGoal(g.GoalId, g.GoalName, g.TargetAmount, g.SavedAmount, g.TargetDate)).ToList();

        public SavingsGoal GetSavingsGoalById(int goalId)
        {
            var g = _goals.FirstOrDefault(x => x.GoalId == goalId);
            return g == null ? null : new SavingsGoal(g.GoalId, g.GoalName, g.TargetAmount, g.SavedAmount, g.TargetDate);
        }

        public SavingsGoal AddSavingsGoal(SavingsGoal goal)
        {
            var created = new SavingsGoal(_nextGoalId++, goal.GoalName, goal.TargetAmount, goal.SavedAmount, goal.TargetDate);
            _goals.Add(created);
            return new SavingsGoal(created.GoalId, created.GoalName, created.TargetAmount, created.SavedAmount, created.TargetDate);
        }

        public void UpdateSavingsGoal(SavingsGoal goal)
        {
            var existing = _goals.FirstOrDefault(g => g.GoalId == goal.GoalId);
            if (existing == null)
                throw new KeyNotFoundException("Savings goal not found.");
            existing.GoalName = goal.GoalName;
            existing.TargetAmount = goal.TargetAmount;
            existing.SavedAmount = goal.SavedAmount;
            existing.TargetDate = goal.TargetDate;
        }

        public void DeleteSavingsGoal(int goalId)
        {
            var linkedTransactionIds = _deposits
                .Where(d => d.GoalId == goalId)
                .Select(d => d.TransactionId)
                .ToHashSet();
            _deposits.RemoveAll(d => d.GoalId == goalId);
            _transactions.RemoveAll(t => linkedTransactionIds.Contains(t.Id));
            _goals.RemoveAll(g => g.GoalId == goalId);
        }

        public List<SavingsDeposit> GetDepositsForGoal(int goalId) =>
            _deposits
                .Where(d => d.GoalId == goalId)
                .OrderByDescending(d => d.Date)
                .ThenByDescending(d => d.DepositId)
                .Select(d => new SavingsDeposit(d.DepositId, d.GoalId, d.TransactionId, d.Amount, d.Date, d.Notes, d.IsExtraAllowance))
                .ToList();

        public SavingsDeposit GetDepositByTransactionId(int transactionId)
        {
            var d = _deposits.FirstOrDefault(x => x.TransactionId == transactionId);
            return d == null ? null : new SavingsDeposit(d.DepositId, d.GoalId, d.TransactionId, d.Amount, d.Date, d.Notes, d.IsExtraAllowance);
        }

        public SavingsDeposit AddSavingsDeposit(SavingsDeposit deposit)
        {
            var created = new SavingsDeposit(_nextDepositId++, deposit.GoalId, deposit.TransactionId, deposit.Amount, deposit.Date, deposit.Notes, deposit.IsExtraAllowance);
            _deposits.Add(created);
            return new SavingsDeposit(created.DepositId, created.GoalId, created.TransactionId, created.Amount, created.Date, created.Notes, created.IsExtraAllowance);
        }

        public void DeleteSavingsDeposit(int depositId)
        {
            _deposits.RemoveAll(d => d.DepositId == depositId);
        }
    }
}
