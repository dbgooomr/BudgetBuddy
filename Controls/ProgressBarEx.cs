using System.ComponentModel;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using BudgetBuddy.Utils;

namespace BudgetBuddy.Controls
{
    [DesignerCategory("Code")]
    public class ProgressBarEx : Control
    {
        private double _percentage;
        [Browsable(true)]
        [Category("Appearance")]
        [DefaultValue(0d)]
        public double Percentage
        {
            get { return _percentage; }
            set
            {
                _percentage = Math.Max(0, value);
                Invalidate();
            }
        }

        [Browsable(true)]
        [Category("Appearance")]
        public Color BarColor { get; set; } = ThemePalette.PrimaryTeal;
        [Browsable(true)]
        [Category("Appearance")]
        public Color TrackColor { get; set; } = ThemePalette.SurfaceTrack;
        [Browsable(true)]
        [Category("Appearance")]
        [DefaultValue(null)]
        public string CustomLabel { get; set; }

        public ProgressBarEx()
        {
            SetStyle(ControlStyles.ResizeRedraw, true);
            DoubleBuffered = true;
            Height = 22;
            Font = ThemePalette.FontCaptionBold;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(ThemePalette.Backdrop(this));

            Rectangle track = new Rectangle(0, 0, Width - 1, Height - 1);
            if (track.Width < 4 || track.Height < 4) return;
            int radius = track.Height / 2;

            using (GraphicsPath trackPath = ThemePalette.RoundedRect(track, radius))
            using (SolidBrush trackBrush = new SolidBrush(TrackColor))
                g.FillPath(trackBrush, trackPath);

            double clamped = Math.Min(100.0, _percentage);
            int fillWidth = (int)Math.Round((Width - 1) * (clamped / 100.0));
            if (fillWidth > 0)
            {
                fillWidth = Math.Max(fillWidth, track.Height);
                Rectangle fillRect = new Rectangle(0, 0, Math.Min(fillWidth, track.Width), track.Height);
                using (GraphicsPath fillPath = ThemePalette.RoundedRect(fillRect, radius))
                using (LinearGradientBrush fillBrush = new LinearGradientBrush(
                    fillRect, ThemePalette.Lighten(BarColor, 0.22f), BarColor, LinearGradientMode.Horizontal))
                    g.FillPath(fillBrush, fillPath);
            }

            string label = !string.IsNullOrEmpty(CustomLabel) ? CustomLabel : string.Format("{0:F0}%", _percentage);
            TextRenderer.DrawText(
                g,
                label,
                Font,
                track,
                clamped > 52 ? Color.White : ThemePalette.TextPrimary,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPrefix);
        }
    }
}
