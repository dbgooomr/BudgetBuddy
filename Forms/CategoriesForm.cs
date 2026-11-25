using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BudgetBuddy.Data;
using BudgetBuddy.Models;
using BudgetBuddy.Utils;

namespace BudgetBuddy.Forms
{
    public partial class CategoriesForm : Form
    {
        private readonly DatabaseHelper _db;
        private int? _selectedCategoryId = null;
        private bool _selectionMode = false;

        public CategoriesForm()
        {
            InitializeComponent();
        }

        public CategoriesForm(DatabaseHelper db)
        {
            _db = db;
            InitializeComponent();

            btnAddCategory.Click += BtnAddCategory_Click;
            btnRenameCategory.Click += BtnRenameCategory_Click;
            btnDeleteCategory.Click += BtnDeleteCategory_Click;
            btnMoveRecords.Click += BtnMoveRecords_Click;
            dgvCategories.CellClick += DgvCategories_CellClick;
            dgvCategories.CurrentCellDirtyStateChanged += (s, e) => { if (dgvCategories.IsCurrentCellDirty) dgvCategories.CommitEdit(DataGridViewDataErrorContexts.Commit); };

            btnSelectCategories.Click += (s,e) => { _selectionMode = !_selectionMode; RefreshCategories(); };
            btnSelectAllCategories.Click += (s,e) => { if (!_selectionMode) { _selectionMode=true; RefreshCategories(); } ToggleAllCategoryChecks(); };
            btnDeleteCategories.Click += BtnBulkDeleteCategories_Click;
            this.Load += (s, e) => RefreshCategories();
        }

