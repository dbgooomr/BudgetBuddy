using System;
using System.Drawing;
using System.Windows.Forms;
using BudgetBuddy.Controls;
using BudgetBuddy.Utils;

namespace BudgetBuddy.Forms
{
    partial class DashboardForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private Label lblSubtitle;
        private Button btnBigWhatIf;
        private Button btnQuickAddTx;
        private Button btnQuickNewCycle;

        private RoundedPanel cardBalance;
        private Label lblBalanceCaption;
        private Label lblBalanceValue;
        private Label lblCycleDates;

        private RoundedPanel cardDaysLeft;
        private Label lblDaysCaption;
        private Label lblDaysValue;
        private Label lblOverdueStatus;

        private RoundedPanel cardDailyLimit;
        private Label lblDailyCaption;
        private Label lblDailyValue;
        private Label lblDailyFlag;

        private RoundedPanel cardMonthSummary;
        private Label lblMonthCaption;
        private Label lblMonthIncome;
        private Label lblMonthExpense;

        private RoundedPanel cardNeedsWants;
        private Label lblNeedsWantsTitle;
        private Panel pnlNeedsWantsChart;
        private Label lblNeedsWantsLegend;

        private RoundedPanel cardCategoryChart;
        private Label lblCatChartTitle;
        private Panel pnlCategoryBars;

        private RoundedPanel cardRecentTx;
        private Label lblRecentTitle;
        private DataGridView dgvRecent;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.lblSubtitle = new Label();
            this.btnBigWhatIf = new Button();
            this.btnQuickAddTx = new Button();
            this.btnQuickNewCycle = new Button();

            this.cardBalance = new RoundedPanel();
            this.lblBalanceCaption = new Label();
            this.lblBalanceValue = new Label();
            this.lblCycleDates = new Label();

            this.cardDaysLeft = new RoundedPanel();
            this.lblDaysCaption = new Label();
            this.lblDaysValue = new Label();
            this.lblOverdueStatus = new Label();

            this.cardDailyLimit = new RoundedPanel();
            this.lblDailyCaption = new Label();
            this.lblDailyValue = new Label();
            this.lblDailyFlag = new Label();

            this.cardMonthSummary = new RoundedPanel();
            this.lblMonthCaption = new Label();
            this.lblMonthIncome = new Label();
            this.lblMonthExpense = new Label();

            this.cardNeedsWants = new RoundedPanel();
            this.lblNeedsWantsTitle = new Label();
            this.pnlNeedsWantsChart = new Panel();
            this.lblNeedsWantsLegend = new Label();

            this.cardCategoryChart = new RoundedPanel();
            this.lblCatChartTitle = new Label();
            this.pnlCategoryBars = new Panel();

            this.cardRecentTx = new RoundedPanel();
            this.lblRecentTitle = new Label();
            this.dgvRecent = new DataGridView();

            this.SuspendLayout();

            // Header
            this.lblTitle.Text = "Student Financial Dashboard";
            this.lblTitle.Font = ThemePalette.FontPageTitle;
            this.lblTitle.ForeColor = ThemePalette.TextPrimary;
            this.lblTitle.Location = new Point(0, 0);
            this.lblTitle.Size = new Size(460, 32);

            this.lblSubtitle.Text = DateTime.Today.ToString("dddd, MMMM d") + "  \u2022  Live overview of your allowance";
            this.lblSubtitle.Font = ThemePalette.FontBody;
            this.lblSubtitle.ForeColor = ThemePalette.TextSecondary;
            this.lblSubtitle.Location = new Point(0, 32);
            this.lblSubtitle.Size = new Size(540, 22);

            // Large What-If Simulator Button (Explicitly required on Dashboard by Section 9)
            this.btnBigWhatIf.Text = "\u26A1 What-If Simulator";
            this.btnBigWhatIf.Size = new Size(190, 44);
            this.btnBigWhatIf.Location = new Point(770, 6);
            this.btnBigWhatIf.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ThemePalette.StyleButton(this.btnBigWhatIf, ThemePalette.AccentIndigo, Color.White);

            this.btnQuickAddTx.Text = "+ Record Transaction";
            this.btnQuickAddTx.Size = new Size(175, 44);
            this.btnQuickAddTx.Location = new Point(585, 6);
            this.btnQuickAddTx.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ThemePalette.StyleButton(this.btnQuickAddTx, ThemePalette.PrimaryTeal, Color.White);

