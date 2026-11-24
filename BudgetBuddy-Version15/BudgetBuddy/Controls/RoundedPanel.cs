using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using BudgetBuddy.Utils;

namespace BudgetBuddy.Controls
{
    /// <summary>
    /// Double-buffered card with rounded corners, a soft drop shadow and optional extras:
    /// a colored accent strip, a gradient fill, and a round icon chip in the top-right corner.
    /// </summary>
    public class RoundedPanel : Panel
    {
        public int CornerRadius { get; set; } = 14;
        public Color BorderColor { get; set; } = ThemePalette.CardBorder;
        public Color FillColor { get; set; } = ThemePalette.CardBackground;
        public int BorderThickness { get; set; } = 1;

        /// <summary>Soft shadow drawn inside the bottom edge of the control (0 = none).</summary>
        public int ShadowDepth { get; set; } = 4;

        /// <summary>When set, a colored strip is drawn along the top edge of the card.</summary>
        public Color AccentColor { get; set; } = Color.Empty;

        /// <summary>When set, the card fills with a diagonal gradient from FillColor to this color.</summary>
        public Color GradientEnd { get; set; } = Color.Empty;

        /// <summary>Optional vector icon shown in a round chip at the top-right.</summary>
        public IconKind Icon { get; set; } = IconKind.None;
        public Color IconColor { get; set; } = ThemePalette.PrimaryTeal;

        public RoundedPanel()
        {
            SetStyle(ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            BackColor = Color.Transparent;
            Padding = new Padding(16);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int shadow = Math.Max(0, ShadowDepth);
            Rectangle bounds = new Rectangle(0, 0, Width - 1, Height - 1 - shadow);
            if (bounds.Width < 4 || bounds.Height < 4) return;

            // Soft layered shadow
            for (int i = shadow; i >= 1; i--)
            {
                Rectangle sr = new Rectangle(bounds.X + 2, bounds.Y + i, bounds.Width - 4, bounds.Height);
                using (GraphicsPath sp = ThemePalette.RoundedRect(sr, CornerRadius))
                using (SolidBrush sb = new SolidBrush(Color.FromArgb(9, 15, 23, 42)))
                    g.FillPath(sb, sp);
            }

            bool gradient = !GradientEnd.IsEmpty;
            using (GraphicsPath path = ThemePalette.RoundedRect(bounds, CornerRadius))
            {
                if (gradient)
                {
                    using (LinearGradientBrush lg = new LinearGradientBrush(bounds, FillColor, GradientEnd, 25f))
                        g.FillPath(lg, path);
                }
                else
                {
                    using (SolidBrush brush = new SolidBrush(FillColor))
                        g.FillPath(brush, path);
                }

                if (!AccentColor.IsEmpty)
                {
                    GraphicsState state = g.Save();
                    g.SetClip(path, CombineMode.Intersect);
                    using (SolidBrush accent = new SolidBrush(AccentColor))
                        g.FillRectangle(accent, bounds.X, bounds.Y, bounds.Width + 1, 4);
                    g.Restore(state);
                }

                if (BorderThickness > 0 && !gradient)
                    using (Pen pen = new Pen(BorderColor, BorderThickness))
                        g.DrawPath(pen, path);
            }

            if (Icon != IconKind.None)
            {
                int chip = 34;
                Rectangle chipRect = new Rectangle(bounds.Right - chip - 14, bounds.Y + 14, chip, chip);
                Color chipFill = gradient ? Color.FromArgb(46, 255, 255, 255) : ThemePalette.Blend(IconColor, Color.White, 0.86f);
                Color iconColor = gradient ? Color.White : IconColor;

                using (SolidBrush chipBrush = new SolidBrush(chipFill))
                    g.FillEllipse(chipBrush, chipRect);

                Rectangle iconRect = new Rectangle(chipRect.X + 8, chipRect.Y + 8, chip - 16, chip - 16);
                IconPainter.Draw(g, Icon, iconRect, iconColor, 1.8f);
            }
        }
    }
}
