using System;
using System.Drawing;
using System.Windows.Forms;
using BudgetBuddy.Controls;
using BudgetBuddy.Data;
using BudgetBuddy.Services;
using BudgetBuddy.Utils;

namespace BudgetBuddy.Forms
{
    public partial class MainForm : Form
    {
        private readonly DatabaseHelper _db;
        private readonly TransactionManager _txManager;
        private readonly BudgetManager _budgetManager;
        private readonly ScenarioSimulator _simulator;

        private Form _activeChildForm;
        private NavButton _activeNavButton;
        private int _tipIndex;

        private static readonly string[] MoneyTips =
        {
            "Pay yourself first: set aside a little savings the day your allowance arrives.",
            "Needs before wants. Cover fare, food and school costs, then treat yourself.",
            "Small daily spends add up. Check your safe-to-spend limit before you buy.",
            "Bring baon a few days a week. Little savings stack up fast.",
            "Planning a big purchase? Try it in the What-If Simulator first.",
            "A savings goal with a deadline feels real. Give yours a date!",
            "Review your history every week to spot where your money quietly goes."
        };

        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(
            DatabaseHelper db,
            TransactionManager txManager,
            BudgetManager budgetManager,
            ScenarioSimulator simulator)
        {
            _db = db;
            _txManager = txManager;
            _budgetManager = budgetManager;
            _simulator = simulator;

            InitializeComponent();
            WireNavigationEvents();
            Load += (s, e) => OpenDashboard();
        }

        private void WireNavigationEvents()
        {
            btnNavDashboard.Click += (s, e) => OpenDashboard();
            btnNavTransactions.Click += (s, e) => OpenTransactions();
            btnNavCategories.Click += (s, e) => OpenCategories();
            btnNavBudgets.Click += (s, e) => OpenBudgets();
            btnNavAllowanceGoals.Click += (s, e) => OpenAllowanceGoals();
            btnNavWhatIf.Click += (s, e) => OpenWhatIf();
            btnNavHistory.Click += (s, e) => OpenHistory();
        }

        public void OpenDashboard()
        {
            ShowChildForm(new DashboardForm(_txManager, _budgetManager, _db, OpenWhatIf, OpenTransactions, OpenAllowanceGoals), btnNavDashboard);
        }

        public void OpenTransactions()
        {
            ShowChildForm(new TransactionsForm(_txManager, _budgetManager, _db), btnNavTransactions);
        }

        public void OpenCategories()
        {
            ShowChildForm(new CategoriesForm(_db), btnNavCategories);
        }

        public void OpenBudgets()
        {
            ShowChildForm(new BudgetsForm(_budgetManager, _db, _txManager), btnNavBudgets);
        }

        public void OpenAllowanceGoals()
        {
            ShowChildForm(new AllowanceGoalsForm(_txManager, _db), btnNavAllowanceGoals);
        }

        public void OpenWhatIf()
        {
            ShowChildForm(new WhatIfForm(_simulator, _txManager, _db), btnNavWhatIf);
        }

        public void OpenHistory()
        {
            ShowChildForm(new HistorySearchForm(_txManager, _db), btnNavHistory);
        }

        private void ShowChildForm(Form childForm, NavButton navButton)
        {
            if (_activeChildForm != null)
            {
                _activeChildForm.Close();
                _activeChildForm.Dispose();
            }

            if (_activeNavButton != null)
            {
                _activeNavButton.IsActive = false;
            }

            _activeNavButton = navButton;
            if (_activeNavButton != null)
            {
                _activeNavButton.IsActive = true;
            }

            lblTipText.Text = MoneyTips[_tipIndex % MoneyTips.Length];
            _tipIndex++;

            _activeChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            childForm.AutoScroll = true;

            pnlContentHost.Controls.Add(childForm);
            childForm.Show();
        }

        private void BtnResetDemo_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "Reset all sample transactions, budgets, allowance cycle, and savings goals to default student demo data?",
                "Confirm Demo Reset",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                _db.ResetSampleData();
                OpenDashboard();
                MessageBox.Show("Demo data has been restored.", "Reset Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
