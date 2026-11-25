using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BudgetBuddy.Data;
using BudgetBuddy.Models;
using BudgetBuddy.Services;
using BudgetBuddy.Utils;

namespace BudgetBuddy.Forms
{
    public partial class TransactionsForm : Form
    {
        private readonly TransactionManager _txManager;
        private readonly BudgetManager _budgetManager;
        private readonly DatabaseHelper _db;

        private int? _editingTransactionId = null;
        private List<Category> _categories = new List<Category>();

        private bool _filterBusy = false;
        private const string DefaultGridHeader = "All Recorded Transactions (Click a row to edit or delete)";

        public TransactionsForm()
        {
            InitializeComponent();
        }

        public TransactionsForm(TransactionManager txManager, BudgetManager budgetManager, DatabaseHelper db)
        {
            _txManager = txManager;
            _budgetManager = budgetManager;
            _db = db;

            InitializeComponent();
            Load += LoadTransactionData;
        }

        private void LoadTransactionData(object sender, EventArgs args)
        {
            Load -= LoadTransactionData;
            cmbFilterType.SelectedIndex = 0;
            cmbFilterCategory.SelectedIndex = 0;
            WireFilterBarEvents();

            cmbType.SelectedIndexChanged += (s, e) => UpdateFormFieldsForType();
            btnSave.Click += BtnSave_Click;
            btnClear.Click += (s, e) => ResetForm();
            btnDelete.Click += BtnDelete_Click;
            btnSelectAll.Click += (s, e) => ToggleAllRowsChecked();
            btnDeselectAll.Click += (s, e) => ToggleSelectionMode();
            dgvTransactions.CurrentCellDirtyStateChanged += (s, e) => { if (dgvTransactions.IsCurrentCellDirty) dgvTransactions.CommitEdit(DataGridViewDataErrorContexts.Commit); };
            btnBulkDelete.Click += BtnBulkDelete_Click;
            dgvTransactions.CellClick += DgvTransactions_CellClick;

            cmbType.SelectedIndex = 0;
            cmbPaymentMethod.SelectedIndex = 0;
            cmbExpenseKind.SelectedIndex = 0;
            UpdateFormFieldsForType();
            RefreshTransactionsGrid();
        }

        private void WireFilterBarEvents()
        {
            cmbFilterType.SelectedIndexChanged += (s, e) => { if (!_filterBusy) RefreshTransactionsGrid(); };
            cmbFilterCategory.SelectedIndexChanged += (s, e) => { if (!_filterBusy) RefreshTransactionsGrid(); };
            txtFilterSearch.TextChanged += (s, e) => { if (!_filterBusy) RefreshTransactionsGrid(); };
            btnFilterClear.Click += (s, e) =>
            {
                _filterBusy = true;
                cmbFilterType.SelectedIndex = 0;
                cmbFilterCategory.SelectedIndex = 0;
                txtFilterSearch.Clear();
                _filterBusy = false;
                RefreshTransactionsGrid();
            };
        }

        private void RebuildCategoryFilter()
        {
            var names = new List<string>();
            names.AddRange(Income.ValidSources);
            foreach (var c in _db.GetAllCategories())
                if (!names.Contains(c.Name, StringComparer.OrdinalIgnoreCase)) names.Add(c.Name);

            bool same = cmbFilterCategory.Items.Count == names.Count + 1;
            for (int i = 0; same && i < names.Count; i++)
                if (cmbFilterCategory.Items[i + 1].ToString() != names[i]) same = false;
            if (same) return;

            string previous = cmbFilterCategory.SelectedItem?.ToString();
            _filterBusy = true;
            cmbFilterCategory.Items.Clear();
            cmbFilterCategory.Items.Add("All Categories");
            foreach (string n in names) cmbFilterCategory.Items.Add(n);
            int idx = previous != null ? cmbFilterCategory.Items.IndexOf(previous) : 0;
            cmbFilterCategory.SelectedIndex = idx >= 0 ? idx : 0;
            _filterBusy = false;
        }

