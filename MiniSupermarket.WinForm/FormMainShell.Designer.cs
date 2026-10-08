namespace MiniSupermarket.WinForms
{
    partial class FormMainShell
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            panelSidebar = new Panel();
            btnUserManage = new Button();
            btnReports = new Button();
            btnCustomer = new Button();
            btnProduct = new Button();
            btnCategory = new Button();
            btnPOS = new Button();
            lblLogo = new Label();
            panelUserFooter = new Panel();
            btnLogout = new Button();
            panelTopHeader = new Panel();
            lblUserInfo = new Label();
            lblTitle = new Label();
            panelMainContent = new Panel();
            panelSidebar.SuspendLayout();
            panelUserFooter.SuspendLayout();
            panelTopHeader.SuspendLayout();
            SuspendLayout();
            // 
            // panelSidebar
            // 
            panelSidebar.BackColor = Color.FromArgb(24, 30, 48);
            panelSidebar.Controls.Add(btnUserManage);
            panelSidebar.Controls.Add(btnReports);
            panelSidebar.Controls.Add(btnCustomer);
            panelSidebar.Controls.Add(btnProduct);
            panelSidebar.Controls.Add(btnCategory);
            panelSidebar.Controls.Add(btnPOS);
            panelSidebar.Controls.Add(lblLogo);
            panelSidebar.Controls.Add(panelUserFooter);
            panelSidebar.Dock = DockStyle.Left;
            panelSidebar.Location = new Point(0, 0);
            panelSidebar.Name = "panelSidebar";
            panelSidebar.Size = new Size(230, 720);
            panelSidebar.TabIndex = 0;
            // 
            // btnUserManage
            // 
            btnUserManage.BackColor = Color.FromArgb(24, 30, 48);
            btnUserManage.Cursor = Cursors.Hand;
            btnUserManage.Dock = DockStyle.Top;
            btnUserManage.FlatAppearance.BorderSize = 0;
            btnUserManage.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 20, 32);
            btnUserManage.FlatAppearance.MouseOverBackColor = Color.FromArgb(41, 52, 80);
            btnUserManage.FlatStyle = FlatStyle.Flat;
            btnUserManage.Font = new Font("Segoe UI", 10F);
            btnUserManage.ForeColor = Color.White;
            btnUserManage.Location = new Point(0, 285);
            btnUserManage.Name = "btnUserManage";
            btnUserManage.Size = new Size(230, 45);
            btnUserManage.TabIndex = 6;
            btnUserManage.Text = "  🛡 Quản trị Tài khoản";
            btnUserManage.TextAlign = ContentAlignment.MiddleLeft;
            btnUserManage.UseVisualStyleBackColor = false;
            // 
            // btnReports
            // 
            btnReports.BackColor = Color.FromArgb(24, 30, 48);
            btnReports.Cursor = Cursors.Hand;
            btnReports.Dock = DockStyle.Top;
            btnReports.FlatAppearance.BorderSize = 0;
            btnReports.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 20, 32);
            btnReports.FlatAppearance.MouseOverBackColor = Color.FromArgb(41, 52, 80);
            btnReports.FlatStyle = FlatStyle.Flat;
            btnReports.Font = new Font("Segoe UI", 10F);
            btnReports.ForeColor = Color.White;
            btnReports.Location = new Point(0, 240);
            btnReports.Name = "btnReports";
            btnReports.Size = new Size(230, 45);
            btnReports.TabIndex = 5;
            btnReports.Text = "  📊 Báo cáo Doanh thu";
            btnReports.TextAlign = ContentAlignment.MiddleLeft;
            btnReports.UseVisualStyleBackColor = false;
            // 
            // btnCustomer
            // 
            btnCustomer.BackColor = Color.FromArgb(24, 30, 48);
            btnCustomer.Cursor = Cursors.Hand;
            btnCustomer.Dock = DockStyle.Top;
            btnCustomer.FlatAppearance.BorderSize = 0;
            btnCustomer.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 20, 32);
            btnCustomer.FlatAppearance.MouseOverBackColor = Color.FromArgb(41, 52, 80);
            btnCustomer.FlatStyle = FlatStyle.Flat;
            btnCustomer.Font = new Font("Segoe UI", 10F);
            btnCustomer.ForeColor = Color.White;
            btnCustomer.Location = new Point(0, 195);
            btnCustomer.Name = "btnCustomer";
            btnCustomer.Size = new Size(230, 45);
            btnCustomer.TabIndex = 4;
            btnCustomer.Text = "  👥 Quản lý Khách hàng";
            btnCustomer.TextAlign = ContentAlignment.MiddleLeft;
            btnCustomer.UseVisualStyleBackColor = false;
            // 
            // btnProduct
            // 
            btnProduct.BackColor = Color.FromArgb(24, 30, 48);
            btnProduct.Cursor = Cursors.Hand;
            btnProduct.Dock = DockStyle.Top;
            btnProduct.FlatAppearance.BorderSize = 0;
            btnProduct.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 20, 32);
            btnProduct.FlatAppearance.MouseOverBackColor = Color.FromArgb(41, 52, 80);
            btnProduct.FlatStyle = FlatStyle.Flat;
            btnProduct.Font = new Font("Segoe UI", 10F);
            btnProduct.ForeColor = Color.White;
            btnProduct.Location = new Point(0, 150);
            btnProduct.Name = "btnProduct";
            btnProduct.Size = new Size(230, 45);
            btnProduct.TabIndex = 3;
            btnProduct.Text = "  📦 Quản lý Sản phẩm";
            btnProduct.TextAlign = ContentAlignment.MiddleLeft;
            btnProduct.UseVisualStyleBackColor = false;
            // 
            // btnCategory
            // 
            btnCategory.BackColor = Color.FromArgb(24, 30, 48);
            btnCategory.Cursor = Cursors.Hand;
            btnCategory.Dock = DockStyle.Top;
            btnCategory.FlatAppearance.BorderSize = 0;
            btnCategory.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 20, 32);
            btnCategory.FlatAppearance.MouseOverBackColor = Color.FromArgb(41, 52, 80);
            btnCategory.FlatStyle = FlatStyle.Flat;
            btnCategory.Font = new Font("Segoe UI", 10F);
            btnCategory.ForeColor = Color.White;
            btnCategory.Location = new Point(0, 105);
            btnCategory.Name = "btnCategory";
            btnCategory.Size = new Size(230, 45);
            btnCategory.TabIndex = 2;
            btnCategory.Text = "  📁 Quản lý Danh mục";
            btnCategory.TextAlign = ContentAlignment.MiddleLeft;
            btnCategory.UseVisualStyleBackColor = false;
            // 
            // btnPOS
            // 
            btnPOS.BackColor = Color.FromArgb(24, 30, 48);
            btnPOS.Cursor = Cursors.Hand;
            btnPOS.Dock = DockStyle.Top;
            btnPOS.FlatAppearance.BorderSize = 0;
            btnPOS.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 20, 32);
            btnPOS.FlatAppearance.MouseOverBackColor = Color.FromArgb(41, 52, 80);
            btnPOS.FlatStyle = FlatStyle.Flat;
            btnPOS.Font = new Font("Segoe UI", 10F);
            btnPOS.ForeColor = Color.White;
            btnPOS.Location = new Point(0, 60);
            btnPOS.Name = "btnPOS";
            btnPOS.Size = new Size(230, 45);
            btnPOS.TabIndex = 1;
            btnPOS.Text = "  \U0001f6d2 Bán hàng (POS)";
            btnPOS.TextAlign = ContentAlignment.MiddleLeft;
            btnPOS.UseVisualStyleBackColor = false;
            // 
            // lblLogo
            // 
            lblLogo.Dock = DockStyle.Top;
            lblLogo.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblLogo.ForeColor = Color.White;
            lblLogo.Location = new Point(0, 0);
            lblLogo.Name = "lblLogo";
            lblLogo.Size = new Size(230, 60);
            lblLogo.TabIndex = 0;
            lblLogo.Text = "\U0001f6d2 MiniMart POS";
            lblLogo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelUserFooter
            // 
            panelUserFooter.BackColor = Color.FromArgb(18, 22, 36);
            panelUserFooter.Controls.Add(btnLogout);
            panelUserFooter.Dock = DockStyle.Bottom;
            panelUserFooter.Location = new Point(0, 660);
            panelUserFooter.Name = "panelUserFooter";
            panelUserFooter.Size = new Size(230, 60);
            panelUserFooter.TabIndex = 7;
            // 
            // btnLogout
            // 
            btnLogout.BackColor = Color.FromArgb(32, 40, 62);
            btnLogout.Cursor = Cursors.Hand;
            btnLogout.Dock = DockStyle.Fill;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 20, 32);
            btnLogout.FlatAppearance.MouseOverBackColor = Color.FromArgb(41, 52, 80);
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Font = new Font("Segoe UI", 10F);
            btnLogout.ForeColor = Color.White;
            btnLogout.Location = new Point(0, 0);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(230, 60);
            btnLogout.TabIndex = 0;
            btnLogout.Text = "  🚪 Đăng xuất";
            btnLogout.TextAlign = ContentAlignment.MiddleLeft;
            btnLogout.UseVisualStyleBackColor = false;
            // 
            // panelTopHeader
            // 
            panelTopHeader.BackColor = Color.White;
            panelTopHeader.Controls.Add(lblUserInfo);
            panelTopHeader.Controls.Add(lblTitle);
            panelTopHeader.Dock = DockStyle.Top;
            panelTopHeader.Location = new Point(230, 0);
            panelTopHeader.Name = "panelTopHeader";
            panelTopHeader.Size = new Size(1050, 60);
            panelTopHeader.TabIndex = 1;
            // 
            // lblUserInfo
            // 
            lblUserInfo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblUserInfo.AutoSize = true;
            lblUserInfo.Font = new Font("Segoe UI", 9.5F);
            lblUserInfo.ForeColor = Color.DimGray;
            lblUserInfo.Location = new Point(820, 20);
            lblUserInfo.Name = "lblUserInfo";
            lblUserInfo.Size = new Size(176, 17);
            lblUserInfo.TabIndex = 1;
            lblUserInfo.Text = "Nhân viên: admin01 | ADMIN";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(30, 30, 30);
            lblTitle.Location = new Point(20, 18);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(239, 25);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "BÀN LÀM VIỆC HỆ THỐNG";
            // 
            // panelMainContent
            // 
            panelMainContent.BackColor = Color.FromArgb(244, 245, 247);
            panelMainContent.Dock = DockStyle.Fill;
            panelMainContent.Location = new Point(230, 60);
            panelMainContent.Name = "panelMainContent";
            panelMainContent.Size = new Size(1050, 660);
            panelMainContent.TabIndex = 2;
            panelMainContent.Paint += panelMainContent_Paint;
            // 
            // FormMainShell
            // 
            ClientSize = new Size(1280, 720);
            Controls.Add(panelMainContent);
            Controls.Add(panelTopHeader);
            Controls.Add(panelSidebar);
            Font = new Font("Segoe UI", 10F);
            Name = "FormMainShell";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Hệ thống Quản lý Bán lẻ & Tồn kho Siêu thị Mini";
            panelSidebar.ResumeLayout(false);
            panelUserFooter.ResumeLayout(false);
            panelTopHeader.ResumeLayout(false);
            panelTopHeader.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.Panel panelTopHeader;
        private System.Windows.Forms.Panel panelMainContent;
        private System.Windows.Forms.Panel panelUserFooter;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblUserInfo;
        private System.Windows.Forms.Button btnPOS;
        private System.Windows.Forms.Button btnCategory;
        private System.Windows.Forms.Button btnProduct;
        private System.Windows.Forms.Button btnCustomer;
        private System.Windows.Forms.Button btnReports;
        private System.Windows.Forms.Button btnUserManage;
        private System.Windows.Forms.Button btnLogout;
    }
}