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
        private Label lblBadgeGlyph;
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
            this.lblBadgeGlyph = new Label();
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
            this.btnResetDemo = new BudgetBuddy.Controls.StyledButton();
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

            this.pnlSidebar.BackColor = ThemePalette.SidebarBg;
            this.pnlSidebar.ColorTop = ThemePalette.SidebarTop;
            this.pnlSidebar.ColorBottom = ThemePalette.SidebarBottom;
            this.pnlSidebar.Dock = DockStyle.Left;
            this.pnlSidebar.Width = 224;
            this.pnlSidebar.Controls.Add(this.pnlNavButtons);
            this.pnlSidebar.Controls.Add(this.pnlSidebarFooter);
            this.pnlSidebar.Controls.Add(this.pnlBrandHeader);

            this.pnlBrandHeader.Dock = DockStyle.Top;
            this.pnlBrandHeader.Height = 77;
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
            this.pnlBrandBadge.Location = new Point(16, 20);
            this.pnlBrandBadge.Size = new Size(40, 40);
            this.lblBadgeGlyph.AutoSize = false;
            this.lblBadgeGlyph.UseMnemonic = false;
            this.lblBadgeGlyph.Text = "\u20B1";
            this.lblBadgeGlyph.Font = new Font("Segoe UI Semibold", 15f, FontStyle.Regular);
            this.lblBadgeGlyph.ForeColor = Color.White;
            this.lblBadgeGlyph.BackColor = Color.Transparent;
            this.lblBadgeGlyph.TextAlign = ContentAlignment.MiddleCenter;
            this.lblBadgeGlyph.Location = new Point(4, 4);
            this.lblBadgeGlyph.Size = new Size(32, 32);
            this.pnlBrandBadge.Controls.Add(this.lblBadgeGlyph);

            this.lblBrandTitle.AutoSize = false;
            this.lblBrandTitle.UseMnemonic = false;
            this.lblBrandTitle.Text = "BudgetBuddy";
            this.lblBrandTitle.Font = new Font("Segoe UI Semibold", 12.5f, FontStyle.Regular);
            this.lblBrandTitle.ForeColor = Color.White;
            this.lblBrandTitle.TextAlign = ContentAlignment.MiddleLeft;
            this.lblBrandTitle.Location = new Point(66, 20);
            this.lblBrandTitle.Size = new Size(154, 25);
            this.lblBrandTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            this.lblBrandSubtitle.AutoSize = false;
            this.lblBrandSubtitle.UseMnemonic = false;
            this.lblBrandSubtitle.Text = "Your allowance, sorted";
            this.lblBrandSubtitle.Font = new Font("Segoe UI", 8f, FontStyle.Regular);
            this.lblBrandSubtitle.ForeColor = ThemePalette.MintText;
            this.lblBrandSubtitle.TextAlign = ContentAlignment.TopLeft;
            this.lblBrandSubtitle.AutoEllipsis = true;
            this.lblBrandSubtitle.Location = new Point(66, 53);
            this.lblBrandSubtitle.Size = new Size(154, 20);
            this.lblBrandSubtitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            this.pnlBrandDivider.Dock = DockStyle.Bottom;
            this.pnlBrandDivider.Height = 1;
            this.pnlBrandDivider.BackColor = ThemePalette.SidebarHover;

            this.pnlNavButtons.Dock = DockStyle.Fill;
            this.pnlNavButtons.AutoScroll = true;
            this.pnlNavButtons.BackColor = Color.Transparent;
            this.pnlNavButtons.Padding = new Padding(10, 6, 10, 6);

            this.btnNavDashboard.Text = "Dashboard";
            this.btnNavDashboard.Icon = IconKind.Dashboard;
            this.btnNavDashboard.Font = ThemePalette.FontBodyBold;
            this.btnNavDashboard.ForeColor = Color.FromArgb(203, 225, 225);
            this.btnNavDashboard.BackColor = Color.Transparent;
            this.btnNavDashboard.Location = new Point(0, 8);
            this.btnNavDashboard.Size = new Size(204, 44);
            this.btnNavDashboard.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.btnNavDashboard.TextAlign = ContentAlignment.MiddleLeft;
            this.btnNavDashboard.UseMnemonic = false;
            this.pnlNavButtons.Controls.Add(this.btnNavDashboard);
            this.btnNavTransactions.Text = "Income & Expenses";
            this.btnNavTransactions.Icon = IconKind.Transactions;
            this.btnNavTransactions.Font = ThemePalette.FontBodyBold;
            this.btnNavTransactions.ForeColor = Color.FromArgb(203, 225, 225);
            this.btnNavTransactions.BackColor = Color.Transparent;
            this.btnNavTransactions.Location = new Point(0, 56);
            this.btnNavTransactions.Size = new Size(204, 44);
            this.btnNavTransactions.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.btnNavTransactions.TextAlign = ContentAlignment.MiddleLeft;
            this.btnNavTransactions.UseMnemonic = false;
            this.pnlNavButtons.Controls.Add(this.btnNavTransactions);
            this.btnNavBudgets.Text = "Category Budgets";
            this.btnNavBudgets.Icon = IconKind.Budget;
            this.btnNavBudgets.Font = ThemePalette.FontBodyBold;
            this.btnNavBudgets.ForeColor = Color.FromArgb(203, 225, 225);
            this.btnNavBudgets.BackColor = Color.Transparent;
            this.btnNavBudgets.Location = new Point(0, 104);
            this.btnNavBudgets.Size = new Size(204, 44);
            this.btnNavBudgets.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.btnNavBudgets.TextAlign = ContentAlignment.MiddleLeft;
            this.btnNavBudgets.UseMnemonic = false;
            this.pnlNavButtons.Controls.Add(this.btnNavBudgets);
            this.btnNavAllowanceGoals.Text = "Allowance & Goals";
            this.btnNavAllowanceGoals.Icon = IconKind.Goal;
            this.btnNavAllowanceGoals.Font = ThemePalette.FontBodyBold;
            this.btnNavAllowanceGoals.ForeColor = Color.FromArgb(203, 225, 225);
            this.btnNavAllowanceGoals.BackColor = Color.Transparent;
            this.btnNavAllowanceGoals.Location = new Point(0, 152);
            this.btnNavAllowanceGoals.Size = new Size(204, 44);
            this.btnNavAllowanceGoals.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.btnNavAllowanceGoals.TextAlign = ContentAlignment.MiddleLeft;
            this.btnNavAllowanceGoals.UseMnemonic = false;
            this.pnlNavButtons.Controls.Add(this.btnNavAllowanceGoals);
            this.btnNavWhatIf.Text = "What-If Simulator";
            this.btnNavWhatIf.Icon = IconKind.WhatIf;
            this.btnNavWhatIf.Font = ThemePalette.FontBodyBold;
            this.btnNavWhatIf.ForeColor = Color.FromArgb(203, 225, 225);
            this.btnNavWhatIf.BackColor = Color.Transparent;
            this.btnNavWhatIf.Location = new Point(0, 200);
            this.btnNavWhatIf.Size = new Size(204, 44);
            this.btnNavWhatIf.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.btnNavWhatIf.TextAlign = ContentAlignment.MiddleLeft;
            this.btnNavWhatIf.UseMnemonic = false;
            this.pnlNavButtons.Controls.Add(this.btnNavWhatIf);
            this.btnNavHistory.Text = "History & Search";
            this.btnNavHistory.Icon = IconKind.History;
            this.btnNavHistory.Font = ThemePalette.FontBodyBold;
            this.btnNavHistory.ForeColor = Color.FromArgb(203, 225, 225);
            this.btnNavHistory.BackColor = Color.Transparent;
            this.btnNavHistory.Location = new Point(0, 248);
            this.btnNavHistory.Size = new Size(204, 44);
            this.btnNavHistory.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.btnNavHistory.TextAlign = ContentAlignment.MiddleLeft;
            this.btnNavHistory.UseMnemonic = false;
            this.pnlNavButtons.Controls.Add(this.btnNavHistory);
            this.btnNavCategories.Text = "Expense Categories";
            this.btnNavCategories.Icon = IconKind.Category;
            this.btnNavCategories.Font = ThemePalette.FontBodyBold;
            this.btnNavCategories.ForeColor = Color.FromArgb(203, 225, 225);
            this.btnNavCategories.BackColor = Color.Transparent;
            this.btnNavCategories.Location = new Point(0, 296);
            this.btnNavCategories.Size = new Size(204, 44);
            this.btnNavCategories.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.btnNavCategories.TextAlign = ContentAlignment.MiddleLeft;
            this.btnNavCategories.UseMnemonic = false;
            this.pnlNavButtons.Controls.Add(this.btnNavCategories);

            this.pnlSidebarFooter.Size = new Size(224, 176);   
            this.pnlSidebarFooter.Dock = DockStyle.Bottom;
            this.pnlSidebarFooter.BackColor = Color.Transparent;
            this.pnlSidebarFooter.Controls.Add(this.cardTip);
            this.pnlSidebarFooter.Controls.Add(this.btnResetDemo);

            this.cardTip.Location = new Point(12, 6);
            this.cardTip.Size = new Size(200, 116);
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
            this.lblTipCaption.Location = new Point(14, 10);
            this.lblTipCaption.Size = new Size(160, 20);

            this.lblTipText.AutoSize = false;
            this.lblTipText.UseMnemonic = false;
            this.lblTipText.Font = ThemePalette.FontCaption;
            this.lblTipText.ForeColor = Color.FromArgb(214, 236, 236);
            this.lblTipText.Location = new Point(14, 38);
            this.lblTipText.Size = new Size(172, 72);
            this.lblTipText.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            this.btnResetDemo.Location = new Point(12, 128);
            this.btnResetDemo.Size = new Size(200, 38);
            this.btnResetDemo.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnResetDemo.Text = "Reset Demo Data";
            this.btnResetDemo.FlatStyle = FlatStyle.Flat;
            this.btnResetDemo.FlatAppearance.BorderSize = 0;
            this.btnResetDemo.BackColor = Color.FromArgb(26, 66, 72);
            this.btnResetDemo.ForeColor = Color.FromArgb(203, 225, 225);
            this.btnResetDemo.Font = ThemePalette.FontBodyBold;
            this.btnResetDemo.Cursor = Cursors.Hand;
            ((BudgetBuddy.Controls.StyledButton)this.btnResetDemo).SecondaryStyle = false;
            this.btnResetDemo.Font = ThemePalette.FontCaptionBold;
            this.btnResetDemo.Click += BtnResetDemo_Click;

            this.pnlContentHost.Dock = DockStyle.Fill;
            this.pnlContentHost.BackColor = ThemePalette.AppBackground;
            this.pnlContentHost.Padding = new Padding(16);
            this.pnlContentHost.AutoScroll = true;

            this.Text = "BudgetBuddy - Student Allowance and Expense Tracker";
            this.ClientSize = new Size(1280, 800);
            this.DoubleBuffered = true;
            this.MinimumSize = new Size(820, 600);
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.btnResetDemo.UseMnemonic = false;

            this.BackColor = ThemePalette.AppBackground;
            this.Controls.Add(this.pnlContentHost);
            this.Controls.Add(this.pnlSidebar);

            this.pnlSidebarFooter.ResumeLayout(false);
            this.pnlSidebar.ResumeLayout(false);
            this.pnlBrandHeader.ResumeLayout(false);
            this.pnlNavButtons.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
