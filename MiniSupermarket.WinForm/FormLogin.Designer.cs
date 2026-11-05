namespace MiniSupermarket.WinForms
{
    partial class FormLogin
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
            panelBrand = new Panel();
            lblBrandVersion = new Label();
            panelBrandCard = new Panel();
            pnlStepLine2 = new Panel();
            pnlStepLine1 = new Panel();
            lblStep3Desc = new Label();
            lblStep3Title = new Label();
            lblStep3Icon = new Label();
            lblStep2Desc = new Label();
            lblStep2Title = new Label();
            lblStep2Icon = new Label();
            lblStep1Desc = new Label();
            lblStep1Title = new Label();
            lblStep1Icon = new Label();
            lblWorkflowTitle = new Label();
            lblBrandSub = new Label();
            lblBrandTitle = new Label();
            lblBrandIcon = new Label();
            panelLogin = new Panel();
            lblMessage = new Label();
            btnLogin = new Button();
            txtPass = new TextBox();
            lblPass = new Label();
            txtUser = new TextBox();
            lblUser = new Label();
            lblWelcomeSub = new Label();
            lblWelcome = new Label();
            lblTitle = new Label();
            panelBrand.SuspendLayout();
            panelBrandCard.SuspendLayout();
            panelLogin.SuspendLayout();
            SuspendLayout();
            // 
            // panelBrand
            // 
            panelBrand.BackColor = Color.FromArgb(15, 23, 42);
            panelBrand.Controls.Add(lblBrandVersion);
            panelBrand.Controls.Add(panelBrandCard);
            panelBrand.Controls.Add(lblBrandSub);
            panelBrand.Controls.Add(lblBrandTitle);
            panelBrand.Controls.Add(lblBrandIcon);
            panelBrand.Dock = DockStyle.Left;
            panelBrand.Location = new Point(0, 0);
            panelBrand.Name = "panelBrand";
            panelBrand.Padding = new Padding(35, 40, 35, 30);
            panelBrand.Size = new Size(360, 520);
            panelBrand.TabIndex = 7;
            // 
            // lblBrandVersion
            // 
            lblBrandVersion.Dock = DockStyle.Bottom;
            lblBrandVersion.Font = new Font("Segoe UI", 9F);
            lblBrandVersion.ForeColor = Color.FromArgb(100, 116, 139);
            lblBrandVersion.Location = new Point(35, 465);
            lblBrandVersion.Name = "lblBrandVersion";
            lblBrandVersion.Size = new Size(290, 25);
            lblBrandVersion.TabIndex = 4;
            lblBrandVersion.Text = "MiniMart POS v2.4 • Phiên bản Doanh nghiệp";
            lblBrandVersion.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelBrandCard
            // 
            panelBrandCard.BackColor = Color.FromArgb(24, 33, 47);
            panelBrandCard.Controls.Add(pnlStepLine2);
            panelBrandCard.Controls.Add(pnlStepLine1);
            panelBrandCard.Controls.Add(lblStep3Desc);
            panelBrandCard.Controls.Add(lblStep3Title);
            panelBrandCard.Controls.Add(lblStep3Icon);
            panelBrandCard.Controls.Add(lblStep2Desc);
            panelBrandCard.Controls.Add(lblStep2Title);
            panelBrandCard.Controls.Add(lblStep2Icon);
            panelBrandCard.Controls.Add(lblStep1Desc);
            panelBrandCard.Controls.Add(lblStep1Title);
            panelBrandCard.Controls.Add(lblStep1Icon);
            panelBrandCard.Controls.Add(lblWorkflowTitle);
            panelBrandCard.Location = new Point(32, 185);
            panelBrandCard.Name = "panelBrandCard";
            panelBrandCard.Padding = new Padding(14, 12, 14, 12);
            panelBrandCard.Size = new Size(296, 268);
            panelBrandCard.TabIndex = 3;
            // 
            // lblWorkflowTitle
            // 
            lblWorkflowTitle.AutoSize = true;
            lblWorkflowTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblWorkflowTitle.ForeColor = Color.FromArgb(16, 185, 129);
            lblWorkflowTitle.Location = new Point(14, 12);
            lblWorkflowTitle.Name = "lblWorkflowTitle";
            lblWorkflowTitle.Size = new Size(160, 17);
            lblWorkflowTitle.TabIndex = 0;
            lblWorkflowTitle.Text = "QUY TRÌNH VẬN HÀNH";
            // 
            // lblStep1Icon
            // 
            lblStep1Icon.Font = new Font("Segoe UI", 12F);
            lblStep1Icon.ForeColor = Color.FromArgb(16, 185, 129);
            lblStep1Icon.Location = new Point(14, 40);
            lblStep1Icon.Name = "lblStep1Icon";
            lblStep1Icon.Size = new Size(26, 26);
            lblStep1Icon.TabIndex = 1;
            lblStep1Icon.Text = "📦";
            lblStep1Icon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblStep1Title
            // 
            lblStep1Title.AutoSize = true;
            lblStep1Title.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblStep1Title.ForeColor = Color.White;
            lblStep1Title.Location = new Point(46, 38);
            lblStep1Title.Name = "lblStep1Title";
            lblStep1Title.Size = new Size(140, 15);
            lblStep1Title.TabIndex = 2;
            lblStep1Title.Text = "BƯỚC 01 – NHẬP HÀNG";
            // 
            // lblStep1Desc
            // 
            lblStep1Desc.Font = new Font("Segoe UI", 8.5F);
            lblStep1Desc.ForeColor = Color.FromArgb(148, 163, 184);
            lblStep1Desc.Location = new Point(46, 56);
            lblStep1Desc.Name = "lblStep1Desc";
            lblStep1Desc.Size = new Size(236, 32);
            lblStep1Desc.TabIndex = 3;
            lblStep1Desc.Text = "Quản lý sản phẩm, nhập kho và nhà cung cấp.";
            // 
            // pnlStepLine1
            // 
            pnlStepLine1.BackColor = Color.FromArgb(16, 185, 129);
            pnlStepLine1.Location = new Point(26, 68);
            pnlStepLine1.Name = "pnlStepLine1";
            pnlStepLine1.Size = new Size(2, 44);
            pnlStepLine1.TabIndex = 4;
            // 
            // lblStep2Icon
            // 
            lblStep2Icon.Font = new Font("Segoe UI", 12F);
            lblStep2Icon.ForeColor = Color.FromArgb(16, 185, 129);
            lblStep2Icon.Location = new Point(14, 114);
            lblStep2Icon.Name = "lblStep2Icon";
            lblStep2Icon.Size = new Size(26, 26);
            lblStep2Icon.TabIndex = 5;
            lblStep2Icon.Text = "🛒";
            lblStep2Icon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblStep2Title
            // 
            lblStep2Title.AutoSize = true;
            lblStep2Title.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblStep2Title.ForeColor = Color.White;
            lblStep2Title.Location = new Point(46, 112);
            lblStep2Title.Name = "lblStep2Title";
            lblStep2Title.Size = new Size(130, 15);
            lblStep2Title.TabIndex = 6;
            lblStep2Title.Text = "BƯỚC 02 – BÁN HÀNG";
            // 
            // lblStep2Desc
            // 
            lblStep2Desc.Font = new Font("Segoe UI", 8.5F);
            lblStep2Desc.ForeColor = Color.FromArgb(148, 163, 184);
            lblStep2Desc.Location = new Point(46, 130);
            lblStep2Desc.Name = "lblStep2Desc";
            lblStep2Desc.Size = new Size(236, 32);
            lblStep2Desc.TabIndex = 7;
            lblStep2Desc.Text = "Thanh toán nhanh chóng và xử lý giao dịch tại quầy POS.";
            // 
            // pnlStepLine2
            // 
            pnlStepLine2.BackColor = Color.FromArgb(16, 185, 129);
            pnlStepLine2.Location = new Point(26, 142);
            pnlStepLine2.Name = "pnlStepLine2";
            pnlStepLine2.Size = new Size(2, 44);
            pnlStepLine2.TabIndex = 8;
            // 
            // lblStep3Icon
            // 
            lblStep3Icon.Font = new Font("Segoe UI", 12F);
            lblStep3Icon.ForeColor = Color.FromArgb(16, 185, 129);
            lblStep3Icon.Location = new Point(14, 188);
            lblStep3Icon.Name = "lblStep3Icon";
            lblStep3Icon.Size = new Size(26, 26);
            lblStep3Icon.TabIndex = 9;
            lblStep3Icon.Text = "📊";
            lblStep3Icon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblStep3Title
            // 
            lblStep3Title.AutoSize = true;
            lblStep3Title.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblStep3Title.ForeColor = Color.White;
            lblStep3Title.Location = new Point(46, 186);
            lblStep3Title.Name = "lblStep3Title";
            lblStep3Title.Size = new Size(125, 15);
            lblStep3Title.TabIndex = 10;
            lblStep3Title.Text = "BƯỚC 03 – QUẢN LÝ";
            // 
            // lblStep3Desc
            // 
            lblStep3Desc.Font = new Font("Segoe UI", 8.5F);
            lblStep3Desc.ForeColor = Color.FromArgb(148, 163, 184);
            lblStep3Desc.Location = new Point(46, 204);
            lblStep3Desc.Name = "lblStep3Desc";
            lblStep3Desc.Size = new Size(236, 32);
            lblStep3Desc.TabIndex = 11;
            lblStep3Desc.Text = "Theo dõi doanh thu, tồn kho và hiệu quả kinh doanh.";
            // 
            // lblBrandSub
            // 
            lblBrandSub.Font = new Font("Segoe UI", 9.5F);
            lblBrandSub.ForeColor = Color.FromArgb(148, 163, 184);
            lblBrandSub.Location = new Point(35, 136);
            lblBrandSub.Name = "lblBrandSub";
            lblBrandSub.Size = new Size(290, 42);
            lblBrandSub.TabIndex = 2;
            lblBrandSub.Text = "Hệ thống Quản lý Bán lẻ & Tồn kho Siêu thị Nông sản Hữu cơ";
            // 
            // lblBrandTitle
            // 
            lblBrandTitle.AutoSize = true;
            lblBrandTitle.Font = new Font("Segoe UI", 21F, FontStyle.Bold);
            lblBrandTitle.ForeColor = Color.White;
            lblBrandTitle.Location = new Point(35, 94);
            lblBrandTitle.Name = "lblBrandTitle";
            lblBrandTitle.Size = new Size(215, 38);
            lblBrandTitle.TabIndex = 1;
            lblBrandTitle.Text = "MINIMART POS";
            // 
            // lblBrandIcon
            // 
            lblBrandIcon.AutoSize = true;
            lblBrandIcon.Font = new Font("Segoe UI", 36F);
            lblBrandIcon.ForeColor = Color.FromArgb(16, 185, 129);
            lblBrandIcon.Location = new Point(28, 26);
            lblBrandIcon.Name = "lblBrandIcon";
            lblBrandIcon.Size = new Size(130, 65);
            lblBrandIcon.TabIndex = 0;
            lblBrandIcon.Text = "🌿🛒";
            // 
            // panelLogin
            // 
            panelLogin.BackColor = Color.White;
            panelLogin.Controls.Add(lblMessage);
            panelLogin.Controls.Add(btnLogin);
            panelLogin.Controls.Add(txtPass);
            panelLogin.Controls.Add(lblPass);
            panelLogin.Controls.Add(txtUser);
            panelLogin.Controls.Add(lblUser);
            panelLogin.Controls.Add(lblWelcomeSub);
            panelLogin.Controls.Add(lblWelcome);
            panelLogin.Controls.Add(lblTitle);
            panelLogin.Dock = DockStyle.Fill;
            panelLogin.Location = new Point(360, 0);
            panelLogin.Name = "panelLogin";
            panelLogin.Padding = new Padding(50, 45, 50, 35);
            panelLogin.Size = new Size(464, 520);
            panelLogin.TabIndex = 8;
            // 
            // lblMessage
            // 
            lblMessage.Font = new Font("Segoe UI", 9.5F);
            lblMessage.ForeColor = Color.FromArgb(220, 38, 38);
            lblMessage.Location = new Point(50, 405);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(364, 30);
            lblMessage.TabIndex = 8;
            lblMessage.TextAlign = ContentAlignment.MiddleCenter;
            lblMessage.Click += lblMessage_Click;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(5, 150, 105);
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(50, 340);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(364, 50);
            btnLogin.TabIndex = 7;
            btnLogin.Text = "ĐĂNG NHẬP HỆ THỐNG →";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // txtPass
            // 
            txtPass.BorderStyle = BorderStyle.FixedSingle;
            txtPass.Font = new Font("Segoe UI", 11.5F);
            txtPass.Location = new Point(50, 272);
            txtPass.Name = "txtPass";
            txtPass.PasswordChar = '●';
            txtPass.PlaceholderText = "Nhập mật khẩu của bạn...";
            txtPass.Size = new Size(364, 28);
            txtPass.TabIndex = 6;
            txtPass.UseSystemPasswordChar = true;
            // 
            // lblPass
            // 
            lblPass.AutoSize = true;
            lblPass.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPass.ForeColor = Color.FromArgb(15, 23, 42);
            lblPass.Location = new Point(48, 245);
            lblPass.Name = "lblPass";
            lblPass.Size = new Size(95, 19);
            lblPass.TabIndex = 5;
            lblPass.Text = "🔒 Mật khẩu:";
            // 
            // txtUser
            // 
            txtUser.BorderStyle = BorderStyle.FixedSingle;
            txtUser.Font = new Font("Segoe UI", 11.5F);
            txtUser.Location = new Point(50, 192);
            txtUser.Name = "txtUser";
            txtUser.PlaceholderText = "Nhập tên đăng nhập...";
            txtUser.Size = new Size(364, 28);
            txtUser.TabIndex = 4;
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblUser.ForeColor = Color.FromArgb(15, 23, 42);
            lblUser.Location = new Point(48, 165);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(160, 19);
            lblUser.TabIndex = 3;
            lblUser.Text = "👤 Tài khoản nhân viên:";
            // 
            // lblWelcomeSub
            // 
            lblWelcomeSub.Font = new Font("Segoe UI", 10F);
            lblWelcomeSub.ForeColor = Color.FromArgb(100, 116, 139);
            lblWelcomeSub.Location = new Point(48, 98);
            lblWelcomeSub.Name = "lblWelcomeSub";
            lblWelcomeSub.Size = new Size(366, 45);
            lblWelcomeSub.TabIndex = 2;
            lblWelcomeSub.Text = "Vui lòng nhập thông tin xác thực để bắt đầu ca làm việc và kết nối với trạm POS.";
            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = true;
            lblWelcome.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblWelcome.ForeColor = Color.FromArgb(15, 23, 42);
            lblWelcome.Location = new Point(46, 55);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(286, 32);
            lblWelcome.TabIndex = 1;
            lblWelcome.Text = "Chào mừng trở lại! 👋";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 9F);
            lblTitle.ForeColor = Color.Transparent;
            lblTitle.Location = new Point(0, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(0, 15);
            lblTitle.TabIndex = 0;
            // 
            // FormLogin
            // 
            AcceptButton = btnLogin;
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(824, 520);
            Controls.Add(panelLogin);
            Controls.Add(panelBrand);
            Font = new Font("Segoe UI", 9.75F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MiniMart POS - Đăng nhập hệ thống";
            Load += FormLogin_Load;
            panelBrand.ResumeLayout(false);
            panelBrand.PerformLayout();
            panelBrandCard.ResumeLayout(false);
            panelBrandCard.PerformLayout();
            panelLogin.ResumeLayout(false);
            panelLogin.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelBrand;
        private Label lblBrandIcon;
        private Label lblBrandTitle;
        private Label lblBrandSub;
        private Panel panelBrandCard;
        private Label lblWorkflowTitle;
        private Label lblStep1Icon;
        private Label lblStep1Title;
        private Label lblStep1Desc;
        private Panel pnlStepLine1;
        private Label lblStep2Icon;
        private Label lblStep2Title;
        private Label lblStep2Desc;
        private Panel pnlStepLine2;
        private Label lblStep3Icon;
        private Label lblStep3Title;
        private Label lblStep3Desc;
        private Label lblBrandVersion;
        private Panel panelLogin;
        private Label lblWelcome;
        private Label lblWelcomeSub;
        internal Label lblTitle;
        internal Label lblUser;
        internal TextBox txtUser;
        internal Label lblPass;
        internal TextBox txtPass;
        internal Button btnLogin;
        internal Label lblMessage;
    }
}
