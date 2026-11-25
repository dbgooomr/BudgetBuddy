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
        private TableLayoutPanel tlpHeader;
        private TableLayoutPanel tlpHeaderText;
        private FlowLayoutPanel flpHeaderActions;
        private Label lblTitle;
        private Label lblSubtitle;
        private Button btnBigWhatIf;
        private Button btnQuickAddTx;
        private Button btnQuickNewCycle;

        private TableLayoutPanel tlpKpiCards;
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

        private TableLayoutPanel tlpAnalysisCards;
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
            this.tlpHeader = new TableLayoutPanel();
            this.tlpHeaderText = new TableLayoutPanel();
            this.flpHeaderActions = new FlowLayoutPanel();
            this.lblTitle = new Label();
            this.lblSubtitle = new Label();
            this.btnBigWhatIf = new BudgetBuddy.Controls.StyledButton();
            this.btnQuickAddTx = new BudgetBuddy.Controls.StyledButton();
            this.btnQuickNewCycle = new BudgetBuddy.Controls.StyledButton();

            this.tlpKpiCards = new TableLayoutPanel();
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

            this.tlpAnalysisCards = new TableLayoutPanel();
            this.cardNeedsWants = new RoundedPanel();
            this.lblNeedsWantsTitle = new Label();
            this.pnlNeedsWantsChart = new Panel();
            this.lblNeedsWantsLegend = new Label();

            this.cardCategoryChart = new RoundedPanel();
            this.lblCatChartTitle = new Label();
            this.pnlCategoryBars = new Panel();

            this.cardRecentTx = new RoundedPanel();
            this.lblRecentTitle = new Label();
            this.dgvRecent = new BudgetBuddy.Controls.StyledDataGridView();

            this.tlpHeader.SuspendLayout();
            this.tlpHeaderText.SuspendLayout();
            this.flpHeaderActions.SuspendLayout();
            this.SuspendLayout();

            this.tlpHeader.Location = new Point(0, 0);
            this.tlpHeader.Size = new Size(960, 85);
            this.tlpHeader.MinimumSize = new Size(960, 85);
            this.tlpHeader.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.tlpHeader.Margin = new Padding(0);
            this.tlpHeader.ColumnCount = 2;
            this.tlpHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.tlpHeader.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            this.tlpHeader.RowCount = 1;
            this.tlpHeader.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.tlpHeader.Controls.Add(this.tlpHeaderText, 0, 0);
            this.tlpHeader.Controls.Add(this.flpHeaderActions, 1, 0);

            this.tlpHeaderText.Dock = DockStyle.Fill;
            this.tlpHeaderText.Margin = new Padding(0, 0, 24, 0);
            this.tlpHeaderText.ColumnCount = 1;
            this.tlpHeaderText.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.tlpHeaderText.RowCount = 2;
            this.tlpHeaderText.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.tlpHeaderText.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.tlpHeaderText.Controls.Add(this.lblTitle, 0, 0);
            this.tlpHeaderText.Controls.Add(this.lblSubtitle, 0, 1);

            this.flpHeaderActions.AutoSize = true;
            this.flpHeaderActions.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.flpHeaderActions.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.flpHeaderActions.Margin = new Padding(0, 6, 0, 0);
            this.flpHeaderActions.WrapContents = false;
            this.flpHeaderActions.Controls.Add(this.btnQuickNewCycle);
            this.flpHeaderActions.Controls.Add(this.btnQuickAddTx);
            this.flpHeaderActions.Controls.Add(this.btnBigWhatIf);

            this.lblTitle.Text = "Student Financial Dashboard";
            this.lblTitle.Font = ThemePalette.FontPageTitle;
            this.lblTitle.ForeColor = ThemePalette.TextPrimary;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Margin = new Padding(0, 0, 0, 8);

            this.lblSubtitle.Text = "Live overview of your allowance";
            this.lblSubtitle.Font = ThemePalette.FontBody;
            this.lblSubtitle.ForeColor = ThemePalette.TextSecondary;
            this.lblSubtitle.Dock = DockStyle.Fill;
            this.lblSubtitle.Margin = new Padding(0);

            this.btnBigWhatIf.Text = "\u26A1 What-If Simulator";
            this.btnBigWhatIf.Size = new Size(190, 44);
            this.btnBigWhatIf.Margin = new Padding(0, 0, 0, 0);
            this.btnBigWhatIf.FlatStyle = FlatStyle.Flat;
            this.btnBigWhatIf.FlatAppearance.BorderSize = 0;
            this.btnBigWhatIf.BackColor = ThemePalette.AccentIndigo;
            this.btnBigWhatIf.ForeColor = Color.White;
            this.btnBigWhatIf.Font = ThemePalette.FontBodyBold;
            this.btnBigWhatIf.Cursor = Cursors.Hand;
            ((BudgetBuddy.Controls.StyledButton)this.btnBigWhatIf).SecondaryStyle = false;

            this.btnQuickAddTx.Text = "+ Record Transaction";
            this.btnQuickAddTx.Size = new Size(175, 44);
            this.btnQuickAddTx.Margin = new Padding(0, 0, 10, 0);
            this.btnQuickAddTx.FlatStyle = FlatStyle.Flat;
            this.btnQuickAddTx.FlatAppearance.BorderSize = 0;
            this.btnQuickAddTx.BackColor = ThemePalette.PrimaryTeal;
            this.btnQuickAddTx.ForeColor = Color.White;
            this.btnQuickAddTx.Font = ThemePalette.FontBodyBold;
            this.btnQuickAddTx.Cursor = Cursors.Hand;
            ((BudgetBuddy.Controls.StyledButton)this.btnQuickAddTx).SecondaryStyle = false;

            this.btnQuickNewCycle.Text = "New Allowance";
            this.btnQuickNewCycle.Size = new Size(135, 44);
            this.btnQuickNewCycle.Margin = new Padding(0, 0, 10, 0);
            this.btnQuickNewCycle.FlatStyle = FlatStyle.Flat;
            this.btnQuickNewCycle.FlatAppearance.BorderColor = ThemePalette.CardBorder;
            this.btnQuickNewCycle.FlatAppearance.BorderSize = 1;
            this.btnQuickNewCycle.BackColor = ThemePalette.SurfaceAlt;
            this.btnQuickNewCycle.ForeColor = ThemePalette.TextPrimary;
            this.btnQuickNewCycle.Font = ThemePalette.FontBodyBold;
            this.btnQuickNewCycle.Cursor = Cursors.Hand;
            ((BudgetBuddy.Controls.StyledButton)this.btnQuickNewCycle).SecondaryStyle = true;

            this.tlpKpiCards.Location = new Point(0, 97);
            this.tlpKpiCards.Size = new Size(960, 122);
            this.tlpKpiCards.MinimumSize = new Size(960, 0);
            this.tlpKpiCards.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.tlpKpiCards.ColumnCount = 4;
            this.tlpKpiCards.RowCount = 1;
            this.tlpKpiCards.Margin = new Padding(0);
            this.tlpKpiCards.Padding = new Padding(0);
            this.tlpKpiCards.BackColor = Color.Transparent;
            this.tlpKpiCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            this.tlpKpiCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            this.tlpKpiCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            this.tlpKpiCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            this.tlpKpiCards.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            this.cardBalance.Dock = DockStyle.Fill;
            this.cardBalance.Margin = new Padding(0, 0, 9, 0);
            this.lblBalanceCaption.Text = "MONEY LEFT THIS CYCLE";
            this.lblBalanceCaption.Font = ThemePalette.FontCaption;
            this.lblBalanceCaption.ForeColor = ThemePalette.TextSecondary;
            this.lblBalanceCaption.Location = new Point(14, 14);
            this.lblBalanceCaption.Size = new Size(166, 20);
            this.lblBalanceCaption.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.lblBalanceCaption.AutoEllipsis = true;
            this.lblBalanceValue.Text = "\u20B10.00";
            this.lblBalanceValue.Font = ThemePalette.FontKpiValue;
            this.lblBalanceValue.ForeColor = ThemePalette.TextPrimary;
            this.lblBalanceValue.Location = new Point(14, 42);
            this.lblBalanceValue.Size = new Size(204, 36);
            this.lblBalanceValue.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.lblCycleDates.Text = "Cycle: -";
            this.lblCycleDates.Font = ThemePalette.FontCaption;
            this.lblCycleDates.ForeColor = ThemePalette.TextSecondary;
            this.lblCycleDates.Location = new Point(14, 90);
            this.lblCycleDates.Size = new Size(204, 24);
            this.lblCycleDates.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.lblCycleDates.AutoEllipsis = true;
            this.cardBalance.Controls.Add(this.lblBalanceCaption);
            this.cardBalance.Controls.Add(this.lblBalanceValue);
            this.cardBalance.Controls.Add(this.lblCycleDates);
            this.cardDaysLeft.Dock = DockStyle.Fill;
            this.cardDaysLeft.Margin = new Padding(3, 0, 6, 0);
            this.lblDaysCaption.Text = "DAYS TO NEXT ALLOWANCE";
            this.lblDaysCaption.Font = ThemePalette.FontCaption;
            this.lblDaysCaption.ForeColor = ThemePalette.TextSecondary;
            this.lblDaysCaption.Location = new Point(14, 14);
            this.lblDaysCaption.Size = new Size(154, 20);
            this.lblDaysCaption.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.lblDaysCaption.AutoEllipsis = true;
            this.lblDaysValue.Text = "0 days";
            this.lblDaysValue.Font = ThemePalette.FontKpiValue;
            this.lblDaysValue.ForeColor = ThemePalette.TextPrimary;
            this.lblDaysValue.Location = new Point(14, 42);
            this.lblDaysValue.Size = new Size(192, 36);
            this.lblDaysValue.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.lblOverdueStatus.Text = "On track";
            this.lblOverdueStatus.Font = ThemePalette.FontCaption;
            this.lblOverdueStatus.ForeColor = ThemePalette.TextSecondary;
            this.lblOverdueStatus.Location = new Point(14, 86);
            this.lblOverdueStatus.Size = new Size(192, 24);
            this.lblOverdueStatus.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.lblOverdueStatus.AutoEllipsis = true;
            this.cardDaysLeft.Controls.Add(this.lblDaysCaption);
            this.cardDaysLeft.Controls.Add(this.lblDaysValue);
            this.cardDaysLeft.Controls.Add(this.lblOverdueStatus);
            this.cardDailyLimit.Dock = DockStyle.Fill;
            this.cardDailyLimit.Margin = new Padding(6, 0, 3, 0);
            this.lblDailyCaption.Text = "SAFE TO SPEND TODAY";
            this.lblDailyCaption.Font = ThemePalette.FontCaption;
            this.lblDailyCaption.ForeColor = ThemePalette.TextSecondary;
            this.lblDailyCaption.Location = new Point(14, 14);
            this.lblDailyCaption.Size = new Size(179, 20);
            this.lblDailyCaption.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.lblDailyCaption.AutoEllipsis = true;
            this.lblDailyValue.Text = "\u20B10.00";
            this.lblDailyValue.Font = ThemePalette.FontKpiValue;
            this.lblDailyValue.ForeColor = ThemePalette.TextPrimary;
            this.lblDailyValue.Location = new Point(14, 42);
            this.lblDailyValue.Size = new Size(217, 36);
            this.lblDailyValue.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.lblDailyFlag.Text = "Within daily limit";
            this.lblDailyFlag.Font = ThemePalette.FontCaption;
            this.lblDailyFlag.ForeColor = ThemePalette.TextSecondary;
            this.lblDailyFlag.Location = new Point(14, 86);
            this.lblDailyFlag.Size = new Size(217, 24);
            this.lblDailyFlag.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.lblDailyFlag.AutoEllipsis = true;
            this.cardDailyLimit.Controls.Add(this.lblDailyCaption);
            this.cardDailyLimit.Controls.Add(this.lblDailyValue);
            this.cardDailyLimit.Controls.Add(this.lblDailyFlag);
            this.cardMonthSummary.Dock = DockStyle.Fill;
            this.cardMonthSummary.Margin = new Padding(9, 0, 0, 0);
            this.lblMonthCaption.Text = "THIS MONTH SUMMARY";
            this.lblMonthCaption.Font = ThemePalette.FontCaption;
            this.lblMonthCaption.ForeColor = ThemePalette.TextSecondary;
            this.lblMonthCaption.Location = new Point(14, 14);
            this.lblMonthCaption.Size = new Size(161, 20);
            this.lblMonthCaption.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.lblMonthCaption.AutoEllipsis = true;
            this.lblMonthIncome.Text = "Income: \u20B10.00";
            this.lblMonthIncome.Font = ThemePalette.FontKpiValue;
            this.lblMonthIncome.ForeColor = ThemePalette.TextPrimary;
            this.lblMonthIncome.Location = new Point(14, 42);
            this.lblMonthIncome.Size = new Size(199, 36);
            this.lblMonthIncome.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.lblMonthIncome.AutoEllipsis = true;
            this.lblMonthExpense.Text = "Expenses: \u20B10.00";
            this.lblMonthExpense.Font = ThemePalette.FontCaption;
            this.lblMonthExpense.ForeColor = ThemePalette.TextSecondary;
            this.lblMonthExpense.Location = new Point(14, 86);
            this.lblMonthExpense.Size = new Size(199, 24);
            this.lblMonthExpense.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.lblMonthExpense.AutoEllipsis = true;
            this.cardMonthSummary.Controls.Add(this.lblMonthCaption);
            this.cardMonthSummary.Controls.Add(this.lblMonthIncome);
            this.cardMonthSummary.Controls.Add(this.lblMonthExpense);
            lblMonthIncome.Font = ThemePalette.FontBodyBold;
            lblMonthIncome.ForeColor = ThemePalette.SafeGreen;
            lblMonthExpense.Font = ThemePalette.FontBodyBold;
            lblMonthExpense.ForeColor = ThemePalette.DangerRose;

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

            this.tlpAnalysisCards.Location = new Point(0, 231);
            this.tlpAnalysisCards.Size = new Size(960, 264);
            this.tlpAnalysisCards.MinimumSize = new Size(960, 0);
            this.tlpAnalysisCards.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.tlpAnalysisCards.ColumnCount = 2;
            this.tlpAnalysisCards.RowCount = 1;
            this.tlpAnalysisCards.Margin = new Padding(0);
            this.tlpAnalysisCards.Padding = new Padding(0);
            this.tlpAnalysisCards.BackColor = Color.Transparent;
            this.tlpAnalysisCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this.tlpAnalysisCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this.tlpAnalysisCards.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            this.cardCategoryChart.Size = new Size(473, 264);
            this.cardCategoryChart.Padding = new Padding(24, 16, 24, 24);
            this.cardCategoryChart.Dock = DockStyle.Fill;
            this.cardCategoryChart.Margin = new Padding(0, 0, 7, 0);

            this.lblCatChartTitle.Text = "Category Spending (Current Month)";
            this.lblCatChartTitle.Font = ThemePalette.FontSectionTitle;
            this.lblCatChartTitle.ForeColor = ThemePalette.TextPrimary;
            this.lblCatChartTitle.Location = new Point(20, 16);
            this.lblCatChartTitle.Size = new Size(425, 34);
            this.lblCatChartTitle.Dock = DockStyle.Top;
            this.lblCatChartTitle.Padding = new Padding(0, 0, 0, 8);
            this.lblCatChartTitle.AutoEllipsis = true;

            this.pnlCategoryBars.Location = new Point(20, 50);
            this.pnlCategoryBars.Size = new Size(433, 173);
            this.pnlCategoryBars.Dock = DockStyle.Fill;
            this.pnlCategoryBars.BackColor = ThemePalette.CardBackground;
            this.cardCategoryChart.Controls.Add(this.pnlCategoryBars);
            this.cardCategoryChart.Controls.Add(this.lblCatChartTitle);

            this.cardNeedsWants.Size = new Size(473, 264);
            this.cardNeedsWants.Padding = new Padding(24, 16, 24, 24);
            this.cardNeedsWants.Dock = DockStyle.Fill;
            this.cardNeedsWants.Margin = new Padding(7, 0, 0, 0);

            this.lblNeedsWantsTitle.Text = "Needs vs. Wants Split";
            this.lblNeedsWantsTitle.Font = ThemePalette.FontSectionTitle;
            this.lblNeedsWantsTitle.ForeColor = ThemePalette.TextPrimary;
            this.lblNeedsWantsTitle.Location = new Point(20, 16);
            this.lblNeedsWantsTitle.Size = new Size(425, 34);
            this.lblNeedsWantsTitle.AutoEllipsis = true;
            this.lblNeedsWantsTitle.Dock = DockStyle.Top;
            this.lblNeedsWantsTitle.Padding = new Padding(0, 0, 0, 8);

            this.pnlNeedsWantsChart.Location = new Point(20, 54);
            this.pnlNeedsWantsChart.Size = new Size(433, 95);
            this.pnlNeedsWantsChart.Dock = DockStyle.Top;
            this.pnlNeedsWantsChart.BackColor = ThemePalette.CardBackground;

            this.lblNeedsWantsLegend.Location = new Point(20, 160);
            this.lblNeedsWantsLegend.Size = new Size(433, 62);
            this.lblNeedsWantsLegend.Dock = DockStyle.Fill;
            this.lblNeedsWantsLegend.Padding = new Padding(0, 8, 0, 0);
            this.lblNeedsWantsLegend.Font = ThemePalette.FontBody;
            this.lblNeedsWantsLegend.ForeColor = ThemePalette.TextSecondary;

            this.cardNeedsWants.Controls.Add(this.lblNeedsWantsLegend);
            this.cardNeedsWants.Controls.Add(this.pnlNeedsWantsChart);
            this.cardNeedsWants.Controls.Add(this.lblNeedsWantsTitle);

            this.tlpAnalysisCards.Controls.Add(this.cardCategoryChart, 0, 0);
            this.tlpAnalysisCards.Controls.Add(this.cardNeedsWants, 1, 0);

            this.cardRecentTx.Location = new Point(0, 508);
            this.cardRecentTx.Size = new Size(960, 225);
            this.cardRecentTx.MinimumSize = new Size(600, 225);
            this.cardRecentTx.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            this.lblRecentTitle.Text = "Recent Transactions";
            this.lblRecentTitle.Font = ThemePalette.FontSectionTitle;
            this.lblRecentTitle.ForeColor = ThemePalette.TextPrimary;
            this.lblRecentTitle.Location = new Point(16, 12);
            this.lblRecentTitle.Size = new Size(300, 24);

            this.dgvRecent.Location = new Point(16, 44);
            this.dgvRecent.Size = new Size(926, 166);
            this.dgvRecent.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.dgvRecent.BackgroundColor = ThemePalette.CardBackground;
            this.dgvRecent.BorderStyle = BorderStyle.None;
            this.dgvRecent.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvRecent.GridColor = ThemePalette.CardBorder;
            this.dgvRecent.EnableHeadersVisualStyles = false;
            this.dgvRecent.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.dgvRecent.ColumnHeadersDefaultCellStyle.BackColor = ThemePalette.SurfaceHeader;
            this.dgvRecent.ColumnHeadersDefaultCellStyle.ForeColor = ThemePalette.TextSecondary;
            this.dgvRecent.ColumnHeadersDefaultCellStyle.SelectionBackColor = ThemePalette.SurfaceHeader;
            this.dgvRecent.ColumnHeadersDefaultCellStyle.SelectionForeColor = ThemePalette.TextSecondary;
            this.dgvRecent.ColumnHeadersDefaultCellStyle.Font = ThemePalette.FontCaptionBold;
            this.dgvRecent.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 6, 8, 6);
            this.dgvRecent.ColumnHeadersHeight = 38;
            this.dgvRecent.DefaultCellStyle.BackColor = ThemePalette.CardBackground;
            this.dgvRecent.DefaultCellStyle.ForeColor = ThemePalette.TextPrimary;
            this.dgvRecent.DefaultCellStyle.Font = ThemePalette.FontBody;
            this.dgvRecent.DefaultCellStyle.Padding = new Padding(8, 4, 8, 4);
            this.dgvRecent.DefaultCellStyle.SelectionBackColor = Color.FromArgb(204, 251, 241);
            this.dgvRecent.DefaultCellStyle.SelectionForeColor = ThemePalette.TextPrimary;
            this.dgvRecent.RowTemplate.Height = 38;
            this.dgvRecent.AlternatingRowsDefaultCellStyle.BackColor = ThemePalette.SurfaceAlt;
            this.dgvRecent.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(204, 251, 241);
            this.dgvRecent.AlternatingRowsDefaultCellStyle.SelectionForeColor = ThemePalette.TextPrimary;
            this.dgvRecent.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecent.MultiSelect = false;
            this.dgvRecent.ReadOnly = true;
            this.dgvRecent.AllowUserToAddRows = false;
            this.dgvRecent.AllowUserToDeleteRows = false;
            this.dgvRecent.AllowUserToResizeRows = false;
            this.dgvRecent.RowHeadersVisible = false;
            this.dgvRecent.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            this.cardRecentTx.Controls.Add(this.lblRecentTitle);
            this.cardRecentTx.Controls.Add(this.dgvRecent);

            this.btnBigWhatIf.UseMnemonic = false;
            this.btnQuickAddTx.UseMnemonic = false;
            this.btnQuickNewCycle.UseMnemonic = false;
            this.lblBalanceCaption.UseMnemonic = false;
            this.lblBalanceValue.UseMnemonic = false;
            this.lblCatChartTitle.UseMnemonic = false;
            this.lblCycleDates.UseMnemonic = false;
            this.lblDailyCaption.UseMnemonic = false;
            this.lblDailyFlag.UseMnemonic = false;
            this.lblDailyValue.UseMnemonic = false;
            this.lblDaysCaption.UseMnemonic = false;
            this.lblDaysValue.UseMnemonic = false;
            this.lblMonthCaption.UseMnemonic = false;
            this.lblMonthExpense.UseMnemonic = false;
            this.lblMonthIncome.UseMnemonic = false;
            this.lblNeedsWantsLegend.UseMnemonic = false;
            this.lblNeedsWantsTitle.UseMnemonic = false;
            this.lblOverdueStatus.UseMnemonic = false;
            this.lblRecentTitle.UseMnemonic = false;
            this.lblSubtitle.UseMnemonic = false;
            this.lblTitle.UseMnemonic = false;

            this.tlpKpiCards.Controls.Add(this.cardBalance, 0, 0);
            this.tlpKpiCards.Controls.Add(this.cardDaysLeft, 1, 0);
            this.tlpKpiCards.Controls.Add(this.cardDailyLimit, 2, 0);
            this.tlpKpiCards.Controls.Add(this.cardMonthSummary, 3, 0);

            this.BackColor = ThemePalette.AppBackground;
            this.ClientSize = new Size(960, 740);
            this.AutoScroll = true;
            this.AutoScrollMinSize = new Size(960, 740);
            this.Controls.Add(this.cardRecentTx);
            this.Controls.Add(this.tlpAnalysisCards);
            this.Controls.Add(this.tlpKpiCards);
            this.Controls.Add(this.tlpHeader);

            this.flpHeaderActions.ResumeLayout(false);
            this.tlpHeaderText.ResumeLayout(false);
            this.tlpHeaderText.PerformLayout();
            this.tlpHeader.ResumeLayout(false);
            this.tlpHeader.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
