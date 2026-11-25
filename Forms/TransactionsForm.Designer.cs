using System.Drawing;
using System.Windows.Forms;
using BudgetBuddy.Controls;
using BudgetBuddy.Utils;

namespace BudgetBuddy.Forms
{
    partial class TransactionsForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private Label lblSubtitle;

        private RoundedPanel cardForm;
        private Label lblFormHeader;
        private Label lblType;
        private ComboBox cmbType;
        private Label lblDate;
        private DateTimePicker dtpDate;
        private Label lblAmount;
        private TextBox txtAmount;

        private Label lblSourceOrCategory;
        private ComboBox cmbSourceOrCategory;

        private Label lblPaymentMethod;
        private ComboBox cmbPaymentMethod;

        private Label lblExpenseKind;
        private ComboBox cmbExpenseKind;

        private Label lblNotes;
        private TextBox txtNotes;

        private Button btnSave;
        private Button btnClear;
        private Button btnDelete;

        private RoundedPanel cardGrid;
        private Label lblGridHeader;
        private DataGridView dgvTransactions;
        private Button btnSelectAll;
        private Button btnDeselectAll;
        private Button btnBulkDelete;
        private ComboBox cmbFilterType, cmbFilterCategory;
        private TextBox txtFilterSearch;
        private Button btnFilterClear;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.lblSubtitle = new Label();
            this.cardForm = new RoundedPanel();
            this.lblFormHeader = new Label();
            this.lblType = new Label();
            this.cmbType = new ComboBox();
            this.lblDate = new Label();
            this.dtpDate = new DateTimePicker();
            this.lblAmount = new Label();
            this.txtAmount = new TextBox();
            this.lblSourceOrCategory = new Label();
            this.cmbSourceOrCategory = new ComboBox();
            this.lblPaymentMethod = new Label();
            this.cmbPaymentMethod = new ComboBox();
            this.lblExpenseKind = new Label();
            this.cmbExpenseKind = new ComboBox();
            this.lblNotes = new Label();
            this.txtNotes = new TextBox();
            this.btnSave = new BudgetBuddy.Controls.StyledButton();
            this.btnClear = new BudgetBuddy.Controls.StyledButton();
            this.btnDelete = new BudgetBuddy.Controls.StyledButton();

            this.cardGrid = new RoundedPanel();
            this.lblGridHeader = new Label();
            this.dgvTransactions = new BudgetBuddy.Controls.StyledDataGridView();
            this.btnSelectAll = new BudgetBuddy.Controls.StyledButton();
            this.btnDeselectAll = new BudgetBuddy.Controls.StyledButton();
            this.btnBulkDelete = new BudgetBuddy.Controls.StyledButton();
            this.cmbFilterType = new ComboBox();
            this.cmbFilterCategory = new ComboBox();
            this.txtFilterSearch = new TextBox();
            this.btnFilterClear = new BudgetBuddy.Controls.StyledButton();

            this.SuspendLayout();

            this.lblTitle.Text = "Income & Expense Transactions";
            this.lblTitle.Font = ThemePalette.FontPageTitle;
            this.lblTitle.ForeColor = ThemePalette.TextPrimary;
            this.lblTitle.Location = new Point(0, 0);
            this.lblTitle.AutoSize = true;
            this.lblTitle.MinimumSize = new Size(0, 44);
            this.lblTitle.Padding = new Padding(0, 5, 0, 5);

            this.lblSubtitle.Text = "Record, edit, or delete student allowances, part-time income, and daily expenses";
            this.lblSubtitle.Font = ThemePalette.FontBody;
            this.lblSubtitle.ForeColor = ThemePalette.TextSecondary;
            this.lblSubtitle.Location = new Point(0, 52);
            this.lblSubtitle.Size = new Size(650, 22);

            this.cardForm.Location = new Point(0, 82);
            this.cardForm.Size = new Size(340, 610);

            this.lblFormHeader.Text = "Add New Transaction";
            this.lblFormHeader.Font = ThemePalette.FontSectionTitle;
            this.lblFormHeader.ForeColor = ThemePalette.TextPrimary;
            this.lblFormHeader.Location = new Point(16, 14);
            this.lblFormHeader.Size = new Size(300, 24);

