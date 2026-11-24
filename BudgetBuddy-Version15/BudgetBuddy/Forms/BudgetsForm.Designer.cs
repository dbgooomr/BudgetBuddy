using System.Drawing;
using System.Windows.Forms;
using BudgetBuddy.Controls;
using BudgetBuddy.Utils;

namespace BudgetBuddy.Forms
{
    partial class BudgetsForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private Label lblSubtitle;

        private RoundedPanel cardSetBudget;
        private Label lblCardHeader;
        private Label lblPeriod;
        private ComboBox cmbPeriodType;
        private Label lblWeekStart;
        private DateTimePicker dtpWeekStart;
        private Label lblMonth;
        private ComboBox cmbMonth;
        private Label lblYear;
        private NumericUpDown numYear;
        private Label lblCategory;
        private ComboBox cmbCategory;
        private Label lblLimit;
        private TextBox txtLimitAmount;
        private Button btnSaveBudget;
        private Button btnDeleteBudget;

        private RoundedPanel cardTable;
        private Label lblTableHeader;
        private DataGridView dgvBudgets;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.lblSubtitle = new Label();
            this.cardSetBudget = new RoundedPanel();
            this.lblCardHeader = new Label();
            this.lblPeriod = new Label();
            this.cmbPeriodType = new ComboBox();
            this.lblWeekStart = new Label();
            this.dtpWeekStart = new DateTimePicker();
            this.lblMonth = new Label();
            this.cmbMonth = new ComboBox();
            this.lblYear = new Label();
            this.numYear = new NumericUpDown();
            this.lblCategory = new Label();
            this.cmbCategory = new ComboBox();
            this.lblLimit = new Label();
            this.txtLimitAmount = new TextBox();
            this.btnSaveBudget = new Button();
            this.btnDeleteBudget = new Button();

            this.cardTable = new RoundedPanel();
            this.lblTableHeader = new Label();
            this.dgvBudgets = new DataGridView();

            ((System.ComponentModel.ISupportInitialize)(this.numYear)).BeginInit();
            this.SuspendLayout();

            this.lblTitle.Text = "Category Budgets";
            this.lblTitle.Font = ThemePalette.FontPageTitle;
            this.lblTitle.ForeColor = ThemePalette.TextPrimary;
            this.lblTitle.Location = new Point(0, 0);
            this.lblTitle.Size = new Size(500, 32);

            this.lblSubtitle.Text = "Choose a weekly or monthly spending limit for each expense category";
            this.lblSubtitle.Font = ThemePalette.FontBody;
            this.lblSubtitle.ForeColor = ThemePalette.TextSecondary;
            this.lblSubtitle.Location = new Point(0, 32);
            this.lblSubtitle.Size = new Size(760, 22);

            // Set Budget Card
            this.cardSetBudget.Location = new Point(0, 66);
            this.cardSetBudget.Size = new Size(340, 470);

            this.lblCardHeader.Text = "Configure Spending Limit";
            this.lblCardHeader.Font = ThemePalette.FontSectionTitle;
            this.lblCardHeader.ForeColor = ThemePalette.TextPrimary;
            this.lblCardHeader.Location = new Point(16, 14);
            this.lblCardHeader.Size = new Size(300, 24);

            this.lblPeriod.Text = "Budget Period";
            this.lblPeriod.Font = ThemePalette.FontBodyBold;
            this.lblPeriod.ForeColor = ThemePalette.TextSecondary;
            this.lblPeriod.Location = new Point(16, 48);
            this.lblPeriod.Size = new Size(140, 18);
            this.cmbPeriodType.Location = new Point(16, 68);
            this.cmbPeriodType.Size = new Size(304, 28);
            this.cmbPeriodType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbPeriodType.Font = ThemePalette.FontBody;
            this.cmbPeriodType.Items.AddRange(new object[] { "Weekly", "Monthly" });
            this.cmbPeriodType.SelectedIndex = 1;

            this.lblWeekStart.Text = "Week Starting (Monday)";
            this.lblWeekStart.Font = ThemePalette.FontBodyBold;
            this.lblWeekStart.ForeColor = ThemePalette.TextSecondary;
            this.lblWeekStart.Location = new Point(16, 108);
            this.lblWeekStart.Size = new Size(300, 18);
            this.dtpWeekStart.Location = new Point(16, 128);
            this.dtpWeekStart.Size = new Size(304, 28);
            this.dtpWeekStart.Font = ThemePalette.FontBody;
            this.dtpWeekStart.Format = DateTimePickerFormat.Short;
            this.lblWeekStart.Visible = false;
            this.dtpWeekStart.Visible = false;

