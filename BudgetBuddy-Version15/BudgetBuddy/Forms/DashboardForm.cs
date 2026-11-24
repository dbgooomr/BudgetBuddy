using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BudgetBuddy.Data;
using BudgetBuddy.Controls;
using BudgetBuddy.Models;
using BudgetBuddy.Services;
using BudgetBuddy.Utils;

namespace BudgetBuddy.Forms
{
    public partial class DashboardForm : Form
    {
        private readonly TransactionManager _txManager;
        private readonly BudgetManager _budgetManager;
        private readonly DatabaseHelper _db;
        private readonly Action _openWhatIfAction;
        private readonly Action _openTransactionsAction;
        private readonly Action _openAllowanceAction;

        private decimal _needsAmount;
        private decimal _wantsAmount;
        private Dictionary<string, decimal> _categoryTotals = new Dictionary<string, decimal>();

        public DashboardForm(
            TransactionManager txManager,
            BudgetManager budgetManager,
            DatabaseHelper db,
            Action openWhatIf,
            Action openTransactions,
            Action openAllowance)
        {
            _txManager = txManager;
            _budgetManager = budgetManager;
            _db = db;
            _openWhatIfAction = openWhatIf;
            _openTransactionsAction = openTransactions;
            _openAllowanceAction = openAllowance;

            InitializeComponent();
            ThemePalette.ApplySpacing(this);
            Resize += (s, e) => ArrangeResponsiveLayout();
            ArrangeResponsiveLayout();

            btnBigWhatIf.Click += (s, e) => _openWhatIfAction?.Invoke();
            btnQuickAddTx.Click += (s, e) => _openTransactionsAction?.Invoke();
            btnQuickNewCycle.Click += (s, e) => _openAllowanceAction?.Invoke();

            pnlNeedsWantsChart.Paint += PnlNeedsWantsChart_Paint;
            pnlCategoryBars.Paint += PnlCategoryBars_Paint;

            this.Load += (s, e) => LoadDashboardData();
        }


        private void ArrangeResponsiveLayout()
        {
            int w = Math.Max(320, ClientSize.Width);
            int gap = 12;
            int cardW = Math.Max(250, (w - gap * 3) / 4);
            int top = 66 + ThemePalette.PageShift;
            int H = ThemePalette.SectionShift;
            int cardH = 116;

            // Keep the KPI row airy on wide screens and flow into two columns on compact windows.
            if (w >= 1040)
            {
                int[] xs = { 0, cardW + gap, (cardW + gap) * 2, (cardW + gap) * 3 };
                RoundedPanel[] cards = { cardBalance, cardDaysLeft, cardDailyLimit, cardMonthSummary };
                for (int i = 0; i < cards.Length; i++)
                {
                    cards[i].SetBounds(xs[i], top, cardW, cardH);
                    ResizeKpiChildren(cards[i], cardW);
                }
                cardCategoryChart.SetBounds(0, top + cardH + 12, (w - gap) * 2 / 3, 238 + H);
                cardNeedsWants.SetBounds(cardCategoryChart.Right + gap, cardCategoryChart.Top, w - cardCategoryChart.Right - gap, 238 + H);
                cardRecentTx.SetBounds(0, cardCategoryChart.Bottom + 12, w, Math.Max(190 + H, ClientSize.Height - cardCategoryChart.Bottom - 22));
            }
            else
            {
                int cols = w >= 620 ? 2 : 1;
                int kW = (w - gap * (cols - 1)) / cols;
                RoundedPanel[] cards = { cardBalance, cardDaysLeft, cardDailyLimit, cardMonthSummary };
                for (int i = 0; i < cards.Length; i++)
                {
                    int row = i / cols;
                    int col = i % cols;
                    cards[i].SetBounds(col * (kW + gap), top + row * (cardH + 10), kW, cardH);
                    ResizeKpiChildren(cards[i], kW);
                }
                int chartTop = top + ((cards.Length + cols - 1) / cols) * (cardH + 10) + 8;
                int chartW = cols == 1 ? w : (w - gap) / 2;
                cardCategoryChart.SetBounds(0, chartTop, chartW, 238 + H);
                cardNeedsWants.SetBounds(cols == 1 ? 0 : chartW + gap, cols == 1 ? chartTop + 250 + H : chartTop, chartW, 238 + H);
                int recentTop = chartTop + (cols == 1 ? 500 + 2 * H : 250 + H);
                cardRecentTx.SetBounds(0, recentTop, w, Math.Max(190 + H, ClientSize.Height - recentTop - 8));
            }

            btnBigWhatIf.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnQuickAddTx.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnQuickNewCycle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            int right = w;
            btnBigWhatIf.Left = right - btnBigWhatIf.Width;
            btnQuickAddTx.Left = btnBigWhatIf.Left - 10 - btnQuickAddTx.Width;
            btnQuickNewCycle.Left = btnQuickAddTx.Left - 10 - btnQuickNewCycle.Width;
            if (w < 760)
            {
                lblSubtitle.Visible = false;
                btnQuickNewCycle.Visible = false;
                btnQuickAddTx.Visible = false;
                btnBigWhatIf.Visible = false;
                lblTitle.Width = w - 8;
            }
            else
            {
                lblSubtitle.Visible = true;
                btnQuickNewCycle.Visible = true;
                btnQuickAddTx.Visible = true;
                btnBigWhatIf.Visible = true;
            }
            Invalidate();
        }

