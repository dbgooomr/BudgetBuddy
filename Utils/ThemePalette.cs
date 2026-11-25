using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using BudgetBuddy.Controls;

namespace BudgetBuddy.Utils
{
    public static class ThemePalette
    {
        public static readonly Color SidebarBg = Color.FromArgb(15, 42, 46);         
        public static readonly Color SidebarTop = Color.FromArgb(17, 58, 63);        
        public static readonly Color SidebarBottom = Color.FromArgb(8, 26, 32);      
        public static readonly Color SidebarActive = Color.FromArgb(15, 118, 110);   
        public static readonly Color SidebarHover = Color.FromArgb(24, 62, 68);
        public static readonly Color AppBackground = Color.FromArgb(207, 222, 228);  
        public static readonly Color CardBackground = Color.FromArgb(228, 240, 242);   
        public static readonly Color InputBackground = Color.FromArgb(243, 249, 250); 
        public static readonly Color SurfaceAlt = Color.FromArgb(214, 231, 234);      
        public static readonly Color SurfaceHeader = Color.FromArgb(205, 224, 228);   
        public static readonly Color SurfaceTrack = Color.FromArgb(193, 213, 219);    
        public static readonly Color CardBorder = Color.FromArgb(180, 202, 209);

        public static readonly Color HeroStart = Color.FromArgb(13, 148, 136);
        public static readonly Color HeroEnd = Color.FromArgb(67, 56, 202);
        public static readonly Color MintText = Color.FromArgb(167, 243, 208);

        public static readonly Color PrimaryTeal = Color.FromArgb(13, 148, 136);
        public static readonly Color PrimaryTealDark = Color.FromArgb(15, 118, 110);
        public static readonly Color AccentIndigo = Color.FromArgb(79, 70, 229);     
        public static readonly Color SafeGreen = Color.FromArgb(5, 150, 105);
        public static readonly Color SafeGreenBg = Color.FromArgb(209, 250, 229);
        public static readonly Color WarningAmber = Color.FromArgb(217, 119, 6);
        public static readonly Color WarningAmberBg = Color.FromArgb(254, 243, 199);
        public static readonly Color DangerRose = Color.FromArgb(225, 29, 72);
        public static readonly Color DangerRoseBg = Color.FromArgb(255, 228, 230);

        public static readonly Color TextPrimary = Color.FromArgb(15, 23, 42);
        public static readonly Color TextSecondary = Color.FromArgb(71, 85, 105);
        public static readonly Color TextMuted = Color.FromArgb(148, 163, 184);

        public static readonly Color[] ChartPalette =
        {
            Color.FromArgb(13, 148, 136),   
            Color.FromArgb(79, 70, 229),    
            Color.FromArgb(245, 158, 11),   
            Color.FromArgb(244, 63, 94),    
            Color.FromArgb(14, 165, 233),   
            Color.FromArgb(139, 92, 246)    
        };

        public static readonly Font FontBrand = new Font("Segoe UI Semibold", 15f, FontStyle.Regular);
        public static readonly Font FontPageTitle = new Font("Segoe UI Semibold", 18f, FontStyle.Regular);
        public static readonly Font FontSectionTitle = new Font("Segoe UI Semibold", 11.5f, FontStyle.Regular);
        public static readonly Font FontKpiValue = new Font("Segoe UI Semibold", 19f, FontStyle.Regular);
        public static readonly Font FontBody = new Font("Segoe UI", 9.75f, FontStyle.Regular);
        public static readonly Font FontBodyBold = new Font("Segoe UI Semibold", 9.75f, FontStyle.Regular);
        public static readonly Font FontCaption = new Font("Segoe UI", 8.5f, FontStyle.Regular);
        public static readonly Font FontCaptionBold = new Font("Segoe UI Semibold", 8.5f, FontStyle.Regular);

        public static Color Blend(Color a, Color b, float t)
        {
            t = Math.Max(0f, Math.Min(1f, t));
            return Color.FromArgb(
                (int)Math.Round(a.R + (b.R - a.R) * t),
                (int)Math.Round(a.G + (b.G - a.G) * t),
                (int)Math.Round(a.B + (b.B - a.B) * t));
        }

