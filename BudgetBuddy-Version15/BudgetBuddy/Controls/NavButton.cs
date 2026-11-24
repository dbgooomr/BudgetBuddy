using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using BudgetBuddy.Utils;

namespace BudgetBuddy.Controls
{
    /// <summary>
    /// Sidebar navigation item: vector icon + label, rounded highlight on hover,
    /// gradient pill with a mint indicator bar when active.
    /// </summary>
    public class NavButton : Button
    {
        private bool _active;
        private bool _hover;

        public IconKind Icon { get; set; } = IconKind.None;

        public bool IsActive
        {
            get { return _active; }
            set { _active = value; Invalidate(); }
        }

        public NavButton()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            BackColor = ThemePalette.SidebarBg;
            Cursor = Cursors.Hand;
            TabStop = false;
        }

        protected override void OnMouseEnter(EventArgs e) { _hover = true; Invalidate(); base.OnMouseEnter(e); }
        protected override void OnMouseLeave(EventArgs e) { _hover = false; Invalidate(); base.OnMouseLeave(e); }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            // Everything is painted in OnPaint (opaque), so skip the default background pass.
        }

        private void PaintSidebarBackground(Graphics g)
        {
            Control p = Parent;
            while (p != null && !(p is GradientPanel)) p = p.Parent;

            if (p is GradientPanel gp && gp.Height > 2)
            {
                int offsetY = gp.PointToClient(PointToScreen(Point.Empty)).Y;
                Rectangle full = new Rectangle(0, -offsetY, Math.Max(Width, 1), gp.Height);
                using (LinearGradientBrush lg = new LinearGradientBrush(full, gp.ColorTop, gp.ColorBottom, LinearGradientMode.Vertical))
                    g.FillRectangle(lg, ClientRectangle);
            }
            else
            {
                using (SolidBrush b = new SolidBrush(ThemePalette.SidebarBg))
                    g.FillRectangle(b, ClientRectangle);
            }
        }

        protected override void OnMove(EventArgs e) { base.OnMove(e); Invalidate(); }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Repaint the sidebar gradient slice behind this button every time. Without this the
            // semi-transparent hover fill and the text pile up on each repaint (ghosting).
            PaintSidebarBackground(g);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle pill = new Rectangle(8, 2, Width - 16, Height - 5);
            if (pill.Width < 8 || pill.Height < 8) return;

            if (_active)
            {
                using (GraphicsPath path = ThemePalette.RoundedRect(pill, 11))
                using (LinearGradientBrush lg = new LinearGradientBrush(pill, ThemePalette.HeroStart, Color.FromArgb(15, 118, 110), LinearGradientMode.Horizontal))
                    g.FillPath(lg, path);

                using (GraphicsPath bar = ThemePalette.RoundedRect(new Rectangle(0, (Height - 22) / 2, 5, 22), 2))
                using (SolidBrush mint = new SolidBrush(ThemePalette.MintText))
                    g.FillPath(mint, bar);
            }
            else if (_hover)
            {
                using (GraphicsPath path = ThemePalette.RoundedRect(pill, 11))
                using (SolidBrush hover = new SolidBrush(Color.FromArgb(34, 255, 255, 255)))
                    g.FillPath(hover, path);
            }

            Color iconColor = _active ? Color.White : (_hover ? Color.FromArgb(226, 240, 240) : Color.FromArgb(125, 184, 182));
            Color textColor = _active ? Color.White : (_hover ? Color.White : Color.FromArgb(203, 225, 225));

            int iconSize = 20;
            Rectangle iconRect = new Rectangle(pill.X + 14, pill.Y + (pill.Height - iconSize) / 2, iconSize, iconSize);
            IconPainter.Draw(g, Icon, iconRect, iconColor, 1.8f);

            Rectangle textRect = new Rectangle(iconRect.Right + 12, pill.Y, pill.Right - iconRect.Right - 16, pill.Height);
            TextRenderer.DrawText(g, Text.Trim(), Font, textRect, textColor,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix);
        }
    }
}
