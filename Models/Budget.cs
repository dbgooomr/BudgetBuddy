using System;

namespace BudgetBuddy.Models
{
    public class Budget
    {
        private int _budgetId;
        private int _categoryId;
        private string _categoryName;
        private int _month;
        private int _year;
        private decimal _limitAmount;
        public string PeriodType { get; set; } = "Monthly";
        public DateTime PeriodStart { get; set; } = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        public DateTime PeriodEnd { get; set; } = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month));

        public int BudgetId
        {
            get => _budgetId;
            set => _budgetId = value;
        }

        public int CategoryId
        {
            get => _categoryId;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Valid category is required for a budget.");
                _categoryId = value;
            }
        }

        public string CategoryName
        {
            get => _categoryName;
            set => _categoryName = string.IsNullOrWhiteSpace(value) ? "Category" : value.Trim();
        }

        public int Month
        {
            get => _month;
            set
            {
                if (value < 1 || value > 12)
                    throw new ArgumentException("Month must be between 1 and 12.");
                _month = value;
            }
        }

        public int Year
        {
            get => _year;
            set
            {
                if (value < 2000 || value > 2100)
                    throw new ArgumentException("Year must be valid.");
                _year = value;
            }
        }

        public decimal LimitAmount
        {
            get => _limitAmount;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Budget limit must be greater than zero.");
                _limitAmount = Math.Round(value, 2);
            }
        }

        public Budget()
        {
            _month = DateTime.Today.Month;
            _year = DateTime.Today.Year;
            _limitAmount = 1000m;
        }

        public Budget(int budgetId, int categoryId, string categoryName, int month, int year, decimal limitAmount)
        {
            BudgetId = budgetId;
            CategoryId = categoryId;
            CategoryName = categoryName;
            Month = month;
            Year = year;
            LimitAmount = limitAmount;
            PeriodType = "Monthly";
            PeriodStart = new DateTime(year, month, 1);
            PeriodEnd = new DateTime(year, month, DateTime.DaysInMonth(year, month));
        }

        public Budget(int budgetId, int categoryId, string categoryName, DateTime start, DateTime end, string periodType, decimal limitAmount)
            : this(budgetId, categoryId, categoryName, start.Month, start.Year, limitAmount)
        {
            if (end.Date < start.Date) throw new ArgumentException("End date must be on or after start date.");
            PeriodType = periodType == "Weekly" ? "Weekly" : "Monthly";
            PeriodStart = start.Date;
            PeriodEnd = end.Date;
        }

        public decimal ComputeRemaining(decimal usedAmount)
        {
            return Math.Round(_limitAmount - usedAmount, 2);
        }

        public double ComputeUsagePercentage(decimal usedAmount)
        {
            if (_limitAmount <= 0) return 0;
            return (double)(usedAmount / _limitAmount) * 100.0;
        }

        public string GetStatus(decimal usedAmount)
        {
            if (usedAmount > _limitAmount)
                return "Over Budget";
            if (usedAmount >= _limitAmount * 0.80m)
                return "Near Limit";
            return "Safe";
        }
    }
}
