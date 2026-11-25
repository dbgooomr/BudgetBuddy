using System;
using System.Collections.Generic;
using System.Linq;
using BudgetBuddy.Data;
using BudgetBuddy.Models;

namespace BudgetBuddy.Services
{
    public class BudgetManager
    {
        private readonly DatabaseHelper _db;

        public BudgetManager(DatabaseHelper db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public List<Budget> GetBudgetsForMonth(int month, int year)
        {
            return _db.GetBudgetsForMonth(month, year);
        }

        public List<Budget> GetBudgetsForPeriod(DateTime start, DateTime end, string periodType)
        {
            return _db.GetBudgetsForMonth(start.Month, start.Year)
                .Where(b => b.PeriodType == periodType && b.PeriodStart.Date == start.Date && b.PeriodEnd.Date == end.Date).ToList();
        }

        public decimal GetCategoryUsedAmount(int categoryId, DateTime start, DateTime end, int? excludeTransactionId = null)
        {
            return _db.GetAllTransactions().OfType<Expense>()
                .Where(e => e.CategoryId == categoryId && e.Date.Date >= start.Date && e.Date.Date <= end.Date &&
                    (!excludeTransactionId.HasValue || e.Id != excludeTransactionId.Value)).Sum(e => e.Amount);
        }

        public decimal GetTotalRemainingBudget(DateTime start, DateTime end, string periodType, int? excludeCategoryId = null)
        {
            decimal total = 0m;
            foreach (var b in GetBudgetsForPeriod(start, end, periodType))
            {
                if (excludeCategoryId.HasValue && b.CategoryId == excludeCategoryId.Value) continue;
                decimal used = GetCategoryUsedAmount(b.CategoryId, start, end);
                total += Math.Max(0m, b.ComputeRemaining(used));
            }
            return Math.Round(total, 2);
        }

        public Budget SetBudget(int categoryId, DateTime start, DateTime end, string periodType, decimal limitAmount)
        {
            var category = _db.GetCategoryById(categoryId);
            if (category == null)
                throw new ArgumentException("Selected category does not exist.");

            var budget = new Budget(0, categoryId, category.Name, start, end, periodType, limitAmount);
            return _db.SaveOrUpdateBudget(budget);
        }

        public void DeleteBudget(int budgetId)
        {
            _db.DeleteBudget(budgetId);
        }

        public decimal GetCategoryUsedAmount(int categoryId, int month, int year, int? excludeTransactionId = null)
        {
            var transactions = _db.GetAllTransactions();
            return transactions
                .OfType<Expense>()
                .Where(e => e.CategoryId == categoryId &&
                            e.Date.Month == month &&
                            e.Date.Year == year &&
                            (!excludeTransactionId.HasValue || e.Id != excludeTransactionId.Value))
                .Sum(e => e.Amount);
        }

        public List<string> EvaluateExpenseWarnings(
            Expense candidateExpense,
            decimal currentCycleBalance,
            decimal todaysExpensesExcludingCandidate,
            AllowanceCycle activeCycle,
            int? editingTransactionId = null)
        {
            var warnings = new List<string>();
            if (candidateExpense == null) return warnings;

            var previousMonth = candidateExpense.Date.AddMonths(-1);
            var matchingBudgets = _db.GetBudgetsForMonth(candidateExpense.Date.Month, candidateExpense.Date.Year)
                .Concat(_db.GetBudgetsForMonth(previousMonth.Month, previousMonth.Year))
                .Where(b => candidateExpense.Date.Date >= b.PeriodStart.Date && candidateExpense.Date.Date <= b.PeriodEnd.Date &&
                            b.CategoryId == candidateExpense.CategoryId).GroupBy(b => b.BudgetId).Select(g => g.First()).ToList();
            foreach (var budget in matchingBudgets)
            {
                decimal usedBefore = GetCategoryUsedAmount(candidateExpense.CategoryId, budget.PeriodStart, budget.PeriodEnd, editingTransactionId);
                decimal projectedUsed = usedBefore + candidateExpense.Amount;
                string status = budget.GetStatus(projectedUsed);
                string periodLabel = budget.PeriodType == "Weekly" ? "weekly" : "monthly";
                if (status == "Over Budget")
                    warnings.Add($"Category Budget Alert: '{budget.CategoryName}' will exceed its {periodLabel} budget by ₱{projectedUsed - budget.LimitAmount:N2} (₱{projectedUsed:N2} / ₱{budget.LimitAmount:N2}).");
                else if (status == "Near Limit")
                    warnings.Add($"Category Budget Notice: '{budget.CategoryName}' will reach {budget.ComputeUsagePercentage(projectedUsed):F0}% of its {periodLabel} limit (₱{projectedUsed:N2} / ₱{budget.LimitAmount:N2}).");
            }

            if (activeCycle != null && candidateExpense.Date.Date == DateTime.Today)
            {
                decimal safeDaily = activeCycle.ComputeDailyLimit(currentCycleBalance, todaysExpensesExcludingCandidate, DateTime.Today);
                decimal projectedToday = todaysExpensesExcludingCandidate + candidateExpense.Amount;

                if (projectedToday > safeDaily)
                {
                    decimal exceedBy = projectedToday - safeDaily;
                    warnings.Add($"Daily Limit Alert: Today's total spending (\u20B1{projectedToday:N2}) will exceed your safe daily amount of \u20B1{safeDaily:N2} by \u20B1{exceedBy:N2}.");
                }
            }

            return warnings;
        }
    }
}
