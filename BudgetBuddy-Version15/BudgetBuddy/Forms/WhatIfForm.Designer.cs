using System.Drawing;
using System.Windows.Forms;
using BudgetBuddy.Controls;
using BudgetBuddy.Utils;

namespace BudgetBuddy.Forms
{
    partial class WhatIfForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private Label lblSubtitle;

        private RoundedPanel cardInputs;
        private Label lblInputHeader;
        private Label lblScenarioType;
        private ComboBox cmbScenarioType;

        private Label lblAmount;
        private TextBox txtAmount;

        private Label lblRepeatCount;
        private NumericUpDown numRepeatCount;

        private Label lblCategoryOrGoal;
        private ComboBox cmbCategoryOrGoal;

        private Button btnSimulate;
        private Label lblNoSaveNote;

        private RoundedPanel cardResults;
        private Label lblResultHeader;
        private Label lblSimSummaryBanner;

        private Label lblBalanceCompare;
        private Label lblDailyCompare;
        private Label lblBudgetImpact;
        private Label lblDateImpactTitle;
        private Label lblDateImpactValue;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.lblSubtitle = new Label();
            this.cardInputs = new RoundedPanel();
            this.lblInputHeader = new Label();
            this.lblScenarioType = new Label();
            this.cmbScenarioType = new ComboBox();
            this.lblAmount = new Label();
            this.txtAmount = new TextBox();
            this.lblRepeatCount = new Label();
            this.numRepeatCount = new NumericUpDown();
            this.lblCategoryOrGoal = new Label();
            this.cmbCategoryOrGoal = new ComboBox();
            this.btnSimulate = new Button();
            this.lblNoSaveNote = new Label();

            this.cardResults = new RoundedPanel();
            this.lblResultHeader = new Label();
            this.lblSimSummaryBanner = new Label();
            this.lblBalanceCompare = new Label();
            this.lblDailyCompare = new Label();
            this.lblBudgetImpact = new Label();
            this.lblDateImpactTitle = new Label();
            this.lblDateImpactValue = new Label();

            ((System.ComponentModel.ISupportInitialize)(this.numRepeatCount)).BeginInit();
            this.SuspendLayout();

            this.lblTitle.Text = "What-If Scenario Simulator";
            this.lblTitle.Font = ThemePalette.FontPageTitle;
            this.lblTitle.ForeColor = ThemePalette.TextPrimary;
            this.lblTitle.Location = new Point(0, 0);
            this.lblTitle.Size = new Size(500, 32);

            this.lblSubtitle.Text = "Test a planned purchase, repeating expense, or daily saving goal before spending \u2014 nothing is saved";
            this.lblSubtitle.Font = ThemePalette.FontBody;
            this.lblSubtitle.ForeColor = ThemePalette.TextSecondary;
            this.lblSubtitle.Location = new Point(0, 32);
            this.lblSubtitle.Size = new Size(760, 22);

            // Left Input Card
            this.cardInputs.Location = new Point(0, 66);
            this.cardInputs.Size = new Size(380, 540);

            this.lblInputHeader.Text = "1. Configure Your Scenario";
            this.lblInputHeader.Font = ThemePalette.FontSectionTitle;
            this.lblInputHeader.ForeColor = ThemePalette.TextPrimary;
            this.lblInputHeader.Location = new Point(18, 16);
            this.lblInputHeader.Size = new Size(340, 24);

            this.lblScenarioType.Text = "Scenario Type";
            this.lblScenarioType.Font = ThemePalette.FontBodyBold;
            this.lblScenarioType.ForeColor = ThemePalette.TextSecondary;
            this.lblScenarioType.Location = new Point(18, 54);
            this.lblScenarioType.Size = new Size(340, 18);

            this.cmbScenarioType.Location = new Point(18, 76);
            this.cmbScenarioType.Size = new Size(342, 30);
            this.cmbScenarioType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbScenarioType.Font = ThemePalette.FontBody;
            this.cmbScenarioType.Items.AddRange(new object[]
            {
                "One-Time Purchase",
                "Repeating Expense",
                "Daily Saving Toward Goal"
            });

            this.lblAmount.Text = "Planned Purchase Amount (\u20B1)";
            this.lblAmount.Font = ThemePalette.FontBodyBold;
            this.lblAmount.ForeColor = ThemePalette.TextSecondary;
            this.lblAmount.Location = new Point(18, 122);
            this.lblAmount.Size = new Size(340, 18);

            this.txtAmount.Location = new Point(18, 144);
            this.txtAmount.Size = new Size(342, 30);
            this.txtAmount.Font = ThemePalette.FontBody;

            this.lblRepeatCount.Text = "How Many Times? (For Repeating Expense)";
            this.lblRepeatCount.Font = ThemePalette.FontBodyBold;
            this.lblRepeatCount.ForeColor = ThemePalette.TextSecondary;
            this.lblRepeatCount.Location = new Point(18, 190);
            this.lblRepeatCount.Size = new Size(340, 18);

            this.numRepeatCount.Location = new Point(18, 212);
            this.numRepeatCount.Size = new Size(342, 30);
            this.numRepeatCount.Minimum = 1;
            this.numRepeatCount.Maximum = 365;
            this.numRepeatCount.Value = 3;
            this.numRepeatCount.Font = ThemePalette.FontBody;

            this.lblCategoryOrGoal.Text = "Expense Category";
            this.lblCategoryOrGoal.Font = ThemePalette.FontBodyBold;
            this.lblCategoryOrGoal.ForeColor = ThemePalette.TextSecondary;
            this.lblCategoryOrGoal.Location = new Point(18, 258);
            this.lblCategoryOrGoal.Size = new Size(340, 18);

