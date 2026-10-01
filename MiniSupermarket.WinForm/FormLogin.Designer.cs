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
            lblTitle = new Label();
            lblUser = new Label();
            txtUser = new TextBox();
            lblPass = new Label();
            txtPass = new TextBox();
            btnLogin = new Button();
            lblMessage = new Label();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(30, 30, 80);
            lblTitle.Location = new Point(96, 11);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(183, 30);
            lblTitle.TabIndex = 6;
            lblTitle.Text = "🔐 ĐĂNG NHẬP";
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.Font = new Font("Segoe UI", 10F);
            lblUser.Location = new Point(44, 56);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(69, 19);
            lblUser.TabIndex = 5;
            lblUser.Text = "Tài khoản:";
            // 
            // txtUser
            // 
            txtUser.Font = new Font("Segoe UI", 10F);
            txtUser.Location = new Point(131, 55);
            txtUser.Margin = new Padding(3, 2, 3, 2);
            txtUser.Name = "txtUser";
            txtUser.Size = new Size(176, 25);
            txtUser.TabIndex = 1;
            // 
            // lblPass
            // 
            lblPass.AutoSize = true;
            lblPass.Font = new Font("Segoe UI", 10F);
            lblPass.Location = new Point(44, 90);
            lblPass.Name = "lblPass";
            lblPass.Size = new Size(71, 19);
            lblPass.TabIndex = 4;
            lblPass.Text = "Mật khẩu:";
            // 
            // txtPass
            // 
            txtPass.Font = new Font("Segoe UI", 10F);
            txtPass.Location = new Point(131, 88);
            txtPass.Margin = new Padding(3, 2, 3, 2);
            txtPass.Name = "txtPass";
            txtPass.PasswordChar = '●';
            txtPass.Size = new Size(176, 25);
            txtPass.TabIndex = 2;
            txtPass.UseSystemPasswordChar = true;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(45, 130, 200);
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(131, 128);
            btnLogin.Margin = new Padding(3, 2, 3, 2);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(175, 30);
            btnLogin.TabIndex = 3;
            btnLogin.Text = "Đăng nhập hệ thống";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // lblMessage
            // 
            lblMessage.Font = new Font("Segoe UI", 9F);
            lblMessage.Location = new Point(44, 165);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(262, 19);
            lblMessage.TabIndex = 0;
            lblMessage.TextAlign = ContentAlignment.MiddleCenter;
            lblMessage.Click += lblMessage_Click;
            // 
            // FormLogin
            // 
            AcceptButton = btnLogin;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(350, 202);
            Controls.Add(lblMessage);
            Controls.Add(btnLogin);
            Controls.Add(txtPass);
            Controls.Add(lblPass);
            Controls.Add(txtUser);
            Controls.Add(lblUser);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng nhập hệ thống";
            Load += FormLogin_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        internal System.Windows.Forms.Label lblTitle;
        internal System.Windows.Forms.Label lblUser;
        internal System.Windows.Forms.TextBox txtUser;
        internal System.Windows.Forms.Label lblPass;
        internal System.Windows.Forms.TextBox txtPass;
        internal System.Windows.Forms.Button btnLogin;
        internal System.Windows.Forms.Label lblMessage;
    }
}