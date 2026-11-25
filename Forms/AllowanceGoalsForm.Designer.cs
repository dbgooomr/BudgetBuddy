using System.Drawing;
using System.Windows.Forms;
using BudgetBuddy.Controls;
using BudgetBuddy.Utils;

namespace BudgetBuddy.Forms
{
    partial class AllowanceGoalsForm
    {
        private System.ComponentModel.IContainer components = null;
        private ToolTip _tips;
        private Label lblTitle;
        private Label lblSubtitle;

        private RoundedPanel cardCycle;
        private Label lblCycleHeader;
        private Label lblActiveCycleInfo;
        private Label lblAllowanceAmt;
        private TextBox txtAllowanceAmount;
        private Label lblStartDate;
        private DateTimePicker dtpStartDate;
        private Label lblNextDate;
        private DateTimePicker dtpNextDate;
        private Button btnStartCycle;

        private RoundedPanel cardCreateGoal;
        private Label lblGoalFormHeader;
        private Label lblGoalName;
        private TextBox txtGoalName;
        private Label lblGoalTarget;
        private TextBox txtGoalTarget;
        private Label lblGoalDate;
        private DateTimePicker dtpGoalTargetDate;
        private Button btnCreateGoal;

        private RoundedPanel cardGoalsList;
        private RoundedPanel panelGoalsSection;
        private RoundedPanel panelDepositsSection;
        private Label lblDepositsHeader;
        private Label lblGoalsHeader;
        private DataGridView dgvGoals;
        private ProgressBarEx progressSelectedGoal;
        private Label lblGoalProgressDetail;
        private Label lblDepositAmt;
        private TextBox txtDepositAmount;
        private ComboBox cmbDepositMethod;
        private Button btnDeposit;
        private Button btnDeleteGoal;
        private DataGridView dgvDeposits;
        private Button btnUndoDeposit;
        private Button btnSelectGoals, btnSelectAllGoals, btnDeleteSelectedGoals;
        private Button btnSelectDeposits, btnSelectAllDeposits, btnDeleteSelectedDeposits;
        private CheckBox chkExtraAllowance;
        private TextBox txtGoalSearch;
        private ComboBox cmbGoalStatus;
        private Button btnGoalClearFilter;
        private Label lblCycleHistory;
        private ComboBox cmbCycleStatus, cmbCycleYear;
        private Button btnCycleClearFilter;
        private DataGridView dgvCycles;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this._tips = new ToolTip(this.components);
            this.lblTitle = new Label();
            this.lblSubtitle = new Label();

            this.cardCycle = new RoundedPanel();
            this.lblCycleHeader = new Label();
            this.lblActiveCycleInfo = new Label();
            this.lblAllowanceAmt = new Label();
            this.txtAllowanceAmount = new TextBox();
            this.lblStartDate = new Label();
            this.dtpStartDate = new DateTimePicker();
            this.lblNextDate = new Label();
            this.dtpNextDate = new DateTimePicker();
            this.btnStartCycle = new BudgetBuddy.Controls.StyledButton();

            this.cardCreateGoal = new RoundedPanel();
            this.lblGoalFormHeader = new Label();
            this.lblGoalName = new Label();
            this.txtGoalName = new TextBox();
            this.lblGoalTarget = new Label();
            this.txtGoalTarget = new TextBox();
            this.lblGoalDate = new Label();
            this.dtpGoalTargetDate = new DateTimePicker();
            this.btnCreateGoal = new BudgetBuddy.Controls.StyledButton();

            this.cardGoalsList = new RoundedPanel();
            this.panelGoalsSection = new RoundedPanel();
            this.panelDepositsSection = new RoundedPanel();
            this.lblDepositsHeader = new Label();
            this.lblGoalsHeader = new Label();
            this.dgvGoals = new BudgetBuddy.Controls.StyledDataGridView();
            this.progressSelectedGoal = new ProgressBarEx();
            this.lblGoalProgressDetail = new Label();
            this.lblDepositAmt = new Label();
            this.txtDepositAmount = new TextBox();
            this.cmbDepositMethod = new ComboBox();
            this.btnDeposit = new BudgetBuddy.Controls.StyledButton();
            this.btnDeleteGoal = new BudgetBuddy.Controls.StyledButton();
            this.dgvDeposits = new BudgetBuddy.Controls.StyledDataGridView();
            this.btnUndoDeposit = new BudgetBuddy.Controls.StyledButton();
            this.btnSelectGoals = new BudgetBuddy.Controls.StyledButton();
            this.btnSelectAllGoals = new BudgetBuddy.Controls.StyledButton();
            this.btnDeleteSelectedGoals = new BudgetBuddy.Controls.StyledButton();
            this.btnSelectDeposits = new BudgetBuddy.Controls.StyledButton();
            this.btnSelectAllDeposits = new BudgetBuddy.Controls.StyledButton();
            this.btnDeleteSelectedDeposits = new BudgetBuddy.Controls.StyledButton();
            this.chkExtraAllowance = new CheckBox();
            this.txtGoalSearch = new TextBox();
            this.cmbGoalStatus = new ComboBox();
            this.btnGoalClearFilter = new BudgetBuddy.Controls.StyledButton();
            this.lblCycleHistory = new Label();
            this.cmbCycleStatus = new ComboBox();
            this.cmbCycleYear = new ComboBox();
            this.btnCycleClearFilter = new BudgetBuddy.Controls.StyledButton();
            this.dgvCycles = new BudgetBuddy.Controls.StyledDataGridView();