            this.lblType.Text = "Transaction Type";
            this.lblType.Font = ThemePalette.FontBodyBold;
            this.lblType.ForeColor = ThemePalette.TextSecondary;
            this.lblType.Location = new Point(16, 46);
            this.lblType.AutoSize = true;
            this.lblType.MinimumSize = new Size(0, 24);
            this.lblType.Size = new Size(300, 20);
            this.cardForm.Controls.Add(this.lblType);
            this.cmbType.Location = new Point(16, 76);
            this.cmbType.Size = new Size(304, 28);
            this.cmbType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbType.Font = ThemePalette.FontBody;
            this.cmbType.Items.AddRange(new object[] { "Expense", "Income" });
            this.cardForm.Controls.Add(this.cmbType);

            this.lblDate.Text = "Date";
            this.lblDate.Font = ThemePalette.FontBodyBold;
            this.lblDate.ForeColor = ThemePalette.TextSecondary;
            this.lblDate.Location = new Point(16, 109);
            this.lblDate.AutoSize = true;
            this.lblDate.MinimumSize = new Size(0, 24);
            this.lblDate.Size = new Size(300, 20);
            this.cardForm.Controls.Add(this.lblDate);
            this.dtpDate.Location = new Point(16, 139);
            this.dtpDate.Size = new Size(304, 28);
            this.dtpDate.Format = DateTimePickerFormat.Short;
            this.dtpDate.Font = ThemePalette.FontBody;
            this.cardForm.Controls.Add(this.dtpDate);

            this.lblAmount.Text = "Amount (\u20B1)";
            this.lblAmount.Font = ThemePalette.FontBodyBold;
            this.lblAmount.ForeColor = ThemePalette.TextSecondary;
            this.lblAmount.Location = new Point(16, 172);
            this.lblAmount.AutoSize = true;
            this.lblAmount.MinimumSize = new Size(0, 24);
            this.lblAmount.Size = new Size(300, 20);
            this.cardForm.Controls.Add(this.lblAmount);
            this.txtAmount.Location = new Point(16, 202);
            this.txtAmount.Size = new Size(304, 28);
            this.txtAmount.Font = ThemePalette.FontBody;
            this.cardForm.Controls.Add(this.txtAmount);

            this.lblSourceOrCategory.Text = "Expense Category";
            this.lblSourceOrCategory.Font = ThemePalette.FontBodyBold;
            this.lblSourceOrCategory.ForeColor = ThemePalette.TextSecondary;
            this.lblSourceOrCategory.Location = new Point(16, 235);
            this.lblSourceOrCategory.AutoSize = true;
            this.lblSourceOrCategory.MinimumSize = new Size(0, 24);
            this.lblSourceOrCategory.Size = new Size(300, 20);
            this.cardForm.Controls.Add(this.lblSourceOrCategory);
            this.cmbSourceOrCategory.Location = new Point(16, 265);
            this.cmbSourceOrCategory.Size = new Size(304, 28);
            this.cmbSourceOrCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbSourceOrCategory.Font = ThemePalette.FontBody;
            this.cardForm.Controls.Add(this.cmbSourceOrCategory);

            this.lblPaymentMethod.Text = "Payment Method";
            this.lblPaymentMethod.Font = ThemePalette.FontBodyBold;
            this.lblPaymentMethod.ForeColor = ThemePalette.TextSecondary;
            this.lblPaymentMethod.Location = new Point(16, 298);
            this.lblPaymentMethod.AutoSize = true;
            this.lblPaymentMethod.MinimumSize = new Size(0, 24);
            this.lblPaymentMethod.Size = new Size(300, 20);
            this.cardForm.Controls.Add(this.lblPaymentMethod);
            this.cmbPaymentMethod.Location = new Point(16, 328);
            this.cmbPaymentMethod.Size = new Size(304, 28);
            this.cmbPaymentMethod.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbPaymentMethod.Font = ThemePalette.FontBody;
            this.cmbPaymentMethod.Items.AddRange(new object[] { "Cash", "E-wallet" });
            this.cardForm.Controls.Add(this.cmbPaymentMethod);

            this.lblExpenseKind.Text = "Need or Want Tag";
            this.lblExpenseKind.Font = ThemePalette.FontBodyBold;
            this.lblExpenseKind.ForeColor = ThemePalette.TextSecondary;
            this.lblExpenseKind.Location = new Point(16, 361);
            this.lblExpenseKind.AutoSize = true;
            this.lblExpenseKind.MinimumSize = new Size(0, 24);
            this.lblExpenseKind.Size = new Size(300, 20);
            this.cardForm.Controls.Add(this.lblExpenseKind);
            this.cmbExpenseKind.Location = new Point(16, 391);
            this.cmbExpenseKind.Size = new Size(304, 28);
            this.cmbExpenseKind.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbExpenseKind.Font = ThemePalette.FontBody;
            this.cmbExpenseKind.Items.AddRange(new object[] { "Need", "Want" });
            this.cardForm.Controls.Add(this.cmbExpenseKind);

