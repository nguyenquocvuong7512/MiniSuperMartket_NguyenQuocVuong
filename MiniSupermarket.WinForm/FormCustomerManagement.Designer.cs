using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    partial class FormCustomerManagement
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
            panelTop = new Panel();
            btnLoad = new Button();
            btnSearch = new Button();
            txtKeyword = new TextBox();
            lblSearch = new Label();
            panelBody = new Panel();
            dgvCustomers = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colName = new DataGridViewTextBoxColumn();
            colPhone = new DataGridViewTextBoxColumn();
            colAddress = new DataGridViewTextBoxColumn();
            colPoints = new DataGridViewTextBoxColumn();
            colRank = new DataGridViewTextBoxColumn();
            panelInfo = new Panel();
            lblInfoTitle = new Label();
            cbMembershipRank = new ComboBox();
            lblMembershipRank = new Label();
            txtRewardPoints = new TextBox();
            lblRewardPoints = new Label();
            txtAddress = new TextBox();
            lblAddress = new Label();
            txtPhoneNumber = new TextBox();
            lblPhoneNumber = new Label();
            txtCustomerName = new TextBox();
            lblCustomerName = new Label();
            txtCustomerId = new TextBox();
            lblCustomerId = new Label();
            btnDelete = new Button();
            btnUpdate = new Button();
            btnAdd = new Button();
            statusStrip1 = new StatusStrip();
            lblReady = new ToolStripStatusLabel();
            panelTop.SuspendLayout();
            panelBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).BeginInit();
            panelInfo.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.White;
            panelTop.Controls.Add(btnLoad);
            panelTop.Controls.Add(btnSearch);
            panelTop.Controls.Add(txtKeyword);
            panelTop.Controls.Add(lblSearch);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(12, 12);
            panelTop.Name = "panelTop";
            panelTop.Padding = new Padding(15, 12, 15, 12);
            panelTop.Size = new Size(1176, 60);
            panelTop.TabIndex = 0;
            // 
            // btnLoad
            // 
            btnLoad.BackColor = Color.FromArgb(241, 245, 249);
            btnLoad.Cursor = Cursors.Hand;
            btnLoad.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnLoad.FlatStyle = FlatStyle.Flat;
            btnLoad.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnLoad.ForeColor = Color.FromArgb(71, 85, 105);
            btnLoad.Location = new Point(485, 15);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(90, 30);
            btnLoad.TabIndex = 3;
            btnLoad.Text = "🔄 Tải lại";
            btnLoad.UseVisualStyleBackColor = false;
            btnLoad.Click += btnLoad_Click;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.FromArgb(37, 99, 235);
            btnSearch.Cursor = Cursors.Hand;
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSearch.ForeColor = Color.White;
            btnSearch.Location = new Point(390, 15);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(88, 30);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "🔍 Tìm kiếm";
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += btnSearch_Click;
            // 
            // txtKeyword
            // 
            txtKeyword.BorderStyle = BorderStyle.FixedSingle;
            txtKeyword.Font = new Font("Segoe UI", 10F);
            txtKeyword.Location = new Point(95, 17);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.PlaceholderText = "Nhập tên hoặc số điện thoại khách hàng...";
            txtKeyword.Size = new Size(280, 25);
            txtKeyword.TabIndex = 1;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblSearch.ForeColor = Color.FromArgb(71, 85, 105);
            lblSearch.Location = new Point(18, 21);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(69, 17);
            lblSearch.TabIndex = 0;
            lblSearch.Text = "Tìm kiếm:";
            // 
            // panelBody
            // 
            panelBody.Controls.Add(dgvCustomers);
            panelBody.Controls.Add(panelInfo);
            panelBody.Dock = DockStyle.Fill;
            panelBody.Location = new Point(12, 72);
            panelBody.Name = "panelBody";
            panelBody.Padding = new Padding(0, 10, 0, 0);
            panelBody.Size = new Size(1176, 582);
            panelBody.TabIndex = 1;
            // 
            // dgvCustomers
            // 
            dgvCustomers.AllowUserToAddRows = false;
            dgvCustomers.AllowUserToDeleteRows = false;
            dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCustomers.BackgroundColor = Color.White;
            dgvCustomers.BorderStyle = BorderStyle.None;
            dgvCustomers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCustomers.Columns.AddRange(new DataGridViewColumn[] { colId, colName, colPhone, colAddress, colPoints, colRank });
            dgvCustomers.Dock = DockStyle.Fill;
            dgvCustomers.Location = new Point(0, 10);
            dgvCustomers.MultiSelect = false;
            dgvCustomers.Name = "dgvCustomers";
            dgvCustomers.ReadOnly = true;
            dgvCustomers.RowHeadersVisible = false;
            dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCustomers.Size = new Size(806, 572);
            dgvCustomers.TabIndex = 0;
            dgvCustomers.CellClick += dgvCustomers_CellClick;
            dgvCustomers.CellContentClick += dgvCustomers_CellContentClick;
            // 
            // colId
            // 
            colId.DataPropertyName = "CustomerId";
            colId.FillWeight = 15F;
            colId.HeaderText = "Mã ID";
            colId.Name = "colId";
            colId.ReadOnly = true;
            // 
            // colName
            // 
            colName.DataPropertyName = "CustomerName";
            colName.FillWeight = 35F;
            colName.HeaderText = "Tên Khách hàng";
            colName.Name = "colName";
            colName.ReadOnly = true;
            // 
            // colPhone
            // 
            colPhone.DataPropertyName = "PhoneNumber";
            colPhone.FillWeight = 25F;
            colPhone.HeaderText = "Số điện thoại";
            colPhone.Name = "colPhone";
            colPhone.ReadOnly = true;
            // 
            // colAddress
            // 
            colAddress.DataPropertyName = "Address";
            colAddress.FillWeight = 40F;
            colAddress.HeaderText = "Địa chỉ";
            colAddress.Name = "colAddress";
            colAddress.ReadOnly = true;
            // 
            // colPoints
            // 
            colPoints.DataPropertyName = "RewardPoints";
            colPoints.FillWeight = 20F;
            colPoints.HeaderText = "Điểm tích lũy";
            colPoints.Name = "colPoints";
            colPoints.ReadOnly = true;
            // 
            // colRank
            // 
            colRank.DataPropertyName = "MembershipRank";
            colRank.FillWeight = 20F;
            colRank.HeaderText = "Hạng thẻ";
            colRank.Name = "colRank";
            colRank.ReadOnly = true;
            // 
            // panelInfo
            // 
            panelInfo.BackColor = Color.White;
            panelInfo.Controls.Add(lblInfoTitle);
            panelInfo.Controls.Add(cbMembershipRank);
            panelInfo.Controls.Add(lblMembershipRank);
            panelInfo.Controls.Add(txtRewardPoints);
            panelInfo.Controls.Add(lblRewardPoints);
            panelInfo.Controls.Add(txtAddress);
            panelInfo.Controls.Add(lblAddress);
            panelInfo.Controls.Add(txtPhoneNumber);
            panelInfo.Controls.Add(lblPhoneNumber);
            panelInfo.Controls.Add(txtCustomerName);
            panelInfo.Controls.Add(lblCustomerName);
            panelInfo.Controls.Add(txtCustomerId);
            panelInfo.Controls.Add(lblCustomerId);
            panelInfo.Controls.Add(btnDelete);
            panelInfo.Controls.Add(btnUpdate);
            panelInfo.Controls.Add(btnAdd);
            panelInfo.Dock = DockStyle.Right;
            panelInfo.Location = new Point(806, 10);
            panelInfo.Name = "panelInfo";
            panelInfo.Padding = new Padding(22);
            panelInfo.Size = new Size(370, 572);
            panelInfo.TabIndex = 1;
            // 
            // lblInfoTitle
            // 
            lblInfoTitle.AutoSize = true;
            lblInfoTitle.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            lblInfoTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblInfoTitle.Location = new Point(18, 16);
            lblInfoTitle.Name = "lblInfoTitle";
            lblInfoTitle.Size = new Size(204, 21);
            lblInfoTitle.TabIndex = 15;
            lblInfoTitle.Text = "💎 THÔNG TIN HỘI VIÊN";
            // 
            // cbMembershipRank
            // 
            cbMembershipRank.DropDownStyle = ComboBoxStyle.DropDownList;
            cbMembershipRank.Font = new Font("Segoe UI", 10F);
            cbMembershipRank.FormattingEnabled = true;
            cbMembershipRank.Items.AddRange(new object[] { "Chuẩn", "Bạc", "Vàng", "Kim cương" });
            cbMembershipRank.Location = new Point(20, 360);
            cbMembershipRank.Name = "cbMembershipRank";
            cbMembershipRank.Size = new Size(318, 25);
            cbMembershipRank.TabIndex = 11;
            // 
            // lblMembershipRank
            // 
            lblMembershipRank.AutoSize = true;
            lblMembershipRank.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblMembershipRank.ForeColor = Color.FromArgb(71, 85, 105);
            lblMembershipRank.Location = new Point(18, 338);
            lblMembershipRank.Name = "lblMembershipRank";
            lblMembershipRank.Size = new Size(60, 15);
            lblMembershipRank.TabIndex = 10;
            lblMembershipRank.Text = "Hạng thẻ:";
            // 
            // txtRewardPoints
            // 
            txtRewardPoints.BorderStyle = BorderStyle.FixedSingle;
            txtRewardPoints.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            txtRewardPoints.Location = new Point(20, 298);
            txtRewardPoints.Name = "txtRewardPoints";
            txtRewardPoints.PlaceholderText = "0";
            txtRewardPoints.Size = new Size(318, 25);
            txtRewardPoints.TabIndex = 9;
            txtRewardPoints.Text = "0";
            // 
            // lblRewardPoints
            // 
            lblRewardPoints.AutoSize = true;
            lblRewardPoints.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblRewardPoints.ForeColor = Color.FromArgb(71, 85, 105);
            lblRewardPoints.Location = new Point(18, 276);
            lblRewardPoints.Name = "lblRewardPoints";
            lblRewardPoints.Size = new Size(82, 15);
            lblRewardPoints.TabIndex = 8;
            lblRewardPoints.Text = "Điểm tích lũy:";
            // 
            // txtAddress
            // 
            txtAddress.BorderStyle = BorderStyle.FixedSingle;
            txtAddress.Font = new Font("Segoe UI", 10F);
            txtAddress.Location = new Point(20, 218);
            txtAddress.Multiline = true;
            txtAddress.Name = "txtAddress";
            txtAddress.PlaceholderText = "Địa chỉ cư trú...";
            txtAddress.ScrollBars = ScrollBars.Vertical;
            txtAddress.Size = new Size(318, 48);
            txtAddress.TabIndex = 7;
            // 
            // lblAddress
            // 
            lblAddress.AutoSize = true;
            lblAddress.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblAddress.ForeColor = Color.FromArgb(71, 85, 105);
            lblAddress.Location = new Point(18, 196);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(47, 15);
            lblAddress.TabIndex = 6;
            lblAddress.Text = "Địa chỉ:";
            // 
            // txtPhoneNumber
            // 
            txtPhoneNumber.BorderStyle = BorderStyle.FixedSingle;
            txtPhoneNumber.Font = new Font("Segoe UI", 10F);
            txtPhoneNumber.Location = new Point(20, 158);
            txtPhoneNumber.Name = "txtPhoneNumber";
            txtPhoneNumber.PlaceholderText = "Ví dụ: 0901234567";
            txtPhoneNumber.Size = new Size(318, 25);
            txtPhoneNumber.TabIndex = 5;
            // 
            // lblPhoneNumber
            // 
            lblPhoneNumber.AutoSize = true;
            lblPhoneNumber.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPhoneNumber.ForeColor = Color.FromArgb(71, 85, 105);
            lblPhoneNumber.Location = new Point(18, 136);
            lblPhoneNumber.Name = "lblPhoneNumber";
            lblPhoneNumber.Size = new Size(83, 15);
            lblPhoneNumber.TabIndex = 4;
            lblPhoneNumber.Text = "Số điện thoại:";
            // 
            // txtCustomerName
            // 
            txtCustomerName.BorderStyle = BorderStyle.FixedSingle;
            txtCustomerName.Font = new Font("Segoe UI", 10F);
            txtCustomerName.Location = new Point(20, 98);
            txtCustomerName.Name = "txtCustomerName";
            txtCustomerName.PlaceholderText = "Họ và tên khách hàng...";
            txtCustomerName.Size = new Size(318, 25);
            txtCustomerName.TabIndex = 3;
            // 
            // lblCustomerName
            // 
            lblCustomerName.AutoSize = true;
            lblCustomerName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCustomerName.ForeColor = Color.FromArgb(71, 85, 105);
            lblCustomerName.Location = new Point(18, 76);
            lblCustomerName.Name = "lblCustomerName";
            lblCustomerName.Size = new Size(97, 15);
            lblCustomerName.TabIndex = 2;
            lblCustomerName.Text = "Tên Khách hàng:";
            // 
            // txtCustomerId
            // 
            txtCustomerId.BackColor = Color.FromArgb(241, 245, 249);
            txtCustomerId.BorderStyle = BorderStyle.FixedSingle;
            txtCustomerId.Font = new Font("Segoe UI", 10F);
            txtCustomerId.Location = new Point(20, 44);
            txtCustomerId.Name = "txtCustomerId";
            txtCustomerId.ReadOnly = true;
            txtCustomerId.Size = new Size(318, 25);
            txtCustomerId.TabIndex = 1;
            // 
            // lblCustomerId
            // 
            lblCustomerId.AutoSize = true;
            lblCustomerId.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCustomerId.ForeColor = Color.FromArgb(71, 85, 105);
            lblCustomerId.Location = new Point(18, 24);
            lblCustomerId.Name = "lblCustomerId";
            lblCustomerId.Size = new Size(46, 15);
            lblCustomerId.TabIndex = 0;
            lblCustomerId.Text = "Mã ID:";
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.FromArgb(220, 38, 38);
            btnDelete.Cursor = Cursors.Hand;
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnDelete.ForeColor = Color.White;
            btnDelete.Location = new Point(235, 415);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(103, 40);
            btnDelete.TabIndex = 14;
            btnDelete.Text = "🗑 Xóa";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = Color.FromArgb(37, 99, 235);
            btnUpdate.Cursor = Cursors.Hand;
            btnUpdate.FlatAppearance.BorderSize = 0;
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnUpdate.ForeColor = Color.White;
            btnUpdate.Location = new Point(127, 415);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(102, 40);
            btnUpdate.TabIndex = 13;
            btnUpdate.Text = "✏️ Sửa";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.FromArgb(5, 150, 105);
            btnAdd.Cursor = Cursors.Hand;
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(20, 415);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(101, 40);
            btnAdd.TabIndex = 12;
            btnAdd.Text = "➕ Thêm";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = Color.FromArgb(241, 245, 249);
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblReady });
            statusStrip1.Location = new Point(12, 654);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1176, 22);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblReady
            // 
            lblReady.Font = new Font("Segoe UI", 8.5F);
            lblReady.ForeColor = Color.FromArgb(100, 116, 139);
            lblReady.Name = "lblReady";
            lblReady.Size = new Size(54, 17);
            lblReady.Text = "Sẵn sàng";
            // 
            // FormCustomerManagement
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(241, 245, 249);
            ClientSize = new Size(1200, 688);
            Controls.Add(panelBody);
            Controls.Add(statusStrip1);
            Controls.Add(panelTop);
            Font = new Font("Segoe UI", 9.75F);
            Name = "FormCustomerManagement";
            Padding = new Padding(12);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý Khách hàng & Tích điểm hội viên";
            Load += FormCustomerManagement_Load;
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).EndInit();
            panelInfo.ResumeLayout(false);
            panelInfo.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panelTop;
        private Label lblSearch;
        private TextBox txtKeyword;
        private Button btnSearch;
        private Button btnLoad;
        private Panel panelBody;
        private DataGridView dgvCustomers;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colPhone;
        private DataGridViewTextBoxColumn colAddress;
        private DataGridViewTextBoxColumn colPoints;
        private DataGridViewTextBoxColumn colRank;
        private Panel panelInfo;
        private Label lblInfoTitle;
        private Label lblCustomerId;
        private TextBox txtCustomerId;
        private Label lblCustomerName;
        private TextBox txtCustomerName;
        private Label lblPhoneNumber;
        private TextBox txtPhoneNumber;
        private Label lblAddress;
        private TextBox txtAddress;
        private Label lblRewardPoints;
        private TextBox txtRewardPoints;
        private Label lblMembershipRank;
        private ComboBox cbMembershipRank;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblReady;
    }
}
