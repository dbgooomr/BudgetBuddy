using System.Drawing;
using System.Windows.Forms;
using BudgetBuddy.Controls;
using BudgetBuddy.Utils;

namespace BudgetBuddy.Forms
{
    partial class CategoriesForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private Label lblSubtitle;

        private RoundedPanel cardManage;
        private Label lblManageHeader;
        private Label lblCatName;
        private TextBox txtCategoryName;
        private Button btnAddCategory;
        private Button btnRenameCategory;
        private Button btnDeleteCategory;
        private Label lblRuleHint;

        private RoundedPanel cardReassign;
        private Label lblReassignHeader;
        private Label lblReassignDesc;
        private ComboBox cmbTargetCategory;
        private Button btnMoveRecords;

        private RoundedPanel cardList;
        private Label lblListHeader;
        private DataGridView dgvCategories;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.lblSubtitle = new Label();
            this.cardManage = new RoundedPanel();
            this.lblManageHeader = new Label();
            this.lblCatName = new Label();
            this.txtCategoryName = new TextBox();
            this.btnAddCategory = new Button();
            this.btnRenameCategory = new Button();
            this.btnDeleteCategory = new Button();
            this.lblRuleHint = new Label();

            this.cardReassign = new RoundedPanel();
            this.lblReassignHeader = new Label();
            this.lblReassignDesc = new Label();
            this.cmbTargetCategory = new ComboBox();
            this.btnMoveRecords = new Button();

            this.cardList = new RoundedPanel();
            this.lblListHeader = new Label();
            this.dgvCategories = new DataGridView();

            this.SuspendLayout();

            this.lblTitle.Text = "Student Expense Categories";
            this.lblTitle.Font = ThemePalette.FontPageTitle;
            this.lblTitle.ForeColor = ThemePalette.TextPrimary;
            this.lblTitle.Location = new Point(0, 0);
            this.lblTitle.Size = new Size(500, 32);

            this.lblSubtitle.Text = "Manage preset and custom student categories. Preset categories are protected from deletion.";
            this.lblSubtitle.Font = ThemePalette.FontBody;
            this.lblSubtitle.ForeColor = ThemePalette.TextSecondary;
            this.lblSubtitle.Location = new Point(0, 32);
            this.lblSubtitle.Size = new Size(700, 22);

            // Manage Card
            this.cardManage.Location = new Point(0, 66);
            this.cardManage.Size = new Size(360, 320);

            this.lblManageHeader.Text = "Add or Edit Category";
            this.lblManageHeader.Font = ThemePalette.FontSectionTitle;
            this.lblManageHeader.ForeColor = ThemePalette.TextPrimary;
            this.lblManageHeader.Location = new Point(16, 14);
            this.lblManageHeader.Size = new Size(320, 24);

            this.lblCatName.Text = "Category Name";
            this.lblCatName.Font = ThemePalette.FontBodyBold;
            this.lblCatName.ForeColor = ThemePalette.TextSecondary;
            this.lblCatName.Location = new Point(16, 50);
            this.lblCatName.Size = new Size(300, 18);

            this.txtCategoryName.Location = new Point(16, 72);
            this.txtCategoryName.Size = new Size(324, 28);
            this.txtCategoryName.Font = ThemePalette.FontBody;

            this.btnAddCategory.Text = "Add New Category";
            this.btnAddCategory.Location = new Point(16, 114);
            this.btnAddCategory.Size = new Size(324, 38);
            ThemePalette.StyleButton(this.btnAddCategory, ThemePalette.PrimaryTeal, Color.White);

            this.btnRenameCategory.Text = "Rename Selected";
            this.btnRenameCategory.Location = new Point(16, 160);
            this.btnRenameCategory.Size = new Size(158, 36);
            ThemePalette.StyleSecondaryButton(this.btnRenameCategory);

