using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BudgetBuddy.Controls
{
    /// <summary>Panel with a vertical two-color gradient background (used for the sidebar).</summary>
    public class GradientPanel : Panel
    {
        public Color ColorTop { get; set; } = Color.FromArgb(17, 58, 63);
        public Color ColorBottom { get; set; } = Color.FromArgb(8, 26, 32);

        public GradientPanel()
        {
            SetStyle(ControlStyles.ResizeRedraw, true);
            DoubleBuffered = true;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (Width < 2 || Height < 2)
            {
                base.OnPaintBackground(e);
                return;
            }

            using (LinearGradientBrush brush = new LinearGradientBrush(ClientRectangle, ColorTop, ColorBottom, LinearGradientMode.Vertical))
                e.Graphics.FillRectangle(brush, ClientRectangle);
        }
    }
}
