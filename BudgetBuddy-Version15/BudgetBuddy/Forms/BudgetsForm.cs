using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using BudgetBuddy.Data;
using BudgetBuddy.Models;
using BudgetBuddy.Services;
using BudgetBuddy.Utils;

namespace BudgetBuddy.Forms
{
    public partial class BudgetsForm : Form
    {
        private readonly BudgetManager _budgetManager;
        private readonly DatabaseHelper _db;
        private readonly TransactionManager _txManager;
        private Label lblFundsStatus;
        private int? _selectedBudgetId = null;
        private bool _budgetSelectionMode = false;
        private Button btnSelectBudgets, btnSelectAllBudgets, btnBulkDeleteBudgets;
        private ComboBox cmbBudgetCategoryFilter, cmbBudgetStatusFilter;
        private Button btnBudgetFilterClear;

        public BudgetsForm(BudgetManager budgetManager, DatabaseHelper db, TransactionManager txManager = null)
        {
            _budgetManager = budgetManager;
            _db = db;
            _txManager = txManager;

            InitializeComponent();

            for (int m = 1; m <= 12; m++)
                cmbMonth.Items.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(m));

            cmbMonth.SelectedIndex = DateTime.Today.Month - 1;
            numYear.Value = DateTime.Today.Year;

            var categories = _db.GetAllCategories()
                .Where(c => !c.Name.Equals("Savings", StringComparison.OrdinalIgnoreCase))
                .ToList();
            cmbCategory.DisplayMember = "Name";
            cmbCategory.ValueMember = "CategoryId";
            cmbCategory.DataSource = categories;

            // Filter row (category + status) above the Select / Delete buttons
            cmbBudgetCategoryFilter = new ComboBox { Font = ThemePalette.FontBody, DropDownStyle = ComboBoxStyle.DropDownList, Location = new Point(16, 42), Size = new Size(190, 28) };
            cmbBudgetCategoryFilter.Items.Add("All Categories");
            foreach (var c in categories) cmbBudgetCategoryFilter.Items.Add(c.Name);
            cmbBudgetCategoryFilter.SelectedIndex = 0;
            cmbBudgetStatusFilter = new ComboBox { Font = ThemePalette.FontBody, DropDownStyle = ComboBoxStyle.DropDownList, Location = new Point(214, 42), Size = new Size(150, 28) };
            cmbBudgetStatusFilter.Items.AddRange(new object[] { "All Statuses", "Safe", "Near Limit", "Over Budget" });
            cmbBudgetStatusFilter.SelectedIndex = 0;
            btnBudgetFilterClear = new Button { Text = "Clear", Location = new Point(372, 40), Size = new Size(80, 32) };
            ThemePalette.StyleSecondaryButton(btnBudgetFilterClear);
            cardTable.Controls.Add(cmbBudgetCategoryFilter);
            cardTable.Controls.Add(cmbBudgetStatusFilter);
            cardTable.Controls.Add(btnBudgetFilterClear);
            cmbBudgetCategoryFilter.SelectedIndexChanged += (s, e) => RefreshBudgetsTable();
            cmbBudgetStatusFilter.SelectedIndexChanged += (s, e) => RefreshBudgetsTable();
            btnBudgetFilterClear.Click += (s, e) => { cmbBudgetCategoryFilter.SelectedIndex = 0; cmbBudgetStatusFilter.SelectedIndex = 0; };

