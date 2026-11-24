using System;

namespace BudgetBuddy.Models
{
    /// <summary>
    /// Base class representing a financial transaction (Income or Expense).
    /// Demonstrates encapsulation via private fields and validated public properties.
    /// </summary>
    public class Transaction
    {
        private int _id;
        private DateTime _date;
        private decimal _amount;
        private string _notes;

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public DateTime Date
        {
            get => _date;
            set
            {
                if (value == default)
                    throw new ArgumentException("Transaction date must be valid.");
                _date = value.Date;
            }
        }

        public decimal Amount
        {
            get => _amount;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Transaction amount must be greater than zero.");
                _amount = Math.Round(value, 2);
            }
        }

        public string Notes
        {
            get => _notes;
            set => _notes = string.IsNullOrWhiteSpace(value) ? string.Empty : value.Trim();
        }

        /// <summary>
        /// Converts decimal Pesos to whole centavos (INTEGER) as specified in the database schema.
        /// </summary>
        public long AmountInCentavos => (long)Math.Round(_amount * 100m);

        public string TransactionType => this is Income ? "Income" : (this is Expense ? "Expense" : "Transaction");

        public Transaction()
        {
            _date = DateTime.Today;
            _amount = 1.00m;
            _notes = string.Empty;
        }

        public Transaction(int id, DateTime date, decimal amount, string notes)
        {
            Id = id;
            Date = date;
            Amount = amount;
            Notes = notes;
        }
    }
}