            this.lblNotes.Text = "Notes (Optional)";
            this.lblNotes.Font = ThemePalette.FontBodyBold;
            this.lblNotes.ForeColor = ThemePalette.TextSecondary;
            this.lblNotes.Location = new Point(16, 424);
            this.lblNotes.AutoSize = true;
            this.lblNotes.MinimumSize = new Size(0, 24);
            this.lblNotes.Size = new Size(300, 20);
            this.cardForm.Controls.Add(this.lblNotes);
            this.txtNotes.Location = new Point(16, 454);
            this.txtNotes.Size = new Size(304, 54);
            this.txtNotes.Multiline = true;
            this.txtNotes.Font = ThemePalette.FontBody;
            this.cardForm.Controls.Add(this.txtNotes);

            this.btnSave.Text = "Save Record";
            this.btnSave.Location = new Point(16, 518);
            this.btnSave.Size = new Size(148, 38);
            this.btnSave.FlatStyle = FlatStyle.Flat;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.BackColor = ThemePalette.PrimaryTeal;
            this.btnSave.ForeColor = Color.White;
            this.btnSave.Font = ThemePalette.FontBodyBold;
            this.btnSave.Cursor = Cursors.Hand;
            ((BudgetBuddy.Controls.StyledButton)this.btnSave).SecondaryStyle = false;
            this.cardForm.Controls.Add(this.btnSave);

            this.btnClear.Text = "Clear / New";
            this.btnClear.Location = new Point(172, 518);
            this.btnClear.Size = new Size(148, 38);
            this.btnClear.FlatStyle = FlatStyle.Flat;
            this.btnClear.FlatAppearance.BorderColor = ThemePalette.CardBorder;
            this.btnClear.FlatAppearance.BorderSize = 1;
            this.btnClear.BackColor = ThemePalette.SurfaceAlt;
            this.btnClear.ForeColor = ThemePalette.TextPrimary;
            this.btnClear.Font = ThemePalette.FontBodyBold;
            this.btnClear.Cursor = Cursors.Hand;
            ((BudgetBuddy.Controls.StyledButton)this.btnClear).SecondaryStyle = true;
            this.cardForm.Controls.Add(this.btnClear);

            this.btnDelete.Text = "Delete Selected";
            this.btnDelete.Location = new Point(16, 564);
            this.btnDelete.Size = new Size(304, 34);
            this.btnDelete.Enabled = false;
            this.btnDelete.FlatStyle = FlatStyle.Flat;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.BackColor = ThemePalette.DangerRose;
            this.btnDelete.ForeColor = Color.White;
            this.btnDelete.Font = ThemePalette.FontBodyBold;
            this.btnDelete.Cursor = Cursors.Hand;
            ((BudgetBuddy.Controls.StyledButton)this.btnDelete).SecondaryStyle = false;
            this.cardForm.Controls.Add(this.btnDelete);

            cardForm.Controls.Add(lblFormHeader);

            this.cardGrid.Location = new Point(356, 82);
            this.cardGrid.Size = new Size(604, 610);
            this.cardGrid.MinimumSize = new Size(574, 610);
            this.cardGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            this.lblGridHeader.Text = "All Recorded Transactions (Click a row to edit or delete)";
            this.lblGridHeader.Font = ThemePalette.FontSectionTitle;
            this.lblGridHeader.ForeColor = ThemePalette.TextPrimary;
            this.lblGridHeader.Location = new Point(16, 14);
            this.lblGridHeader.Size = new Size(540, 24);

            this.btnSelectAll.Text = "Select All";
            this.btnSelectAll.Location = new Point(16, 84);
            this.btnSelectAll.Size = new Size(120, 32);
            this.btnSelectAll.FlatStyle = FlatStyle.Flat;
            this.btnSelectAll.FlatAppearance.BorderColor = ThemePalette.CardBorder;
            this.btnSelectAll.FlatAppearance.BorderSize = 1;
            this.btnSelectAll.BackColor = ThemePalette.SurfaceAlt;
            this.btnSelectAll.ForeColor = ThemePalette.TextPrimary;
            this.btnSelectAll.Font = ThemePalette.FontBodyBold;
            this.btnSelectAll.Cursor = Cursors.Hand;
            ((BudgetBuddy.Controls.StyledButton)this.btnSelectAll).SecondaryStyle = true;
            this.cardGrid.Controls.Add(this.btnSelectAll);