            this.btnQuickNewCycle.Text = "New Allowance";
            this.btnQuickNewCycle.Size = new Size(135, 44);
            this.btnQuickNewCycle.Location = new Point(440, 6);
            this.btnQuickNewCycle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ThemePalette.StyleSecondaryButton(this.btnQuickNewCycle);

            // KPI Cards Row (Y = 66, Height = 122)
            SetupKpiCard(cardBalance, 0, 66, 232, 122, lblBalanceCaption, "MONEY LEFT THIS CYCLE", lblBalanceValue, "\u20B10.00", lblCycleDates, "Cycle: -");
            SetupKpiCard(cardDaysLeft, 244, 66, 220, 122, lblDaysCaption, "DAYS TO NEXT ALLOWANCE", lblDaysValue, "0 days", lblOverdueStatus, "On track");
            SetupKpiCard(cardDailyLimit, 476, 66, 245, 122, lblDailyCaption, "SAFE TO SPEND TODAY", lblDailyValue, "\u20B10.00", lblDailyFlag, "Within daily limit");
            SetupKpiCard(cardMonthSummary, 733, 66, 227, 122, lblMonthCaption, "THIS MONTH SUMMARY", lblMonthIncome, "Income: \u20B10.00", lblMonthExpense, "Expenses: \u20B10.00");
            lblMonthIncome.Font = ThemePalette.FontBodyBold;
            lblMonthIncome.ForeColor = ThemePalette.SafeGreen;
            lblMonthExpense.Font = ThemePalette.FontBodyBold;
            lblMonthExpense.ForeColor = ThemePalette.DangerRose;

            // Hero balance card: signature teal -> indigo gradient with white text
            cardBalance.FillColor = ThemePalette.HeroStart;
            cardBalance.GradientEnd = ThemePalette.HeroEnd;
            cardBalance.BorderThickness = 0;
            cardBalance.Icon = IconKind.Wallet;
            lblBalanceCaption.ForeColor = Color.FromArgb(204, 251, 241);
            lblBalanceCaption.Font = ThemePalette.FontCaptionBold;
            lblBalanceValue.ForeColor = Color.White;
            lblBalanceValue.Font = new Font("Segoe UI Semibold", 22f, FontStyle.Regular);
            lblBalanceValue.Height = 40;
            lblCycleDates.ForeColor = Color.FromArgb(224, 242, 254);

            // Other KPI cards: colored accent strip + round icon chip
            cardDaysLeft.AccentColor = ThemePalette.AccentIndigo;
            cardDaysLeft.Icon = IconKind.Calendar;
            cardDaysLeft.IconColor = ThemePalette.AccentIndigo;

            cardDailyLimit.AccentColor = ThemePalette.SafeGreen;
            cardDailyLimit.Icon = IconKind.Check;
            cardDailyLimit.IconColor = ThemePalette.SafeGreen;

            cardMonthSummary.AccentColor = Color.FromArgb(245, 158, 11);
            cardMonthSummary.Icon = IconKind.Chart;
            cardMonthSummary.IconColor = Color.FromArgb(217, 119, 6);

            lblDaysCaption.Font = ThemePalette.FontCaptionBold;
            lblDailyCaption.Font = ThemePalette.FontCaptionBold;
            lblMonthCaption.Font = ThemePalette.FontCaptionBold;

            // Middle Row: Category Spending Chart (Left) & Needs vs Wants Split (Right)
            this.cardCategoryChart.Location = new Point(0, 200);
            this.cardCategoryChart.Size = new Size(560, 245);

            this.lblCatChartTitle.Text = "Category Spending (Current Month)";
            this.lblCatChartTitle.Font = ThemePalette.FontSectionTitle;
            this.lblCatChartTitle.ForeColor = ThemePalette.TextPrimary;
            this.lblCatChartTitle.Location = new Point(16, 14);
            this.lblCatChartTitle.Size = new Size(420, 24);

            this.pnlCategoryBars.Location = new Point(16, 44);
            this.pnlCategoryBars.Size = new Size(526, 185);
            this.pnlCategoryBars.BackColor = ThemePalette.CardBackground;
            this.cardCategoryChart.Controls.Add(this.lblCatChartTitle);
            this.cardCategoryChart.Controls.Add(this.pnlCategoryBars);

