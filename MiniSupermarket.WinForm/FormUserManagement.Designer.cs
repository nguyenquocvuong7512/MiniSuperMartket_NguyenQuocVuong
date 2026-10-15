namespace MiniSupermarket.WinForms
{
    partial class FormUserManagement
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
            panelRight = new Panel();
            btnRefresh = new Button();
            btnDeleteUser = new Button();
            btnResetPassword = new Button();
            btnAddUser = new Button();
            cboRole = new ComboBox();
            lblRole = new Label();
            txtFullName = new TextBox();
            lblFullName = new Label();
            txtPassword = new TextBox();
            lblPassword = new Label();
            txtUsername = new TextBox();
            lblUsername = new Label();
            lblPanelTitle = new Label();
            panelMain = new Panel();
            dgvUsers = new DataGridView();
            panelMainHeader = new Panel();
            lblUserCount = new Label();
            lblListTitle = new Label();
            panelRight.SuspendLayout();
            panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
            panelMainHeader.SuspendLayout();
            SuspendLayout();
            // 
            // panelRight
            // 
            panelRight.BackColor = Color.White;
            panelRight.Controls.Add(btnRefresh);
            panelRight.Controls.Add(btnDeleteUser);
            panelRight.Controls.Add(btnResetPassword);
            panelRight.Controls.Add(btnAddUser);
            panelRight.Controls.Add(cboRole);
            panelRight.Controls.Add(lblRole);
            panelRight.Controls.Add(txtFullName);
            panelRight.Controls.Add(lblFullName);
            panelRight.Controls.Add(txtPassword);
            panelRight.Controls.Add(lblPassword);
            panelRight.Controls.Add(txtUsername);
            panelRight.Controls.Add(lblUsername);
            panelRight.Controls.Add(lblPanelTitle);
            panelRight.Dock = DockStyle.Right;
            panelRight.Location = new Point(690, 15);
            panelRight.Name = "panelRight";
            panelRight.Padding = new Padding(20);
            panelRight.Size = new Size(345, 590);
            panelRight.TabIndex = 1;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.FromArgb(108, 117, 125);
            btnRefresh.Cursor = Cursors.Hand;
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(20, 465);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(305, 38);
            btnRefresh.TabIndex = 12;
            btnRefresh.Text = "🔄 Tải Lại Danh Sách";
            btnRefresh.UseVisualStyleBackColor = false;
            // 
            // btnDeleteUser
            // 
            btnDeleteUser.BackColor = Color.FromArgb(220, 53, 69);
            btnDeleteUser.Cursor = Cursors.Hand;
            btnDeleteUser.FlatAppearance.BorderSize = 0;
            btnDeleteUser.FlatStyle = FlatStyle.Flat;
            btnDeleteUser.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnDeleteUser.ForeColor = Color.White;
            btnDeleteUser.Location = new Point(20, 415);
            btnDeleteUser.Name = "btnDeleteUser";
            btnDeleteUser.Size = new Size(305, 38);
            btnDeleteUser.TabIndex = 11;
            btnDeleteUser.Text = "🗑 Xóa Tài Khoản";
            btnDeleteUser.UseVisualStyleBackColor = false;
            // 
            // btnResetPassword
            // 
            btnResetPassword.BackColor = Color.FromArgb(255, 152, 0);
            btnResetPassword.Cursor = Cursors.Hand;
            btnResetPassword.FlatAppearance.BorderSize = 0;
            btnResetPassword.FlatStyle = FlatStyle.Flat;
            btnResetPassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnResetPassword.ForeColor = Color.White;
            btnResetPassword.Location = new Point(20, 365);
            btnResetPassword.Name = "btnResetPassword";
            btnResetPassword.Size = new Size(305, 38);
            btnResetPassword.TabIndex = 10;
            btnResetPassword.Text = "🔑 Đặt Lại Mật Khẩu (123456)";
            btnResetPassword.UseVisualStyleBackColor = false;
            // 
            // btnAddUser
            // 
            btnAddUser.BackColor = Color.FromArgb(40, 167, 69);
            btnAddUser.Cursor = Cursors.Hand;
            btnAddUser.FlatAppearance.BorderSize = 0;
            btnAddUser.FlatStyle = FlatStyle.Flat;
            btnAddUser.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAddUser.ForeColor = Color.White;
            btnAddUser.Location = new Point(20, 315);
            btnAddUser.Name = "btnAddUser";
            btnAddUser.Size = new Size(305, 38);
            btnAddUser.TabIndex = 9;
            btnAddUser.Text = "➕ Thêm Tài Khoản Mới";
            btnAddUser.UseVisualStyleBackColor = false;
            // 
            // cboRole
            // 
            cboRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRole.Font = new Font("Segoe UI", 10.5F);
            cboRole.FormattingEnabled = true;
            cboRole.Location = new Point(20, 260);
            cboRole.Name = "cboRole";
            cboRole.Size = new Size(305, 27);
            cboRole.TabIndex = 8;
            // 
            // lblRole
            // 
            lblRole.AutoSize = true;
            lblRole.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblRole.ForeColor = Color.FromArgb(64, 64, 64);
            lblRole.Location = new Point(20, 238);
            lblRole.Name = "lblRole";
            lblRole.Size = new Size(53, 17);
            lblRole.TabIndex = 7;
            lblRole.Text = "Vai trò:";
            // 
            // txtFullName
            // 
            txtFullName.Font = new Font("Segoe UI", 10.5F);
            txtFullName.Location = new Point(20, 195);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(305, 26);
            txtFullName.TabIndex = 6;
            // 
            // lblFullName
            // 
            lblFullName.AutoSize = true;
            lblFullName.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblFullName.ForeColor = Color.FromArgb(64, 64, 64);
            lblFullName.Location = new Point(20, 173);
            lblFullName.Name = "lblFullName";
            lblFullName.Size = new Size(72, 17);
            lblFullName.TabIndex = 5;
            lblFullName.Text = "Họ và tên:";
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Segoe UI", 10.5F);
            txtPassword.Location = new Point(20, 130);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(305, 26);
            txtPassword.TabIndex = 4;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblPassword.ForeColor = Color.FromArgb(64, 64, 64);
            lblPassword.Location = new Point(20, 108);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(70, 17);
            lblPassword.TabIndex = 3;
            lblPassword.Text = "Mật khẩu:";
            // 
            // txtUsername
            // 
            txtUsername.Font = new Font("Segoe UI", 10.5F);
            txtUsername.Location = new Point(20, 65);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(305, 26);
            txtUsername.TabIndex = 2;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblUsername.ForeColor = Color.FromArgb(64, 64, 64);
            lblUsername.Location = new Point(20, 43);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(105, 17);
            lblUsername.TabIndex = 1;
            lblUsername.Text = "Tên đăng nhập:";
            // 
            // lblPanelTitle
            // 
            lblPanelTitle.AutoSize = true;
            lblPanelTitle.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            lblPanelTitle.ForeColor = Color.FromArgb(24, 30, 48);
            lblPanelTitle.Location = new Point(20, 15);
            lblPanelTitle.Name = "lblPanelTitle";
            lblPanelTitle.Size = new Size(189, 21);
            lblPanelTitle.TabIndex = 0;
            lblPanelTitle.Text = "THÔNG TIN TÀI KHOẢN";
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.White;
            panelMain.Controls.Add(dgvUsers);
            panelMain.Controls.Add(panelMainHeader);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(15, 15);
            panelMain.Margin = new Padding(0, 0, 15, 0);
            panelMain.Name = "panelMain";
            panelMain.Padding = new Padding(15);
            panelMain.Size = new Size(660, 590);
            panelMain.TabIndex = 2;
            // 
            // dgvUsers
            // 
            dgvUsers.AllowUserToAddRows = false;
            dgvUsers.AllowUserToDeleteRows = false;
            dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsers.BackgroundColor = Color.White;
            dgvUsers.BorderStyle = BorderStyle.None;
            dgvUsers.ColumnHeadersHeight = 35;
            dgvUsers.Dock = DockStyle.Fill;
            dgvUsers.Location = new Point(15, 60);
            dgvUsers.MultiSelect = false;
            dgvUsers.Name = "dgvUsers";
            dgvUsers.ReadOnly = true;
            dgvUsers.RowHeadersVisible = false;
            dgvUsers.RowTemplate.Height = 30;
            dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.Size = new Size(630, 515);
            dgvUsers.TabIndex = 1;
            // 
            // panelMainHeader
            // 
            panelMainHeader.Controls.Add(lblUserCount);
            panelMainHeader.Controls.Add(lblListTitle);
            panelMainHeader.Dock = DockStyle.Top;
            panelMainHeader.Location = new Point(15, 15);
            panelMainHeader.Name = "panelMainHeader";
            panelMainHeader.Size = new Size(630, 45);
            panelMainHeader.TabIndex = 0;
            // 
            // lblUserCount
            // 
            lblUserCount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblUserCount.AutoSize = true;
            lblUserCount.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
            lblUserCount.ForeColor = Color.DimGray;
            lblUserCount.Location = new Point(480, 12);
            lblUserCount.Name = "lblUserCount";
            lblUserCount.Size = new Size(137, 19);
            lblUserCount.TabIndex = 1;
            lblUserCount.Text = "Tổng: 0 người dùng";
            // 
            // lblListTitle
            // 
            lblListTitle.AutoSize = true;
            lblListTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblListTitle.ForeColor = Color.FromArgb(24, 30, 48);
            lblListTitle.Location = new Point(0, 10);
            lblListTitle.Name = "lblListTitle";
            lblListTitle.Size = new Size(199, 21);
            lblListTitle.TabIndex = 0;
            lblListTitle.Text = "DANH SÁCH TÀI KHOẢN";
            // 
            // FormUserManagement
            // 
            BackColor = Color.FromArgb(244, 245, 247);
            ClientSize = new Size(1050, 620);
            Controls.Add(panelMain);
            Controls.Add(panelRight);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormUserManagement";
            Padding = new Padding(15);
            panelRight.ResumeLayout(false);
            panelRight.PerformLayout();
            panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
            panelMainHeader.ResumeLayout(false);
            panelMainHeader.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelRight;
        private Label lblPanelTitle;
        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblPassword;
        private TextBox txtPassword;
        private Label lblFullName;
        private TextBox txtFullName;
        private Label lblRole;
        private ComboBox cboRole;
        private Button btnAddUser;
        private Button btnResetPassword;
        private Button btnDeleteUser;
        private Button btnRefresh;
        private Panel panelMain;
        private Panel panelMainHeader;
        private Label lblListTitle;
        private Label lblUserCount;
        private DataGridView dgvUsers;
    }
}
