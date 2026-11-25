using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using BudgetBuddy.Utils;

namespace BudgetBuddy.Controls
{
    [DesignerCategory("Code")]
    public class StyledButton : Button
    {
        private bool _hover;
        private bool _down;
        private bool _secondaryStyle;

        [Browsable(true)]
        [Category("Appearance")]
        [DefaultValue(false)]
        public bool SecondaryStyle
        {
            get => _secondaryStyle;
            set { _secondaryStyle = value; Invalidate(); }
        }

        [Browsable(true)]
        [Category("Appearance")]
        [DefaultValue(10)]
        public int CornerRadius { get; set; } = 10;

        public StyledButton()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            UseMnemonic = false;
        }

        protected override void OnMouseEnter(EventArgs e) { _hover = true; Invalidate(); base.OnMouseEnter(e); }
        protected override void OnMouseLeave(EventArgs e) { _hover = false; _down = false; Invalidate(); base.OnMouseLeave(e); }
        protected override void OnMouseDown(MouseEventArgs e) { _down = true; Invalidate(); base.OnMouseDown(e); }
        protected override void OnMouseUp(MouseEventArgs e) { _down = false; Invalidate(); base.OnMouseUp(e); }
        protected override void OnEnabledChanged(EventArgs e) { Invalidate(); base.OnEnabledChanged(e); }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(ThemePalette.Backdrop(this));
            Rectangle r = new Rectangle(0, 0, Width - 1, Height - 1);
            if (r.Width < 4 || r.Height < 4) return;

            Color fill = BackColor;
            Color fg = ForeColor;
            if (!Enabled) { fill = SecondaryStyle ? ThemePalette.SurfaceAlt : Color.FromArgb(203, 213, 225); fg = ThemePalette.TextMuted; }
            else if (_down) fill = ThemePalette.Darken(fill, 0.12f);
            else if (_hover) fill = ThemePalette.Lighten(fill, SecondaryStyle ? 0.08f : 0.10f);

            using (GraphicsPath path = ThemePalette.RoundedRect(r, Math.Max(1, CornerRadius)))
            using (SolidBrush brush = new SolidBrush(fill))
            {
                g.FillPath(brush, path);
                if (SecondaryStyle && FlatAppearance.BorderSize > 0)
                    using (Pen pen = new Pen(FlatAppearance.BorderColor, FlatAppearance.BorderSize)) g.DrawPath(pen, path);
            }
            TextRenderer.DrawText(g, Text, Font, r, fg, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix);
        }
    }
}