            this.SuspendLayout();

            this.lblTitle.Text = "Allowance Cycles & Savings Goals";
            this.lblTitle.Font = ThemePalette.FontPageTitle;
            this.lblTitle.ForeColor = ThemePalette.TextPrimary;
            this.lblTitle.Location = new Point(0, 0);
            this.lblTitle.AutoSize = true;
            this.lblTitle.MinimumSize = new Size(0, 44);
            this.lblTitle.Padding = new Padding(0, 5, 0, 5);

            this.lblSubtitle.Text = "Start a new allowance period (with automatic carry-over) and fund your student savings goals";
            this.lblSubtitle.Font = ThemePalette.FontBody;
            this.lblSubtitle.ForeColor = ThemePalette.TextSecondary;
            this.lblSubtitle.Location = new Point(0, 52);
            this.lblSubtitle.Size = new Size(760, 22);

            this.cardCycle.Location = new Point(0, 82);
            this.cardCycle.Size = new Size(340, 610);

            this.lblCycleHeader.Text = "Allowance Cycle Manager";
            this.lblCycleHeader.Font = ThemePalette.FontSectionTitle;
            this.lblCycleHeader.ForeColor = ThemePalette.TextPrimary;
            this.lblCycleHeader.Location = new Point(16, 14);
            this.lblCycleHeader.Size = new Size(300, 24);

            this.lblActiveCycleInfo.Location = new Point(16, 44);
            this.lblActiveCycleInfo.Size = new Size(304, 118);
            this.lblActiveCycleInfo.BackColor = Color.FromArgb(200, 232, 226);
            this.lblActiveCycleInfo.ForeColor = ThemePalette.TextPrimary;
            this.lblActiveCycleInfo.Font = ThemePalette.FontBody;
            this.lblActiveCycleInfo.Padding = new Padding(10);

            this.lblAllowanceAmt.Text = "New Allowance Amount (\u20B1)";
            this.lblAllowanceAmt.Font = ThemePalette.FontBodyBold;
            this.lblAllowanceAmt.ForeColor = ThemePalette.TextSecondary;
            this.lblAllowanceAmt.Location = new Point(16, 170);
            this.lblAllowanceAmt.AutoSize = true;
            this.lblAllowanceAmt.MinimumSize = new Size(0, 24);
            this.lblAllowanceAmt.Size = new Size(300, 20);

            this.txtAllowanceAmount.Location = new Point(16, 200);
            this.txtAllowanceAmount.Size = new Size(304, 28);
            this.txtAllowanceAmount.Font = ThemePalette.FontBody;

            this.lblStartDate.Text = "Allowance Received Date";
            this.lblStartDate.Font = ThemePalette.FontBodyBold;
            this.lblStartDate.ForeColor = ThemePalette.TextSecondary;
            this.lblStartDate.Location = new Point(16, 230);
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.MinimumSize = new Size(0, 24);
            this.lblStartDate.Size = new Size(300, 20);

            this.dtpStartDate.Location = new Point(16, 260);
            this.dtpStartDate.Size = new Size(304, 28);
            this.dtpStartDate.Format = DateTimePickerFormat.Short;
            this.dtpStartDate.Font = ThemePalette.FontBody;

            this.lblNextDate.Text = "Expected Next Allowance Date";
            this.lblNextDate.Font = ThemePalette.FontBodyBold;
            this.lblNextDate.ForeColor = ThemePalette.TextSecondary;
            this.lblNextDate.Location = new Point(16, 290);
            this.lblNextDate.AutoSize = true;
            this.lblNextDate.MinimumSize = new Size(0, 24);
            this.lblNextDate.Size = new Size(300, 20);

            this.dtpNextDate.Location = new Point(16, 320);
            this.dtpNextDate.Size = new Size(304, 28);
            this.dtpNextDate.Format = DateTimePickerFormat.Short;
            this.dtpNextDate.Font = ThemePalette.FontBody;

            this.btnStartCycle.Text = "Start New Allowance Cycle";
            this.btnStartCycle.Location = new Point(16, 362);
            this.btnStartCycle.Size = new Size(304, 42);
            this.btnStartCycle.FlatStyle = FlatStyle.Flat;
            this.btnStartCycle.FlatAppearance.BorderSize = 0;
            this.btnStartCycle.BackColor = ThemePalette.PrimaryTeal;
            this.btnStartCycle.ForeColor = Color.White;
            this.btnStartCycle.Font = ThemePalette.FontBodyBold;
            this.btnStartCycle.Cursor = Cursors.Hand;
            ((BudgetBuddy.Controls.StyledButton)this.btnStartCycle).SecondaryStyle = false;

            this.cardCycle.Controls.Add(this.btnStartCycle);
            this.cardCycle.Controls.Add(this.dtpNextDate);
            this.cardCycle.Controls.Add(this.lblNextDate);
            this.cardCycle.Controls.Add(this.dtpStartDate);
            this.cardCycle.Controls.Add(this.lblStartDate);
            this.cardCycle.Controls.Add(this.txtAllowanceAmount);
            this.cardCycle.Controls.Add(this.lblAllowanceAmt);
            this.cardCycle.Controls.Add(this.lblActiveCycleInfo);
            this.cardCycle.Controls.Add(this.lblCycleHeader);

