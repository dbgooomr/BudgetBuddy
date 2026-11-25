using System;
using System.Linq;
using System.Windows.Forms;
using BudgetBuddy.Data;
using BudgetBuddy.Models;
using BudgetBuddy.Services;
using BudgetBuddy.Utils;

namespace BudgetBuddy.Forms
{
    public partial class WhatIfForm : Form
    {
        private readonly ScenarioSimulator _simulator;
        private readonly TransactionManager _txManager;
        private readonly DatabaseHelper _db;

        public WhatIfForm()
        {
            InitializeComponent();
        }

        public WhatIfForm(ScenarioSimulator simulator, TransactionManager txManager, DatabaseHelper db)
        {
            _simulator = simulator;
            _txManager = txManager;
            _db = db;

            InitializeComponent();

            Load += LoadScenarioInputs;
        }

        private void LoadScenarioInputs(object sender, EventArgs args)
        {
            Load -= LoadScenarioInputs;
            cmbScenarioType.SelectedIndexChanged += (s, e) => UpdateScenarioInputs();
            btnSimulate.Click += BtnSimulate_Click;

            cmbScenarioType.SelectedIndex = 0;
            UpdateScenarioInputs();
        }

        private void UpdateScenarioInputs()
        {
            int idx = cmbScenarioType.SelectedIndex;
            cmbCategoryOrGoal.DataSource = null;
            cmbCategoryOrGoal.Items.Clear();

            if (idx == 0) 
            {
                lblAmount.Text = "Planned Purchase Amount (\u20B1)";
                lblRepeatCount.Enabled = false;
                numRepeatCount.Enabled = false;

                lblCategoryOrGoal.Text = "Expense Category";
                var cats = _db.GetAllCategories().Where(c => !c.Name.Equals("Savings", StringComparison.OrdinalIgnoreCase)).ToList();
                cmbCategoryOrGoal.DisplayMember = "Name";
                cmbCategoryOrGoal.ValueMember = "CategoryId";
                cmbCategoryOrGoal.DataSource = cats;
            }
            else if (idx == 1) 
            {
                lblAmount.Text = "Amount Per Occurrence (\u20B1)";
                lblRepeatCount.Enabled = true;
                numRepeatCount.Enabled = true;

                lblCategoryOrGoal.Text = "Expense Category";
                var cats = _db.GetAllCategories().Where(c => !c.Name.Equals("Savings", StringComparison.OrdinalIgnoreCase)).ToList();
                cmbCategoryOrGoal.DisplayMember = "Name";
                cmbCategoryOrGoal.ValueMember = "CategoryId";
                cmbCategoryOrGoal.DataSource = cats;
            }
            else 
            {
                lblAmount.Text = "Planned Daily Saving Amount (\u20B1)";
                lblRepeatCount.Enabled = false;
                numRepeatCount.Enabled = false;

                lblCategoryOrGoal.Text = "Target Savings Goal";
                var goals = _db.GetAllSavingsGoals();
                cmbCategoryOrGoal.DisplayMember = "GoalName";
                cmbCategoryOrGoal.ValueMember = "GoalId";
                cmbCategoryOrGoal.DataSource = goals;
            }
        }

        private void BtnSimulate_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtAmount.Text.Trim(), out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid positive amount in Pesos.", "Invalid Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmount.Focus();
                return;
            }

            try
            {
                int idx = cmbScenarioType.SelectedIndex;
                WhatIfScenarioResult result;

                if (idx == 0)
                {
                    int catId = cmbCategoryOrGoal.SelectedItem is Category c ? c.CategoryId : 1;
                    result = _simulator.SimulateOneTimePurchase(amount, catId);
                }
                else if (idx == 1)
                {
                    int catId = cmbCategoryOrGoal.SelectedItem is Category c ? c.CategoryId : 1;
                    int count = (int)numRepeatCount.Value;
                    result = _simulator.SimulateRepeatingExpense(amount, count, catId);
                }
                else
                {
                    if (!(cmbCategoryOrGoal.SelectedItem is SavingsGoal goal))
                    {
                        MessageBox.Show("Please create a savings goal first in Allowance & Goals.", "No Savings Goal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    result = _simulator.SimulateDailySaving(amount, goal);
                }

                DisplaySimulationResult(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Simulation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplaySimulationResult(WhatIfScenarioResult res)
        {
            lblSimSummaryBanner.Text = res.AdviceSummary;
            lblSimSummaryBanner.BackColor = res.NewBalance < 0 ? ThemePalette.DangerRoseBg : ThemePalette.SafeGreenBg;
            lblSimSummaryBanner.ForeColor = res.NewBalance < 0 ? ThemePalette.DangerRose : ThemePalette.SafeGreen;

            lblBalanceCompare.Text = $"Cycle Balance:   \u20B1{res.CurrentBalance:N2}  \u2192  \u20B1{res.NewBalance:N2}";
            lblBalanceCompare.ForeColor = res.NewBalance < 0 ? ThemePalette.DangerRose : ThemePalette.TextPrimary;

            lblDailyCompare.Text = $"Safe Daily Limit:   \u20B1{res.CurrentSafeDailyAmount:N2}  \u2192  \u20B1{res.NewSafeDailyAmount:N2}/day";
            lblBudgetImpact.Text = $"Category Budget Status:   {res.BudgetStatusAfter}";
            lblDateImpactTitle.Text = res.DateLabel.ToUpperInvariant();
            lblDateImpactValue.Text = res.EstimatedDateText;
        }
    }
}