            this.cmbCategoryOrGoal.Location = new Point(18, 280);
            this.cmbCategoryOrGoal.Size = new Size(342, 30);
            this.cmbCategoryOrGoal.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbCategoryOrGoal.Font = ThemePalette.FontBody;

            this.btnSimulate.Text = "\u26A1 Run What-If Simulation";
            this.btnSimulate.Location = new Point(18, 334);
            this.btnSimulate.Size = new Size(342, 46);
            ThemePalette.StyleButton(this.btnSimulate, ThemePalette.AccentIndigo, Color.White);

            this.lblNoSaveNote.Text = "Simulation mode only: Your real allowance balance, budgets, and transactions are never modified.";
            this.lblNoSaveNote.Font = ThemePalette.FontCaption;
            this.lblNoSaveNote.ForeColor = ThemePalette.TextSecondary;
            this.lblNoSaveNote.Location = new Point(18, 396);
            this.lblNoSaveNote.Size = new Size(342, 50);

            this.cardInputs.Controls.Add(this.lblNoSaveNote);
            this.cardInputs.Controls.Add(this.btnSimulate);
            this.cardInputs.Controls.Add(this.cmbCategoryOrGoal);
            this.cardInputs.Controls.Add(this.lblCategoryOrGoal);
            this.cardInputs.Controls.Add(this.numRepeatCount);
            this.cardInputs.Controls.Add(this.lblRepeatCount);
            this.cardInputs.Controls.Add(this.txtAmount);
            this.cardInputs.Controls.Add(this.lblAmount);
            this.cardInputs.Controls.Add(this.cmbScenarioType);
            this.cardInputs.Controls.Add(this.lblScenarioType);
            this.cardInputs.Controls.Add(this.lblInputHeader);

            // Right Results Card
            this.cardResults.Location = new Point(398, 66);
            this.cardResults.Size = new Size(562, 540);
            this.cardResults.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            this.lblResultHeader.Text = "2. Projected Financial Impact";
            this.lblResultHeader.Font = ThemePalette.FontSectionTitle;
            this.lblResultHeader.ForeColor = ThemePalette.TextPrimary;
            this.lblResultHeader.Location = new Point(20, 16);
            this.lblResultHeader.Size = new Size(500, 24);

            this.lblSimSummaryBanner.Text = "Choose a scenario on the left and click 'Run What-If Simulation' to preview the impact.";
            this.lblSimSummaryBanner.Font = ThemePalette.FontBodyBold;
            this.lblSimSummaryBanner.BackColor = Color.FromArgb(214, 220, 248);
            this.lblSimSummaryBanner.ForeColor = ThemePalette.AccentIndigo;
            this.lblSimSummaryBanner.Location = new Point(20, 52);
            this.lblSimSummaryBanner.Size = new Size(520, 62);
            this.lblSimSummaryBanner.Padding = new Padding(12);

            this.lblBalanceCompare.Text = "Projected Cycle Balance:  \u2014";
            this.lblBalanceCompare.Font = new System.Drawing.Font("Segoe UI", 12f, FontStyle.Bold);
            this.lblBalanceCompare.ForeColor = ThemePalette.TextPrimary;
            this.lblBalanceCompare.Location = new Point(20, 134);
            this.lblBalanceCompare.Size = new Size(520, 32);

            this.lblDailyCompare.Text = "Projected Safe Daily Limit:  \u2014";
            this.lblDailyCompare.Font = new System.Drawing.Font("Segoe UI", 12f, FontStyle.Bold);
            this.lblDailyCompare.ForeColor = ThemePalette.TextPrimary;
            this.lblDailyCompare.Location = new Point(20, 178);
            this.lblDailyCompare.Size = new Size(520, 32);

            this.lblBudgetImpact.Text = "Category Budget Status After:  \u2014";
            this.lblBudgetImpact.Font = ThemePalette.FontBodyBold;
            this.lblBudgetImpact.ForeColor = ThemePalette.TextSecondary;
            this.lblBudgetImpact.Location = new Point(20, 224);
            this.lblBudgetImpact.Size = new Size(520, 32);

            this.lblDateImpactTitle.Text = "ESTIMATED TIMELINE";
            this.lblDateImpactTitle.Font = ThemePalette.FontCaption;
            this.lblDateImpactTitle.ForeColor = ThemePalette.TextSecondary;
            this.lblDateImpactTitle.Location = new Point(20, 276);
            this.lblDateImpactTitle.Size = new Size(520, 20);

            this.lblDateImpactValue.Text = "\u2014";
            this.lblDateImpactValue.Font = new System.Drawing.Font("Segoe UI", 13f, FontStyle.Bold);
            this.lblDateImpactValue.ForeColor = ThemePalette.PrimaryTealDark;
            this.lblDateImpactValue.Location = new Point(20, 298);
            this.lblDateImpactValue.Size = new Size(520, 36);

            this.cardResults.Controls.Add(this.lblDateImpactValue);
            this.cardResults.Controls.Add(this.lblDateImpactTitle);
            this.cardResults.Controls.Add(this.lblBudgetImpact);
            this.cardResults.Controls.Add(this.lblDailyCompare);
            this.cardResults.Controls.Add(this.lblBalanceCompare);
            this.cardResults.Controls.Add(this.lblSimSummaryBanner);
            this.cardResults.Controls.Add(this.lblResultHeader);

            this.BackColor = ThemePalette.AppBackground;
            this.ClientSize = new Size(960, 690);
            this.Controls.Add(this.cardResults);
            this.Controls.Add(this.cardInputs);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblTitle);

            ((System.ComponentModel.ISupportInitialize)(this.numRepeatCount)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