            cmbMonth.SelectedIndexChanged += (s, e) => RefreshBudgetsTable();
            numYear.ValueChanged += (s, e) => RefreshBudgetsTable();
            cmbPeriodType.SelectedIndexChanged += (s, e) => UpdatePeriodControls();
            dtpWeekStart.ValueChanged += (s, e) => RefreshBudgetsTable();
            btnSaveBudget.Click += BtnSaveBudget_Click;
            btnDeleteBudget.Click += BtnDeleteBudget_Click;
            dgvBudgets.CellClick += DgvBudgets_CellClick;
            dgvBudgets.CellFormatting += DgvBudgets_CellFormatting;
            dgvBudgets.CurrentCellDirtyStateChanged += DgvBudgets_CurrentCellDirtyStateChanged;
            btnSelectBudgets = new Button { Text = "Select", Location = new Point(16, 82), Size = new Size(100, 28) };
            btnSelectAllBudgets = new Button { Text = "Select All", Location = new Point(122, 82), Size = new Size(100, 28) };
            btnBulkDeleteBudgets = new Button { Text = "Delete Selected", Location = new Point(228, 82), Size = new Size(130, 28) };
            ThemePalette.StyleSecondaryButton(btnSelectBudgets); ThemePalette.StyleSecondaryButton(btnSelectAllBudgets); ThemePalette.StyleButton(btnBulkDeleteBudgets, ThemePalette.DangerRose, Color.White);
            cardTable.Controls.Add(btnSelectBudgets); cardTable.Controls.Add(btnSelectAllBudgets); cardTable.Controls.Add(btnBulkDeleteBudgets);
            dgvBudgets.SetBounds(16, 124, dgvBudgets.Width, 390);
            lblFundsStatus = new Label
            {
                Font = ThemePalette.FontBodyBold,
                Location = new Point(16, 522),
                Size = new Size(570, 44),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                AutoSize = false
            };
            cardTable.Controls.Add(lblFundsStatus);
            btnSelectBudgets.Click += (s, e) => { _budgetSelectionMode = !_budgetSelectionMode; RefreshBudgetsTable(); };
            btnSelectAllBudgets.Click += (s, e) => { if (!_budgetSelectionMode) { _budgetSelectionMode = true; RefreshBudgetsTable(); } ToggleAllBudgetChecks(); };
            btnBulkDeleteBudgets.Click += BtnBulkDeleteBudgets_Click;
            btnDeleteBudget.Visible = false;

            ThemePalette.ApplySpacing(this);
            this.Load += (s, e) => { UpdatePeriodControls(); RefreshBudgetsTable(); };
        }

        private void UpdatePeriodControls()
        {
            bool weekly = cmbPeriodType.SelectedItem?.ToString() == "Weekly";
            lblWeekStart.Visible = dtpWeekStart.Visible = weekly;
            lblMonth.Visible = cmbMonth.Visible = lblYear.Visible = numYear.Visible = !weekly;
            lblLimit.Text = weekly ? "Weekly Limit Amount (₱)" : "Monthly Limit Amount (₱)";
            btnSaveBudget.Text = weekly ? "Save Weekly Budget" : "Save Monthly Budget";
            int categoryLabelY = (weekly ? 166 : 226) + ThemePalette.SectionShift;
            lblCategory.Top = categoryLabelY;
            cmbCategory.Top = lblCategory.Bottom + 2;
            lblLimit.Top = cmbCategory.Bottom + 14;
            txtLimitAmount.Top = lblLimit.Bottom + 2;
            btnSaveBudget.Top = txtLimitAmount.Bottom + 18;
            btnDeleteBudget.Top = btnSaveBudget.Bottom + 10;
            lblCardHeader.Text = weekly ? "Configure Weekly Limit" : "Configure Monthly Limit";
            RefreshBudgetsTable();
        }

        private (DateTime Start, DateTime End) GetSelectedPeriod()
        {
            if (cmbPeriodType.SelectedItem?.ToString() == "Weekly")
            {
                DateTime chosen = dtpWeekStart.Value.Date;
                int diff = ((int)chosen.DayOfWeek + 6) % 7;
                DateTime monday = chosen.AddDays(-diff);
                return (monday, monday.AddDays(6));
            }
            int month = cmbMonth.SelectedIndex + 1;
            int year = (int)numYear.Value;
            DateTime start = new DateTime(year, month, 1);
            return (start, start.AddMonths(1).AddDays(-1));
        }