            this.lblMonth.Text = "Month";
            this.lblMonth.Font = ThemePalette.FontBodyBold;
            this.lblMonth.ForeColor = ThemePalette.TextSecondary;
            this.lblMonth.Location = new Point(16, 166);
            this.lblMonth.Size = new Size(140, 18);

            this.cmbMonth.Location = new Point(16, 186);
            this.cmbMonth.Size = new Size(185, 28);
            this.cmbMonth.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbMonth.Font = ThemePalette.FontBody;

            this.lblYear.Text = "Year";
            this.lblYear.Font = ThemePalette.FontBodyBold;
            this.lblYear.ForeColor = ThemePalette.TextSecondary;
            this.lblYear.Location = new Point(212, 166);
            this.lblYear.Size = new Size(100, 18);

            this.numYear.Location = new Point(212, 186);
            this.numYear.Size = new Size(108, 28);
            this.numYear.Minimum = 2020;
            this.numYear.Maximum = 2100;
            this.numYear.Font = ThemePalette.FontBody;

            this.lblCategory.Text = "Expense Category";
            this.lblCategory.Font = ThemePalette.FontBodyBold;
            this.lblCategory.ForeColor = ThemePalette.TextSecondary;
            this.lblCategory.Location = new Point(16, 226);
            this.lblCategory.Size = new Size(300, 18);

            this.cmbCategory.Location = new Point(16, 246);
            this.cmbCategory.Size = new Size(304, 28);
            this.cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbCategory.Font = ThemePalette.FontBody;

            this.lblLimit.Text = "Budget Limit Amount (\u20B1)";
            this.lblLimit.Font = ThemePalette.FontBodyBold;
            this.lblLimit.ForeColor = ThemePalette.TextSecondary;
            this.lblLimit.Location = new Point(16, 290);
            this.lblLimit.Size = new Size(300, 18);

            this.txtLimitAmount.Location = new Point(16, 310);
            this.txtLimitAmount.Size = new Size(304, 28);
            this.txtLimitAmount.Font = ThemePalette.FontBody;

            this.btnSaveBudget.Text = "Save Budget";
            this.btnSaveBudget.Location = new Point(16, 360);
            this.btnSaveBudget.Size = new Size(304, 40);
            ThemePalette.StyleButton(this.btnSaveBudget, ThemePalette.PrimaryTeal, Color.White);

            this.btnDeleteBudget.Text = "Remove Selected Budget";
            this.btnDeleteBudget.Location = new Point(16, 410);
            this.btnDeleteBudget.Size = new Size(304, 36);
            ThemePalette.StyleButton(this.btnDeleteBudget, ThemePalette.DangerRose, Color.White);

            this.cardSetBudget.Controls.Add(this.lblWeekStart);
            this.cardSetBudget.Controls.Add(this.dtpWeekStart);
            this.cardSetBudget.Controls.Add(this.cmbPeriodType);
            this.cardSetBudget.Controls.Add(this.lblPeriod);
            this.cardSetBudget.Controls.Add(this.btnDeleteBudget);
            this.cardSetBudget.Controls.Add(this.btnSaveBudget);
            this.cardSetBudget.Controls.Add(this.txtLimitAmount);
            this.cardSetBudget.Controls.Add(this.lblLimit);
            this.cardSetBudget.Controls.Add(this.cmbCategory);
            this.cardSetBudget.Controls.Add(this.lblCategory);
            this.cardSetBudget.Controls.Add(this.numYear);
            this.cardSetBudget.Controls.Add(this.lblYear);
            this.cardSetBudget.Controls.Add(this.cmbMonth);
            this.cardSetBudget.Controls.Add(this.lblMonth);
            this.cardSetBudget.Controls.Add(this.lblCardHeader);

            // Table Card
            this.cardTable.Location = new Point(356, 66);
            this.cardTable.Size = new Size(604, 580);
            this.cardTable.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            this.lblTableHeader.Text = "Budget Status Breakdown";
            this.lblTableHeader.Font = ThemePalette.FontSectionTitle;
            this.lblTableHeader.ForeColor = ThemePalette.TextPrimary;
            this.lblTableHeader.Location = new Point(16, 14);
            this.lblTableHeader.Size = new Size(540, 24);

            this.dgvBudgets.Location = new Point(16, 46);
            this.dgvBudgets.Size = new Size(570, 516);
            this.dgvBudgets.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ThemePalette.StyleDataGridView(this.dgvBudgets);

            this.cardTable.Controls.Add(this.lblTableHeader);
            this.cardTable.Controls.Add(this.dgvBudgets);

            this.BackColor = ThemePalette.AppBackground;
            this.ClientSize = new Size(960, 690);
            this.Controls.Add(this.cardTable);
            this.Controls.Add(this.cardSetBudget);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblTitle);

            ((System.ComponentModel.ISupportInitialize)(this.numYear)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