            this.btnDeleteCategory.Text = "Delete Selected";
            this.btnDeleteCategory.Location = new Point(182, 160);
            this.btnDeleteCategory.Size = new Size(158, 36);
            ThemePalette.StyleButton(this.btnDeleteCategory, ThemePalette.DangerRose, Color.White);

            this.lblRuleHint.Text = "Note: A custom category that already has expense records cannot be deleted until its records are moved to another category below.";
            this.lblRuleHint.Font = ThemePalette.FontCaption;
            this.lblRuleHint.ForeColor = ThemePalette.TextSecondary;
            this.lblRuleHint.Location = new Point(16, 212);
            this.lblRuleHint.Size = new Size(324, 80);

            this.cardManage.Controls.Add(this.lblRuleHint);
            this.cardManage.Controls.Add(this.btnDeleteCategory);
            this.cardManage.Controls.Add(this.btnRenameCategory);
            this.cardManage.Controls.Add(this.btnAddCategory);
            this.cardManage.Controls.Add(this.txtCategoryName);
            this.cardManage.Controls.Add(this.lblCatName);
            this.cardManage.Controls.Add(this.lblManageHeader);

            // Reassign Card
            this.cardReassign.Location = new Point(0, 400);
            this.cardReassign.Size = new Size(360, 220);

            this.lblReassignHeader.Text = "Move Category Records";
            this.lblReassignHeader.Font = ThemePalette.FontSectionTitle;
            this.lblReassignHeader.ForeColor = ThemePalette.TextPrimary;
            this.lblReassignHeader.Location = new Point(16, 14);
            this.lblReassignHeader.Size = new Size(320, 24);

            this.lblReassignDesc.Text = "Select a category in the table on the right, then choose a destination category to move all its transactions:";
            this.lblReassignDesc.Font = ThemePalette.FontCaption;
            this.lblReassignDesc.ForeColor = ThemePalette.TextSecondary;
            this.lblReassignDesc.Location = new Point(16, 44);
            this.lblReassignDesc.Size = new Size(324, 48);

            this.cmbTargetCategory.Location = new Point(16, 100);
            this.cmbTargetCategory.Size = new Size(324, 28);
            this.cmbTargetCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbTargetCategory.Font = ThemePalette.FontBody;

            this.btnMoveRecords.Text = "Move Records to Destination";
            this.btnMoveRecords.Location = new Point(16, 142);
            this.btnMoveRecords.Size = new Size(324, 38);
            ThemePalette.StyleButton(this.btnMoveRecords, ThemePalette.AccentIndigo, Color.White);

            this.cardReassign.Controls.Add(this.btnMoveRecords);
            this.cardReassign.Controls.Add(this.cmbTargetCategory);
            this.cardReassign.Controls.Add(this.lblReassignDesc);
            this.cardReassign.Controls.Add(this.lblReassignHeader);

            // List Card
            this.cardList.Location = new Point(376, 66);
            this.cardList.Size = new Size(584, 554);
            this.cardList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            this.lblListHeader.Text = "Configured Categories & Record Counts";
            this.lblListHeader.Font = ThemePalette.FontSectionTitle;
            this.lblListHeader.ForeColor = ThemePalette.TextPrimary;
            this.lblListHeader.Location = new Point(16, 14);
            this.lblListHeader.Size = new Size(520, 24);

            this.dgvCategories.Location = new Point(16, 46);
            this.dgvCategories.Size = new Size(550, 490);
            this.dgvCategories.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ThemePalette.StyleDataGridView(this.dgvCategories);

            this.cardList.Controls.Add(this.lblListHeader);
            this.cardList.Controls.Add(this.dgvCategories);

            this.BackColor = ThemePalette.AppBackground;
            this.ClientSize = new Size(960, 690);
            this.Controls.Add(this.cardList);
            this.Controls.Add(this.cardReassign);
            this.Controls.Add(this.cardManage);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblTitle);

            this.ResumeLayout(false);
        }
    }
}