        private void RefreshBudgetsTable()
        {
            if (cmbPeriodType.SelectedItem == null || cmbMonth.SelectedIndex < 0) return;
            var period = GetSelectedPeriod();
            string periodType = cmbPeriodType.SelectedItem.ToString();
            lblTableHeader.Text = periodType == "Weekly"
                ? $"Weekly Budget Status — {period.Start:MMM d} to {period.End:MMM d, yyyy}"
                : $"Monthly Budget Status — {period.Start:MMMM yyyy}";

            var budgets = _budgetManager.GetBudgetsForPeriod(period.Start, period.End, periodType);
            var rows = budgets.Select(b =>
            {
                decimal used = _budgetManager.GetCategoryUsedAmount(b.CategoryId, period.Start, period.End);
                decimal remaining = b.ComputeRemaining(used);
                string status = b.GetStatus(used);
                double pct = b.ComputeUsagePercentage(used);
                return new { b.BudgetId, Category = b.CategoryName, Limit = $"₱{b.LimitAmount:N2}", Used = $"₱{used:N2}", Remaining = $"₱{remaining:N2}", Usage = $"{pct:F0}%", Status = status };
            }).ToList();
            // Apply the category / status filters
            if (cmbBudgetCategoryFilter != null && cmbBudgetCategoryFilter.SelectedIndex > 0)
            {
                string categoryFilter = cmbBudgetCategoryFilter.SelectedItem.ToString();
                rows = rows.Where(r => r.Category == categoryFilter).ToList();
            }
            if (cmbBudgetStatusFilter != null && cmbBudgetStatusFilter.SelectedIndex > 0)
            {
                string statusFilter = cmbBudgetStatusFilter.SelectedItem.ToString();
                rows = rows.Where(r => r.Status == statusFilter).ToList();
            }
            dgvBudgets.DataSource = rows;
            ThemePalette.ConfigureColumn(dgvBudgets, "BudgetId", visible: false);
            if (dgvBudgets.Columns.Contains("SelectRow")) dgvBudgets.Columns.Remove("SelectRow");
            if (_budgetSelectionMode)
            {
                var check = new DataGridViewCheckBoxColumn { Name = "SelectRow", HeaderText = "Select", Width = 55, ReadOnly = false };
                dgvBudgets.Columns.Insert(0, check); dgvBudgets.ReadOnly = false;
                foreach (DataGridViewColumn col in dgvBudgets.Columns) col.ReadOnly = col.Name != "SelectRow";
                dgvBudgets.EditMode = DataGridViewEditMode.EditOnEnter;
            }
            else { dgvBudgets.ReadOnly = true; }
            btnSelectBudgets.Text = _budgetSelectionMode ? "Cancel Select" : "Select";
            RefreshFundsStatus(period.Start, period.End, periodType);
        }

        /// <summary>Money Left This Cycle (same figure as the Dashboard), or null when no cycle is active.</summary>
        private decimal? GetMoneyLeftThisCycle()
        {
            if (_txManager == null) return null;
            var cycle = _txManager.GetCurrentCycle();
            if (cycle == null) return null;
            return Math.Max(0m, _txManager.ComputeCycleBalance(cycle));
        }

        private void RefreshFundsStatus(DateTime start, DateTime end, string periodType)
        {
            if (lblFundsStatus == null) return;
            decimal? moneyLeft = GetMoneyLeftThisCycle();
            if (moneyLeft == null)
            {
                lblFundsStatus.Text = "No active allowance cycle. Start one to compare budgets with your money left.";
                lblFundsStatus.ForeColor = ThemePalette.TextSecondary;
                return;
            }

            decimal remainingBudgets = _budgetManager.GetTotalRemainingBudget(start, end, periodType);
            string label = periodType == "Weekly" ? "weekly" : "monthly";
            if (remainingBudgets > moneyLeft.Value)
            {
                lblFundsStatus.Text = $"\u26A0 Remaining {label} budgets (\u20B1{remainingBudgets:N2}) are more than your Money Left This Cycle (\u20B1{moneyLeft.Value:N2}) by \u20B1{remainingBudgets - moneyLeft.Value:N2}.";
                lblFundsStatus.ForeColor = ThemePalette.DangerRose;
            }
            else
            {
                lblFundsStatus.Text = $"\u2713 Remaining {label} budgets: \u20B1{remainingBudgets:N2}  |  Money Left This Cycle: \u20B1{moneyLeft.Value:N2}";
                lblFundsStatus.ForeColor = ThemePalette.SafeGreen;
            }
        }

