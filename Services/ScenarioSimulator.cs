using System;
using System.Linq;
using BudgetBuddy.Models;

namespace BudgetBuddy.Services
{
    public class ScenarioSimulator
    {
        private readonly TransactionManager _txManager;
        private readonly BudgetManager _budgetManager;

        public ScenarioSimulator(TransactionManager txManager, BudgetManager budgetManager)
        {
            _txManager = txManager ?? throw new ArgumentNullException(nameof(txManager));
            _budgetManager = budgetManager ?? throw new ArgumentNullException(nameof(budgetManager));
        }

        public WhatIfScenarioResult SimulateOneTimePurchase(decimal purchaseAmount, int categoryId)
        {
            if (purchaseAmount <= 0)
                throw new ArgumentException("Planned purchase amount must be greater than zero.");

            return BuildExpenseSimulation(
                scenarioType: "One-Time Purchase",
                totalPlannedExpense: purchaseAmount,
                categoryId: categoryId,
                detailNote: $"Planned one-time purchase of \u20B1{purchaseAmount:N2}");
        }

        public WhatIfScenarioResult SimulateRepeatingExpense(decimal amountPerTime, int repeatCount, int categoryId)
        {
            if (amountPerTime <= 0)
                throw new ArgumentException("Amount per occurrence must be greater than zero.");
            if (repeatCount <= 0)
                throw new ArgumentException("Number of occurrences must be at least 1.");

            decimal total = Math.Round(amountPerTime * repeatCount, 2);
            return BuildExpenseSimulation(
                scenarioType: "Repeating Expense",
                totalPlannedExpense: total,
                categoryId: categoryId,
                detailNote: $"\u20B1{amountPerTime:N2} \u00D7 {repeatCount} times = \u20B1{total:N2}");
        }

        public WhatIfScenarioResult SimulateDailySaving(decimal plannedDailySaving, SavingsGoal goal)
        {
            if (plannedDailySaving <= 0)
                throw new ArgumentException("Planned daily saving must be greater than zero.");
            if (goal == null)
                throw new ArgumentException("Please select a savings goal to simulate.");

            var cycle = _txManager.GetCurrentCycle();
            decimal currentBalance = _txManager.ComputeCycleBalance(cycle);
            decimal todaysExpenses = _txManager.ComputeDailyLimitExpensesForDate(DateTime.Today);
            decimal extraAllowanceAddBack = _txManager.ComputeExtraAllowanceInCycle(cycle);
            decimal currentSafeDaily = cycle != null
                ? cycle.ComputeDailyLimit(currentBalance + extraAllowanceAddBack, todaysExpenses, DateTime.Today)
                : 0m;

            decimal newBalance = Math.Round(currentBalance - plannedDailySaving, 2);
            decimal newSafeDaily = cycle != null
                ? cycle.ComputeDailyLimit(newBalance + extraAllowanceAddBack, todaysExpenses, DateTime.Today)
                : 0m;

            decimal remainingNeeded = goal.RemainingAmount;
            int daysNeeded = remainingNeeded <= 0
                ? 0
                : (int)Math.Ceiling((double)(remainingNeeded / plannedDailySaving));

            DateTime estimatedGoalDate = DateTime.Today.AddDays(daysNeeded);

            string advice = remainingNeeded <= 0
                ? $"You have already reached '{goal.GoalName}'!"
                : $"Saving \u20B1{plannedDailySaving:N2}/day leaves \u20B1{newSafeDaily:N2}/day for regular expenses and completes '{goal.GoalName}' in {daysNeeded} day(s).";

            return new WhatIfScenarioResult
            {
                ScenarioType = "Daily Saving Toward Goal",
                TotalPlannedAmount = plannedDailySaving,
                CurrentBalance = currentBalance,
                NewBalance = newBalance,
                CurrentSafeDailyAmount = currentSafeDaily,
                NewSafeDailyAmount = newSafeDaily,
                CategoryName = "Savings",
                BudgetStatusAfter = "Goal Simulation",
                DateLabel = "Estimated Goal Completion Date",
                EstimatedDateText = remainingNeeded <= 0
                    ? "Already Reached!"
                    : $"{estimatedGoalDate:MMM dd, yyyy} ({daysNeeded} days)",
                AdviceSummary = advice
            };
        }

