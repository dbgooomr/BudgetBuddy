using System.Drawing;
using System.Windows.Forms;
using BudgetBuddy.Controls;
using BudgetBuddy.Utils;

namespace BudgetBuddy.Forms
{
    partial class HistorySearchForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private Label lblSubtitle;

        private RoundedPanel cardFilters;
        private CheckBox chkDateRange;
        private DateTimePicker dtpFrom;
        private DateTimePicker dtpTo;
        private ComboBox cmbFilterType;
        private ComboBox cmbFilterCategory;
        private ComboBox cmbFilterKind;
        private TextBox txtKeyword;
        private Button btnSearch;
        private Button btnResetFilters;
        private Button btnExportCsv;

        private RoundedPanel cardResults;
        private Label lblResultsSummary;
        private DataGridView dgvHistory;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.lblSubtitle = new Label();
            this.cardFilters = new RoundedPanel();
            this.chkDateRange = new CheckBox();
            this.dtpFrom = new DateTimePicker();
            this.dtpTo = new DateTimePicker();
            this.cmbFilterType = new ComboBox();
            this.cmbFilterCategory = new ComboBox();
            this.cmbFilterKind = new ComboBox();
            this.txtKeyword = new TextBox();
            this.btnSearch = new Button();
            this.btnResetFilters = new Button();
            this.btnExportCsv = new Button();

            this.cardResults = new RoundedPanel();
            this.lblResultsSummary = new Label();
            this.dgvHistory = new DataGridView();

            this.SuspendLayout();

            this.lblTitle.Text = "Transaction History & Search";
            this.lblTitle.Font = ThemePalette.FontPageTitle;
            this.lblTitle.ForeColor = ThemePalette.TextPrimary;
            this.lblTitle.Location = new Point(0, 0);
            this.lblTitle.Size = new Size(500, 32);

            this.lblSubtitle.Text = "Search and filter past income and expenses by date range, category, Need/Want tag, or keyword";
            this.lblSubtitle.Font = ThemePalette.FontBody;
            this.lblSubtitle.ForeColor = ThemePalette.TextSecondary;
            this.lblSubtitle.Location = new Point(0, 32);
            this.lblSubtitle.Size = new Size(760, 22);

            // Filter Bar Card
            this.cardFilters.Location = new Point(0, 66);
            this.cardFilters.Size = new Size(960, 122);
            this.cardFilters.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            this.chkDateRange.Text = "Filter by Date Range:";
            this.chkDateRange.Font = ThemePalette.FontBodyBold;
            this.chkDateRange.Location = new Point(16, 16);
            this.chkDateRange.Size = new Size(165, 24);

            this.dtpFrom.Location = new Point(185, 14);
            this.dtpFrom.Size = new Size(125, 28);
            this.dtpFrom.Format = DateTimePickerFormat.Short;
            this.dtpFrom.Enabled = false;

            this.dtpTo.Location = new Point(320, 14);
            this.dtpTo.Size = new Size(125, 28);
            this.dtpTo.Format = DateTimePickerFormat.Short;
            this.dtpTo.Enabled = false;

            this.cmbFilterType.Location = new Point(460, 14);
            this.cmbFilterType.Size = new Size(135, 28);
            this.cmbFilterType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbFilterType.Items.AddRange(new object[] { "All Types", "Income", "Expense" });

            this.cmbFilterCategory.Location = new Point(605, 14);
            this.cmbFilterCategory.Size = new Size(185, 28);
            this.cmbFilterCategory.DropDownStyle = ComboBoxStyle.DropDownList;

            this.cmbFilterKind.Location = new Point(800, 14);
            this.cmbFilterKind.Size = new Size(140, 28);
            this.cmbFilterKind.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbFilterKind.Items.AddRange(new object[] { "All Tags", "Need", "Want" });

            this.txtKeyword.Location = new Point(16, 62);
            this.txtKeyword.Size = new Size(429, 30);
            this.txtKeyword.Font = ThemePalette.FontBody;
            this.txtKeyword.PlaceholderText = "Search keyword in notes, category, or source...";

            this.btnSearch.Text = "Search / Filter";
            this.btnSearch.Location = new Point(460, 58);
            this.btnSearch.Size = new Size(150, 36);
            ThemePalette.StyleButton(this.btnSearch, ThemePalette.PrimaryTeal, Color.White);

            this.btnResetFilters.Text = "Clear Filters";
            this.btnResetFilters.Location = new Point(620, 58);
            this.btnResetFilters.Size = new Size(135, 36);
            ThemePalette.StyleSecondaryButton(this.btnResetFilters);

            this.btnExportCsv.Text = "Export CSV";
            this.btnExportCsv.Location = new Point(765, 58);
            this.btnExportCsv.Size = new Size(175, 36);
            ThemePalette.StyleButton(this.btnExportCsv, ThemePalette.AccentIndigo, Color.White);

            this.cardFilters.Controls.Add(this.btnExportCsv);
            this.cardFilters.Controls.Add(this.btnResetFilters);
            this.cardFilters.Controls.Add(this.btnSearch);
            this.cardFilters.Controls.Add(this.txtKeyword);
            this.cardFilters.Controls.Add(this.cmbFilterKind);
            this.cardFilters.Controls.Add(this.cmbFilterCategory);
            this.cardFilters.Controls.Add(this.cmbFilterType);
            this.cardFilters.Controls.Add(this.dtpTo);
            this.cardFilters.Controls.Add(this.dtpFrom);
            this.cardFilters.Controls.Add(this.chkDateRange);

            // Results Card
            this.cardResults.Location = new Point(0, 200);
            this.cardResults.Size = new Size(960, 480);
            this.cardResults.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            this.lblResultsSummary.Text = "Matching Records: 0";
            this.lblResultsSummary.Font = ThemePalette.FontSectionTitle;
            this.lblResultsSummary.ForeColor = ThemePalette.TextPrimary;
            this.lblResultsSummary.Location = new Point(16, 14);
            this.lblResultsSummary.Size = new Size(800, 24);

            this.dgvHistory.Location = new Point(16, 46);
            this.dgvHistory.Size = new Size(926, 416);
            this.dgvHistory.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ThemePalette.StyleDataGridView(this.dgvHistory);

            this.cardResults.Controls.Add(this.lblResultsSummary);
            this.cardResults.Controls.Add(this.dgvHistory);

            this.BackColor = ThemePalette.AppBackground;
            this.ClientSize = new Size(960, 690);
            this.Controls.Add(this.cardResults);
            this.Controls.Add(this.cardFilters);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblTitle);

            this.ResumeLayout(false);
        }
    }
}
