using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BudgetBuddy.Controls
{
    [DesignerCategory("Code")]
    public class GradientPanel : Panel
    {
        [Browsable(true)]
        [Category("Appearance")]
        public Color ColorTop { get; set; } = Color.FromArgb(17, 58, 63);
        [Browsable(true)]
        [Category("Appearance")]
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