        private WhatIfScenarioResult BuildExpenseSimulation(
            string scenarioType,
            decimal totalPlannedExpense,
            int categoryId,
            string detailNote)
        {
            var cycle = _txManager.GetCurrentCycle();
            decimal currentBalance = _txManager.ComputeCycleBalance(cycle);
            decimal todaysExpenses = _txManager.ComputeDailyLimitExpensesForDate(DateTime.Today);
            decimal extraAllowanceAddBack = _txManager.ComputeExtraAllowanceInCycle(cycle);
            decimal currentSafeDaily = cycle != null
                ? cycle.ComputeDailyLimit(currentBalance + extraAllowanceAddBack, todaysExpenses, DateTime.Today)
                : 0m;

            decimal newBalance = Math.Round(currentBalance - totalPlannedExpense, 2);
            decimal newSafeDaily = cycle != null
                ? cycle.ComputeDailyLimit(newBalance + extraAllowanceAddBack, todaysExpenses, DateTime.Today)
                : 0m;

            var category = _txManager.GetAllCategories().FirstOrDefault(c => c.CategoryId == categoryId);
            string catName = category != null ? category.Name : "General";
            var today = DateTime.Today;
            var previousMonth = today.AddMonths(-1);
            var activeBudgets = _budgetManager.GetBudgetsForMonth(today.Month, today.Year)
                .Concat(_budgetManager.GetBudgetsForMonth(previousMonth.Month, previousMonth.Year))
                .Where(b => b.CategoryId == categoryId && today >= b.PeriodStart.Date && today <= b.PeriodEnd.Date)
                .GroupBy(b => b.BudgetId).Select(g => g.First()).ToList();
            var budget = activeBudgets.OrderBy(b => b.PeriodType == "Weekly" ? 0 : 1).FirstOrDefault();

            string budgetStatusText;
            if (budget == null)
            {
                budgetStatusText = $"No active weekly or monthly budget set for {catName}";
            }
            else
            {
                decimal used = _budgetManager.GetCategoryUsedAmount(categoryId, budget.PeriodStart, budget.PeriodEnd);
                decimal projected = used + totalPlannedExpense;
                budgetStatusText = $"{budget.PeriodType} budget: {budget.GetStatus(projected)} (₱{projected:N2} / ₱{budget.LimitAmount:N2})";
            }

            string runOutText;
            if (newBalance <= 0)
            {
                runOutText = $"{DateTime.Today:MMM dd, yyyy} (Immediately depleted!)";
            }
            else if (cycle == null)
            {
                runOutText = "No active allowance cycle";
            }
            else
            {
                int elapsedDays = cycle.ComputeElapsedDays(DateTime.Today);
                decimal totalSpentInCycle = _txManager.GetAllTransactions()
                    .OfType<Expense>()
                    .Where(e => e.Date >= cycle.StartDate && e.Date <= DateTime.Today)
                    .Sum(e => e.Amount);

                decimal avgDailySpend = totalSpentInCycle / elapsedDays;
                if (avgDailySpend <= 0)
                {
                    runOutText = $"Beyond {cycle.EndDate:MMM dd, yyyy} (No spending recorded yet)";
                }
                else
                {
                    int daysUntilRunOut = (int)Math.Floor((double)(newBalance / avgDailySpend));
                    DateTime runOutDate = DateTime.Today.AddDays(daysUntilRunOut);
                    runOutText = $"{runOutDate:MMM dd, yyyy} ({daysUntilRunOut} days from today)";
                }
            }

            string advice = newBalance < 0
                ? $"Warning: {detailNote} exceeds your remaining cycle balance by \u20B1{Math.Abs(newBalance):N2}."
                : $"After {detailNote}, your safe daily limit drops from \u20B1{currentSafeDaily:N2} to \u20B1{newSafeDaily:N2}.";

            return new WhatIfScenarioResult
            {
                ScenarioType = scenarioType,
                TotalPlannedAmount = totalPlannedExpense,
                CurrentBalance = currentBalance,
                NewBalance = newBalance,
                CurrentSafeDailyAmount = currentSafeDaily,
                NewSafeDailyAmount = newSafeDaily,
                CategoryName = catName,
                BudgetStatusAfter = budgetStatusText,
                DateLabel = "Projected Allowance Run-Out Date",
                EstimatedDateText = runOutText,
                AdviceSummary = advice
            };
        }
    }
}