            // Needs vs Wants Card
            this.cardNeedsWants.Location = new Point(574, 200);
            this.cardNeedsWants.Size = new Size(386, 245);

            this.lblNeedsWantsTitle.Text = "Needs vs. Wants Split";
            this.lblNeedsWantsTitle.Font = ThemePalette.FontSectionTitle;
            this.lblNeedsWantsTitle.ForeColor = ThemePalette.TextPrimary;
            this.lblNeedsWantsTitle.Location = new Point(16, 14);
            this.lblNeedsWantsTitle.Size = new Size(350, 24);
            this.lblNeedsWantsTitle.AutoEllipsis = true;
            this.lblNeedsWantsTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            this.pnlNeedsWantsChart.Location = new Point(16, 52);
            this.pnlNeedsWantsChart.Size = new Size(352, 95);
            this.pnlNeedsWantsChart.BackColor = ThemePalette.CardBackground;

            this.lblNeedsWantsLegend.Location = new Point(16, 158);
            this.lblNeedsWantsLegend.Size = new Size(352, 68);
            this.lblNeedsWantsLegend.Font = ThemePalette.FontBody;
            this.lblNeedsWantsLegend.ForeColor = ThemePalette.TextSecondary;

            this.cardNeedsWants.Controls.Add(this.lblNeedsWantsTitle);
            this.cardNeedsWants.Controls.Add(this.pnlNeedsWantsChart);
            this.cardNeedsWants.Controls.Add(this.lblNeedsWantsLegend);

            // Bottom Row: Recent Transactions
            this.cardRecentTx.Location = new Point(0, 458);
            this.cardRecentTx.Size = new Size(960, 225);
            this.cardRecentTx.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            this.lblRecentTitle.Text = "Recent Transactions";
            this.lblRecentTitle.Font = ThemePalette.FontSectionTitle;
            this.lblRecentTitle.ForeColor = ThemePalette.TextPrimary;
            this.lblRecentTitle.Location = new Point(16, 12);
            this.lblRecentTitle.Size = new Size(300, 24);

            this.dgvRecent.Location = new Point(16, 42);
            this.dgvRecent.Size = new Size(926, 168);
            this.dgvRecent.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ThemePalette.StyleDataGridView(this.dgvRecent);

            this.cardRecentTx.Controls.Add(this.lblRecentTitle);
            this.cardRecentTx.Controls.Add(this.dgvRecent);

            this.BackColor = ThemePalette.AppBackground;
            this.ClientSize = new Size(960, 690);
            this.Controls.Add(this.cardRecentTx);
            this.Controls.Add(this.cardNeedsWants);
            this.Controls.Add(this.cardCategoryChart);
            this.Controls.Add(this.cardMonthSummary);
            this.Controls.Add(this.cardDailyLimit);
            this.Controls.Add(this.cardDaysLeft);
            this.Controls.Add(this.cardBalance);
            this.Controls.Add(this.btnQuickNewCycle);
            this.Controls.Add(this.btnQuickAddTx);
            this.Controls.Add(this.btnBigWhatIf);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblTitle);

            this.ResumeLayout(false);
        }

        private void SetupKpiCard(
            RoundedPanel card, int x, int y, int w, int h,
            Label caption, string captionText,
            Label valueLbl, string defaultVal,
            Label footLbl, string defaultFoot)
        {
            card.Location = new Point(x, y);
            card.Size = new Size(w, h);

            caption.Text = captionText;
            caption.Font = ThemePalette.FontCaption;
            caption.ForeColor = ThemePalette.TextSecondary;
            caption.Location = new Point(14, 14);
            caption.Size = new Size(w - 28, 18);

            valueLbl.Text = defaultVal;
            valueLbl.Font = ThemePalette.FontKpiValue;
            valueLbl.ForeColor = ThemePalette.TextPrimary;
            valueLbl.Location = new Point(14, 38);
            valueLbl.Size = new Size(w - 28, 36);

            footLbl.Text = defaultFoot;
            footLbl.Font = ThemePalette.FontCaption;
            footLbl.ForeColor = ThemePalette.TextSecondary;
            footLbl.Location = new Point(14, 82);
            footLbl.Size = new Size(w - 28, 24);

            card.Controls.Add(caption);
            card.Controls.Add(valueLbl);
            card.Controls.Add(footLbl);
        }
    }
}