        public static Color Lighten(Color c, float amount) { return Blend(c, Color.White, amount); }
        public static Color Darken(Color c, float amount) { return Blend(c, Color.Black, amount); }

        public static GraphicsPath RoundedRect(Rectangle rect, int radius)
        {
            int d = Math.Max(1, Math.Min(radius * 2, Math.Min(rect.Width, rect.Height)));
            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        public static Color Backdrop(Control c)
        {
            Control current = c;
            int y = c.Top + c.Height / 2;
            Control p = c.Parent;
            while (p != null)
            {
                RoundedPanel card = p as RoundedPanel;
                if (card != null)
                    return card.GradientEnd.IsEmpty ? card.FillColor : Blend(card.FillColor, card.GradientEnd, 0.5f);

                GradientPanel gradient = p as GradientPanel;
                if (gradient != null)
                    return Blend(gradient.ColorTop, gradient.ColorBottom, (float)y / Math.Max(1, gradient.Height));

                if (p.BackColor.A == 255)
                    return p.BackColor;

                current = p;
                y += current.Top;
                p = p.Parent;
            }
            return AppBackground;
        }

        private class ButtonState
        {
            public bool Hover;
            public bool Down;
            public bool Secondary;
        }

        private static readonly ConditionalWeakTable<Button, ButtonState> ButtonStates =
            new ConditionalWeakTable<Button, ButtonState>();

        public static void StyleButton(Button btn, Color bgColor, Color fgColor)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = bgColor;
            btn.ForeColor = fgColor;
            btn.Font = FontBodyBold;
            btn.Cursor = Cursors.Hand;
            MakeModernButton(btn, false);
        }

        public static void StyleSecondaryButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderColor = CardBorder;
            btn.FlatAppearance.BorderSize = 1;
            btn.BackColor = SurfaceAlt;
            btn.ForeColor = TextPrimary;
            btn.Font = FontBodyBold;
            btn.Cursor = Cursors.Hand;
            MakeModernButton(btn, true);
        }

        private static void MakeModernButton(Button btn, bool secondary)
        {
            ButtonState existing;
            if (ButtonStates.TryGetValue(btn, out existing))
            {
                existing.Secondary = secondary;
                btn.Invalidate();
                return;
            }

            ButtonState state = new ButtonState();
            state.Secondary = secondary;
            ButtonStates.Add(btn, state);

            btn.MouseEnter += (s, e) => { state.Hover = true; btn.Invalidate(); };
            btn.MouseLeave += (s, e) => { state.Hover = false; state.Down = false; btn.Invalidate(); };
            btn.MouseDown += (s, e) => { state.Down = true; btn.Invalidate(); };
            btn.MouseUp += (s, e) => { state.Down = false; btn.Invalidate(); };
            btn.EnabledChanged += (s, e) => btn.Invalidate();
            btn.Paint += (s, e) => PaintModernButton(btn, state, e.Graphics);
        }

        private static void PaintModernButton(Button btn, ButtonState state, Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Backdrop(btn));

            Rectangle r = new Rectangle(0, 0, btn.Width - 1, btn.Height - 1);
            if (r.Width < 4 || r.Height < 4) return;

            Color fill = btn.BackColor;
            Color fg = btn.ForeColor;
            Color border = Color.Empty;

            if (state.Secondary)
                border = CardBorder;

            if (!btn.Enabled)
            {
                fill = state.Secondary ? SurfaceAlt : Color.FromArgb(203, 213, 225);
                fg = TextMuted;
            }
            else if (state.Down)
            {
                fill = state.Secondary ? Color.FromArgb(204, 251, 241) : Darken(fill, 0.16f);
                if (state.Secondary) border = PrimaryTeal;
            }
            else if (state.Hover)
            {
                fill = state.Secondary ? Color.FromArgb(200, 230, 226) : Darken(fill, 0.08f);
                if (state.Secondary) border = PrimaryTeal;
            }