            this.btnDeselectAll.Text = "Select";
            this.btnDeselectAll.Location = new Point(144, 84);
            this.btnDeselectAll.Size = new Size(120, 32);
            this.btnDeselectAll.FlatStyle = FlatStyle.Flat;
            this.btnDeselectAll.FlatAppearance.BorderColor = ThemePalette.CardBorder;
            this.btnDeselectAll.FlatAppearance.BorderSize = 1;
            this.btnDeselectAll.BackColor = ThemePalette.SurfaceAlt;
            this.btnDeselectAll.ForeColor = ThemePalette.TextPrimary;
            this.btnDeselectAll.Font = ThemePalette.FontBodyBold;
            this.btnDeselectAll.Cursor = Cursors.Hand;
            ((BudgetBuddy.Controls.StyledButton)this.btnDeselectAll).SecondaryStyle = true;
            this.cardGrid.Controls.Add(this.btnDeselectAll);

            this.btnBulkDelete.Text = "Delete";
            this.btnBulkDelete.Location = new Point(276, 84);
            this.btnBulkDelete.Size = new Size(140, 32);
            this.btnBulkDelete.FlatStyle = FlatStyle.Flat;
            this.btnBulkDelete.FlatAppearance.BorderSize = 0;
            this.btnBulkDelete.BackColor = ThemePalette.DangerRose;
            this.btnBulkDelete.ForeColor = Color.White;
            this.btnBulkDelete.Font = ThemePalette.FontBodyBold;
            this.btnBulkDelete.Cursor = Cursors.Hand;
            ((BudgetBuddy.Controls.StyledButton)this.btnBulkDelete).SecondaryStyle = false;
            this.cardGrid.Controls.Add(this.btnBulkDelete);

            this.cmbFilterType.Location = new Point(16, 48);
            this.cmbFilterType.Size = new Size(104, 28);
            this.cmbFilterType.Font = ThemePalette.FontBody;
            this.cmbFilterType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbFilterType.Items.AddRange(new object[] { "All Types", "Income", "Expense" });
            this.cmbFilterCategory.Location = new Point(128, 48);
            this.cmbFilterCategory.Size = new Size(176, 28);
            this.cmbFilterCategory.Font = ThemePalette.FontBody;
            this.cmbFilterCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbFilterCategory.Items.Add("All Categories");
            this.txtFilterSearch.Location = new Point(312, 48);
            this.txtFilterSearch.Size = new Size(170, 28);
            this.txtFilterSearch.Font = ThemePalette.FontBody;
            this.txtFilterSearch.PlaceholderText = "Search notes...";
            this.btnFilterClear.Text = "Clear";
            this.btnFilterClear.Location = new Point(490, 46);
            this.btnFilterClear.Size = new Size(80, 32);
            this.btnFilterClear.FlatStyle = FlatStyle.Flat;
            this.btnFilterClear.FlatAppearance.BorderColor = ThemePalette.CardBorder;
            this.btnFilterClear.FlatAppearance.BorderSize = 1;
            this.btnFilterClear.BackColor = ThemePalette.SurfaceAlt;
            this.btnFilterClear.ForeColor = ThemePalette.TextPrimary;
            this.btnFilterClear.Font = ThemePalette.FontBodyBold;
            this.btnFilterClear.Cursor = Cursors.Hand;
            ((BudgetBuddy.Controls.StyledButton)this.btnFilterClear).SecondaryStyle = true;
            this.cardGrid.Controls.Add(this.cmbFilterType);
            this.cardGrid.Controls.Add(this.cmbFilterCategory);
            this.cardGrid.Controls.Add(this.txtFilterSearch);
            this.cardGrid.Controls.Add(this.btnFilterClear);