        private void RefreshCategories()
        {
            var categories = _db.GetAllCategories();
            var rows = categories.Select(c => new
            {
                Id = c.CategoryId,
                CategoryName = c.Name,
                Type = c.IsPreset ? "Preset Student Category" : "Custom Category",
                ExpenseRecords = _db.CountTransactionsByCategory(c.CategoryId)
            }).ToList();

            dgvCategories.DataSource = rows;

            ThemePalette.ConfigureColumn(dgvCategories, "Id", width: 58);
            if (dgvCategories.Columns.Contains("CategoryName"))
            {
                dgvCategories.Columns["CategoryName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvCategories.Columns["CategoryName"].FillWeight = 145;
                dgvCategories.Columns["CategoryName"].MinimumWidth = 150;
            }
            if (dgvCategories.Columns.Contains("Type"))
            {
                dgvCategories.Columns["Type"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvCategories.Columns["Type"].FillWeight = 150;
                dgvCategories.Columns["Type"].MinimumWidth = 160;
            }
            if (dgvCategories.Columns.Contains("ExpenseRecords"))
            {
                dgvCategories.Columns["ExpenseRecords"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvCategories.Columns["ExpenseRecords"].FillWeight = 90;
                dgvCategories.Columns["ExpenseRecords"].MinimumWidth = 115;
            }
            if (dgvCategories.Columns.Contains("SelectRow")) dgvCategories.Columns.Remove("SelectRow");
            if (_selectionMode) { var check = new DataGridViewCheckBoxColumn { Name="SelectRow", HeaderText="Select", Width=55, DataPropertyName="", ReadOnly=false }; dgvCategories.Columns.Insert(0, check); dgvCategories.ReadOnly=false; foreach(DataGridViewColumn col in dgvCategories.Columns) if(col.Name!="SelectRow") col.ReadOnly=true; dgvCategories.EditMode=DataGridViewEditMode.EditOnEnter; }
            btnSelectCategories.Text = _selectionMode ? "Cancel Select" : "Select";

            cmbTargetCategory.DataSource = null;
            cmbTargetCategory.DisplayMember = "Name";
            cmbTargetCategory.ValueMember = "CategoryId";
            cmbTargetCategory.DataSource = categories;
        }

        private void DgvCategories_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (_selectionMode && e.ColumnIndex == 0) return;
            _selectedCategoryId = Convert.ToInt32(dgvCategories.Rows[e.RowIndex].Cells["Id"].Value);
            txtCategoryName.Text = dgvCategories.Rows[e.RowIndex].Cells["CategoryName"].Value.ToString();
        }

        private void BtnAddCategory_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtCategoryName.Text.Trim();
                _db.AddCategory(new Category(0, name, false));
                txtCategoryName.Clear();
                _selectedCategoryId = null;
                RefreshCategories();
                MessageBox.Show($"Custom category '{name}' added.", "Category Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Cannot Add Category", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnRenameCategory_Click(object sender, EventArgs e)
        {
            if (!_selectedCategoryId.HasValue)
            {
                MessageBox.Show("Please click a category in the table first.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var cat = _db.GetCategoryById(_selectedCategoryId.Value);
                if (cat == null) return;
                cat.Name = txtCategoryName.Text.Trim();
                _db.UpdateCategory(cat);
                RefreshCategories();
                MessageBox.Show("Category renamed successfully.", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Rename Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnDeleteCategory_Click(object sender, EventArgs e)
        {
            if (!_selectedCategoryId.HasValue)
            {
                MessageBox.Show("Please select a custom category to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var cat = _db.GetCategoryById(_selectedCategoryId.Value);
                if (cat == null) return;

                var confirm = MessageBox.Show(
                    $"Delete custom category '{cat.Name}'?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm != DialogResult.Yes) return;

                _db.DeleteCategory(cat.CategoryId);
                _selectedCategoryId = null;
                txtCategoryName.Clear();
                RefreshCategories();
                MessageBox.Show("Custom category deleted.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Cannot Delete Category", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ToggleAllCategoryChecks()
        {
            bool allChecked = dgvCategories.Rows.Cast<DataGridViewRow>().Where(r => !r.IsNewRow).All(r => Convert.ToBoolean(r.Cells["SelectRow"].Value ?? false));
            foreach (DataGridViewRow row in dgvCategories.Rows) if (!row.IsNewRow) row.Cells["SelectRow"].Value = !allChecked;
        }
        private void BtnBulkDeleteCategories_Click(object sender, EventArgs e)
        {
            if (!_selectionMode) { MessageBox.Show("Click Select first."); return; }
            var ids = dgvCategories.Rows.Cast<DataGridViewRow>().Where(r => !r.IsNewRow && Convert.ToBoolean(r.Cells["SelectRow"].Value ?? false) && r.Cells["Type"].Value.ToString()=="Custom Category").Select(r=>Convert.ToInt32(r.Cells["Id"].Value)).ToList();
            if(ids.Count==0) { MessageBox.Show("Select one or more custom categories."); return; }
            if(MessageBox.Show($"Delete {ids.Count} selected custom category(ies)? Categories with transaction records will be skipped.","Confirm Delete",MessageBoxButtons.YesNo,MessageBoxIcon.Warning)!=DialogResult.Yes) return;
            int deleted=0, skipped=0; foreach(int id in ids) { try { _db.DeleteCategory(id); deleted++; } catch { skipped++; } }
            _selectedCategoryId=null; txtCategoryName.Clear(); RefreshCategories(); MessageBox.Show($"Deleted: {deleted}\nSkipped (preset or linked records): {skipped}","Bulk Delete Results");
        }

        private void BtnMoveRecords_Click(object sender, EventArgs e)
        {
            if (!_selectedCategoryId.HasValue)
            {
                MessageBox.Show("Select the source category from the table first.", "No Source Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!(cmbTargetCategory.SelectedItem is Category target)) return;

            if (target.CategoryId == _selectedCategoryId.Value)
            {
                MessageBox.Show("Please choose a different destination category.", "Same Category", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int count = _db.CountTransactionsByCategory(_selectedCategoryId.Value);
            if (count == 0)
            {
                MessageBox.Show("The selected category has no expense records to move.", "Nothing to Move", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show(
                $"Move {count} expense record(s) into '{target.Name}'?",
                "Confirm Reassignment",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                _db.ReassignTransactionCategory(_selectedCategoryId.Value, target.CategoryId);
                RefreshCategories();
                MessageBox.Show($"Moved {count} record(s) to '{target.Name}'.", "Records Moved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
