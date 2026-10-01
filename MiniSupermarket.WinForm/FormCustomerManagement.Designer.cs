using System.Drawing;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    partial class FormCustomerManagement
    {
        // === Khai báo các điều khiển ===
        private System.ComponentModel.IContainer components = null;

        internal Label lblId;
        internal TextBox txtCustomerId;
        internal Label lblName;
        internal TextBox txtCustomerName;
        internal Label lblPhone;
        internal TextBox txtPhoneNumber;
        internal Label lblAddress;
        internal TextBox txtAddress;
        internal Label lblPoints;
        internal TextBox txtRewardPoints;
        internal Label lblRank;
        internal TextBox txtMembershipRank;
        internal Label lblSearch;
        internal TextBox txtSearch;
        internal Button btnLoad;
        internal Button btnAdd;
        internal Button btnUpdate;
        internal Button btnDelete;
        internal Button btnSearch;
        internal DataGridView dgvCustomers;

        // === Giải phóng tài nguyên ===
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        // === THIẾT KẾ GIAO DIỆN ===
        private void InitializeComponent()
        {
            // === Khởi tạo các điều khiển ===
            lblId = new Label();
            txtCustomerId = new TextBox();
            lblName = new Label();
            txtCustomerName = new TextBox();
            lblPhone = new Label();
            txtPhoneNumber = new TextBox();
            lblAddress = new Label();
            txtAddress = new TextBox();
            lblPoints = new Label();
            txtRewardPoints = new TextBox();
            lblRank = new Label();
            txtMembershipRank = new TextBox();
            lblSearch = new Label();
            txtSearch = new TextBox();
            btnLoad = new Button();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnSearch = new Button();
            dgvCustomers = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).BeginInit();

            // === Cấu hình Form chính ===
            SuspendLayout();
            Text = "QUẢN LÝ THÔNG TIN KHÁCH HÀNG";
            Size = new Size(920, 580);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.WhiteSmoke;
            Font = new Font("Segoe UI", 9f);
            Padding = new Padding(15);

            const int labelWidth = 120;
            const int inputWidth = 280;
            const int btnHeight = 35;
            int y = 15;

            // === DÒNG 1: Mã KH ===
            lblId.Text = "Mã KH:";
            lblId.Location = new Point(0, y);
            lblId.AutoSize = true;
            txtCustomerId.Location = new Point(labelWidth, y);
            txtCustomerId.Width = 150;
            txtCustomerId.ReadOnly = true;
            txtCustomerId.BackColor = Color.LightGray;

            // === DÒNG 2: Tên KH ===
            y += 45;
            lblName.Text = "Tên khách hàng:";
            lblName.Location = new Point(0, y);
            lblName.AutoSize = true;
            txtCustomerName.Location = new Point(labelWidth, y);
            txtCustomerName.Width = inputWidth;

            // === DÒNG 3: SĐT ===
            y += 45;
            lblPhone.Text = "Số điện thoại:";
            lblPhone.Location = new Point(0, y);
            lblPhone.AutoSize = true;
            txtPhoneNumber.Location = new Point(labelWidth, y);
            txtPhoneNumber.Width = inputWidth;

            // === DÒNG 4: Địa chỉ ===
            y += 45;
            lblAddress.Text = "Địa chỉ:";
            lblAddress.Location = new Point(0, y);
            lblAddress.AutoSize = true;
            txtAddress.Location = new Point(labelWidth, y);
            txtAddress.Width = inputWidth;
            txtAddress.Height = 60;
            txtAddress.Multiline = true;

            // === DÒNG 5: Điểm & Hạng ===
            y += 75;
            lblPoints.Text = "Điểm tích lũy:";
            lblPoints.Location = new Point(0, y);
            lblPoints.AutoSize = true;
            txtRewardPoints.Location = new Point(labelWidth, y);
            txtRewardPoints.Width = 120;
            txtRewardPoints.Text = "0";

            lblRank.Text = "Hạng thẻ:";
            lblRank.Location = new Point(labelWidth + 150, y);
            lblRank.AutoSize = true;
            txtMembershipRank.Location = new Point(labelWidth + 230, y);
            txtMembershipRank.Width = 120;
            txtMembershipRank.Text = "Chuẩn";

            // === DÒNG NÚT CHỨC NĂNG ===
            y += 50;
            btnLoad.Text = "📋 Tải danh sách";
            btnLoad.Location = new Point(0, y);
            btnLoad.Size = new Size(140, btnHeight);
            btnLoad.BackColor = Color.LightBlue;

            btnAdd.Text = "➕ Thêm mới";
            btnAdd.Location = new Point(150, y);
            btnAdd.Size = new Size(140, btnHeight);
            btnAdd.BackColor = Color.LightGreen;

            btnUpdate.Text = "✏️ Cập nhật";
            btnUpdate.Location = new Point(300, y);
            btnUpdate.Size = new Size(140, btnHeight);
            btnUpdate.BackColor = Color.LightYellow;

            btnDelete.Text = "🗑️ Xóa";
            btnDelete.Location = new Point(450, y);
            btnDelete.Size = new Size(140, btnHeight);
            btnDelete.BackColor = Color.LightCoral;

            // === DÒNG TÌM KIẾM ===
            y += btnHeight + 15;
            lblSearch.Text = "Tìm kiếm theo tên/SĐT:";
            lblSearch.Location = new Point(0, y);
            lblSearch.AutoSize = true;
            txtSearch.Location = new Point(180, y);
            txtSearch.Width = 300;
            btnSearch.Text = "🔍 Tìm";
            btnSearch.Location = new Point(490, y);
            btnSearch.Size = new Size(100, btnHeight);
            btnSearch.BackColor = Color.LightSkyBlue;

            // === DATA GRID VIEW ===
            y += btnHeight + 15;
            dgvCustomers.Location = new Point(0, y);
            dgvCustomers.Dock = DockStyle.Bottom;
            dgvCustomers.Height = 280;
            dgvCustomers.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCustomers.ReadOnly = true;
            dgvCustomers.AllowUserToAddRows = false;
            dgvCustomers.RowHeadersVisible = false;

            // === Thêm điều khiển vào Form ===
            Controls.AddRange(new Control[] {
                lblId, txtCustomerId,
                lblName, txtCustomerName,
                lblPhone, txtPhoneNumber,
                lblAddress, txtAddress,
                lblPoints, txtRewardPoints,
                lblRank, txtMembershipRank,
                btnLoad, btnAdd, btnUpdate, btnDelete,
                lblSearch, txtSearch, btnSearch,
                dgvCustomers
            });

            ((System.ComponentModel.ISupportInitialize)dgvCustomers).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}