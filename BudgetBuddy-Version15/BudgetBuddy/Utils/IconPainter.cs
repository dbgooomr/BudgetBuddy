using System.Drawing;
using System.Drawing.Drawing2D;

namespace BudgetBuddy.Utils
{
    public enum IconKind
    {
        None,
        Dashboard,
        Transactions,
        Budget,
        Goal,
        WhatIf,
        History,
        Category,
        Wallet,
        Calendar,
        Check,
        Chart
    }

    /// <summary>
    /// Draws small crisp vector icons with GDI+ so the UI needs no image files
    /// and icons stay sharp at any Windows display scale.
    /// </summary>
    public static class IconPainter
    {
        public static void Draw(Graphics g, IconKind kind, Rectangle r, Color color, float stroke = 1.9f)
        {
            if (kind == IconKind.None || r.Width < 4 || r.Height < 4) return;

            SmoothingMode oldMode = g.SmoothingMode;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (Pen pen = new Pen(color, stroke))
            using (SolidBrush brush = new SolidBrush(color))
            {
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;
                pen.LineJoin = LineJoin.Round;

                switch (kind)
                {
                    case IconKind.Dashboard:
                        FillRound(g, brush, P(r, 0.08f, 0.08f, 0.38f, 0.38f), 3);
                        FillRound(g, brush, P(r, 0.54f, 0.08f, 0.38f, 0.26f), 3);
                        FillRound(g, brush, P(r, 0.08f, 0.54f, 0.38f, 0.38f), 3);
                        FillRound(g, brush, P(r, 0.54f, 0.42f, 0.38f, 0.50f), 3);
                        break;

                    case IconKind.Transactions:
                        Line(g, pen, r, 0.30f, 0.86f, 0.30f, 0.14f);
                        Line(g, pen, r, 0.30f, 0.14f, 0.14f, 0.32f);
                        Line(g, pen, r, 0.30f, 0.14f, 0.46f, 0.32f);
                        Line(g, pen, r, 0.70f, 0.14f, 0.70f, 0.86f);
                        Line(g, pen, r, 0.70f, 0.86f, 0.54f, 0.68f);
                        Line(g, pen, r, 0.70f, 0.86f, 0.86f, 0.68f);
                        break;

                    case IconKind.Budget:
                        g.DrawEllipse(pen, P(r, 0.10f, 0.10f, 0.80f, 0.80f));
                        RectangleF pie = P(r, 0.10f, 0.10f, 0.80f, 0.80f);
                        g.FillPie(brush, pie.X, pie.Y, pie.Width, pie.Height, -90f, 115f);
                        break;

                    case IconKind.Goal:
                        g.DrawEllipse(pen, P(r, 0.08f, 0.08f, 0.84f, 0.84f));
                        g.DrawEllipse(pen, P(r, 0.30f, 0.30f, 0.40f, 0.40f));
                        g.FillEllipse(brush, P(r, 0.43f, 0.43f, 0.14f, 0.14f));
                        break;

                    case IconKind.WhatIf:
                        PointF[] bolt =
                        {
                            Pt(r, 0.60f, 0.04f), Pt(r, 0.20f, 0.56f), Pt(r, 0.47f, 0.56f),
                            Pt(r, 0.38f, 0.96f), Pt(r, 0.82f, 0.38f), Pt(r, 0.55f, 0.38f)
                        };
                        g.FillPolygon(brush, bolt);
                        break;

                    case IconKind.History:
                        g.DrawEllipse(pen, P(r, 0.08f, 0.08f, 0.84f, 0.84f));
                        Line(g, pen, r, 0.50f, 0.26f, 0.50f, 0.52f);
                        Line(g, pen, r, 0.50f, 0.52f, 0.68f, 0.64f);
                        break;

                    case IconKind.Category:
                        g.FillEllipse(brush, P(r, 0.08f, 0.17f, 0.14f, 0.14f));
                        g.FillEllipse(brush, P(r, 0.08f, 0.43f, 0.14f, 0.14f));
                        g.FillEllipse(brush, P(r, 0.08f, 0.69f, 0.14f, 0.14f));
                        Line(g, pen, r, 0.38f, 0.24f, 0.92f, 0.24f);
                        Line(g, pen, r, 0.38f, 0.50f, 0.92f, 0.50f);
                        Line(g, pen, r, 0.38f, 0.76f, 0.92f, 0.76f);
                        break;

                    case IconKind.Wallet:
                        DrawRound(g, pen, P(r, 0.06f, 0.20f, 0.88f, 0.66f), 4);
                        Line(g, pen, r, 0.06f, 0.38f, 0.94f, 0.38f);
                        g.FillEllipse(brush, P(r, 0.68f, 0.56f, 0.12f, 0.12f));
                        break;

                    case IconKind.Calendar:
                        DrawRound(g, pen, P(r, 0.08f, 0.17f, 0.84f, 0.75f), 4);
                        Line(g, pen, r, 0.08f, 0.40f, 0.92f, 0.40f);
                        Line(g, pen, r, 0.30f, 0.06f, 0.30f, 0.26f);
                        Line(g, pen, r, 0.70f, 0.06f, 0.70f, 0.26f);
                        break;

                    case IconKind.Check:
                        g.DrawEllipse(pen, P(r, 0.08f, 0.08f, 0.84f, 0.84f));
                        Line(g, pen, r, 0.30f, 0.52f, 0.45f, 0.67f);
                        Line(g, pen, r, 0.45f, 0.67f, 0.72f, 0.36f);
                        break;

                    case IconKind.Chart:
                        FillRound(g, brush, P(r, 0.12f, 0.52f, 0.20f, 0.40f), 2);
                        FillRound(g, brush, P(r, 0.40f, 0.30f, 0.20f, 0.62f), 2);
                        FillRound(g, brush, P(r, 0.68f, 0.10f, 0.20f, 0.82f), 2);
                        break;
                }
            }

            g.SmoothingMode = oldMode;
        }

        private static RectangleF P(Rectangle r, float x, float y, float w, float h)
        {
            return new RectangleF(r.X + x * r.Width, r.Y + y * r.Height, w * r.Width, h * r.Height);
        }

        private static PointF Pt(Rectangle r, float x, float y)
        {
            return new PointF(r.X + x * r.Width, r.Y + y * r.Height);
        }

        private static void Line(Graphics g, Pen pen, Rectangle r, float x1, float y1, float x2, float y2)
        {
            g.DrawLine(pen, Pt(r, x1, y1), Pt(r, x2, y2));
        }

        private static void FillRound(Graphics g, Brush brush, RectangleF rect, float radius)
        {
            using (GraphicsPath path = RoundPath(rect, radius))
                g.FillPath(brush, path);
        }

        private static void DrawRound(Graphics g, Pen pen, RectangleF rect, float radius)
        {
            using (GraphicsPath path = RoundPath(rect, radius))
                g.DrawPath(pen, path);
        }

        private static GraphicsPath RoundPath(RectangleF rect, float radius)
        {
            float d = radius * 2f;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