        private List<Transaction> ApplyFilters(List<Transaction> source)
        {
            IEnumerable<Transaction> query = source;

            string type = cmbFilterType.SelectedItem?.ToString();
            if (type == "Income") query = query.Where(t => t is Income);
            else if (type == "Expense") query = query.Where(t => t is Expense);

            if (cmbFilterCategory.SelectedIndex > 0)
            {
                string cat = cmbFilterCategory.SelectedItem.ToString();
                query = query.Where(t =>
                    (t is Income inc && inc.Source.Equals(cat, StringComparison.OrdinalIgnoreCase)) ||
                    (t is Expense exp && exp.CategoryName.Equals(cat, StringComparison.OrdinalIgnoreCase)));
            }

            string kw = txtFilterSearch.Text.Trim();
            if (kw.Length > 0)
            {
                query = query.Where(t =>
                    (t.Notes != null && t.Notes.IndexOf(kw, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (t is Income inc2 && inc2.Source.IndexOf(kw, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (t is Expense exp2 && exp2.CategoryName.IndexOf(kw, StringComparison.OrdinalIgnoreCase) >= 0));
            }

            return query.ToList();
        }

        private void UpdateFormFieldsForType()
        {
            bool isIncome = cmbType.SelectedItem?.ToString() == "Income";
            cmbSourceOrCategory.DataSource = null;

            if (isIncome)
            {
                lblSourceOrCategory.Text = "Income Source";
                cmbSourceOrCategory.Items.Clear();
                cmbSourceOrCategory.Items.AddRange(Income.ValidSources);
                cmbSourceOrCategory.SelectedIndex = 0;

                lblPaymentMethod.Enabled = false;
                cmbPaymentMethod.Enabled = false;
                lblExpenseKind.Enabled = false;
                cmbExpenseKind.Enabled = false;
            }
            else
            {
                lblSourceOrCategory.Text = "Expense Category";
                _categories = _db.GetAllCategories();
                cmbSourceOrCategory.Items.Clear();
                cmbSourceOrCategory.DisplayMember = "Name";
                cmbSourceOrCategory.ValueMember = "CategoryId";
                cmbSourceOrCategory.DataSource = _categories;

                lblPaymentMethod.Enabled = true;
                cmbPaymentMethod.Enabled = true;
                lblExpenseKind.Enabled = true;
                cmbExpenseKind.Enabled = true;
            }
        }

        private void RefreshTransactionsGrid()
        {
            RebuildCategoryFilter();
            var allTransactions = _txManager.GetAllTransactions();
            var filteredTransactions = ApplyFilters(allTransactions);
            lblGridHeader.Text = filteredTransactions.Count == allTransactions.Count
                ? DefaultGridHeader
                : $"Filtered Transactions \u2014 showing {filteredTransactions.Count} of {allTransactions.Count}";

            var rows = filteredTransactions.Select(t => new
            {
                t.Id,
                Date = t.Date.ToString("yyyy-MM-dd"),
                Type = t.TransactionType,
                CategoryOrSource = t is Income inc ? inc.Source : ((Expense)t).CategoryName,
                Amount = $"\u20B1{t.Amount:N2}",
                Method = t is Expense exp1 ? exp1.PaymentMethod : "-",
                NeedWant = t is Expense exp2 && !string.IsNullOrEmpty(exp2.ExpenseKind) ? exp2.ExpenseKind : "-",
                t.Notes
            }).ToList();

            dgvTransactions.DataSource = rows;
            dgvTransactions.ReadOnly = false;
            dgvTransactions.EditMode = DataGridViewEditMode.EditOnEnter;

            if (!dgvTransactions.Columns.Contains("SelectRow"))
            {
                var selectColumn = new DataGridViewCheckBoxColumn
                {
                    Name = "SelectRow",
                    HeaderText = "Select",
                    Width = 48,
                    ReadOnly = false,
                    ThreeState = false
                };
                dgvTransactions.Columns.Insert(0, selectColumn);
            }
            dgvTransactions.Columns["SelectRow"].Visible = _selectionMode;
            btnDeselectAll.Text = _selectionMode ? "Cancel Select" : "Select";
            dgvTransactions.Columns["SelectRow"].ReadOnly = false;
            foreach (DataGridViewColumn column in dgvTransactions.Columns)
                if (column.Name != "SelectRow") column.ReadOnly = true;
            dgvTransactions.AllowUserToAddRows = false;
            ThemePalette.ConfigureColumn(dgvTransactions, "Id", width: 45);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtAmount.Text.Trim(), out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid positive amount in Pesos (e.g., 120.50).",
                    "Invalid Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmount.Focus();
                return;
            }

            try
            {
                bool isIncome = cmbType.SelectedItem?.ToString() == "Income";
                DateTime date = dtpDate.Value.Date;
                string notes = txtNotes.Text.Trim();

                if (isIncome)
                {
                    string source = cmbSourceOrCategory.SelectedItem?.ToString() ?? "Allowance";
                    if (_editingTransactionId.HasValue)
                    {
                        _txManager.UpdateTransaction(new Income(_editingTransactionId.Value, date, amount, source, notes));
                        MessageBox.Show("Income transaction updated.", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        _txManager.AddIncome(date, amount, source, notes);
                        string extra = source == "Allowance" ? "\nA new allowance cycle has been started automatically." : "";
                        MessageBox.Show("Income transaction recorded." + extra, "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    if (!(cmbSourceOrCategory.SelectedItem is Category selectedCat))
                    {
                        MessageBox.Show("Please select an expense category.", "Missing Category", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string method = cmbPaymentMethod.SelectedItem?.ToString() ?? "Cash";
                    string kind = selectedCat.Name.Equals("Savings", StringComparison.OrdinalIgnoreCase)
                        ? string.Empty
                        : (cmbExpenseKind.SelectedItem?.ToString() ?? "Need");

                    var candidate = new Expense(_editingTransactionId ?? 0, date, amount, selectedCat.CategoryId, selectedCat.Name, method, kind, notes);

                    var warnings = _txManager.CheckExpenseWarnings(candidate, _editingTransactionId);

                    if (warnings.Count > 0)
                    {
                        string msg = string.Join("\n\n", warnings) + "\n\nDo you still want to save this expense?";
                        var proceed = MessageBox.Show(msg, "Spending Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (proceed != DialogResult.Yes)
                            return;
                    }

                    if (_editingTransactionId.HasValue)
                    {
                        _txManager.UpdateTransaction(candidate);
                        MessageBox.Show("Expense updated successfully.", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        _txManager.AddExpense(date, amount, selectedCat.CategoryId, method, kind, notes);
                        MessageBox.Show("Expense recorded successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                ResetForm();
                RefreshTransactionsGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvTransactions_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvTransactions.Columns.Contains("SelectRow") && e.ColumnIndex == dgvTransactions.Columns["SelectRow"].Index) return;
            int id = Convert.ToInt32(dgvTransactions.Rows[e.RowIndex].Cells["Id"].Value);
            var tx = _db.GetTransactionById(id);
            if (tx == null) return;

            _editingTransactionId = tx.Id;
            lblFormHeader.Text = $"Edit Transaction #{tx.Id}";
            btnSave.Text = "Update Record";
            btnDelete.Enabled = true;

            dtpDate.Value = tx.Date;
            txtAmount.Text = tx.Amount.ToString("F2");
            txtNotes.Text = tx.Notes;

            if (tx is Income inc)
            {
                cmbType.SelectedItem = "Income";
                UpdateFormFieldsForType();
                cmbSourceOrCategory.SelectedItem = inc.Source;
            }
            else if (tx is Expense exp)
            {
                cmbType.SelectedItem = "Expense";
                UpdateFormFieldsForType();
                cmbSourceOrCategory.SelectedValue = exp.CategoryId;
                cmbPaymentMethod.SelectedItem = exp.PaymentMethod;
                if (!string.IsNullOrEmpty(exp.ExpenseKind))
                    cmbExpenseKind.SelectedItem = exp.ExpenseKind;
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (!_editingTransactionId.HasValue) return;

            var confirm = MessageBox.Show(
                $"Are you sure you want to delete Transaction #{_editingTransactionId.Value}? This action cannot be undone.",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                _txManager.DeleteTransaction(_editingTransactionId.Value);
                ResetForm();
                RefreshTransactionsGrid();
                _selectionMode = false;
                _allChecked = false;
                if (dgvTransactions.Columns.Contains("SelectRow")) dgvTransactions.Columns["SelectRow"].Visible = false;
                btnDeselectAll.Text = "Select";
                MessageBox.Show("Transaction deleted.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }



        private bool _selectionMode = false;
        private bool _allChecked = false;

        private void ToggleSelectionMode()
        {
            _selectionMode = !_selectionMode;
            var col = dgvTransactions.Columns["SelectRow"];
            col.Visible = _selectionMode;
            btnDeselectAll.Text = _selectionMode ? "Cancel Select" : "Select";
            if (!_selectionMode)
            {
                foreach (DataGridViewRow row in dgvTransactions.Rows)
                    if (!row.IsNewRow) row.Cells["SelectRow"].Value = false;
                _allChecked = false;
            }
        }

        private void ToggleAllRowsChecked()
        {
            if (!_selectionMode) ToggleSelectionMode();
            bool allChecked = dgvTransactions.Rows.Cast<DataGridViewRow>().Where(r => !r.IsNewRow).All(r => Convert.ToBoolean(r.Cells["SelectRow"].Value ?? false));
            _allChecked = !allChecked;
            foreach (DataGridViewRow row in dgvTransactions.Rows)
                if (!row.IsNewRow) row.Cells["SelectRow"].Value = _allChecked;
        }

        private void BtnBulkDelete_Click(object sender, EventArgs e)
        {
            dgvTransactions.EndEdit();
            if (!_selectionMode || !dgvTransactions.Columns.Contains("SelectRow") || !dgvTransactions.Columns["SelectRow"].Visible)
            {
                MessageBox.Show("Click Select All first to enter selection mode, then choose transactions to delete.", "Selection Mode", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var ids = dgvTransactions.Rows.Cast<DataGridViewRow>()
                .Where(r => !r.IsNewRow && Convert.ToBoolean(r.Cells["SelectRow"].Value ?? false))
                .Select(r => Convert.ToInt32(r.Cells["Id"].Value))
                .ToList();

            if (ids.Count == 0)
            {
                MessageBox.Show("Please check the transactions you want to delete.", "Nothing Selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show(
                $"Delete {ids.Count} selected transaction(s)? This action cannot be undone.",
                "Confirm Bulk Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            foreach (int id in ids)
                _txManager.DeleteTransaction(id);

            ResetForm();
            _selectionMode = false;
            _allChecked = false;
            RefreshTransactionsGrid();
            MessageBox.Show($"{ids.Count} transaction(s) deleted.", "Deleted",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ResetForm()
        {
            _editingTransactionId = null;
            lblFormHeader.Text = "Add New Transaction";
            btnSave.Text = "Save Record";
            btnDelete.Enabled = false;
            dtpDate.Value = DateTime.Today;
            txtAmount.Clear();
            txtNotes.Clear();
            cmbType.SelectedIndex = 0;
            UpdateFormFieldsForType();
        }
    }
}