            this.cardCreateGoal.Location = new Point(356, 82);
            this.cardCreateGoal.Size = new Size(844, 130);
            this.cardCreateGoal.MinimumSize = new Size(606, 0);
            this.cardCreateGoal.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            this.lblGoalFormHeader.Text = "Create New Savings Goal";
            this.lblGoalFormHeader.Font = ThemePalette.FontSectionTitle;
            this.lblGoalFormHeader.ForeColor = ThemePalette.TextPrimary;
            this.lblGoalFormHeader.Location = new Point(16, 12);
            this.lblGoalFormHeader.Size = new Size(300, 22);

            this.lblGoalName.Text = "Goal Name";
            this.lblGoalName.Font = ThemePalette.FontCaption;
            this.lblGoalName.Location = new Point(16, 42);
            this.lblGoalName.AutoSize = true;
            this.lblGoalName.MinimumSize = new Size(0, 20);
            this.lblGoalName.Size = new Size(160, 20);

            this.txtGoalName.Location = new Point(16, 68);
            this.txtGoalName.Size = new Size(175, 28);
            this.txtGoalName.Font = ThemePalette.FontBody;

            this.lblGoalTarget.Text = "Target Amount (\u20B1)";
            this.lblGoalTarget.Font = ThemePalette.FontCaption;
            this.lblGoalTarget.Location = new Point(200, 42);
            this.lblGoalTarget.AutoSize = true;
            this.lblGoalTarget.MinimumSize = new Size(0, 20);
            this.lblGoalTarget.Size = new Size(125, 20);

            this.txtGoalTarget.Location = new Point(200, 68);
            this.txtGoalTarget.Size = new Size(125, 28);
            this.txtGoalTarget.Font = ThemePalette.FontBody;

            this.lblGoalDate.Text = "Target Date";
            this.lblGoalDate.Font = ThemePalette.FontCaption;
            this.lblGoalDate.Location = new Point(334, 42);
            this.lblGoalDate.AutoSize = true;
            this.lblGoalDate.MinimumSize = new Size(0, 20);
            this.lblGoalDate.Size = new Size(115, 20);

            this.dtpGoalTargetDate.Location = new Point(334, 68);
            this.dtpGoalTargetDate.Size = new Size(118, 28);
            this.dtpGoalTargetDate.Format = DateTimePickerFormat.Short;
            this.dtpGoalTargetDate.Font = ThemePalette.FontBody;

            this.btnCreateGoal.Text = "+ Add Goal";
            this.btnCreateGoal.Location = new Point(462, 64);
            this.btnCreateGoal.Size = new Size(124, 34);
            this.btnCreateGoal.FlatStyle = FlatStyle.Flat;
            this.btnCreateGoal.FlatAppearance.BorderSize = 0;
            this.btnCreateGoal.BackColor = ThemePalette.PrimaryTeal;
            this.btnCreateGoal.ForeColor = Color.White;
            this.btnCreateGoal.Font = ThemePalette.FontBodyBold;
            this.btnCreateGoal.Cursor = Cursors.Hand;
            ((BudgetBuddy.Controls.StyledButton)this.btnCreateGoal).SecondaryStyle = false;

            this.cardCreateGoal.Controls.Add(this.btnCreateGoal);
            this.cardCreateGoal.Controls.Add(this.dtpGoalTargetDate);
            this.cardCreateGoal.Controls.Add(this.lblGoalDate);
            this.cardCreateGoal.Controls.Add(this.txtGoalTarget);
            this.cardCreateGoal.Controls.Add(this.lblGoalTarget);
            this.cardCreateGoal.Controls.Add(this.txtGoalName);
            this.cardCreateGoal.Controls.Add(this.lblGoalName);
            this.cardCreateGoal.Controls.Add(this.lblGoalFormHeader);

            this.cardGoalsList.Location = new Point(356, 224);
            this.cardGoalsList.Size = new Size(844, 750);
            this.cardGoalsList.MinimumSize = new Size(606, 0);
            this.cardGoalsList.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            this.panelGoalsSection.Location = new Point(16, 16);
            this.panelGoalsSection.Size = new Size(812, 350);
            this.panelGoalsSection.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            this.lblGoalsHeader.Text = "Savings Goals";
            this.lblGoalsHeader.Font = ThemePalette.FontSectionTitle;
            this.lblGoalsHeader.ForeColor = ThemePalette.TextPrimary;
            this.lblGoalsHeader.Location = new Point(12, 14);
            this.lblGoalsHeader.Size = new Size(420, 26);
            this.lblGoalsHeader.AutoSize = false;

