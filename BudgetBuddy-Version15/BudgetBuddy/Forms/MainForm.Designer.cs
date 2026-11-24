using System.Drawing;
using System.Windows.Forms;
using BudgetBuddy.Utils;

namespace BudgetBuddy.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private BudgetBuddy.Controls.GradientPanel pnlSidebar;
        private Panel pnlBrandHeader;
        private Label lblBrandTitle;
        private Label lblBrandSubtitle;
        private BudgetBuddy.Controls.RoundedPanel pnlBrandBadge;
        private Panel pnlBrandDivider;
        private Font fontBrandName;
        private Font fontBrandBadge;
        private Panel pnlNavButtons;
        private BudgetBuddy.Controls.NavButton btnNavDashboard;
        private BudgetBuddy.Controls.NavButton btnNavTransactions;
        private BudgetBuddy.Controls.NavButton btnNavCategories;
        private BudgetBuddy.Controls.NavButton btnNavBudgets;
        private BudgetBuddy.Controls.NavButton btnNavAllowanceGoals;
        private BudgetBuddy.Controls.NavButton btnNavWhatIf;
        private BudgetBuddy.Controls.NavButton btnNavHistory;
        private Button btnResetDemo;
        private Panel pnlSidebarFooter;
        private BudgetBuddy.Controls.RoundedPanel cardTip;
        private Label lblTipCaption;
        private Label lblTipText;
        private Panel pnlContentHost;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlSidebar = new BudgetBuddy.Controls.GradientPanel();
            this.pnlBrandHeader = new Panel();
            this.lblBrandTitle = new Label();
            this.lblBrandSubtitle = new Label();
            this.pnlBrandBadge = new BudgetBuddy.Controls.RoundedPanel();
            this.pnlBrandDivider = new Panel();
            this.pnlNavButtons = new Panel();
            this.btnNavDashboard = new BudgetBuddy.Controls.NavButton();
            this.btnNavTransactions = new BudgetBuddy.Controls.NavButton();
            this.btnNavCategories = new BudgetBuddy.Controls.NavButton();
            this.btnNavBudgets = new BudgetBuddy.Controls.NavButton();
            this.btnNavAllowanceGoals = new BudgetBuddy.Controls.NavButton();
            this.btnNavWhatIf = new BudgetBuddy.Controls.NavButton();
            this.btnNavHistory = new BudgetBuddy.Controls.NavButton();
            this.btnResetDemo = new Button();
            this.pnlSidebarFooter = new Panel();
            this.cardTip = new BudgetBuddy.Controls.RoundedPanel();
            this.lblTipCaption = new Label();
            this.lblTipText = new Label();
            this.pnlContentHost = new Panel();

            this.pnlSidebar.SuspendLayout();
            this.pnlBrandHeader.SuspendLayout();
            this.pnlNavButtons.SuspendLayout();
            this.pnlSidebarFooter.SuspendLayout();
            this.SuspendLayout();

            // Sidebar
            this.pnlSidebar.BackColor = ThemePalette.SidebarBg;
            this.pnlSidebar.ColorTop = ThemePalette.SidebarTop;
            this.pnlSidebar.ColorBottom = ThemePalette.SidebarBottom;
            this.pnlSidebar.Dock = DockStyle.Left;
            this.pnlSidebar.Width = 224;
            this.pnlSidebar.Controls.Add(this.pnlSidebarFooter);
            this.pnlSidebar.Controls.Add(this.pnlNavButtons);
            this.pnlSidebar.Controls.Add(this.pnlBrandHeader);

            // Brand Header: rounded peso badge + app name + tagline, with a divider underneath
            this.fontBrandName = new Font("Segoe UI Semibold", 12.5f, FontStyle.Regular);
            this.fontBrandBadge = new Font("Segoe UI Semibold", 15f, FontStyle.Regular);

            int nameH = this.fontBrandName.Height + 4;
            int tagH = ThemePalette.FontCaption.Height + 4;
            int badgeSize = 40;
            int blockH = nameH + tagH;
            int rowH = System.Math.Max(badgeSize, blockH);
            int textLeft = 16 + badgeSize + 10;

            this.pnlBrandHeader.Dock = DockStyle.Top;
            this.pnlBrandHeader.Height = 18 + rowH + 18 + 1;
            this.pnlBrandHeader.Padding = new Padding(0);
            this.pnlBrandHeader.Controls.Add(this.pnlBrandDivider);
            this.pnlBrandHeader.Controls.Add(this.pnlBrandBadge);
            this.pnlBrandHeader.Controls.Add(this.lblBrandTitle);
            this.pnlBrandHeader.Controls.Add(this.lblBrandSubtitle);

            this.pnlBrandBadge.CornerRadius = 12;
            this.pnlBrandBadge.FillColor = ThemePalette.HeroStart;
            this.pnlBrandBadge.GradientEnd = ThemePalette.HeroEnd;
            this.pnlBrandBadge.ShadowDepth = 0;
            this.pnlBrandBadge.BorderThickness = 0;
            this.pnlBrandBadge.Padding = new Padding(0);
            this.pnlBrandBadge.SetBounds(16, 18 + (rowH - badgeSize) / 2, badgeSize, badgeSize);
            var lblBadgeGlyph = new Label();
            lblBadgeGlyph.AutoSize = false;
            lblBadgeGlyph.UseMnemonic = false;
            lblBadgeGlyph.Text = "\u20B1";
            lblBadgeGlyph.Font = this.fontBrandBadge;
            lblBadgeGlyph.ForeColor = Color.White;
            lblBadgeGlyph.BackColor = Color.Transparent;
            lblBadgeGlyph.TextAlign = ContentAlignment.MiddleCenter;
            lblBadgeGlyph.SetBounds(4, 4, badgeSize - 8, badgeSize - 8);
            this.pnlBrandBadge.Controls.Add(lblBadgeGlyph);

            int textTop = 18 + (rowH - blockH) / 2;
            this.lblBrandTitle.AutoSize = false;
            this.lblBrandTitle.UseMnemonic = false;
            this.lblBrandTitle.Text = "BudgetBuddy";
            this.lblBrandTitle.Font = this.fontBrandName;
            this.lblBrandTitle.ForeColor = Color.White;
            this.lblBrandTitle.TextAlign = ContentAlignment.MiddleLeft;
            this.lblBrandTitle.SetBounds(textLeft, textTop, 224 - textLeft - 4, nameH);
            this.lblBrandTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            this.lblBrandSubtitle.AutoSize = false;
            this.lblBrandSubtitle.UseMnemonic = false;
            this.lblBrandSubtitle.Text = "Your allowance, sorted";
            this.lblBrandSubtitle.Font = new Font("Segoe UI", 8f, FontStyle.Regular);
            this.lblBrandSubtitle.ForeColor = ThemePalette.MintText;
            this.lblBrandSubtitle.TextAlign = ContentAlignment.TopLeft;
            this.lblBrandSubtitle.AutoEllipsis = true;
            this.lblBrandSubtitle.SetBounds(textLeft, textTop + nameH, 224 - textLeft - 4, tagH);
            this.lblBrandSubtitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            this.pnlBrandDivider.Dock = DockStyle.Bottom;
            this.pnlBrandDivider.Height = 1;
            this.pnlBrandDivider.BackColor = ThemePalette.SidebarHover;

            // Nav Container
            this.pnlNavButtons.Dock = DockStyle.Fill;
            this.pnlNavButtons.BackColor = Color.Transparent;
            this.pnlNavButtons.Padding = new Padding(10, 6, 10, 6);

            ConfigureNavButton(this.btnNavDashboard, "Dashboard", 0, IconKind.Dashboard);
            ConfigureNavButton(this.btnNavTransactions, "Income & Expenses", 1, IconKind.Transactions);
            ConfigureNavButton(this.btnNavBudgets, "Category Budgets", 2, IconKind.Budget);
            ConfigureNavButton(this.btnNavAllowanceGoals, "Allowance & Goals", 3, IconKind.Goal);
            ConfigureNavButton(this.btnNavWhatIf, "What-If Simulator", 4, IconKind.WhatIf);
            ConfigureNavButton(this.btnNavHistory, "History & Search", 5, IconKind.History);
            ConfigureNavButton(this.btnNavCategories, "Expense Categories", 6, IconKind.Category);

            // Sidebar footer: rotating money tip + reset button
            this.pnlSidebarFooter.Size = new Size(224, 176);   // match sidebar width so right-anchored children keep their margin
            this.pnlSidebarFooter.Dock = DockStyle.Bottom;
            this.pnlSidebarFooter.BackColor = Color.Transparent;
            this.pnlSidebarFooter.Controls.Add(this.cardTip);
            this.pnlSidebarFooter.Controls.Add(this.btnResetDemo);

            this.cardTip.SetBounds(12, 6, 200, 112);
            this.cardTip.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.cardTip.CornerRadius = 12;
            this.cardTip.FillColor = Color.FromArgb(21, 66, 72);
            this.cardTip.BorderColor = Color.FromArgb(34, 94, 100);
            this.cardTip.ShadowDepth = 0;
            this.cardTip.Padding = new Padding(0);
            this.cardTip.Controls.Add(this.lblTipCaption);
            this.cardTip.Controls.Add(this.lblTipText);

            this.lblTipCaption.AutoSize = false;
            this.lblTipCaption.UseMnemonic = false;
            this.lblTipCaption.Text = "QUICK TIP";
            this.lblTipCaption.Font = ThemePalette.FontCaptionBold;
            this.lblTipCaption.ForeColor = ThemePalette.MintText;
            this.lblTipCaption.SetBounds(14, 10, 160, 18);

            this.lblTipText.AutoSize = false;
            this.lblTipText.UseMnemonic = false;
            this.lblTipText.Font = ThemePalette.FontCaption;
            this.lblTipText.ForeColor = Color.FromArgb(214, 236, 236);
            this.lblTipText.SetBounds(14, 32, 172, 72);
            this.lblTipText.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            this.btnResetDemo.SetBounds(12, 128, 200, 38);
            this.btnResetDemo.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnResetDemo.Text = "Reset Demo Data";
            ThemePalette.StyleButton(this.btnResetDemo, Color.FromArgb(26, 66, 72), Color.FromArgb(203, 225, 225));
            this.btnResetDemo.Font = ThemePalette.FontCaptionBold;
            this.btnResetDemo.Click += BtnResetDemo_Click;

            // Content Host
            this.pnlContentHost.Dock = DockStyle.Fill;
            this.pnlContentHost.BackColor = ThemePalette.AppBackground;
            this.pnlContentHost.Padding = new Padding(16);
            this.pnlContentHost.AutoScroll = true;

            // MainForm
            this.Text = "BudgetBuddy - Student Allowance and Expense Tracker";
            this.ClientSize = new Size(1280, 800);
            this.MinimumSize = new Size(820, 600);
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = ThemePalette.AppBackground;
            this.Controls.Add(this.pnlContentHost);
            this.Controls.Add(this.pnlSidebar);

            this.pnlSidebarFooter.ResumeLayout(false);
            this.pnlSidebar.ResumeLayout(false);
            this.pnlBrandHeader.ResumeLayout(false);
            this.pnlNavButtons.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private void ConfigureNavButton(BudgetBuddy.Controls.NavButton btn, string text, int index, IconKind icon)
        {
            btn.Text = text;
            btn.Icon = icon;
            btn.Width = 204;
            btn.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btn.Height = 46;
            btn.Left = 10;
            btn.Top = 8 + (index * 52);
            btn.Font = ThemePalette.FontBodyBold;
            this.pnlNavButtons.Controls.Add(btn);
        }
    }
}