        private void DgvBudgets_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.ColumnIndex >= dgvBudgets.Columns.Count)
                return;

            if (dgvBudgets.Columns[e.ColumnIndex].Name == "Status" && e.Value != null)
            {
                string status = e.Value.ToString();
                if (status == "Over Budget")
                {
                    e.CellStyle.BackColor = ThemePalette.DangerRoseBg;
                    e.CellStyle.ForeColor = ThemePalette.DangerRose;
                    e.CellStyle.Font = ThemePalette.FontBodyBold;
                }
                else if (status == "Near Limit")
                {
                    e.CellStyle.BackColor = ThemePalette.WarningAmberBg;
                    e.CellStyle.ForeColor = ThemePalette.WarningAmber;
                    e.CellStyle.Font = ThemePalette.FontBodyBold;
                }
                else
                {
                    e.CellStyle.BackColor = ThemePalette.SafeGreenBg;
                    e.CellStyle.ForeColor = ThemePalette.SafeGreen;
                    e.CellStyle.Font = ThemePalette.FontBodyBold;
                }
            }
        }

        private void DgvBudgets_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || _budgetSelectionMode) return;
            _selectedBudgetId = Convert.ToInt32(dgvBudgets.Rows[e.RowIndex].Cells["BudgetId"].Value);
            string catName = dgvBudgets.Rows[e.RowIndex].Cells["Category"].Value.ToString();
            string limitStr = dgvBudgets.Rows[e.RowIndex].Cells["Limit"].Value.ToString().Replace("\u20B1", "").Replace(",", "");

            cmbCategory.Text = catName;
            txtLimitAmount.Text = limitStr;
        }

        private void BtnSaveBudget_Click(object sender, EventArgs e)
        {
            if (!(cmbCategory.SelectedItem is Category selectedCat))
            {
                MessageBox.Show("Please select an expense category.", "Missing Category", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtLimitAmount.Text.Trim(), out decimal limit) || limit <= 0)
            {
                MessageBox.Show("Please enter a valid positive budget limit.", "Invalid Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLimitAmount.Focus();
                return;
            }

            try
            {
                var period = GetSelectedPeriod();
                string periodType = cmbPeriodType.SelectedItem.ToString();

                // Compare the budgets with the Dashboard's Money Left This Cycle before saving
                decimal? moneyLeft = GetMoneyLeftThisCycle();
                if (moneyLeft.HasValue)
                {
                    decimal usedByCategory = _budgetManager.GetCategoryUsedAmount(selectedCat.CategoryId, period.Start, period.End);
                    decimal newRemaining = _budgetManager.GetTotalRemainingBudget(period.Start, period.End, periodType, selectedCat.CategoryId)
                        + Math.Max(0m, limit - usedByCategory);
                    if (newRemaining > moneyLeft.Value)
                    {
                        var answer = MessageBox.Show(
                            $"After saving, your remaining {periodType.ToLower()} budgets will total \u20B1{newRemaining:N2}, " +
                            $"but your Money Left This Cycle is only \u20B1{moneyLeft.Value:N2} (over by \u20B1{newRemaining - moneyLeft.Value:N2}).\n\n" +
                            "You won't be able to spend every budget in full. Save anyway?",
                            "Budget Exceeds Money Left", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (answer != DialogResult.Yes) return;
                    }
                }

                _budgetManager.SetBudget(selectedCat.CategoryId, period.Start, period.End, periodType, limit);
                txtLimitAmount.Clear();
                RefreshBudgetsTable();
                MessageBox.Show($"{periodType} budget for '{selectedCat.Name}' saved.", "Budget Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void DgvBudgets_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvBudgets.IsCurrentCellDirty && dgvBudgets.CurrentCell is DataGridViewCheckBoxCell)
                dgvBudgets.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void ToggleAllBudgetChecks()
        {
            if (!dgvBudgets.Columns.Contains("SelectRow")) return;
            bool allChecked = dgvBudgets.Rows.Cast<DataGridViewRow>().Where(r => !r.IsNewRow).All(r => Convert.ToBoolean(r.Cells["SelectRow"].Value ?? false));
            foreach (DataGridViewRow row in dgvBudgets.Rows) if (!row.IsNewRow) row.Cells["SelectRow"].Value = !allChecked;
        }

        private void BtnBulkDeleteBudgets_Click(object sender, EventArgs e)
        {
            if (!_budgetSelectionMode) { MessageBox.Show("Click Select first."); return; }
            var ids = dgvBudgets.Rows.Cast<DataGridViewRow>().Where(r => !r.IsNewRow && Convert.ToBoolean(r.Cells["SelectRow"].Value ?? false)).Select(r => Convert.ToInt32(r.Cells["BudgetId"].Value)).ToList();
            if (ids.Count == 0) { MessageBox.Show("Select one or more budgets."); return; }
            if (MessageBox.Show($"Remove {ids.Count} selected budget limit(s) from this displayed period?", "Confirm Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            foreach (int id in ids) _budgetManager.DeleteBudget(id);
            _selectedBudgetId = null; txtLimitAmount.Clear(); RefreshBudgetsTable();
            MessageBox.Show($"Removed {ids.Count} budget(s).", "Removed", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnDeleteBudget_Click(object sender, EventArgs e)
        {
            if (!_selectedBudgetId.HasValue)
            {
                MessageBox.Show("Select a budget row in the table first.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show("Remove this category budget limit?", "Confirm Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                _budgetManager.DeleteBudget(_selectedBudgetId.Value);
                _selectedBudgetId = null;
                txtLimitAmount.Clear();
                RefreshBudgetsTable();
            }
        }
    }
}