            this.dgvGoals.Location = new Point(16, 134);
            this.dgvGoals.Size = new Size(780, 140);
            this.dgvGoals.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.dgvGoals.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvGoals.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGoals.RowHeadersVisible = false;
            this.dgvGoals.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            this.dgvGoals.DefaultCellStyle.Padding = new Padding(4, 2, 4, 2);
            this.dgvGoals.RowTemplate.Height = 34;
            this.dgvGoals.MinimumSize = new Size(0, 100);
            this.dgvGoals.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvGoals.BackgroundColor = ThemePalette.CardBackground;
            this.dgvGoals.BorderStyle = BorderStyle.None;
            this.dgvGoals.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvGoals.GridColor = ThemePalette.CardBorder;
            this.dgvGoals.EnableHeadersVisualStyles = false;
            this.dgvGoals.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.dgvGoals.ColumnHeadersDefaultCellStyle.BackColor = ThemePalette.SurfaceHeader;
            this.dgvGoals.ColumnHeadersDefaultCellStyle.ForeColor = ThemePalette.TextSecondary;
            this.dgvGoals.ColumnHeadersDefaultCellStyle.SelectionBackColor = ThemePalette.SurfaceHeader;
            this.dgvGoals.ColumnHeadersDefaultCellStyle.SelectionForeColor = ThemePalette.TextSecondary;
            this.dgvGoals.ColumnHeadersDefaultCellStyle.Font = ThemePalette.FontCaptionBold;
            this.dgvGoals.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 6, 8, 6);
            this.dgvGoals.ColumnHeadersHeight = 38;
            this.dgvGoals.DefaultCellStyle.BackColor = ThemePalette.CardBackground;
            this.dgvGoals.DefaultCellStyle.ForeColor = ThemePalette.TextPrimary;
            this.dgvGoals.DefaultCellStyle.Font = ThemePalette.FontBody;
            this.dgvGoals.DefaultCellStyle.Padding = new Padding(8, 4, 8, 4);
            this.dgvGoals.DefaultCellStyle.SelectionBackColor = Color.FromArgb(204, 251, 241);
            this.dgvGoals.DefaultCellStyle.SelectionForeColor = ThemePalette.TextPrimary;
            this.dgvGoals.RowTemplate.Height = 38;
            this.dgvGoals.AlternatingRowsDefaultCellStyle.BackColor = ThemePalette.SurfaceAlt;
            this.dgvGoals.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(204, 251, 241);
            this.dgvGoals.AlternatingRowsDefaultCellStyle.SelectionForeColor = ThemePalette.TextPrimary;
            this.dgvGoals.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvGoals.MultiSelect = false;
            this.dgvGoals.ReadOnly = true;
            this.dgvGoals.AllowUserToAddRows = false;
            this.dgvGoals.AllowUserToDeleteRows = false;
            this.dgvGoals.AllowUserToResizeRows = false;
            this.dgvGoals.RowHeadersVisible = false;
            this.dgvGoals.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            this.progressSelectedGoal.Location = new Point(16, 292);
            this.progressSelectedGoal.Size = new Size(780, 24);
            this.progressSelectedGoal.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            this.lblGoalProgressDetail.Location = new Point(16, 324);
            this.lblGoalProgressDetail.Size = new Size(780, 20);
            this.lblGoalProgressDetail.AutoSize = false;
            this.lblGoalProgressDetail.Font = ThemePalette.FontCaption;
            this.lblGoalProgressDetail.AutoEllipsis = false;
            this.lblGoalProgressDetail.ForeColor = ThemePalette.TextSecondary;

            this.panelDepositsSection.Location = new Point(16, 384);
            this.panelDepositsSection.Size = new Size(812, 350);
            this.panelDepositsSection.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            this.lblDepositsHeader.Text = "Deposits";
            this.lblDepositsHeader.Font = ThemePalette.FontSectionTitle;
            this.lblDepositsHeader.ForeColor = ThemePalette.TextPrimary;
            this.lblDepositsHeader.Location = new Point(12, 14);
            this.lblDepositsHeader.Size = new Size(792, 26);

            this.lblDepositAmt.Text = "Deposit (\u20B1):";
            this.lblDepositAmt.Font = ThemePalette.FontBodyBold;
            this.lblDepositAmt.Location = new Point(16, 48);
            this.lblDepositAmt.Size = new Size(104, 24);

            this.txtDepositAmount.Location = new Point(16, 78);
            this.txtDepositAmount.Size = new Size(120, 28);
            this.txtDepositAmount.Font = ThemePalette.FontBody;

            this.cmbDepositMethod.Location = new Point(148, 78);
            this.cmbDepositMethod.Size = new Size(130, 28);
            this.cmbDepositMethod.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbDepositMethod.Items.AddRange(new object[] { "Cash", "E-wallet" });

            this.btnDeposit.Text = "Deposit";
            this.btnDeposit.Location = new Point(290, 74);
            this.btnDeposit.Size = new Size(130, 32);
            this.btnDeposit.FlatStyle = FlatStyle.Flat;
            this.btnDeposit.FlatAppearance.BorderSize = 0;
            this.btnDeposit.BackColor = ThemePalette.SafeGreen;
            this.btnDeposit.ForeColor = Color.White;
            this.btnDeposit.Font = ThemePalette.FontBodyBold;
            this.btnDeposit.Cursor = Cursors.Hand;
            ((BudgetBuddy.Controls.StyledButton)this.btnDeposit).SecondaryStyle = false;

            this.btnDeleteGoal.Text = "Delete Goal";
            this.btnDeleteGoal.Location = new Point(466, 248);
            this.btnDeleteGoal.Size = new Size(120, 32);
            this.btnDeleteGoal.Visible = false;
            this.btnDeleteGoal.FlatStyle = FlatStyle.Flat;
            this.btnDeleteGoal.FlatAppearance.BorderSize = 0;
            this.btnDeleteGoal.BackColor = ThemePalette.DangerRose;
            this.btnDeleteGoal.ForeColor = Color.White;
            this.btnDeleteGoal.Font = ThemePalette.FontBodyBold;
            this.btnDeleteGoal.Cursor = Cursors.Hand;
            ((BudgetBuddy.Controls.StyledButton)this.btnDeleteGoal).SecondaryStyle = false;