        private void ResizeKpiChildren(RoundedPanel card, int width)
        {
            foreach (Control control in card.Controls)
            {
                Label label = control as Label;
                if (label == null) continue;

                // captions leave room for the icon chip in the top-right corner
                bool isCaption = label == lblBalanceCaption || label == lblDaysCaption ||
                                 label == lblDailyCaption || label == lblMonthCaption;
                label.Width = Math.Max(100, width - 28 - (isCaption ? 46 : 0));
            }
        }

        public void LoadDashboardData()
        {
            DateTime today = DateTime.Today;
            var cycle = _txManager.GetCurrentCycle();
            decimal balance = _txManager.ComputeCycleBalance(cycle);
            decimal safeDaily = _txManager.ComputeRemainingSafeSpendToday();
            bool isOverDaily = _txManager.IsTodayOverDailyLimit();

            lblBalanceValue.Text = $"\u20B1{balance:N2}";
            lblBalanceValue.ForeColor = Color.White;

            if (cycle != null)
            {
                decimal totalFunds = _txManager.ComputeCycleTotalFunds(cycle);
                lblCycleDates.Text = balance < 0
                    ? $"\u26A0 Overspent this cycle \u2022 {cycle.StartDate:MMM dd} \u2192 {cycle.EndDate:MMM dd}  |  Total this cycle: \u20B1{totalFunds:N2}"
                    : $"{cycle.StartDate:MMM dd} \u2192 {cycle.EndDate:MMM dd}  |  Total this cycle: \u20B1{totalFunds:N2}";
                lblCycleDates.ForeColor = balance < 0 ? Color.FromArgb(254, 205, 211) : Color.FromArgb(224, 242, 254);
                int daysLeft = cycle.ComputeDaysLeft(today);
                lblDaysValue.Text = $"{daysLeft} {(daysLeft == 1 ? "day" : "days")}";

                if (cycle.IsOverdue(today))
                {
                    lblOverdueStatus.Text = "\u26A0 Allowance overdue (using 1 day)";
                    lblOverdueStatus.ForeColor = ThemePalette.DangerRose;
                }
                else
                {
                    lblOverdueStatus.Text = $"Next allowance on {cycle.EndDate:MMM dd, yyyy}";
                    lblOverdueStatus.ForeColor = ThemePalette.SafeGreen;
                }
            }
            else
            {
                lblCycleDates.Text = "No active allowance cycle";
                lblDaysValue.Text = "-";
                lblOverdueStatus.Text = "Start a cycle in Allowance & Goals";
            }

            lblDailyValue.Text = $"\u20B1{safeDaily:N2}";
            if (isOverDaily)
            {
                decimal todaysSpent = _txManager.ComputeDailyLimitExpensesForDate(today);
                lblDailyValue.ForeColor = ThemePalette.DangerRose;
                lblDailyFlag.Text = $"\u26A0 Over daily limit (Spent \u20B1{todaysSpent:N2} today)";
                lblDailyFlag.ForeColor = ThemePalette.DangerRose;
            }
            else
            {
                decimal todaysSpent = _txManager.ComputeDailyLimitExpensesForDate(today);
                lblDailyValue.ForeColor = ThemePalette.TextPrimary;
                lblDailyFlag.Text = $"\u2713 Spent \u20B1{todaysSpent:N2} today \u2014 on track";
                lblDailyFlag.ForeColor = ThemePalette.SafeGreen;
            }

            var summary = _txManager.GetMonthlySummary(today.Month, today.Year);
            lblMonthCaption.Text = $"{today:MMMM yyyy} SUMMARY";
            lblMonthIncome.Text = $"Income:   \u20B1{summary.TotalIncome:N2}";
            lblMonthExpense.Text = $"Expenses: \u20B1{summary.TotalExpenses:N2}";

            _needsAmount = summary.NeedsTotal;
            _wantsAmount = summary.WantsTotal;
            _categoryTotals = summary.CategoryTotals;

            decimal nwSum = _needsAmount + _wantsAmount;
            if (nwSum > 0)
            {
                double needPct = (double)(_needsAmount / nwSum) * 100.0;
                double wantPct = 100.0 - needPct;
                lblNeedsWantsLegend.Text = $"Total Needs + Wants: \u20B1{nwSum:N2}\r\n(Savings not included)";
            }
            else
            {
                lblNeedsWantsLegend.Text = "No Need/Want expenses recorded this month yet.";
            }

            pnlNeedsWantsChart.Invalidate();
            pnlCategoryBars.Invalidate();

            // Populate recent transactions grid
            var recent = _txManager.GetAllTransactions().Take(6).Select(t => new
            {
                Date = t.Date.ToString("yyyy-MM-dd"),
                Type = t.TransactionType,
                CategoryOrSource = t is Income inc ? inc.Source : ((Expense)t).CategoryName,
                Tag = t is Expense exp && !string.IsNullOrEmpty(exp.ExpenseKind) ? exp.ExpenseKind : "-",
                Amount = (t is Income ? "+\u20B1" : "-\u20B1") + t.Amount.ToString("N2"),
                Notes = t.Notes
            }).ToList();

            dgvRecent.DataSource = recent;
        }

