using System;

namespace BudgetBuddy.Models
{
    public class AllowanceCycle
    {
        private int _cycleId;
        private DateTime _startDate;
        private DateTime _endDate;
        private decimal _allowanceAmount;
        private decimal _carryOver;

        public int CycleId
        {
            get => _cycleId;
            set => _cycleId = value;
        }

        public DateTime StartDate
        {
            get => _startDate;
            set => _startDate = value.Date;
        }

        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                if (value.Date < _startDate)
                    throw new ArgumentException("Next allowance date cannot be earlier than the cycle start date.");
                _endDate = value.Date;
            }
        }

        public decimal AllowanceAmount
        {
            get => _allowanceAmount;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Allowance amount must be greater than zero.");
                _allowanceAmount = Math.Round(value, 2);
            }
        }

        public decimal CarryOver
        {
            get => _carryOver;
            set => _carryOver = Math.Round(value, 2);
        }

        public AllowanceCycle()
        {
            _startDate = DateTime.Today;
            _endDate = DateTime.Today.AddDays(14);
            _allowanceAmount = 3000m;
            _carryOver = 0m;
        }

        public AllowanceCycle(int cycleId, DateTime startDate, DateTime endDate, decimal allowanceAmount, decimal carryOver)
        {
            CycleId = cycleId;
            StartDate = startDate;
            EndDate = endDate;
            AllowanceAmount = allowanceAmount;
            CarryOver = carryOver;
        }

        public bool IsOverdue(DateTime referenceDate)
        {
            return referenceDate.Date > _endDate.Date;
        }

        public int ComputeDaysLeft(DateTime referenceDate)
        {
            int rawDays = (_endDate.Date - referenceDate.Date).Days;
            return Math.Max(1, rawDays);
        }

        public int ComputeElapsedDays(DateTime referenceDate)
        {
            int elapsed = (referenceDate.Date - _startDate.Date).Days + 1;
            return Math.Max(1, elapsed);
        }

        public decimal ComputeDailyLimit(decimal currentCycleBalance, decimal todaysExpenses, DateTime referenceDate)
        {
            if (currentCycleBalance <= 0)
                return 0m;

            int daysLeft = ComputeDaysLeft(referenceDate);
            decimal startOfDayPool = currentCycleBalance + todaysExpenses;
            return Math.Round(startOfDayPool / daysLeft, 2);
        }

        public decimal ComputeRemainingSafeSpendToday(decimal currentCycleBalance, decimal todaysExpenses, DateTime referenceDate)
        {
            decimal dailyLimit = ComputeDailyLimit(currentCycleBalance, todaysExpenses, referenceDate);
            return Math.Max(0m, Math.Round(dailyLimit - todaysExpenses, 2));
        }
    }
}
