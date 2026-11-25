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
            this.btnSearch = new BudgetBuddy.Controls.StyledButton();
            this.btnResetFilters = new BudgetBuddy.Controls.StyledButton();
            this.btnExportCsv = new BudgetBuddy.Controls.StyledButton();

            this.cardResults = new RoundedPanel();
            this.lblResultsSummary = new Label();
            this.dgvHistory = new BudgetBuddy.Controls.StyledDataGridView();

            this.SuspendLayout();

            this.lblTitle.Text = "Transaction History & Search";
            this.lblTitle.Font = ThemePalette.FontPageTitle;
            this.lblTitle.ForeColor = ThemePalette.TextPrimary;
            this.lblTitle.Location = new Point(0, 0);
            this.lblTitle.AutoSize = true;
            this.lblTitle.MinimumSize = new Size(0, 44);
            this.lblTitle.Padding = new Padding(0, 5, 0, 5);

            this.lblSubtitle.Text = "Search and filter past income and expenses by date range, category, Need/Want tag, or keyword";
            this.lblSubtitle.Font = ThemePalette.FontBody;
            this.lblSubtitle.ForeColor = ThemePalette.TextSecondary;
            this.lblSubtitle.Location = new Point(0, 52);
            this.lblSubtitle.Size = new Size(760, 22);

            this.cardFilters.Location = new Point(0, 82);
            this.cardFilters.Size = new Size(960, 122);
            this.cardFilters.MinimumSize = new Size(960, 0);
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
            this.btnSearch.FlatStyle = FlatStyle.Flat;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.BackColor = ThemePalette.PrimaryTeal;
            this.btnSearch.ForeColor = Color.White;
            this.btnSearch.Font = ThemePalette.FontBodyBold;
            this.btnSearch.Cursor = Cursors.Hand;
            ((BudgetBuddy.Controls.StyledButton)this.btnSearch).SecondaryStyle = false;

            this.btnResetFilters.Text = "Clear Filters";
            this.btnResetFilters.Location = new Point(620, 58);
            this.btnResetFilters.Size = new Size(135, 36);
            this.btnResetFilters.FlatStyle = FlatStyle.Flat;
            this.btnResetFilters.FlatAppearance.BorderColor = ThemePalette.CardBorder;
            this.btnResetFilters.FlatAppearance.BorderSize = 1;
            this.btnResetFilters.BackColor = ThemePalette.SurfaceAlt;
            this.btnResetFilters.ForeColor = ThemePalette.TextPrimary;
            this.btnResetFilters.Font = ThemePalette.FontBodyBold;
            this.btnResetFilters.Cursor = Cursors.Hand;
            ((BudgetBuddy.Controls.StyledButton)this.btnResetFilters).SecondaryStyle = true;

            this.btnExportCsv.Text = "Export CSV";
            this.btnExportCsv.Location = new Point(765, 58);
            this.btnExportCsv.Size = new Size(175, 36);
            this.btnExportCsv.FlatStyle = FlatStyle.Flat;
            this.btnExportCsv.FlatAppearance.BorderSize = 0;
            this.btnExportCsv.BackColor = ThemePalette.AccentIndigo;
            this.btnExportCsv.ForeColor = Color.White;
            this.btnExportCsv.Font = ThemePalette.FontBodyBold;
            this.btnExportCsv.Cursor = Cursors.Hand;
            ((BudgetBuddy.Controls.StyledButton)this.btnExportCsv).SecondaryStyle = false;

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

            this.cardResults.Location = new Point(0, 216);
            this.cardResults.Size = new Size(960, 480);
            this.cardResults.MinimumSize = new Size(600, 480);
            this.cardResults.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            this.lblResultsSummary.Text = "Matching Records: 0";
            this.lblResultsSummary.Font = ThemePalette.FontSectionTitle;
            this.lblResultsSummary.ForeColor = ThemePalette.TextPrimary;
            this.lblResultsSummary.Location = new Point(16, 14);
            this.lblResultsSummary.Size = new Size(800, 24);

            this.dgvHistory.Location = new Point(16, 46);
            this.dgvHistory.Size = new Size(926, 416);
            this.dgvHistory.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.dgvHistory.BackgroundColor = ThemePalette.CardBackground;
            this.dgvHistory.BorderStyle = BorderStyle.None;
            this.dgvHistory.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvHistory.GridColor = ThemePalette.CardBorder;
            this.dgvHistory.EnableHeadersVisualStyles = false;
            this.dgvHistory.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.dgvHistory.ColumnHeadersDefaultCellStyle.BackColor = ThemePalette.SurfaceHeader;
            this.dgvHistory.ColumnHeadersDefaultCellStyle.ForeColor = ThemePalette.TextSecondary;
            this.dgvHistory.ColumnHeadersDefaultCellStyle.SelectionBackColor = ThemePalette.SurfaceHeader;
            this.dgvHistory.ColumnHeadersDefaultCellStyle.SelectionForeColor = ThemePalette.TextSecondary;
            this.dgvHistory.ColumnHeadersDefaultCellStyle.Font = ThemePalette.FontCaptionBold;
            this.dgvHistory.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 6, 8, 6);
            this.dgvHistory.ColumnHeadersHeight = 38;
            this.dgvHistory.DefaultCellStyle.BackColor = ThemePalette.CardBackground;
            this.dgvHistory.DefaultCellStyle.ForeColor = ThemePalette.TextPrimary;
            this.dgvHistory.DefaultCellStyle.Font = ThemePalette.FontBody;
            this.dgvHistory.DefaultCellStyle.Padding = new Padding(8, 4, 8, 4);
            this.dgvHistory.DefaultCellStyle.SelectionBackColor = Color.FromArgb(204, 251, 241);
            this.dgvHistory.DefaultCellStyle.SelectionForeColor = ThemePalette.TextPrimary;
            this.dgvHistory.RowTemplate.Height = 38;
            this.dgvHistory.AlternatingRowsDefaultCellStyle.BackColor = ThemePalette.SurfaceAlt;
            this.dgvHistory.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(204, 251, 241);
            this.dgvHistory.AlternatingRowsDefaultCellStyle.SelectionForeColor = ThemePalette.TextPrimary;
            this.dgvHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvHistory.MultiSelect = false;
            this.dgvHistory.ReadOnly = true;
            this.dgvHistory.AllowUserToAddRows = false;
            this.dgvHistory.AllowUserToDeleteRows = false;
            this.dgvHistory.AllowUserToResizeRows = false;
            this.dgvHistory.RowHeadersVisible = false;
            this.dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            this.cardResults.Controls.Add(this.lblResultsSummary);
            this.cardResults.Controls.Add(this.dgvHistory);

            this.cmbFilterType.BackColor = ThemePalette.InputBackground;
            this.cmbFilterType.ForeColor = ThemePalette.TextPrimary;
            this.cmbFilterCategory.BackColor = ThemePalette.InputBackground;
            this.cmbFilterCategory.ForeColor = ThemePalette.TextPrimary;
            this.cmbFilterKind.BackColor = ThemePalette.InputBackground;
            this.cmbFilterKind.ForeColor = ThemePalette.TextPrimary;
            this.txtKeyword.BackColor = ThemePalette.InputBackground;
            this.txtKeyword.ForeColor = ThemePalette.TextPrimary;

            this.btnExportCsv.UseMnemonic = false;
            this.btnResetFilters.UseMnemonic = false;
            this.btnSearch.UseMnemonic = false;
            this.chkDateRange.UseMnemonic = false;
            this.lblResultsSummary.UseMnemonic = false;
            this.lblSubtitle.UseMnemonic = false;
            this.lblTitle.UseMnemonic = false;

            this.BackColor = ThemePalette.AppBackground;
            this.ClientSize = new Size(960, 706);
            this.AutoScroll = true;
            this.AutoScrollMinSize = new Size(960, 706);
            this.Controls.Add(this.cardResults);
            this.Controls.Add(this.cardFilters);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblTitle);

            this.ResumeLayout(false);
        }
    }
}
