using System.Drawing;
using System.Windows.Forms;
using BudgetBuddy.Controls;
using BudgetBuddy.Utils;

namespace BudgetBuddy.Forms
{
    partial class AllowanceGoalsForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private Label lblSubtitle;

        // Left: Allowance Cycle Card
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

        // Right Top: Create Savings Goal & Deposit
        private RoundedPanel cardCreateGoal;
        private Label lblGoalFormHeader;
        private Label lblGoalName;
        private TextBox txtGoalName;
        private Label lblGoalTarget;
        private TextBox txtGoalTarget;
        private Label lblGoalDate;
        private DateTimePicker dtpGoalTargetDate;
        private Button btnCreateGoal;

        // Right Bottom: Goals List, Progress Bar, and Deposits
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

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
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
            this.btnStartCycle = new Button();

            this.cardCreateGoal = new RoundedPanel();
            this.lblGoalFormHeader = new Label();
            this.lblGoalName = new Label();
            this.txtGoalName = new TextBox();
            this.lblGoalTarget = new Label();
            this.txtGoalTarget = new TextBox();
            this.lblGoalDate = new Label();
            this.dtpGoalTargetDate = new DateTimePicker();
            this.btnCreateGoal = new Button();

            this.cardGoalsList = new RoundedPanel();
            this.panelGoalsSection = new RoundedPanel();
            this.panelDepositsSection = new RoundedPanel();
            this.lblDepositsHeader = new Label();
            this.lblGoalsHeader = new Label();
            this.dgvGoals = new DataGridView();
            this.progressSelectedGoal = new ProgressBarEx();
            this.lblGoalProgressDetail = new Label();
            this.lblDepositAmt = new Label();
            this.txtDepositAmount = new TextBox();
            this.cmbDepositMethod = new ComboBox();
            this.btnDeposit = new Button();
            this.btnDeleteGoal = new Button();
            this.dgvDeposits = new DataGridView();
            this.btnUndoDeposit = new Button();

            this.SuspendLayout();

            this.lblTitle.Text = "Allowance Cycles & Savings Goals";
            this.lblTitle.Font = ThemePalette.FontPageTitle;
            this.lblTitle.ForeColor = ThemePalette.TextPrimary;
            this.lblTitle.Location = new Point(0, 0);
            this.lblTitle.Size = new Size(560, 32);

            this.lblSubtitle.Text = "Start a new allowance period (with automatic carry-over) and fund your student savings goals";
            this.lblSubtitle.Font = ThemePalette.FontBody;
            this.lblSubtitle.ForeColor = ThemePalette.TextSecondary;
            this.lblSubtitle.Location = new Point(0, 32);
            this.lblSubtitle.Size = new Size(760, 22);

            // Left Card: Allowance Cycle
            this.cardCycle.Location = new Point(0, 66);
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
            this.lblAllowanceAmt.Location = new Point(16, 176);
            this.lblAllowanceAmt.Size = new Size(300, 18);

            this.txtAllowanceAmount.Location = new Point(16, 196);
            this.txtAllowanceAmount.Size = new Size(304, 28);
            this.txtAllowanceAmount.Font = ThemePalette.FontBody;

            this.lblStartDate.Text = "Allowance Received Date";
            this.lblStartDate.Font = ThemePalette.FontBodyBold;
            this.lblStartDate.ForeColor = ThemePalette.TextSecondary;
            this.lblStartDate.Location = new Point(16, 236);
            this.lblStartDate.Size = new Size(300, 18);

            this.dtpStartDate.Location = new Point(16, 256);
            this.dtpStartDate.Size = new Size(304, 28);
            this.dtpStartDate.Format = DateTimePickerFormat.Short;
            this.dtpStartDate.Font = ThemePalette.FontBody;

            this.lblNextDate.Text = "Expected Next Allowance Date";
            this.lblNextDate.Font = ThemePalette.FontBodyBold;
            this.lblNextDate.ForeColor = ThemePalette.TextSecondary;
            this.lblNextDate.Location = new Point(16, 296);
            this.lblNextDate.Size = new Size(300, 18);

            this.dtpNextDate.Location = new Point(16, 316);
            this.dtpNextDate.Size = new Size(304, 28);
            this.dtpNextDate.Format = DateTimePickerFormat.Short;
            this.dtpNextDate.Font = ThemePalette.FontBody;

