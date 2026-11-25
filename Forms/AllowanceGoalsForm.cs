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
    public partial class AllowanceGoalsForm : Form
    {
        private readonly TransactionManager _txManager;
        private readonly DatabaseHelper _db;
        private int? _selectedGoalId = null;
        private bool _goalSelectionMode = false;
        private bool _depositSelectionMode = false;

        private bool _cycleFilterBusy = false;

        public AllowanceGoalsForm()
        {
            InitializeComponent();
        }

        public AllowanceGoalsForm(TransactionManager txManager, DatabaseHelper db)
        {
            _txManager = txManager;
            _db = db;

            InitializeComponent();

            cmbDepositMethod.SelectedIndex = 0;
            cmbGoalStatus.SelectedIndex = 0;
            cmbCycleStatus.SelectedIndex = 0;
            cmbCycleYear.SelectedIndex = 0;

            dtpStartDate.Value = DateTime.Today;
            dtpNextDate.Value = DateTime.Today.AddDays(14);
            dtpGoalTargetDate.Value = DateTime.Today.AddMonths(2);

            btnStartCycle.Click += BtnStartCycle_Click;
            btnCreateGoal.Click += BtnCreateGoal_Click;
            btnDeposit.Click += BtnDeposit_Click;
            btnDeleteGoal.Click += BtnDeleteGoal_Click;
            btnUndoDeposit.Click += BtnUndoDeposit_Click;
            dgvGoals.CellClick += DgvGoals_CellClick;
            dgvGoals.CurrentCellDirtyStateChanged += DgvGoals_CurrentCellDirtyStateChanged;
            dgvDeposits.CurrentCellDirtyStateChanged += DgvDeposits_CurrentCellDirtyStateChanged;
            btnSelectGoals.Click += (s,e) => { _goalSelectionMode=!_goalSelectionMode; RefreshGoalsGrid(); };
            btnSelectAllGoals.Click += (s,e) => { if(!_goalSelectionMode){_goalSelectionMode=true; RefreshGoalsGrid();} ToggleAllGoalChecks(); };
            btnDeleteSelectedGoals.Click += BtnBulkDeleteGoals_Click;
            btnSelectDeposits.Click += (s,e) => { _depositSelectionMode=!_depositSelectionMode; RefreshDepositsGrid(); };
            btnSelectAllDeposits.Click += (s,e) => { if(!_depositSelectionMode){_depositSelectionMode=true; RefreshDepositsGrid();} ToggleAllDepositChecks(); };
            btnDeleteSelectedDeposits.Click += BtnBulkDeleteDeposits_Click;

            WireExtraControlEvents();

            this.Load += (s, e) =>
            {
                RefreshCycleInfo();
                RefreshGoalsGrid();
            };
        }


        private void RefreshCycleInfo()
        {
            RefreshCycleHistory();
            var cycle = _txManager.GetCurrentCycle();
            if (cycle == null)
            {
                lblActiveCycleInfo.Text = "No active allowance cycle yet.\nEnter your allowance amount and next allowance date below to start.";
                return;
            }

            decimal balance = _txManager.ComputeCycleBalance(cycle);
            decimal totalFunds = _txManager.ComputeCycleTotalFunds(cycle);
            int daysLeft = cycle.ComputeDaysLeft(DateTime.Today);
            decimal safeDaily = _txManager.ComputeRemainingSafeSpendToday();

            lblActiveCycleInfo.Text =
                $"Current Cycle: {cycle.StartDate:MMM dd, yyyy} \u2192 {cycle.EndDate:MMM dd, yyyy}\n" +
                $"New Allowance: \u20B1{cycle.AllowanceAmount:N2}  |  Starting Carry-over: \u20B1{cycle.CarryOver:N2}\n" +
                $"Total This Cycle (carry-over + deposits): \u20B1{totalFunds:N2}\n" +
                $"Money Left This Cycle: \u20B1{balance:N2} ({daysLeft} day(s) left)\n" +
                $"Remaining Safe to Spend Today: \u20B1{safeDaily:N2}";
        }

        private void RefreshGoalsGrid()
        {
            var goals = GetFilteredGoals();
            var rows = goals.Select(g => new
            {
                g.GoalId,
                Goal = g.GoalName,
                Target = $"\u20B1{g.TargetAmount:N2}",
                Saved = $"\u20B1{g.SavedAmount:N2}",
                Needed = $"\u20B1{g.RemainingAmount:N2}",
                Progress = $"{g.ComputeProgress():F0}%",
                TargetDate = g.TargetDate.HasValue ? g.TargetDate.Value.ToString("yyyy-MM-dd") : "-"
            }).ToList();

            dgvGoals.DataSource = rows;
            ThemePalette.ConfigureColumn(dgvGoals, "GoalId", visible: false);
            dgvGoals.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvGoals.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvGoals.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            if (dgvGoals.Columns.Contains("Goal")) { dgvGoals.Columns["Goal"].FillWeight = 220; dgvGoals.Columns["Goal"].MinimumWidth = 190; }
            if (dgvGoals.Columns.Contains("Target")) { dgvGoals.Columns["Target"].FillWeight = 115; dgvGoals.Columns["Target"].MinimumWidth = 110; }
            if (dgvGoals.Columns.Contains("Saved")) { dgvGoals.Columns["Saved"].FillWeight = 115; dgvGoals.Columns["Saved"].MinimumWidth = 110; }
            if (dgvGoals.Columns.Contains("Needed")) { dgvGoals.Columns["Needed"].FillWeight = 115; dgvGoals.Columns["Needed"].MinimumWidth = 110; }
            if (dgvGoals.Columns.Contains("Progress")) { dgvGoals.Columns["Progress"].FillWeight = 85; dgvGoals.Columns["Progress"].MinimumWidth = 80; }
            if (dgvGoals.Columns.Contains("TargetDate")) { dgvGoals.Columns["TargetDate"].HeaderText = "Target Date"; dgvGoals.Columns["TargetDate"].FillWeight = 125; dgvGoals.Columns["TargetDate"].MinimumWidth = 115; }
            if (dgvGoals.Columns.Contains("SelectRow")) dgvGoals.Columns.Remove("SelectRow");
            if (_goalSelectionMode) { var check=new DataGridViewCheckBoxColumn { Name="SelectRow", HeaderText="Select", Width=55, ReadOnly=false }; dgvGoals.Columns.Insert(0,check); dgvGoals.ReadOnly=false; foreach(DataGridViewColumn col in dgvGoals.Columns) col.ReadOnly = col.Name != "SelectRow"; dgvGoals.EditMode=DataGridViewEditMode.EditOnEnter; } else { dgvGoals.ReadOnly = true; }
            btnSelectGoals.Text=_goalSelectionMode ? "Cancel Select" : "Select";

            if (goals.Count > 0 && (!_selectedGoalId.HasValue || goals.All(g => g.GoalId != _selectedGoalId.Value)))
            {
                _selectedGoalId = goals[0].GoalId;
            }

            UpdateSelectedGoalDetails();
        }

        private void UpdateSelectedGoalDetails()
        {
            if (!_selectedGoalId.HasValue)
            {
                progressSelectedGoal.Percentage = 0;
                lblGoalProgressDetail.Text = "Create or select a savings goal above.";
                dgvDeposits.DataSource = null;
                return;
            }

            var goal = _db.GetSavingsGoalById(_selectedGoalId.Value);
            if (goal == null) return;

            double pct = goal.ComputeProgress();
            progressSelectedGoal.Percentage = pct;
            progressSelectedGoal.CustomLabel = $"{goal.GoalName}: {pct:F0}%";
            lblGoalProgressDetail.Text = $"Saved \u20B1{goal.SavedAmount:N2} of \u20B1{goal.TargetAmount:N2} \u2014 Still needed: \u20B1{goal.RemainingAmount:N2}";

            var deposits = _db.GetDepositsForGoal(goal.GoalId).Select(d => new
            {
                d.DepositId,
                Date = d.Date.ToString("yyyy-MM-dd"),
                Amount = $"\u20B1{d.Amount:N2}",
                d.Notes
            }).ToList();

            dgvDeposits.DataSource = deposits;
            ThemePalette.ConfigureColumn(dgvDeposits, "DepositId", visible: false);
            dgvDeposits.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDeposits.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDeposits.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvDeposits.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDeposits.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            if (dgvDeposits.Columns.Contains("Date")) dgvDeposits.Columns["Date"].MinimumWidth = 100;
            if (dgvDeposits.Columns.Contains("Amount")) dgvDeposits.Columns["Amount"].MinimumWidth = 110;
            if (dgvDeposits.Columns.Contains("Notes")) dgvDeposits.Columns["Notes"].MinimumWidth = 140;
            if (dgvDeposits.Columns.Contains("SelectRow")) dgvDeposits.Columns.Remove("SelectRow");
            if (_depositSelectionMode)
            {
                var check = new DataGridViewCheckBoxColumn { Name="SelectRow", HeaderText="Select", Width=55, ReadOnly=false };
                dgvDeposits.Columns.Insert(0, check);
                dgvDeposits.ReadOnly = false;
                foreach (DataGridViewColumn col in dgvDeposits.Columns) col.ReadOnly = col.Name != "SelectRow";
                dgvDeposits.EditMode = DataGridViewEditMode.EditOnEnter;
            }
            else dgvDeposits.ReadOnly = true;
            dgvDeposits.ClearSelection();
            btnSelectDeposits.Text = _depositSelectionMode ? "Cancel Select" : "Select";
            btnSelectAllDeposits.Enabled = deposits.Count > 0;
            btnDeleteSelectedDeposits.Enabled = deposits.Count > 0;
        }

        private void WireExtraControlEvents()
        {
            txtGoalSearch.TextChanged += (s, e) => RefreshGoalsGrid();
            cmbGoalStatus.SelectedIndexChanged += (s, e) => RefreshGoalsGrid();
            btnGoalClearFilter.Click += (s, e) => { txtGoalSearch.Clear(); cmbGoalStatus.SelectedIndex = 0; };
            cmbCycleStatus.SelectedIndexChanged += (s, e) => { if (!_cycleFilterBusy) RefreshCycleHistory(); };
            cmbCycleYear.SelectedIndexChanged += (s, e) => { if (!_cycleFilterBusy) RefreshCycleHistory(); };
            btnCycleClearFilter.Click += (s, e) =>
            {
                _cycleFilterBusy = true;
                cmbCycleStatus.SelectedIndex = 0;
                cmbCycleYear.SelectedIndex = 0;
                _cycleFilterBusy = false;
                RefreshCycleHistory();
            };
        }

        private List<SavingsGoal> GetFilteredGoals()
        {
            IEnumerable<SavingsGoal> query = _db.GetAllSavingsGoals();

            string keyword = txtGoalSearch != null ? txtGoalSearch.Text.Trim() : string.Empty;
            if (keyword.Length > 0)
                query = query.Where(g => g.GoalName.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0);

            string status = cmbGoalStatus?.SelectedItem?.ToString();
            if (status == "Completed")
                query = query.Where(g => g.SavedAmount >= g.TargetAmount);
            else if (status == "In Progress")
                query = query.Where(g => g.SavedAmount < g.TargetAmount);

            return query.ToList();
        }

        private void RefreshCycleHistory()
        {
            if (dgvCycles == null) return;

            var all = _db.GetAllCycles();
            var current = _txManager.GetCurrentCycle();
            int currentId = current != null ? current.CycleId : -1;

            var years = all.Select(c => c.StartDate.Year).Distinct().OrderByDescending(x => x).Select(x => x.ToString()).ToList();
            bool sameYears = cmbCycleYear.Items.Count == years.Count + 1;
            for (int i = 0; sameYears && i < years.Count; i++)
                if (cmbCycleYear.Items[i + 1].ToString() != years[i]) sameYears = false;

            if (!sameYears)
            {
                _cycleFilterBusy = true;
                string previousYear = cmbCycleYear.SelectedItem?.ToString();
                cmbCycleYear.Items.Clear();
                cmbCycleYear.Items.Add("All Years");
                foreach (string yr in years) cmbCycleYear.Items.Add(yr);
                int yearIndex = previousYear != null ? cmbCycleYear.Items.IndexOf(previousYear) : 0;
                cmbCycleYear.SelectedIndex = yearIndex >= 0 ? yearIndex : 0;
                _cycleFilterBusy = false;
            }

            IEnumerable<AllowanceCycle> query = all;
            string status = cmbCycleStatus.SelectedItem?.ToString();
            if (status == "Active") query = query.Where(c => c.CycleId == currentId);
            else if (status == "Past") query = query.Where(c => c.CycleId != currentId);

            if (cmbCycleYear.SelectedIndex > 0 && int.TryParse(cmbCycleYear.SelectedItem.ToString(), out int year))
                query = query.Where(c => c.StartDate.Year == year);

            var rows = query
                .OrderByDescending(c => c.StartDate)
                .ThenByDescending(c => c.CycleId)
                .Select(c => new
                {
                    c.CycleId,
                    Period = $"{c.StartDate:MMM d} \u2192 {c.EndDate:MMM d, yyyy}",
                    Allowance = $"\u20B1{c.AllowanceAmount:N2}",
                    Status = c.CycleId == currentId ? "Active" : "Past"
                }).ToList();

            dgvCycles.DataSource = rows;
            ThemePalette.ConfigureColumn(dgvCycles, "CycleId", visible: false);
            dgvCycles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            if (dgvCycles.Columns.Contains("Period")) { dgvCycles.Columns["Period"].FillWeight = 150; dgvCycles.Columns["Period"].MinimumWidth = 110; }
            if (dgvCycles.Columns.Contains("Allowance")) { dgvCycles.Columns["Allowance"].FillWeight = 95; dgvCycles.Columns["Allowance"].MinimumWidth = 80; }
            if (dgvCycles.Columns.Contains("Status")) { dgvCycles.Columns["Status"].FillWeight = 60; dgvCycles.Columns["Status"].MinimumWidth = 55; }
            dgvCycles.ClearSelection();
        }

        private void DgvGoals_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (_goalSelectionMode) return;
            _selectedGoalId = Convert.ToInt32(dgvGoals.Rows[e.RowIndex].Cells["GoalId"].Value);
            _depositSelectionMode = false;
            UpdateSelectedGoalDetails();
        }

        private void BtnStartCycle_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtAllowanceAmount.Text.Trim(), out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid positive allowance amount.", "Invalid Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAllowanceAmount.Focus();
                return;
            }

            if (dtpNextDate.Value.Date < dtpStartDate.Value.Date)
            {
                MessageBox.Show("Next allowance date cannot be earlier than the received date.", "Invalid Dates", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var created = _txManager.StartNewAllowanceCycle(dtpStartDate.Value.Date, dtpNextDate.Value.Date, amount);
                txtAllowanceAmount.Clear();
                RefreshCycleInfo();
                MessageBox.Show(
                    $"New allowance cycle started!\nAllowance: \u20B1{created.AllowanceAmount:N2}\nCarry-over from previous cycle: \u20B1{created.CarryOver:N2}",
                    "Cycle Started",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCreateGoal_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtGoalName.Text))
            {
                MessageBox.Show("Please enter a goal name (e.g., Laptop Fund).", "Missing Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtGoalTarget.Text.Trim(), out decimal target) || target <= 0)
            {
                MessageBox.Show("Please enter a valid target amount greater than zero.", "Invalid Target", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var goal = _db.AddSavingsGoal(new SavingsGoal(0, txtGoalName.Text.Trim(), target, 0m, dtpGoalTargetDate.Value.Date));
                _selectedGoalId = goal.GoalId;
                txtGoalName.Clear();
                txtGoalTarget.Clear();
                RefreshGoalsGrid();
                MessageBox.Show($"Savings goal '{goal.GoalName}' created.", "Goal Created", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDeposit_Click(object sender, EventArgs e)
        {
            if (!_selectedGoalId.HasValue)
            {
                MessageBox.Show("Please select a savings goal from the list first.", "No Goal Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!decimal.TryParse(txtDepositAmount.Text.Trim(), out decimal depositAmt) || depositAmt <= 0)
            {
                MessageBox.Show("Please enter a valid deposit amount greater than zero.", "Invalid Deposit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string method = cmbDepositMethod.SelectedItem?.ToString() ?? "Cash";

                if (chkExtraAllowance != null && chkExtraAllowance.Checked)
                {
                    _txManager.RecordSavingsDeposit(_selectedGoalId.Value, depositAmt, DateTime.Today, method, "Extra allowance", true);
                    txtDepositAmount.Clear();
                    chkExtraAllowance.Checked = false;
                    RefreshCycleInfo();
                    RefreshGoalsGrid();
                    MessageBox.Show(
                        $"Extra allowance deposited.\nMoney Left This Cycle went down by \u20B1{depositAmt:N2}, but Safe to Spend Today was not changed.",
                        "Extra Allowance Deposited", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                _txManager.RecordSavingsDeposit(_selectedGoalId.Value, depositAmt, DateTime.Today, method, "Goal deposit");
                txtDepositAmount.Clear();
                RefreshCycleInfo();
                RefreshGoalsGrid();
                MessageBox.Show("Deposit saved and recorded under the Savings expense category.", "Deposit Recorded", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnUndoDeposit_Click(object sender, EventArgs e)
        {
            if (!_selectedGoalId.HasValue || dgvDeposits.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a deposit row in the deposit history table first.", "No Deposit Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int depositId = Convert.ToInt32(dgvDeposits.SelectedRows[0].Cells["DepositId"].Value);
            var confirm = MessageBox.Show(
                "Delete this deposit? Its corresponding Savings expense record will also be removed, and the goal progress and balance will be updated.",
                "Confirm Delete Deposit",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                _txManager.DeleteSavingsDeposit(depositId, _selectedGoalId.Value);
                RefreshCycleInfo();
                RefreshGoalsGrid();
                MessageBox.Show("Deposit deleted. Goal progress and balance have been updated.", "Deposit Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DgvDeposits_SelectionChanged(object sender, EventArgs e)
        {
            btnUndoDeposit.Enabled = _selectedGoalId.HasValue && dgvDeposits.SelectedRows.Count > 0;
        }

        private void DgvGoals_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvGoals.IsCurrentCellDirty && dgvGoals.CurrentCell is DataGridViewCheckBoxCell) dgvGoals.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }
        private void DgvDeposits_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvDeposits.IsCurrentCellDirty && dgvDeposits.CurrentCell is DataGridViewCheckBoxCell)
                dgvDeposits.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void ToggleAllGoalChecks()
        {
            bool allChecked=dgvGoals.Rows.Cast<DataGridViewRow>().Where(r=>!r.IsNewRow).All(r=>Convert.ToBoolean(r.Cells["SelectRow"].Value??false));
            foreach(DataGridViewRow row in dgvGoals.Rows) if(!row.IsNewRow) row.Cells["SelectRow"].Value=!allChecked;
        }
        private void ToggleAllDepositChecks()
        {
            if (!dgvDeposits.Columns.Contains("SelectRow")) return;
            bool allChecked = dgvDeposits.Rows.Cast<DataGridViewRow>().Where(r=>!r.IsNewRow).All(r=>Convert.ToBoolean(r.Cells["SelectRow"].Value??false));
            foreach (DataGridViewRow row in dgvDeposits.Rows) if (!row.IsNewRow) row.Cells["SelectRow"].Value = !allChecked;
        }

        private void RefreshDepositsGrid()
        {
            UpdateSelectedGoalDetails();
        }

        private void BtnBulkDeleteDeposits_Click(object sender, EventArgs e)
        {
            if (!_selectedGoalId.HasValue) return;
            if (dgvDeposits.IsCurrentCellDirty) dgvDeposits.CommitEdit(DataGridViewDataErrorContexts.Commit);
            System.Collections.Generic.List<int> ids;
            if (_depositSelectionMode && dgvDeposits.Columns.Contains("SelectRow"))
                ids = dgvDeposits.Rows.Cast<DataGridViewRow>().Where(r=>!r.IsNewRow && Convert.ToBoolean(r.Cells["SelectRow"].Value??false)).Select(r=>Convert.ToInt32(r.Cells["DepositId"].Value)).ToList();
            else
                ids = dgvDeposits.SelectedRows.Cast<DataGridViewRow>().Where(r=>!r.IsNewRow).Select(r=>Convert.ToInt32(r.Cells["DepositId"].Value)).ToList();
            if (ids.Count == 0) { MessageBox.Show("Click the deposit you want to delete first (or use Select for several).", "No Deposit Selected", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (MessageBox.Show($"Delete {ids.Count} selected deposit(s)? Their linked Savings expenses will also be removed.", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            foreach (int id in ids) _txManager.DeleteSavingsDeposit(id, _selectedGoalId.Value);
            _depositSelectionMode = false;
            RefreshCycleInfo();
            RefreshGoalsGrid();
            MessageBox.Show($"Deleted {ids.Count} deposit(s).", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void BtnBulkDeleteGoals_Click(object sender, EventArgs e)
        {
            if(!_goalSelectionMode){MessageBox.Show("Click Select first.");return;}
            var ids=dgvGoals.Rows.Cast<DataGridViewRow>().Where(r=>!r.IsNewRow && Convert.ToBoolean(r.Cells["SelectRow"].Value??false)).Select(r=>Convert.ToInt32(r.Cells["GoalId"].Value)).ToList();
            if(ids.Count==0){MessageBox.Show("Select one or more goals.");return;}
            if(MessageBox.Show($"Delete {ids.Count} selected savings goal(s) and their deposit history?","Confirm Delete",MessageBoxButtons.YesNo,MessageBoxIcon.Warning)!=DialogResult.Yes)return;
            foreach(int id in ids) _db.DeleteSavingsGoal(id);
            _selectedGoalId=null; RefreshGoalsGrid(); RefreshCycleInfo(); MessageBox.Show($"Deleted {ids.Count} goal(s).","Deleted",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void BtnDeleteGoal_Click(object sender, EventArgs e)
        {
            if (!_selectedGoalId.HasValue) return;
            var goal = _db.GetSavingsGoalById(_selectedGoalId.Value);
            if (goal == null) return;

            var confirm = MessageBox.Show($"Delete savings goal '{goal.GoalName}'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.Yes)
            {
                _db.DeleteSavingsGoal(goal.GoalId);
                _selectedGoalId = null;
                RefreshGoalsGrid();
            }
        }
    }
}