            this.dgvTransactions.Location = new Point(16, 124);
            this.dgvTransactions.Size = new Size(570, 468);
            this.dgvTransactions.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.dgvTransactions.BackgroundColor = ThemePalette.CardBackground;
            this.dgvTransactions.BorderStyle = BorderStyle.None;
            this.dgvTransactions.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvTransactions.GridColor = ThemePalette.CardBorder;
            this.dgvTransactions.EnableHeadersVisualStyles = false;
            this.dgvTransactions.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.dgvTransactions.ColumnHeadersDefaultCellStyle.BackColor = ThemePalette.SurfaceHeader;
            this.dgvTransactions.ColumnHeadersDefaultCellStyle.ForeColor = ThemePalette.TextSecondary;
            this.dgvTransactions.ColumnHeadersDefaultCellStyle.SelectionBackColor = ThemePalette.SurfaceHeader;
            this.dgvTransactions.ColumnHeadersDefaultCellStyle.SelectionForeColor = ThemePalette.TextSecondary;
            this.dgvTransactions.ColumnHeadersDefaultCellStyle.Font = ThemePalette.FontCaptionBold;
            this.dgvTransactions.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 6, 8, 6);
            this.dgvTransactions.ColumnHeadersHeight = 38;
            this.dgvTransactions.DefaultCellStyle.BackColor = ThemePalette.CardBackground;
            this.dgvTransactions.DefaultCellStyle.ForeColor = ThemePalette.TextPrimary;
            this.dgvTransactions.DefaultCellStyle.Font = ThemePalette.FontBody;
            this.dgvTransactions.DefaultCellStyle.Padding = new Padding(8, 4, 8, 4);
            this.dgvTransactions.DefaultCellStyle.SelectionBackColor = Color.FromArgb(204, 251, 241);
            this.dgvTransactions.DefaultCellStyle.SelectionForeColor = ThemePalette.TextPrimary;
            this.dgvTransactions.RowTemplate.Height = 38;
            this.dgvTransactions.AlternatingRowsDefaultCellStyle.BackColor = ThemePalette.SurfaceAlt;
            this.dgvTransactions.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(204, 251, 241);
            this.dgvTransactions.AlternatingRowsDefaultCellStyle.SelectionForeColor = ThemePalette.TextPrimary;
            this.dgvTransactions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvTransactions.MultiSelect = false;
            this.dgvTransactions.ReadOnly = true;
            this.dgvTransactions.AllowUserToAddRows = false;
            this.dgvTransactions.AllowUserToDeleteRows = false;
            this.dgvTransactions.AllowUserToResizeRows = false;
            this.dgvTransactions.RowHeadersVisible = false;
            this.dgvTransactions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            this.cardGrid.Controls.Add(this.lblGridHeader);
            this.cardGrid.Controls.Add(this.dgvTransactions);

            this.cmbType.BackColor = ThemePalette.InputBackground;
            this.cmbType.ForeColor = ThemePalette.TextPrimary;
            this.txtAmount.BackColor = ThemePalette.InputBackground;
            this.txtAmount.ForeColor = ThemePalette.TextPrimary;
            this.cmbSourceOrCategory.BackColor = ThemePalette.InputBackground;
            this.cmbSourceOrCategory.ForeColor = ThemePalette.TextPrimary;
            this.cmbPaymentMethod.BackColor = ThemePalette.InputBackground;
            this.cmbPaymentMethod.ForeColor = ThemePalette.TextPrimary;
            this.cmbExpenseKind.BackColor = ThemePalette.InputBackground;
            this.cmbExpenseKind.ForeColor = ThemePalette.TextPrimary;
            this.txtNotes.BackColor = ThemePalette.InputBackground;
            this.txtNotes.ForeColor = ThemePalette.TextPrimary;
            this.cmbFilterType.BackColor = ThemePalette.InputBackground;
            this.cmbFilterType.ForeColor = ThemePalette.TextPrimary;
            this.txtFilterSearch.BackColor = ThemePalette.InputBackground;
            this.txtFilterSearch.ForeColor = ThemePalette.TextPrimary;

            this.btnBulkDelete.UseMnemonic = false;
            this.btnClear.UseMnemonic = false;
            this.btnDelete.UseMnemonic = false;
            this.btnDeselectAll.UseMnemonic = false;
            this.btnFilterClear.UseMnemonic = false;
            this.btnSave.UseMnemonic = false;
            this.btnSelectAll.UseMnemonic = false;
            this.lblAmount.UseMnemonic = false;
            this.lblDate.UseMnemonic = false;
            this.lblExpenseKind.UseMnemonic = false;
            this.lblFormHeader.UseMnemonic = false;
            this.lblGridHeader.UseMnemonic = false;
            this.lblNotes.UseMnemonic = false;
            this.lblPaymentMethod.UseMnemonic = false;
            this.lblSourceOrCategory.UseMnemonic = false;
            this.lblSubtitle.UseMnemonic = false;
            this.lblTitle.UseMnemonic = false;
            this.lblType.UseMnemonic = false;

            this.BackColor = ThemePalette.AppBackground;
            this.ClientSize = new Size(960, 706);
            this.AutoScroll = true;
            this.AutoScrollMinSize = new Size(930, 706);
            this.Controls.Add(this.cardGrid);
            this.Controls.Add(this.cardForm);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblTitle);

            this.ResumeLayout(false);
        }

    }
}