        private static readonly Color WantsAmber = Color.FromArgb(245, 158, 11);

        private void PnlNeedsWantsChart_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(ThemePalette.CardBackground);

            decimal total = _needsAmount + _wantsAmount;
            int barWidth = pnlNeedsWantsChart.Width - 8;
            int barHeight = 30;
            int y = 4;
            Rectangle barRect = new Rectangle(4, y, barWidth, barHeight);

            if (total <= 0)
            {
                using (var path = ThemePalette.RoundedRect(barRect, barHeight / 2))
                using (SolidBrush b = new SolidBrush(Color.FromArgb(226, 232, 240)))
                    g.FillPath(b, path);
                return;
            }

            double needPct = (double)(_needsAmount / total) * 100.0;
            double wantPct = 100.0 - needPct;
            int needsWidth = (int)Math.Round(barWidth * (double)(_needsAmount / total));
            int wantsWidth = barWidth - needsWidth;

            // Stacked pill bar
            using (var pill = ThemePalette.RoundedRect(barRect, barHeight / 2))
            {
                var state = g.Save();
                g.SetClip(pill, System.Drawing.Drawing2D.CombineMode.Intersect);

                if (needsWidth > 0)
                {
                    Rectangle nr = new Rectangle(4, y, needsWidth, barHeight);
                    using (var lg = new System.Drawing.Drawing2D.LinearGradientBrush(nr, ThemePalette.Lighten(ThemePalette.PrimaryTeal, 0.2f), ThemePalette.PrimaryTeal, System.Drawing.Drawing2D.LinearGradientMode.Horizontal))
                        g.FillRectangle(lg, nr);
                }
                if (wantsWidth > 0)
                {
                    Rectangle wr = new Rectangle(4 + needsWidth, y, wantsWidth, barHeight);
                    using (var lg = new System.Drawing.Drawing2D.LinearGradientBrush(wr, ThemePalette.Lighten(WantsAmber, 0.2f), WantsAmber, System.Drawing.Drawing2D.LinearGradientMode.Horizontal))
                        g.FillRectangle(lg, wr);
                }
                g.Restore(state);
            }

