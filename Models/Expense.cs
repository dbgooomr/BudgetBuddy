using System;

namespace BudgetBuddy.Models
{
    public class Expense : Transaction
    {
        public static readonly string[] ValidPaymentMethods = { "Cash", "E-wallet" };
        public static readonly string[] ValidExpenseKinds = { "Need", "Want" };

        private int _categoryId;
        private string _categoryName;
        private string _paymentMethod;
        private string _expenseKind;

        public int CategoryId
        {
            get => _categoryId;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("A valid expense category must be selected.");
                _categoryId = value;
            }
        }

        public string CategoryName
        {
            get => _categoryName;
            set => _categoryName = string.IsNullOrWhiteSpace(value) ? "Uncategorized" : value.Trim();
        }

        public string PaymentMethod
        {
            get => _paymentMethod;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Payment method is required.");
                _paymentMethod = value.Trim();
            }
        }

        public string ExpenseKind
        {
            get => _expenseKind;
            set => _expenseKind = string.IsNullOrWhiteSpace(value) ? string.Empty : value.Trim();
        }

        public Expense() : base()
        {
            _categoryId = 1;
            _categoryName = "Food and Canteen";
            _paymentMethod = "Cash";
            _expenseKind = "Need";
        }

        public Expense(int id, DateTime date, decimal amount, int categoryId, string categoryName,
            string paymentMethod, string expenseKind, string notes)
            : base(id, date, amount, notes)
        {
            CategoryId = categoryId;
            CategoryName = categoryName;
            PaymentMethod = paymentMethod;
            ExpenseKind = expenseKind;
        }
    }
}
