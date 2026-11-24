using System;

namespace BudgetBuddy.Models
{
    /// <summary>
    /// Derived class representing money received by the student (Allowance, Part-time job, Scholarship, Gift).
    /// Inherits shared fields and validation from Transaction.
    /// </summary>
    public class Income : Transaction
    {
        public static readonly string[] ValidSources =
        {
            "Allowance",
            "Part-time job",
            "Scholarship or stipend",
            "Gift"
        };

        private string _source;

        public string Source
        {
            get => _source;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Income source is required.");
                _source = value.Trim();
            }
        }

        public Income() : base()
        {
            _source = "Allowance";
        }

        public Income(int id, DateTime date, decimal amount, string source, string notes)
            : base(id, date, amount, notes)
        {
            Source = source;
        }
    }
}