            if (needsWidth > 54)
                TextRenderer.DrawText(g, $"{needPct:F0}%", ThemePalette.FontBodyBold, new Rectangle(4, y, needsWidth, barHeight),
                    Color.White, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            if (wantsWidth > 54)
                TextRenderer.DrawText(g, $"{wantPct:F0}%", ThemePalette.FontBodyBold, new Rectangle(4 + needsWidth, y, wantsWidth, barHeight),
                    Color.White, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

            // Legend rows
            DrawLegendRow(g, 46, ThemePalette.PrimaryTeal, "Needs", _needsAmount, needPct);
            DrawLegendRow(g, 70, WantsAmber, "Wants", _wantsAmount, wantPct);
        }

        private void DrawLegendRow(Graphics g, int y, Color dot, string name, decimal amount, double pct)
        {
            using (SolidBrush b = new SolidBrush(dot))
                g.FillEllipse(b, 6, y + 6, 10, 10);

            TextRenderer.DrawText(g, name, ThemePalette.FontBodyBold, new Rectangle(24, y, 100, 22),
                ThemePalette.TextPrimary, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
            TextRenderer.DrawText(g, $"\u20B1{amount:N2}  ({pct:F1}%)", ThemePalette.FontBody,
                new Rectangle(110, y, pnlNeedsWantsChart.Width - 114, 22),
                ThemePalette.TextSecondary, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
        }

        private void PnlCategoryBars_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(ThemePalette.CardBackground);

            if (_categoryTotals == null || _categoryTotals.Count == 0)
            {
                TextRenderer.DrawText(g, "No expenses recorded for this month yet.", ThemePalette.FontBody,
                    new Rectangle(0, 40, pnlCategoryBars.Width, 30), ThemePalette.TextMuted, TextFormatFlags.HorizontalCenter);
                return;
            }

            var topCategories = _categoryTotals
                .OrderByDescending(kv => kv.Value)
                .Take(5)
                .ToList();

            decimal maxVal = topCategories.Max(kv => kv.Value);
            if (maxVal <= 0) maxVal = 1;

            int rowHeight = 34;
            int labelWidth = 150;
            int amountWidth = 90;
            int maxBarWidth = Math.Max(40, pnlCategoryBars.Width - labelWidth - amountWidth - 16);
            int barHeight = 14;

            for (int i = 0; i < topCategories.Count; i++)
            {
                var item = topCategories[i];
                int y = i * rowHeight + 4;
                Color barColor = ThemePalette.ChartPalette[i % ThemePalette.ChartPalette.Length];

                TextRenderer.DrawText(g, item.Key, ThemePalette.FontBody,
                    new Rectangle(0, y, labelWidth, 24), ThemePalette.TextPrimary,
                    TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);

                int barW = Math.Max(barHeight, (int)Math.Round(maxBarWidth * (double)(item.Value / maxVal)));
                Rectangle trackRect = new Rectangle(labelWidth + 4, y + 5, maxBarWidth, barHeight);
                Rectangle fillRect = new Rectangle(labelWidth + 4, y + 5, barW, barHeight);

                using (var trackPath = ThemePalette.RoundedRect(trackRect, barHeight / 2))
                using (SolidBrush track = new SolidBrush(ThemePalette.SurfaceTrack))
                    g.FillPath(track, trackPath);

                using (var fillPath = ThemePalette.RoundedRect(fillRect, barHeight / 2))
                using (var lg = new System.Drawing.Drawing2D.LinearGradientBrush(fillRect, ThemePalette.Lighten(barColor, 0.25f), barColor, System.Drawing.Drawing2D.LinearGradientMode.Horizontal))
                    g.FillPath(lg, fillPath);

                TextRenderer.DrawText(g, $"\u20B1{item.Value:N0}", ThemePalette.FontBodyBold,
                    new Rectangle(labelWidth + maxBarWidth + 10, y, amountWidth, 24), ThemePalette.TextPrimary,
                    TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
            }
        }
    }
}
