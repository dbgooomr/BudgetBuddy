using System;
using System.ComponentModel;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using BudgetBuddy.Utils;

namespace BudgetBuddy.Controls
{
    [DesignerCategory("Code")]
    public class StyledDataGridView : DataGridView
    {
        public StyledDataGridView()
        {
            try { typeof(DataGridView).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic)?.SetValue(this, true, null); } catch { }
        }

        protected override void OnDataBindingComplete(DataGridViewBindingCompleteEventArgs e)
        {
            base.OnDataBindingComplete(e);
            foreach (DataGridViewColumn col in Columns)
            {
                if (!string.Equals(col.HeaderText, col.Name, StringComparison.Ordinal)) continue;
                string pretty = Regex.Replace(col.HeaderText, "(?<=[a-z])(?=[A-Z])", " ");
                pretty = pretty.Replace("Category Or Source", "Category / Source").Replace("Need Or Want", "Need / Want").Replace("Need Want", "Need / Want");
                if (pretty == "Id") pretty = "ID";
                col.HeaderText = pretty;
            }
        }

        protected override void OnCellFormatting(DataGridViewCellFormattingEventArgs e)
        {
            base.OnCellFormatting(e);
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.ColumnIndex >= Columns.Count) return;
            DataGridViewColumn col = Columns[e.ColumnIndex];
            bool isAmount = string.Equals(col.Name, "Amount", StringComparison.OrdinalIgnoreCase) || string.Equals(col.DataPropertyName, "Amount", StringComparison.OrdinalIgnoreCase);
            if (!isAmount) return;
            string text = e.Value as string;
            if (string.IsNullOrEmpty(text)) return;
            bool? income = text.StartsWith("+") ? true : (text.StartsWith("-") ? false : (bool?)null);
            if (!income.HasValue && Columns.Contains("Type"))
            {
                string type = Rows[e.RowIndex].Cells["Type"].Value as string;
                if (type != null && type.StartsWith("Income", StringComparison.OrdinalIgnoreCase)) income = true;
                else if (type != null && type.StartsWith("Expense", StringComparison.OrdinalIgnoreCase)) income = false;
            }
            if (income.HasValue) { e.CellStyle.ForeColor = income.Value ? ThemePalette.SafeGreen : ThemePalette.DangerRose; e.CellStyle.Font = ThemePalette.FontBodyBold; }
        }
    }
}
