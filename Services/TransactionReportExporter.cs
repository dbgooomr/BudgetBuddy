using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BudgetBuddy.Interfaces;
using BudgetBuddy.Models;

namespace BudgetBuddy.Services
{
    public class TransactionReportExporter : IExportable
    {
        private readonly IEnumerable<Transaction> _transactions;

        public TransactionReportExporter(IEnumerable<Transaction> transactions)
        {
            _transactions = transactions ?? throw new ArgumentNullException(nameof(transactions));
        }

        public void ExportToCsv(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("Export file path cannot be empty.");

            var sb = new StringBuilder();
            sb.AppendLine("ID,Date,Type,CategoryOrSource,AmountPHP,PaymentMethod,NeedOrWant,Notes");

            foreach (var tx in _transactions)
            {
                string catOrSource = string.Empty;
                string payMethod = string.Empty;
                string kind = string.Empty;

                if (tx is Income inc)
                {
                    catOrSource = inc.Source;
                }
                else if (tx is Expense exp)
                {
                    catOrSource = exp.CategoryName;
                    payMethod = exp.PaymentMethod;
                    kind = exp.ExpenseKind;
                }

                sb.AppendLine(string.Join(",",
                    tx.Id,
                    tx.Date.ToString("yyyy-MM-dd"),
                    EscapeCsv(tx.TransactionType),
                    EscapeCsv(catOrSource),
                    tx.Amount.ToString("F2"),
                    EscapeCsv(payMethod),
                    EscapeCsv(kind),
                    EscapeCsv(tx.Notes)));
            }

            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        }

        private static string EscapeCsv(string value)
        {
            if (string.IsNullOrEmpty(value)) return "\"\"";
            return "\"" + value.Replace("\"", "\"\"") + "\"";
        }
    }
}