            this.dgvDeposits.Location = new Point(16, 184);
            this.dgvDeposits.Size = new Size(780, 150);
            this.dgvDeposits.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.dgvDeposits.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDeposits.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDeposits.RowHeadersVisible = false;
            this.dgvDeposits.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dgvDeposits.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvDeposits.BackgroundColor = ThemePalette.CardBackground;
            this.dgvDeposits.BorderStyle = BorderStyle.None;
            this.dgvDeposits.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvDeposits.GridColor = ThemePalette.CardBorder;
            this.dgvDeposits.EnableHeadersVisualStyles = false;
            this.dgvDeposits.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.dgvDeposits.ColumnHeadersDefaultCellStyle.BackColor = ThemePalette.SurfaceHeader;
            this.dgvDeposits.ColumnHeadersDefaultCellStyle.ForeColor = ThemePalette.TextSecondary;
            this.dgvDeposits.ColumnHeadersDefaultCellStyle.SelectionBackColor = ThemePalette.SurfaceHeader;
            this.dgvDeposits.ColumnHeadersDefaultCellStyle.SelectionForeColor = ThemePalette.TextSecondary;
            this.dgvDeposits.ColumnHeadersDefaultCellStyle.Font = ThemePalette.FontCaptionBold;
            this.dgvDeposits.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 6, 8, 6);
            this.dgvDeposits.ColumnHeadersHeight = 38;
            this.dgvDeposits.DefaultCellStyle.BackColor = ThemePalette.CardBackground;
            this.dgvDeposits.DefaultCellStyle.ForeColor = ThemePalette.TextPrimary;
            this.dgvDeposits.DefaultCellStyle.Font = ThemePalette.FontBody;
            this.dgvDeposits.DefaultCellStyle.Padding = new Padding(8, 4, 8, 4);
            this.dgvDeposits.DefaultCellStyle.SelectionBackColor = Color.FromArgb(204, 251, 241);
            this.dgvDeposits.DefaultCellStyle.SelectionForeColor = ThemePalette.TextPrimary;
            this.dgvDeposits.RowTemplate.Height = 38;
            this.dgvDeposits.AlternatingRowsDefaultCellStyle.BackColor = ThemePalette.SurfaceAlt;
            this.dgvDeposits.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(204, 251, 241);
            this.dgvDeposits.AlternatingRowsDefaultCellStyle.SelectionForeColor = ThemePalette.TextPrimary;
            this.dgvDeposits.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvDeposits.MultiSelect = true;
            this.dgvDeposits.ReadOnly = true;
            this.dgvDeposits.AllowUserToAddRows = false;
            this.dgvDeposits.AllowUserToDeleteRows = false;
            this.dgvDeposits.AllowUserToResizeRows = false;
            this.dgvDeposits.RowHeadersVisible = false;
            this.dgvDeposits.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            this.btnUndoDeposit.Text = "Delete Selected Deposit";
            this.btnUndoDeposit.Location = new Point(16, 290);
            this.btnUndoDeposit.Size = new Size(220, 32);
            this.btnUndoDeposit.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.btnUndoDeposit.Enabled = false;
            this.btnUndoDeposit.Visible = false;
            this.btnUndoDeposit.FlatStyle = FlatStyle.Flat;
            this.btnUndoDeposit.FlatAppearance.BorderSize = 0;
            this.btnUndoDeposit.BackColor = ThemePalette.DangerRose;
            this.btnUndoDeposit.ForeColor = Color.White;
            this.btnUndoDeposit.Font = ThemePalette.FontBodyBold;
            this.btnUndoDeposit.Cursor = Cursors.Hand;
            ((BudgetBuddy.Controls.StyledButton)this.btnUndoDeposit).SecondaryStyle = false;

            this.txtGoalSearch.Location = new Point(16, 50);
            this.txtGoalSearch.Size = new Size(220, 28);
            this.txtGoalSearch.Font = ThemePalette.FontBody;
            this.txtGoalSearch.PlaceholderText = "Search goal name...";
            this.cmbGoalStatus.Location = new Point(244, 50);
            this.cmbGoalStatus.Size = new Size(150, 28);
            this.cmbGoalStatus.Font = ThemePalette.FontBody;
            this.cmbGoalStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbGoalStatus.Items.AddRange(new object[] { "All Goals", "In Progress", "Completed" });
            this.btnGoalClearFilter.Text = "Clear";
            this.btnGoalClearFilter.Location = new Point(402, 48);
            this.btnGoalClearFilter.Size = new Size(80, 32);
            this.btnGoalClearFilter.FlatStyle = FlatStyle.Flat;
            this.btnGoalClearFilter.FlatAppearance.BorderColor = ThemePalette.CardBorder;
            this.btnGoalClearFilter.FlatAppearance.BorderSize = 1;
            this.btnGoalClearFilter.BackColor = ThemePalette.SurfaceAlt;
            this.btnGoalClearFilter.ForeColor = ThemePalette.TextPrimary;
            this.btnGoalClearFilter.Font = ThemePalette.FontBodyBold;
            this.btnGoalClearFilter.Cursor = Cursors.Hand;
            ((BudgetBuddy.Controls.StyledButton)this.btnGoalClearFilter).SecondaryStyle = true;
            this.btnSelectGoals.Text = "Select"; this.btnSelectGoals.Size = new Size(98, 32);
            this.btnSelectAllGoals.Text = "Select All"; this.btnSelectAllGoals.Size = new Size(104, 32);
            this.btnDeleteSelectedGoals.Text = "Delete"; this.btnDeleteSelectedGoals.Size = new Size(120, 32);
            this.btnSelectGoals.FlatStyle = FlatStyle.Flat;
            this.btnSelectGoals.FlatAppearance.BorderColor = ThemePalette.CardBorder;
            this.btnSelectGoals.FlatAppearance.BorderSize = 1;
            this.btnSelectGoals.BackColor = ThemePalette.SurfaceAlt;
            this.btnSelectGoals.ForeColor = ThemePalette.TextPrimary;
            this.btnSelectGoals.Font = ThemePalette.FontBodyBold;
            this.btnSelectGoals.Cursor = Cursors.Hand;
            ((BudgetBuddy.Controls.StyledButton)this.btnSelectGoals).SecondaryStyle = true;
            this.btnSelectAllGoals.FlatStyle = FlatStyle.Flat;
            this.btnSelectAllGoals.FlatAppearance.BorderColor = ThemePalette.CardBorder;
            this.btnSelectAllGoals.FlatAppearance.BorderSize = 1;
            this.btnSelectAllGoals.BackColor = ThemePalette.SurfaceAlt;
            this.btnSelectAllGoals.ForeColor = ThemePalette.TextPrimary;
            this.btnSelectAllGoals.Font = ThemePalette.FontBodyBold;
            this.btnSelectAllGoals.Cursor = Cursors.Hand;
            ((BudgetBuddy.Controls.StyledButton)this.btnSelectAllGoals).SecondaryStyle = true;
            this.btnDeleteSelectedGoals.FlatStyle = FlatStyle.Flat;
            this.btnDeleteSelectedGoals.FlatAppearance.BorderSize = 0;
            this.btnDeleteSelectedGoals.BackColor = ThemePalette.DangerRose;
            this.btnDeleteSelectedGoals.ForeColor = Color.White;
            this.btnDeleteSelectedGoals.Font = ThemePalette.FontBodyBold;
            this.btnDeleteSelectedGoals.Cursor = Cursors.Hand;
            ((BudgetBuddy.Controls.StyledButton)this.btnDeleteSelectedGoals).SecondaryStyle = false;
            this.panelGoalsSection.Controls.Add(this.txtGoalSearch);
            this.panelGoalsSection.Controls.Add(this.cmbGoalStatus);
            this.panelGoalsSection.Controls.Add(this.btnGoalClearFilter);
            this.panelGoalsSection.Controls.Add(this.btnSelectGoals);
            this.panelGoalsSection.Controls.Add(this.btnSelectAllGoals);
            this.panelGoalsSection.Controls.Add(this.btnDeleteSelectedGoals);

            this.btnSelectDeposits.Text = "Select"; this.btnSelectDeposits.Size = new Size(98, 32);
            this.btnSelectAllDeposits.Text = "Select All"; this.btnSelectAllDeposits.Size = new Size(104, 32);
            this.btnDeleteSelectedDeposits.Text = "Delete"; this.btnDeleteSelectedDeposits.Size = new Size(120, 32);
            this.btnSelectDeposits.FlatStyle = FlatStyle.Flat;
            this.btnSelectDeposits.FlatAppearance.BorderColor = ThemePalette.CardBorder;
            this.btnSelectDeposits.FlatAppearance.BorderSize = 1;
            this.btnSelectDeposits.BackColor = ThemePalette.SurfaceAlt;
            this.btnSelectDeposits.ForeColor = ThemePalette.TextPrimary;
            this.btnSelectDeposits.Font = ThemePalette.FontBodyBold;
            this.btnSelectDeposits.Cursor = Cursors.Hand;
            ((BudgetBuddy.Controls.StyledButton)this.btnSelectDeposits).SecondaryStyle = true;
            this.btnSelectAllDeposits.FlatStyle = FlatStyle.Flat;
            this.btnSelectAllDeposits.FlatAppearance.BorderColor = ThemePalette.CardBorder;
            this.btnSelectAllDeposits.FlatAppearance.BorderSize = 1;
            this.btnSelectAllDeposits.BackColor = ThemePalette.SurfaceAlt;
            this.btnSelectAllDeposits.ForeColor = ThemePalette.TextPrimary;
            this.btnSelectAllDeposits.Font = ThemePalette.FontBodyBold;
            this.btnSelectAllDeposits.Cursor = Cursors.Hand;
            ((BudgetBuddy.Controls.StyledButton)this.btnSelectAllDeposits).SecondaryStyle = true;
            this.btnDeleteSelectedDeposits.FlatStyle = FlatStyle.Flat;
            this.btnDeleteSelectedDeposits.FlatAppearance.BorderSize = 0;
            this.btnDeleteSelectedDeposits.BackColor = ThemePalette.DangerRose;
            this.btnDeleteSelectedDeposits.ForeColor = Color.White;
            this.btnDeleteSelectedDeposits.Font = ThemePalette.FontBodyBold;
            this.btnDeleteSelectedDeposits.Cursor = Cursors.Hand;
            ((BudgetBuddy.Controls.StyledButton)this.btnDeleteSelectedDeposits).SecondaryStyle = false;
            this.chkExtraAllowance.Location = new Point(16, 112);
            this.chkExtraAllowance.Text = "Extra allowance (keep Safe to Spend)";
            this.chkExtraAllowance.AutoSize = true;
            this.chkExtraAllowance.Font = ThemePalette.FontBody;
            this.chkExtraAllowance.ForeColor = ThemePalette.TextPrimary;
            this.chkExtraAllowance.BackColor = Color.Transparent;
            this.chkExtraAllowance.Cursor = Cursors.Hand;
            this._tips.SetToolTip(this.chkExtraAllowance, "Check this for extra allowance you are moving to the goal.\nMoney Left This Cycle goes down by the deposit, but Safe to Spend Today stays the same.");
            this.panelDepositsSection.Controls.Add(this.chkExtraAllowance);
            this.panelDepositsSection.Controls.Add(this.btnSelectDeposits);
            this.panelDepositsSection.Controls.Add(this.btnSelectAllDeposits);
            this.panelDepositsSection.Controls.Add(this.btnDeleteSelectedDeposits);

            this.btnSelectGoals.Location = new Point(16, 88);
            this.btnSelectAllGoals.Location = new Point(122, 88);
            this.btnDeleteSelectedGoals.Location = new Point(234, 88);
            this.btnSelectDeposits.Location = new Point(16, 140);
            this.btnSelectAllDeposits.Location = new Point(122, 140);
            this.btnDeleteSelectedDeposits.Location = new Point(234, 140);
            this.panelGoalsSection.Controls.Add(this.lblGoalsHeader);
            this.panelGoalsSection.Controls.Add(this.dgvGoals);
            this.panelGoalsSection.Controls.Add(this.progressSelectedGoal);
            this.panelGoalsSection.Controls.Add(this.lblGoalProgressDetail);

            this.panelDepositsSection.Controls.Add(this.lblDepositsHeader);
            this.panelDepositsSection.Controls.Add(this.lblDepositAmt);
            this.panelDepositsSection.Controls.Add(this.txtDepositAmount);
            this.panelDepositsSection.Controls.Add(this.cmbDepositMethod);
            this.panelDepositsSection.Controls.Add(this.btnDeposit);
            this.panelDepositsSection.Controls.Add(this.dgvDeposits);

            this.cardGoalsList.Controls.Add(this.panelGoalsSection);
            this.cardGoalsList.Controls.Add(this.panelDepositsSection);

            this.lblCycleHistory.Location = new Point(16, 426);
            this.lblCycleHistory.Size = new Size(304, 24);
            this.cmbCycleStatus.Location = new Point(16, 458);
            this.cmbCycleStatus.Size = new Size(124, 28);
            this.cmbCycleYear.Location = new Point(146, 458);
            this.cmbCycleYear.Size = new Size(102, 28);
            this.btnCycleClearFilter.Location = new Point(254, 456);
            this.btnCycleClearFilter.Size = new Size(66, 32);
            this.dgvCycles.Location = new Point(16, 496);
            this.dgvCycles.Size = new Size(304, 98);
            this.lblCycleHistory.Text = "Cycle History";
            this.lblCycleHistory.Font = ThemePalette.FontBodyBold;
            this.lblCycleHistory.ForeColor = ThemePalette.TextPrimary;
            this.lblCycleHistory.BackColor = Color.Transparent;
            this.lblCycleHistory.AutoSize = false;
            this.cmbCycleStatus.Font = ThemePalette.FontBody;
            this.cmbCycleStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbCycleStatus.Items.AddRange(new object[] { "All Cycles", "Active", "Past" });
            this.cmbCycleYear.Font = ThemePalette.FontBody;
            this.cmbCycleYear.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbCycleYear.Items.Add("All Years");
            this.btnCycleClearFilter.Text = "Clear";
            this.btnCycleClearFilter.FlatStyle = FlatStyle.Flat;
            this.btnCycleClearFilter.FlatAppearance.BorderColor = ThemePalette.CardBorder;
            this.btnCycleClearFilter.FlatAppearance.BorderSize = 1;
            this.btnCycleClearFilter.BackColor = ThemePalette.SurfaceAlt;
            this.btnCycleClearFilter.ForeColor = ThemePalette.TextPrimary;
            this.btnCycleClearFilter.Font = ThemePalette.FontBodyBold;
            this.btnCycleClearFilter.Cursor = Cursors.Hand;
            ((BudgetBuddy.Controls.StyledButton)this.btnCycleClearFilter).SecondaryStyle = true;
            this.dgvCycles.ReadOnly = true;
            this.dgvCycles.AllowUserToAddRows = false;
            this.dgvCycles.AllowUserToDeleteRows = false;
            this.dgvCycles.AllowUserToResizeRows = false;
            this.dgvCycles.RowHeadersVisible = false;
            this.dgvCycles.MultiSelect = false;
            this.dgvCycles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvCycles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCycles.BackgroundColor = ThemePalette.CardBackground;
            this.dgvCycles.BorderStyle = BorderStyle.None;
            this.dgvCycles.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvCycles.GridColor = ThemePalette.CardBorder;
            this.dgvCycles.EnableHeadersVisualStyles = false;
            this.dgvCycles.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.dgvCycles.ColumnHeadersDefaultCellStyle.BackColor = ThemePalette.SurfaceHeader;
            this.dgvCycles.ColumnHeadersDefaultCellStyle.ForeColor = ThemePalette.TextSecondary;
            this.dgvCycles.ColumnHeadersDefaultCellStyle.SelectionBackColor = ThemePalette.SurfaceHeader;
            this.dgvCycles.ColumnHeadersDefaultCellStyle.SelectionForeColor = ThemePalette.TextSecondary;
            this.dgvCycles.ColumnHeadersDefaultCellStyle.Font = ThemePalette.FontCaptionBold;
            this.dgvCycles.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 6, 8, 6);
            this.dgvCycles.ColumnHeadersHeight = 38;
            this.dgvCycles.DefaultCellStyle.BackColor = ThemePalette.CardBackground;
            this.dgvCycles.DefaultCellStyle.ForeColor = ThemePalette.TextPrimary;
            this.dgvCycles.DefaultCellStyle.Font = ThemePalette.FontBody;
            this.dgvCycles.DefaultCellStyle.Padding = new Padding(8, 4, 8, 4);
            this.dgvCycles.DefaultCellStyle.SelectionBackColor = Color.FromArgb(204, 251, 241);
            this.dgvCycles.DefaultCellStyle.SelectionForeColor = ThemePalette.TextPrimary;
            this.dgvCycles.RowTemplate.Height = 38;
            this.dgvCycles.AlternatingRowsDefaultCellStyle.BackColor = ThemePalette.SurfaceAlt;
            this.dgvCycles.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(204, 251, 241);
            this.dgvCycles.AlternatingRowsDefaultCellStyle.SelectionForeColor = ThemePalette.TextPrimary;
            this.dgvCycles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvCycles.MultiSelect = false;
            this.dgvCycles.ReadOnly = true;
            this.dgvCycles.AllowUserToAddRows = false;
            this.dgvCycles.AllowUserToDeleteRows = false;
            this.dgvCycles.AllowUserToResizeRows = false;
            this.dgvCycles.RowHeadersVisible = false;
            this.dgvCycles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.cardCycle.Controls.Add(this.lblCycleHistory);
            this.cardCycle.Controls.Add(this.cmbCycleStatus);
            this.cardCycle.Controls.Add(this.cmbCycleYear);
            this.cardCycle.Controls.Add(this.btnCycleClearFilter);
            this.cardCycle.Controls.Add(this.dgvCycles);

            this.txtAllowanceAmount.BackColor = ThemePalette.InputBackground;
            this.txtAllowanceAmount.ForeColor = ThemePalette.TextPrimary;
            this.txtGoalName.BackColor = ThemePalette.InputBackground;
            this.txtGoalName.ForeColor = ThemePalette.TextPrimary;
            this.txtGoalTarget.BackColor = ThemePalette.InputBackground;
            this.txtGoalTarget.ForeColor = ThemePalette.TextPrimary;
            this.txtDepositAmount.BackColor = ThemePalette.InputBackground;
            this.txtDepositAmount.ForeColor = ThemePalette.TextPrimary;
            this.cmbDepositMethod.BackColor = ThemePalette.InputBackground;
            this.cmbDepositMethod.ForeColor = ThemePalette.TextPrimary;
            this.txtGoalSearch.BackColor = ThemePalette.InputBackground;
            this.txtGoalSearch.ForeColor = ThemePalette.TextPrimary;
            this.cmbGoalStatus.BackColor = ThemePalette.InputBackground;
            this.cmbGoalStatus.ForeColor = ThemePalette.TextPrimary;
            this.cmbCycleStatus.BackColor = ThemePalette.InputBackground;
            this.cmbCycleStatus.ForeColor = ThemePalette.TextPrimary;

            this.btnCreateGoal.UseMnemonic = false;
            this.btnCycleClearFilter.UseMnemonic = false;
            this.btnDeleteGoal.UseMnemonic = false;
            this.btnDeleteSelectedDeposits.UseMnemonic = false;
            this.btnDeleteSelectedGoals.UseMnemonic = false;
            this.btnDeposit.UseMnemonic = false;
            this.btnGoalClearFilter.UseMnemonic = false;
            this.btnSelectAllDeposits.UseMnemonic = false;
            this.btnSelectAllGoals.UseMnemonic = false;
            this.btnSelectDeposits.UseMnemonic = false;
            this.btnSelectGoals.UseMnemonic = false;
            this.btnStartCycle.UseMnemonic = false;
            this.btnUndoDeposit.UseMnemonic = false;
            this.chkExtraAllowance.UseMnemonic = false;
            this.lblActiveCycleInfo.UseMnemonic = false;
            this.lblAllowanceAmt.UseMnemonic = false;
            this.lblCycleHeader.UseMnemonic = false;
            this.lblCycleHistory.UseMnemonic = false;
            this.lblDepositAmt.UseMnemonic = false;
            this.lblDepositsHeader.UseMnemonic = false;
            this.lblGoalDate.UseMnemonic = false;
            this.lblGoalFormHeader.UseMnemonic = false;
            this.lblGoalName.UseMnemonic = false;
            this.lblGoalProgressDetail.UseMnemonic = false;
            this.lblGoalTarget.UseMnemonic = false;
            this.lblGoalsHeader.UseMnemonic = false;
            this.lblNextDate.UseMnemonic = false;
            this.lblStartDate.UseMnemonic = false;
            this.lblSubtitle.UseMnemonic = false;
            this.lblTitle.UseMnemonic = false;

            this.BackColor = ThemePalette.AppBackground;
            this.ClientSize = new Size(1200, 706);
            this.AutoScroll = true;
            this.AutoScrollMinSize = new Size(962, 990);
            this.Controls.Add(this.cardGoalsList);
            this.Controls.Add(this.cardCreateGoal);
            this.Controls.Add(this.cardCycle);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblTitle);

            this.ResumeLayout(false);
        }
    }
}