            int radius = Math.Min(10, r.Height / 2);
            using (GraphicsPath path = RoundedRect(r, radius))
            {
                if (!state.Secondary && btn.Enabled && !state.Down)
                {
                    using (GraphicsPath shadow = RoundedRect(new Rectangle(r.X + 1, r.Y + 2, r.Width - 2, r.Height - 2), radius))
                    using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(28, fill)))
                        g.FillPath(shadowBrush, shadow);
                }

                using (SolidBrush brush = new SolidBrush(fill))
                    g.FillPath(brush, path);

                if (!border.IsEmpty)
                    using (Pen pen = new Pen(border, 1f))
                        g.DrawPath(pen, path);
            }

            TextRenderer.DrawText(g, btn.Text, btn.Font, new Rectangle(4, 0, btn.Width - 8, btn.Height - 1), fg,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter |
                TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix);
        }

        public static void ConfigureGridBinding(DataGridView dgv)
        {
            if (dgv.BindingContext == null)
                dgv.BindingContext = new BindingContext();

            EnableGridDoubleBuffering(dgv);
            dgv.CellFormatting -= Grid_CellFormatting;
            dgv.CellFormatting += Grid_CellFormatting;
            dgv.DataBindingComplete -= Grid_DataBindingComplete;
            dgv.DataBindingComplete += Grid_DataBindingComplete;
        }

        private static void Grid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv == null) return;

            foreach (DataGridViewColumn col in dgv.Columns)
            {
                if (!string.Equals(col.HeaderText, col.Name, StringComparison.Ordinal)) continue;

                string pretty = System.Text.RegularExpressions.Regex.Replace(col.HeaderText, "(?<=[a-z])(?=[A-Z])", " ");
                pretty = pretty.Replace("Category Or Source", "Category / Source").Replace("Need Or Want", "Need / Want").Replace("Need Want", "Need / Want");
                if (pretty == "Id") pretty = "ID";
                col.HeaderText = pretty;
            }
        }

        private static void EnableGridDoubleBuffering(DataGridView dgv)
        {
            try
            {
                PropertyInfo prop = typeof(DataGridView).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                if (prop != null) prop.SetValue(dgv, true, null);
            }
            catch {  }
        }

        private static void Grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv == null || e.RowIndex < 0 || e.ColumnIndex < 0 || e.ColumnIndex >= dgv.Columns.Count) return;

            DataGridViewColumn col = dgv.Columns[e.ColumnIndex];
            bool isAmount = string.Equals(col.Name, "Amount", StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(col.DataPropertyName, "Amount", StringComparison.OrdinalIgnoreCase);
            if (!isAmount) return;

            string text = e.Value as string;
            if (string.IsNullOrEmpty(text)) return;

            bool? income = null;
            if (text.StartsWith("+")) income = true;
            else if (text.StartsWith("-")) income = false;
            else if (dgv.Columns.Contains("Type"))
            {
                string type = dgv.Rows[e.RowIndex].Cells["Type"].Value as string;
                if (type != null && type.StartsWith("Income", StringComparison.OrdinalIgnoreCase)) income = true;
                else if (type != null && type.StartsWith("Expense", StringComparison.OrdinalIgnoreCase)) income = false;
            }

            if (income.HasValue)
            {
                e.CellStyle.ForeColor = income.Value ? SafeGreen : DangerRose;
                e.CellStyle.Font = FontBodyBold;
            }
        }

        public static void ConfigureColumn(DataGridView dgv, string columnName, int? width = null, bool? visible = null)
        {
            if (dgv == null || dgv.Columns == null || !dgv.Columns.Contains(columnName))
                return;

            var col = dgv.Columns[columnName];
            if (col == null)
                return;

            if (visible.HasValue)
                col.Visible = visible.Value;

            if (width.HasValue)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                col.Width = width.Value;
            }
        }

        public static void StyleInputs(Control root)
        {
            foreach (Control c in root.Controls)
            {
                if (c is TextBox || c is ComboBox || c is NumericUpDown)
                {
                    c.BackColor = InputBackground;
                    c.ForeColor = TextPrimary;
                }

                StyleInputs(c);
            }
        }

        private const int HeaderBottomGap = 8;

        public static int SectionShift { get { return System.Math.Max(0, FontSectionTitle.Height + 6 - 24) + HeaderBottomGap; } }

        public static int PageShift { get { return System.Math.Max(0, FontPageTitle.Height + 6 - 32) + HeaderBottomGap; } }

        public static void ApplySpacing(Control root)
        {
            FixMnemonics(root);
            SpaceHeaders(root);
            SpaceFieldLabels(root);
            StyleInputs(root);
        }

        public static void FixMnemonics(Control root)
        {
            if (root is Label lbl) lbl.UseMnemonic = false;
            else if (root is ButtonBase btn) btn.UseMnemonic = false;
            foreach (Control child in root.Controls) FixMnemonics(child);
        }

        private static bool IsHeader(Control c)
        {
            return c is Label l && (ReferenceEquals(l.Font, FontSectionTitle) || ReferenceEquals(l.Font, FontPageTitle));
        }

        private static void SpaceHeaders(Control container)
        {
            var kids = new System.Collections.Generic.List<Control>();
            foreach (Control c in container.Controls) kids.Add(c);

            var headers = new System.Collections.Generic.List<Control>();
            foreach (Control c in kids) if (IsHeader(c)) headers.Add(c);
            headers.Sort((a, b) => a.Top.CompareTo(b.Top));

            foreach (Control header in headers)
            {
                var lbl = (Label)header;
                int oldH = lbl.Height;
                int newH = lbl.Font.Height + 6;
                lbl.AutoSize = false;
                lbl.TextAlign = ContentAlignment.TopLeft;
                lbl.Height = System.Math.Max(oldH, newH);
                int delta = System.Math.Max(0, newH - oldH) + HeaderBottomGap;
                int boundary = header.Top + oldH - 2;

                var saved = new System.Collections.Generic.Dictionary<Control, AnchorStyles>();
                foreach (Control c in container.Controls) { saved[c] = c.Anchor; if (c.Dock == DockStyle.None) c.Anchor = AnchorStyles.Top | AnchorStyles.Left; }

                foreach (Control sib in kids)
                    if (sib != header && sib.Dock == DockStyle.None && sib.Top >= boundary) sib.Top += delta;

                if (!(container is Form) && container.Dock == DockStyle.None) GrowControl(container, delta);

                foreach (var kv in saved) if (kv.Key.Dock == DockStyle.None) kv.Key.Anchor = kv.Value;
            }

            foreach (Control c in kids) SpaceHeaders(c);
        }

        private static void GrowControl(Control c, int delta)
        {
            int oldBottom = c.Bottom;
            var parent = c.Parent;
            c.Height += delta;
            if (parent == null) return;

            var saved = new System.Collections.Generic.Dictionary<Control, AnchorStyles>();
            foreach (Control s in parent.Controls) { saved[s] = s.Anchor; if (s.Dock == DockStyle.None) s.Anchor = AnchorStyles.Top | AnchorStyles.Left; }

            foreach (Control sib in parent.Controls)
            {
                if (sib == c || sib.Dock != DockStyle.None) continue;
                bool overlapsHorizontally = sib.Left < c.Right && sib.Right > c.Left;
                if (overlapsHorizontally && sib.Top >= oldBottom - 2) sib.Top += delta;
            }

            if (!(parent is Form) && parent.Dock == DockStyle.None) GrowControl(parent, delta);

            foreach (var kv in saved) if (kv.Key.Dock == DockStyle.None) kv.Key.Anchor = kv.Value;
        }

        private static void SpaceFieldLabels(Control container)
        {
            foreach (Control c in container.Controls)
            {
                if (c is Label lbl && !IsHeader(lbl) && lbl.Dock == DockStyle.None)
                {
                    int need = lbl.Font.Height + 2;
                    bool singleLine = lbl.Height < lbl.Font.Height * 2 - 4;
                    if (singleLine && lbl.Height < need && !lbl.AutoSize)
                    {
                        int bottom = lbl.Bottom;
                        lbl.Height = need;
                        lbl.Top = bottom - need;   
                    }
                }
                SpaceFieldLabels(c);
            }
        }
    }
}
