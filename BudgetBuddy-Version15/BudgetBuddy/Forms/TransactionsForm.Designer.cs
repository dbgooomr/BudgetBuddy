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
            this.btnSave = new Button();
            this.btnClear = new Button();
            this.btnDelete = new Button();

            this.cardGrid = new RoundedPanel();
            this.lblGridHeader = new Label();
            this.dgvTransactions = new DataGridView();
            this.btnSelectAll = new Button();
            this.btnDeselectAll = new Button();
            this.btnBulkDelete = new Button();

            this.SuspendLayout();

            this.lblTitle.Text = "Income & Expense Transactions";
            this.lblTitle.Font = ThemePalette.FontPageTitle;
            this.lblTitle.ForeColor = ThemePalette.TextPrimary;
            this.lblTitle.Location = new Point(0, 0);
            this.lblTitle.Size = new Size(500, 32);

            this.lblSubtitle.Text = "Record, edit, or delete student allowances, part-time income, and daily expenses";
            this.lblSubtitle.Font = ThemePalette.FontBody;
            this.lblSubtitle.ForeColor = ThemePalette.TextSecondary;
            this.lblSubtitle.Location = new Point(0, 32);
            this.lblSubtitle.Size = new Size(650, 22);

            // Left Form Card
            this.cardForm.Location = new Point(0, 66);
            this.cardForm.Size = new Size(340, 610);

            this.lblFormHeader.Text = "Add New Transaction";
            this.lblFormHeader.Font = ThemePalette.FontSectionTitle;
            this.lblFormHeader.ForeColor = ThemePalette.TextPrimary;
            this.lblFormHeader.Location = new Point(16, 14);
            this.lblFormHeader.Size = new Size(300, 24);

            int y = 48;
            AddFieldLabel(cardForm, lblType, "Transaction Type", y);
            cmbType.Location = new Point(16, y + 20);
            cmbType.Size = new Size(304, 28);
            cmbType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbType.Font = ThemePalette.FontBody;
            cmbType.Items.AddRange(new object[] { "Expense", "Income" });
            cardForm.Controls.Add(cmbType);

            y += 58;
            AddFieldLabel(cardForm, lblDate, "Date", y);
            dtpDate.Location = new Point(16, y + 20);
            dtpDate.Size = new Size(304, 28);
            dtpDate.Format = DateTimePickerFormat.Short;
            dtpDate.Font = ThemePalette.FontBody;
            cardForm.Controls.Add(dtpDate);

            y += 58;
            AddFieldLabel(cardForm, lblAmount, "Amount (\u20B1)", y);
            txtAmount.Location = new Point(16, y + 20);
            txtAmount.Size = new Size(304, 28);
            txtAmount.Font = ThemePalette.FontBody;
            cardForm.Controls.Add(txtAmount);

            y += 58;
            AddFieldLabel(cardForm, lblSourceOrCategory, "Expense Category", y);
            cmbSourceOrCategory.Location = new Point(16, y + 20);
            cmbSourceOrCategory.Size = new Size(304, 28);
            cmbSourceOrCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSourceOrCategory.Font = ThemePalette.FontBody;
            cardForm.Controls.Add(cmbSourceOrCategory);

            y += 58;
            AddFieldLabel(cardForm, lblPaymentMethod, "Payment Method", y);
            cmbPaymentMethod.Location = new Point(16, y + 20);
            cmbPaymentMethod.Size = new Size(304, 28);
            cmbPaymentMethod.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPaymentMethod.Font = ThemePalette.FontBody;
            cmbPaymentMethod.Items.AddRange(new object[] { "Cash", "E-wallet" });
            cardForm.Controls.Add(cmbPaymentMethod);

            y += 58;
            AddFieldLabel(cardForm, lblExpenseKind, "Need or Want Tag", y);
            cmbExpenseKind.Location = new Point(16, y + 20);
            cmbExpenseKind.Size = new Size(304, 28);
            cmbExpenseKind.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbExpenseKind.Font = ThemePalette.FontBody;
            cmbExpenseKind.Items.AddRange(new object[] { "Need", "Want" });
            cardForm.Controls.Add(cmbExpenseKind);

            y += 58;
            AddFieldLabel(cardForm, lblNotes, "Notes (Optional)", y);
            txtNotes.Location = new Point(16, y + 20);
            txtNotes.Size = new Size(304, 54);
            txtNotes.Multiline = true;
            txtNotes.Font = ThemePalette.FontBody;
            cardForm.Controls.Add(txtNotes);

            y += 88;
            btnSave.Text = "Save Record";
            btnSave.Location = new Point(16, y);
            btnSave.Size = new Size(148, 38);
            ThemePalette.StyleButton(btnSave, ThemePalette.PrimaryTeal, Color.White);
            cardForm.Controls.Add(btnSave);

            btnClear.Text = "Clear / New";
            btnClear.Location = new Point(172, y);
            btnClear.Size = new Size(148, 38);
            ThemePalette.StyleSecondaryButton(btnClear);
            cardForm.Controls.Add(btnClear);

            btnDelete.Text = "Delete Selected";
            btnDelete.Location = new Point(16, y + 46);
            btnDelete.Size = new Size(304, 34);
            btnDelete.Enabled = false;
            ThemePalette.StyleButton(btnDelete, ThemePalette.DangerRose, Color.White);
            cardForm.Controls.Add(btnDelete);

            cardForm.Controls.Add(lblFormHeader);

            // Right Grid Card
            this.cardGrid.Location = new Point(356, 66);
            this.cardGrid.Size = new Size(604, 610);
            this.cardGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            this.lblGridHeader.Text = "All Recorded Transactions (Click a row to edit or delete)";
            this.lblGridHeader.Font = ThemePalette.FontSectionTitle;
            this.lblGridHeader.ForeColor = ThemePalette.TextPrimary;
            this.lblGridHeader.Location = new Point(16, 14);
            this.lblGridHeader.Size = new Size(540, 24);

            this.btnSelectAll.Text = "Select All";
            this.btnSelectAll.Location = new Point(16, 44);
            this.btnSelectAll.Size = new Size(120, 32);
            ThemePalette.StyleSecondaryButton(this.btnSelectAll);
            this.cardGrid.Controls.Add(this.btnSelectAll);

            this.btnDeselectAll.Text = "Select";
            this.btnDeselectAll.Location = new Point(144, 44);
            this.btnDeselectAll.Size = new Size(120, 32);
            ThemePalette.StyleSecondaryButton(this.btnDeselectAll);
            this.cardGrid.Controls.Add(this.btnDeselectAll);

            this.btnBulkDelete.Text = "Delete";
            this.btnBulkDelete.Location = new Point(276, 44);
            this.btnBulkDelete.Size = new Size(140, 32);
            ThemePalette.StyleButton(this.btnBulkDelete, ThemePalette.DangerRose, Color.White);
            this.cardGrid.Controls.Add(this.btnBulkDelete);

            this.dgvTransactions.Location = new Point(16, 84);
            this.dgvTransactions.Size = new Size(570, 508);
            this.dgvTransactions.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ThemePalette.StyleDataGridView(this.dgvTransactions);

            this.cardGrid.Controls.Add(this.lblGridHeader);
            this.cardGrid.Controls.Add(this.dgvTransactions);

            this.BackColor = ThemePalette.AppBackground;
            this.ClientSize = new Size(960, 690);
            this.Controls.Add(this.cardGrid);
            this.Controls.Add(this.cardForm);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblTitle);

            this.ResumeLayout(false);
        }

        private void AddFieldLabel(Control parent, Label lbl, string text, int y)
        {
            lbl.Text = text;
            lbl.Font = ThemePalette.FontBodyBold;
            lbl.ForeColor = ThemePalette.TextSecondary;
            lbl.Location = new Point(16, y);
            lbl.Size = new Size(300, 18);
            parent.Controls.Add(lbl);
        }
    }
}