            this.btnStartCycle.Text = "Start New Allowance Cycle";
            this.btnStartCycle.Location = new Point(16, 362);
            this.btnStartCycle.Size = new Size(304, 42);
            ThemePalette.StyleButton(this.btnStartCycle, ThemePalette.PrimaryTeal, Color.White);

            this.cardCycle.Controls.Add(this.btnStartCycle);
            this.cardCycle.Controls.Add(this.dtpNextDate);
            this.cardCycle.Controls.Add(this.lblNextDate);
            this.cardCycle.Controls.Add(this.dtpStartDate);
            this.cardCycle.Controls.Add(this.lblStartDate);
            this.cardCycle.Controls.Add(this.txtAllowanceAmount);
            this.cardCycle.Controls.Add(this.lblAllowanceAmt);
            this.cardCycle.Controls.Add(this.lblActiveCycleInfo);
            this.cardCycle.Controls.Add(this.lblCycleHeader);

            // Right Top: Create Savings Goal
            this.cardCreateGoal.Location = new Point(356, 66);
            this.cardCreateGoal.Size = new Size(844, 130);
            this.cardCreateGoal.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            this.lblGoalFormHeader.Text = "Create New Savings Goal";
            this.lblGoalFormHeader.Font = ThemePalette.FontSectionTitle;
            this.lblGoalFormHeader.ForeColor = ThemePalette.TextPrimary;
            this.lblGoalFormHeader.Location = new Point(16, 12);
            this.lblGoalFormHeader.Size = new Size(300, 22);

            this.lblGoalName.Text = "Goal Name";
            this.lblGoalName.Font = ThemePalette.FontCaption;
            this.lblGoalName.Location = new Point(16, 40);
            this.lblGoalName.Size = new Size(160, 16);

            this.txtGoalName.Location = new Point(16, 58);
            this.txtGoalName.Size = new Size(175, 28);
            this.txtGoalName.Font = ThemePalette.FontBody;

            this.lblGoalTarget.Text = "Target Amount (\u20B1)";
            this.lblGoalTarget.Font = ThemePalette.FontCaption;
            this.lblGoalTarget.Location = new Point(200, 40);
            this.lblGoalTarget.Size = new Size(125, 16);

            this.txtGoalTarget.Location = new Point(200, 58);
            this.txtGoalTarget.Size = new Size(125, 28);
            this.txtGoalTarget.Font = ThemePalette.FontBody;

            this.lblGoalDate.Text = "Target Date";
            this.lblGoalDate.Font = ThemePalette.FontCaption;
            this.lblGoalDate.Location = new Point(334, 40);
            this.lblGoalDate.Size = new Size(115, 16);

            this.dtpGoalTargetDate.Location = new Point(334, 58);
            this.dtpGoalTargetDate.Size = new Size(118, 28);
            this.dtpGoalTargetDate.Format = DateTimePickerFormat.Short;
            this.dtpGoalTargetDate.Font = ThemePalette.FontBody;

            this.btnCreateGoal.Text = "+ Add Goal";
            this.btnCreateGoal.Location = new Point(462, 54);
            this.btnCreateGoal.Size = new Size(124, 34);
            ThemePalette.StyleButton(this.btnCreateGoal, ThemePalette.PrimaryTeal, Color.White);

            this.cardCreateGoal.Controls.Add(this.btnCreateGoal);
            this.cardCreateGoal.Controls.Add(this.dtpGoalTargetDate);
            this.cardCreateGoal.Controls.Add(this.lblGoalDate);
            this.cardCreateGoal.Controls.Add(this.txtGoalTarget);
            this.cardCreateGoal.Controls.Add(this.lblGoalTarget);
            this.cardCreateGoal.Controls.Add(this.txtGoalName);
            this.cardCreateGoal.Controls.Add(this.lblGoalName);
            this.cardCreateGoal.Controls.Add(this.lblGoalFormHeader);

            // Right Bottom: split Savings Goals and Deposits into separate sections
            this.cardGoalsList.Location = new Point(356, 208);
            this.cardGoalsList.Size = new Size(844, 468);
            this.cardGoalsList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            this.panelGoalsSection.Location = new Point(12, 12);
            this.panelGoalsSection.Size = new Size(820, 216);
            this.panelGoalsSection.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            this.lblGoalsHeader.Text = "Savings Goals";
            this.lblGoalsHeader.Font = ThemePalette.FontSectionTitle;
            this.lblGoalsHeader.ForeColor = ThemePalette.TextPrimary;
            this.lblGoalsHeader.Location = new Point(14, 12);
            this.lblGoalsHeader.Size = new Size(420, 26);
            this.lblGoalsHeader.AutoSize = false;

