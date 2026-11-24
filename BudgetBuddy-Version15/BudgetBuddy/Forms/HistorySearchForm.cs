using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BudgetBuddy.Data;
using BudgetBuddy.Models;
using BudgetBuddy.Services;
using BudgetBuddy.Utils;

namespace BudgetBuddy.Forms
{
    public partial class HistorySearchForm : Form
    {
        private readonly TransactionManager _txManager;
        private readonly DatabaseHelper _db;
        private List<Transaction> _currentResults = new List<Transaction>();

        public HistorySearchForm(TransactionManager txManager, DatabaseHelper db)
        {
            _txManager = txManager;
            _db = db;

            InitializeComponent();
            ThemePalette.ApplySpacing(this);

            dtpFrom.Value = DateTime.Today.AddDays(-30);
            dtpTo.Value = DateTime.Today;

            var catOptions = new List<Category> { new Category(0, "All Categories", true) };
            catOptions.AddRange(_db.GetAllCategories());
            cmbFilterCategory.DisplayMember = "Name";
            cmbFilterCategory.ValueMember = "CategoryId";
            cmbFilterCategory.DataSource = catOptions;

            cmbFilterType.SelectedIndex = 0;
            cmbFilterKind.SelectedIndex = 0;

            chkDateRange.CheckedChanged += (s, e) =>
            {
                dtpFrom.Enabled = chkDateRange.Checked;
                dtpTo.Enabled = chkDateRange.Checked;
            };

            btnSearch.Click += (s, e) => ExecuteSearch();
            btnResetFilters.Click += BtnResetFilters_Click;
            btnExportCsv.Click += BtnExportCsv_Click;

            this.Load += (s, e) => ExecuteSearch();
        }

        private void ExecuteSearch()
        {
            DateTime? fromDate = chkDateRange.Checked ? dtpFrom.Value.Date : (DateTime?)null;
            DateTime? toDate = chkDateRange.Checked ? dtpTo.Value.Date : (DateTime?)null;

            string typeFilter = cmbFilterType.SelectedIndex <= 0 ? "All" : cmbFilterType.SelectedItem.ToString();
            int? catId = (cmbFilterCategory.SelectedItem is Category c && c.CategoryId > 0) ? c.CategoryId : (int?)null;
            string kindFilter = cmbFilterKind.SelectedIndex <= 0 ? "All" : cmbFilterKind.SelectedItem.ToString();
            string keyword = txtKeyword.Text.Trim();

            _currentResults = _txManager.SearchTransactions(fromDate, toDate, catId, typeFilter, kindFilter, keyword);

            decimal totalIncome = _currentResults.OfType<Income>().Sum(i => i.Amount);
            decimal totalExpense = _currentResults.OfType<Expense>().Sum(e => e.Amount);
            lblResultsSummary.Text = $"Matching Records: {_currentResults.Count}   |   Filtered Income: \u20B1{totalIncome:N2}   |   Filtered Expenses: \u20B1{totalExpense:N2}";

            var rows = _currentResults.Select(t => new
            {
                t.Id,
                Date = t.Date.ToString("yyyy-MM-dd"),
                Type = t.TransactionType,
                CategoryOrSource = t is Income inc ? inc.Source : ((Expense)t).CategoryName,
                Amount = $"\u20B1{t.Amount:N2}",
                Payment = t is Expense e1 ? e1.PaymentMethod : "-",
                NeedOrWant = t is Expense e2 && !string.IsNullOrEmpty(e2.ExpenseKind) ? e2.ExpenseKind : "-",
                t.Notes
            }).ToList();

            dgvHistory.DataSource = rows;
            ThemePalette.ConfigureColumn(dgvHistory, "Id", width: 45);
        }

        private void BtnResetFilters_Click(object sender, EventArgs e)
        {
            chkDateRange.Checked = false;
            cmbFilterType.SelectedIndex = 0;
            cmbFilterCategory.SelectedIndex = 0;
            cmbFilterKind.SelectedIndex = 0;
            txtKeyword.Clear();
            ExecuteSearch();
        }

        private void BtnExportCsv_Click(object sender, EventArgs e)
        {
            if (_currentResults.Count == 0)
            {
                MessageBox.Show("There are no filtered transactions to export.", "Nothing to Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV Files (*.csv)|*.csv";
                sfd.FileName = $"BudgetBuddy_Transactions_{DateTime.Today:yyyyMMdd}.csv";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var exporter = new TransactionReportExporter(_currentResults);
                        exporter.ExportToCsv(sfd.FileName);
                        MessageBox.Show($"Exported {_currentResults.Count} transaction(s) to:\n{sfd.FileName}",
                            "CSV Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
