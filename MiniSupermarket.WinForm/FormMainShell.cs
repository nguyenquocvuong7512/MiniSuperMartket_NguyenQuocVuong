using MiniSupermarket.WinForms.Forms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormMainShell : Form
    {
        private Form? _activeForm = null;

        public FormMainShell()
        {
            InitializeComponent();

            // Đăng ký các sự kiện Click
            btnPOS.Click += btnPOS_Click;
            btnCategory.Click += btnCategory_Click;
            btnProduct.Click += btnProduct_Click;
            btnCustomer.Click += btnCustomer_Click;
            btnReports.Click += btnReports_Click;
            btnUserManage.Click += btnUserManage_Click;
            btnLogout.Click += btnLogout_Click;

            this.Load += FormMainShell_Load;
            this.Resize += FormMainShell_Resize;
        }

        private void FormMainShell_Load(object? sender, EventArgs e)
        {
            string role = SessionManager.CurrentRole;

            lblUserInfo.Text = $"Nhân viên: {SessionManager.CurrentUsername} | Vai trò: {role}";
            UpdateUserInfoPosition();

            ApplyRolePermissions(role);
            OpenDefaultScreenByRole(role);
        }

        private void FormMainShell_Resize(object? sender, EventArgs e)
        {
            UpdateUserInfoPosition();
        }

        private void UpdateUserInfoPosition()
        {
            if (panelTopHeader != null && lblUserInfo != null)
            {
                lblUserInfo.Location = new Point(panelTopHeader.Width - lblUserInfo.PreferredWidth - 20, 20);
            }
        }

        // =====================================================
        // PHÂN QUYỀN MENU
        // =====================================================

        private void ApplyRolePermissions(string role)
        {
            btnPOS.Visible = false;
            btnCategory.Visible = false;
            btnProduct.Visible = false;
            btnCustomer.Visible = false;
            btnReports.Visible = false;
            btnUserManage.Visible = false;

            switch (role.Trim().ToUpper())
            {
                case "ADMIN":
                    btnPOS.Visible = true;
                    btnCategory.Visible = true;
                    btnProduct.Visible = true;
                    btnCustomer.Visible = true;
                    btnReports.Visible = true;
                    btnUserManage.Visible = true;
                    break;

                case "CASHIER":
                    btnPOS.Visible = true;
                    btnCustomer.Visible = true;
                    break;

                case "WAREHOUSE":
                    btnCategory.Visible = true;
                    btnProduct.Visible = true;
                    break;

                default:
                    MessageBox.Show(
                        "Tài khoản chưa được cấp quyền hợp lệ!",
                        "Cảnh báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    this.Close();
                    break;
            }
        }

        // =====================================================
        // MỞ FORM MẶC ĐỊNH
        // =====================================================

        private void OpenDefaultScreenByRole(string role)
        {
            switch (role.Trim().ToUpper())
            {
                case "ADMIN":
                    OpenChildForm(
                        new FormCategoryManagement(),
                        "QUẢN LÝ DANH MỤC",
                        btnCategory
                    );
                    break;

                case "CASHIER":
                    OpenChildForm(
                        new FormCustomerManagement(),
                        "QUẢN LÝ KHÁCH HÀNG",
                        btnCustomer
                    );
                    break;

                case "WAREHOUSE":
                    OpenChildForm(
                        new FormCategoryManagement(),
                        "QUẢN LÝ DANH MỤC",
                        btnCategory
                    );
                    break;
            }
        }

        // =====================================================
        // NHÚNG FORM CON
        // =====================================================

        private void OpenChildForm(
            Form childForm,
            string screenTitle,
            Button senderButton)
        {
            if (_activeForm != null)
            {
                _activeForm.Close();
            }

            HighlightActiveButton(senderButton);

            _activeForm = childForm;

            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            panelMainContent.Controls.Clear();
            panelMainContent.Controls.Add(childForm);
            panelMainContent.Tag = childForm;

            lblTitle.Text = screenTitle;

            childForm.BringToFront();
            childForm.Show();
        }

        // =====================================================
        // HIGHLIGHT MENU
        // =====================================================

        private void HighlightActiveButton(Button activeButton)
        {
            foreach (Control ctrl in panelSidebar.Controls)
            {
                if (ctrl is Button btn && btn != btnLogout)
                {
                    btn.BackColor = Color.FromArgb(24, 30, 48);
                }
            }

            activeButton.BackColor = Color.FromArgb(41, 100, 180);
        }

        // =====================================================
        // CHỨC NĂNG CÁC NÚT BẤM
        // =====================================================

        private void btnCategory_Click(object? sender, EventArgs e)
        {
            if (!IsAllowed("ADMIN", "WAREHOUSE"))
                return;

            OpenChildForm(
                new FormCategoryManagement(),
                "QUẢN LÝ DANH MỤC",
                btnCategory
            );
        }

        private void btnCustomer_Click(object? sender, EventArgs e)
        {
            if (!IsAllowed("ADMIN", "CASHIER"))
                return;

            OpenChildForm(
                new FormCustomerManagement(),
                "QUẢN LÝ KHÁCH HÀNG",
                btnCustomer
            );
        }

        private void btnPOS_Click(object? sender, EventArgs e)
        {
            if (!IsAllowed("ADMIN", "CASHIER"))
                return;

            OpenChildForm(
                new FormPOS(),
                "MÀN HÌNH POS",
                btnPOS
            );
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            if (!IsAllowed("ADMIN", "WAREHOUSE"))
                return;

            OpenChildForm(
                new FormProductManagement(),
                "QUẢN LÝ SẢN PHẨM & KHO HÀNG",
                btnProduct
            );
        }

        private void btnReports_Click(object? sender, EventArgs e)
        {
            if (!IsAllowed("ADMIN"))
                return;

            MessageBox.Show(
                "Màn hình báo cáo sẽ triển khai tiếp.",
                "Báo cáo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void btnUserManage_Click(object? sender, EventArgs e)
        {
            if (!IsAllowed("ADMIN"))
                return;

            MessageBox.Show(
                "Màn hình quản lý tài khoản sẽ triển khai tiếp.",
                "Tài khoản",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        // =====================================================
        // KIỂM TRA QUYỀN
        // =====================================================

        private bool IsAllowed(params string[] allowedRoles)
        {
            string currentRole =
                SessionManager.CurrentRole.Trim().ToUpper();

            foreach (string role in allowedRoles)
            {
                if (currentRole == role.ToUpper())
                    return true;
            }

            MessageBox.Show(
                "Bạn không có quyền truy cập chức năng này!",
                "Từ chối truy cập",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );

            return false;
        }

        // =====================================================
        // LOGOUT
        // =====================================================

        private void btnLogout_Click(object? sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn đăng xuất?",
                "Đăng xuất",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result != DialogResult.Yes)
                return;

            SessionManager.JwtToken = string.Empty;
            SessionManager.CurrentUsername = string.Empty;
            SessionManager.CurrentRole = string.Empty;

            this.Hide();

            FormLogin login = new FormLogin();

            login.ShowDialog();

            this.Close();
        }

        private void panelMainContent_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}