            this.dgvGoals.Location = new Point(14, 76);
            this.dgvGoals.Size = new Size(792, 104);
            this.dgvGoals.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.dgvGoals.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvGoals.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGoals.RowHeadersVisible = false;
            this.dgvGoals.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            this.dgvGoals.DefaultCellStyle.Padding = new Padding(4, 2, 4, 2);
            this.dgvGoals.RowTemplate.Height = 34;
            this.dgvGoals.MinimumSize = new Size(0, 100);
            this.dgvGoals.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            ThemePalette.StyleDataGridView(this.dgvGoals);

            this.progressSelectedGoal.Location = new Point(14, 184);
            this.progressSelectedGoal.Size = new Size(792, 20);
            this.progressSelectedGoal.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            this.lblGoalProgressDetail.Location = new Point(14, 206);
            this.lblGoalProgressDetail.Size = new Size(792, 18);
            this.lblGoalProgressDetail.AutoSize = false;
            this.lblGoalProgressDetail.Font = ThemePalette.FontCaption;
            this.lblGoalProgressDetail.AutoEllipsis = false;
            this.lblGoalProgressDetail.ForeColor = ThemePalette.TextSecondary;

            this.panelDepositsSection.Location = new Point(12, 240);
            this.panelDepositsSection.Size = new Size(820, 216);
            this.panelDepositsSection.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            this.lblDepositsHeader.Text = "Deposits";
            this.lblDepositsHeader.Font = ThemePalette.FontSectionTitle;
            this.lblDepositsHeader.ForeColor = ThemePalette.TextPrimary;
            this.lblDepositsHeader.Location = new Point(14, 12);
            this.lblDepositsHeader.Size = new Size(792, 26);

            this.lblDepositAmt.Text = "Deposit (\u20B1):";
            this.lblDepositAmt.Font = ThemePalette.FontBodyBold;
            this.lblDepositAmt.Location = new Point(14, 42);
            this.lblDepositAmt.Size = new Size(78, 24);

            this.txtDepositAmount.Location = new Point(96, 40);
            this.txtDepositAmount.Size = new Size(100, 28);
            this.txtDepositAmount.Font = ThemePalette.FontBody;

            this.cmbDepositMethod.Location = new Point(204, 40);
            this.cmbDepositMethod.Size = new Size(110, 28);
            this.cmbDepositMethod.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbDepositMethod.Items.AddRange(new object[] { "Cash", "E-wallet" });
            this.cmbDepositMethod.SelectedIndex = 0;

            this.btnDeposit.Text = "Deposit";
            this.btnDeposit.Location = new Point(322, 38);
            this.btnDeposit.Size = new Size(130, 32);
            ThemePalette.StyleButton(this.btnDeposit, ThemePalette.SafeGreen, Color.White);

            this.btnDeleteGoal.Text = "Delete Goal";
            this.btnDeleteGoal.Location = new Point(466, 248);
            this.btnDeleteGoal.Size = new Size(120, 32);
            ThemePalette.StyleButton(this.btnDeleteGoal, ThemePalette.DangerRose, Color.White);

            this.dgvDeposits.Location = new Point(14, 112);
            this.dgvDeposits.Size = new Size(792, 92);
            this.dgvDeposits.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.dgvDeposits.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDeposits.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDeposits.RowHeadersVisible = false;
            this.dgvDeposits.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dgvDeposits.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            ThemePalette.StyleDataGridView(this.dgvDeposits);

            this.btnUndoDeposit.Text = "Delete Selected Deposit";
            this.btnUndoDeposit.Location = new Point(16, 290);
            this.btnUndoDeposit.Size = new Size(220, 32);
            this.btnUndoDeposit.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.btnUndoDeposit.Enabled = false;
            ThemePalette.StyleButton(this.btnUndoDeposit, ThemePalette.DangerRose, Color.White);

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

            this.BackColor = ThemePalette.AppBackground;
            this.ClientSize = new Size(1200, 690);
            this.Controls.Add(this.cardGoalsList);
            this.Controls.Add(this.cardCreateGoal);
            this.Controls.Add(this.cardCycle);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblTitle);

            this.ResumeLayout(false);
        }
    }
}
