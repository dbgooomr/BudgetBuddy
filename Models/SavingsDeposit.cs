using System;

namespace BudgetBuddy.Models
{
    public class SavingsDeposit
    {
        public int DepositId { get; set; }
        public int GoalId { get; set; }
        public int TransactionId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }

        public bool IsExtraAllowance { get; set; }

        public SavingsDeposit()
        {
            Date = DateTime.Today;
            Notes = string.Empty;
        }

        public SavingsDeposit(int depositId, int goalId, int transactionId, decimal amount, DateTime date, string notes, bool isExtraAllowance = false)
        {
            DepositId = depositId;
            GoalId = goalId;
            TransactionId = transactionId;
            Amount = Math.Round(amount, 2);
            Date = date.Date;
            Notes = notes ?? string.Empty;
            IsExtraAllowance = isExtraAllowance;
        }
    }
}
