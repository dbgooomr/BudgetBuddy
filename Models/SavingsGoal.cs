using System;

namespace BudgetBuddy.Models
{
    public class SavingsGoal
    {
        private int _goalId;
        private string _goalName;
        private decimal _targetAmount;
        private decimal _savedAmount;
        private DateTime? _targetDate;

        public int GoalId
        {
            get => _goalId;
            set => _goalId = value;
        }

        public string GoalName
        {
            get => _goalName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Savings goal name is required.");
                _goalName = value.Trim();
            }
        }

        public decimal TargetAmount
        {
            get => _targetAmount;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Target amount must be greater than zero.");
                _targetAmount = Math.Round(value, 2);
            }
        }

        public decimal SavedAmount
        {
            get => _savedAmount;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Saved amount cannot be negative.");
                _savedAmount = Math.Round(value, 2);
            }
        }

        public DateTime? TargetDate
        {
            get => _targetDate;
            set => _targetDate = value?.Date;
        }

        public decimal RemainingAmount => Math.Max(0m, Math.Round(_targetAmount - _savedAmount, 2));

        public SavingsGoal()
        {
            _goalName = "New Savings Goal";
            _targetAmount = 1000m;
            _savedAmount = 0m;
        }

        public SavingsGoal(int goalId, string goalName, decimal targetAmount, decimal savedAmount, DateTime? targetDate)
        {
            GoalId = goalId;
            GoalName = goalName;
            TargetAmount = targetAmount;
            SavedAmount = savedAmount;
            TargetDate = targetDate;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Deposit amount must be greater than zero.");
            _savedAmount = Math.Round(_savedAmount + amount, 2);
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Withdrawal amount must be greater than zero.");
            _savedAmount = Math.Max(0m, Math.Round(_savedAmount - amount, 2));
        }

        public double ComputeProgress()
        {
            if (_targetAmount <= 0) return 0;
            double pct = (double)(_savedAmount / _targetAmount) * 100.0;
            return Math.Min(100.0, Math.Round(pct, 1));
        }
    }
}
