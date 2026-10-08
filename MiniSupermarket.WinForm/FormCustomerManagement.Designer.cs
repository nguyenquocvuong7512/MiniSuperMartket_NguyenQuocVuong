using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

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
            grpSearch = new GroupBox();
            btnLoad = new Button();
            btnSearch = new Button();
            txtKeyword = new TextBox();
            grpList = new GroupBox();
            dgvCustomers = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colName = new DataGridViewTextBoxColumn();
            colPhone = new DataGridViewTextBoxColumn();
            colAddress = new DataGridViewTextBoxColumn();
            colPoints = new DataGridViewTextBoxColumn();
            colRank = new DataGridViewTextBoxColumn();
            grpInfo = new GroupBox();
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
            grpSearch.SuspendLayout();
            grpList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).BeginInit();
            grpInfo.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // grpSearch
            // 
            grpSearch.Controls.Add(btnLoad);
            grpSearch.Controls.Add(btnSearch);
            grpSearch.Controls.Add(txtKeyword);
            grpSearch.Location = new Point(12, 12);
            grpSearch.Name = "grpSearch";
            grpSearch.Size = new Size(560, 65);
            grpSearch.TabIndex = 0;
            grpSearch.TabStop = false;
            grpSearch.Text = "Tìm kiếm khách hàng";
            // 
            // btnLoad
            // 
            btnLoad.Location = new Point(462, 22);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(85, 28);
            btnLoad.TabIndex = 2;
            btnLoad.Text = "Tải lại";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(371, 22);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(85, 28);
            btnSearch.TabIndex = 1;
            btnSearch.Text = "Tìm kiếm";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // txtKeyword
            // 
            txtKeyword.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            txtKeyword.Location = new Point(15, 24);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.PlaceholderText = "Nhập tên hoặc số điện thoại...";
            txtKeyword.Size = new Size(350, 25);
            txtKeyword.TabIndex = 0;
            // 
            // grpList
            // 
            grpList.Controls.Add(dgvCustomers);
            grpList.Location = new Point(12, 83);
            grpList.Name = "grpList";
            grpList.Size = new Size(560, 395);
            grpList.TabIndex = 1;
            grpList.TabStop = false;
            grpList.Text = "Danh sách Khách hàng thân thiết";
            // 
            // dgvCustomers
            // 
            dgvCustomers.AllowUserToAddRows = false;
            dgvCustomers.AllowUserToDeleteRows = false;
            dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCustomers.BackgroundColor = SystemColors.Window;
            dgvCustomers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCustomers.Columns.AddRange(new DataGridViewColumn[] { colId, colName, colPhone, colAddress, colPoints, colRank });
            dgvCustomers.Dock = DockStyle.Fill;
            dgvCustomers.Location = new Point(3, 19);
            dgvCustomers.MultiSelect = false;
            dgvCustomers.Name = "dgvCustomers";
            dgvCustomers.ReadOnly = true;
            dgvCustomers.RowHeadersVisible = false;
            dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCustomers.Size = new Size(554, 373);
            dgvCustomers.TabIndex = 0;
            dgvCustomers.CellClick += dgvCustomers_CellClick;
            dgvCustomers.CellContentClick += dgvCustomers_CellContentClick;
            // 
            // colId
            // 
            colId.DataPropertyName = "CustomerId";
            colId.FillWeight = 20F;
            colId.HeaderText = "Mã ID";
            colId.Name = "colId";
            colId.ReadOnly = true;
            // 
            // colName
            // 
            colName.DataPropertyName = "CustomerName";
            colName.FillWeight = 40F;
            colName.HeaderText = "Tên Khách hàng";
            colName.Name = "colName";
            colName.ReadOnly = true;
            // 
            // colPhone
            // 
            colPhone.DataPropertyName = "PhoneNumber";
            colPhone.FillWeight = 30F;
            colPhone.HeaderText = "Số ĐT";
            colPhone.Name = "colPhone";
            colPhone.ReadOnly = true;
            // 
            // colAddress
            // 
            colAddress.DataPropertyName = "Address";
            colAddress.FillWeight = 50F;
            colAddress.HeaderText = "Địa chỉ";
            colAddress.Name = "colAddress";
            colAddress.ReadOnly = true;
            // 
            // colPoints
            // 
            colPoints.DataPropertyName = "RewardPoints";
            colPoints.FillWeight = 25F;
            colPoints.HeaderText = "Điểm";
            colPoints.Name = "colPoints";
            colPoints.ReadOnly = true;
            // 
            // colRank
            // 
            colRank.DataPropertyName = "MembershipRank";
            colRank.FillWeight = 25F;
            colRank.HeaderText = "Hạng thẻ";
            colRank.Name = "colRank";
            colRank.ReadOnly = true;
            // 
            // grpInfo
            // 
            grpInfo.Controls.Add(cbMembershipRank);
            grpInfo.Controls.Add(lblMembershipRank);
            grpInfo.Controls.Add(txtRewardPoints);
            grpInfo.Controls.Add(lblRewardPoints);
            grpInfo.Controls.Add(txtAddress);
            grpInfo.Controls.Add(lblAddress);
            grpInfo.Controls.Add(txtPhoneNumber);
            grpInfo.Controls.Add(lblPhoneNumber);
            grpInfo.Controls.Add(txtCustomerName);
            grpInfo.Controls.Add(lblCustomerName);
            grpInfo.Controls.Add(txtCustomerId);
            grpInfo.Controls.Add(lblCustomerId);
            grpInfo.Controls.Add(btnDelete);
            grpInfo.Controls.Add(btnUpdate);
            grpInfo.Controls.Add(btnAdd);
            grpInfo.Location = new Point(582, 12);
            grpInfo.Name = "grpInfo";
            grpInfo.Size = new Size(300, 466);
            grpInfo.TabIndex = 2;
            grpInfo.TabStop = false;
            grpInfo.Text = "Thông tin Khách hàng";
            grpInfo.Enter += grpInfo_Enter;
            // 
            // cbMembershipRank
            // 
            cbMembershipRank.DropDownStyle = ComboBoxStyle.DropDownList;
            cbMembershipRank.FormattingEnabled = true;
            cbMembershipRank.Items.AddRange(new object[] { "Chuẩn", "Bạc", "Vàng", "Kim cương" });
            cbMembershipRank.Location = new Point(15, 362);
            cbMembershipRank.Name = "cbMembershipRank";
            cbMembershipRank.Size = new Size(268, 23);
            cbMembershipRank.TabIndex = 11;
            // 
            // lblMembershipRank
            // 
            lblMembershipRank.AutoSize = true;
            lblMembershipRank.Location = new Point(15, 342);
            lblMembershipRank.Name = "lblMembershipRank";
            lblMembershipRank.Size = new Size(59, 15);
            lblMembershipRank.TabIndex = 10;
            lblMembershipRank.Text = "Hạng thẻ:";
            // 
            // txtRewardPoints
            // 
            txtRewardPoints.Location = new Point(15, 302);
            txtRewardPoints.Name = "txtRewardPoints";
            txtRewardPoints.Size = new Size(268, 23);
            txtRewardPoints.TabIndex = 9;
            txtRewardPoints.Text = "0";
            // 
            // lblRewardPoints
            // 
            lblRewardPoints.AutoSize = true;
            lblRewardPoints.Location = new Point(15, 282);
            lblRewardPoints.Name = "lblRewardPoints";
            lblRewardPoints.Size = new Size(80, 15);
            lblRewardPoints.TabIndex = 8;
            lblRewardPoints.Text = "Điểm tích lũy:";
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(15, 222);
            txtAddress.Multiline = true;
            txtAddress.Name = "txtAddress";
            txtAddress.PlaceholderText = "Địa chỉ cư trú...";
            txtAddress.ScrollBars = ScrollBars.Vertical;
            txtAddress.Size = new Size(268, 48);
            txtAddress.TabIndex = 7;
            // 
            // lblAddress
            // 
            lblAddress.AutoSize = true;
            lblAddress.Location = new Point(15, 202);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(46, 15);
            lblAddress.TabIndex = 6;
            lblAddress.Text = "Địa chỉ:";
            // 
            // txtPhoneNumber
            // 
            txtPhoneNumber.Location = new Point(15, 162);
            txtPhoneNumber.Name = "txtPhoneNumber";
            txtPhoneNumber.PlaceholderText = "Ví dụ: 0901234567";
            txtPhoneNumber.Size = new Size(268, 23);
            txtPhoneNumber.TabIndex = 5;
            // 
            // lblPhoneNumber
            // 
            lblPhoneNumber.AutoSize = true;
            lblPhoneNumber.Location = new Point(15, 142);
            lblPhoneNumber.Name = "lblPhoneNumber";
            lblPhoneNumber.Size = new Size(79, 15);
            lblPhoneNumber.TabIndex = 4;
            lblPhoneNumber.Text = "Số điện thoại:";
            // 
            // txtCustomerName
            // 
            txtCustomerName.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            txtCustomerName.Location = new Point(15, 102);
            txtCustomerName.Name = "txtCustomerName";
            txtCustomerName.PlaceholderText = "Họ và tên khách hàng...";
            txtCustomerName.Size = new Size(268, 25);
            txtCustomerName.TabIndex = 3;
            // 
            // lblCustomerName
            // 
            lblCustomerName.AutoSize = true;
            lblCustomerName.Location = new Point(15, 82);
            lblCustomerName.Name = "lblCustomerName";
            lblCustomerName.Size = new Size(94, 15);
            lblCustomerName.TabIndex = 2;
            lblCustomerName.Text = "Tên Khách hàng:";
            // 
            // txtCustomerId
            // 
            txtCustomerId.Location = new Point(15, 48);
            txtCustomerId.Name = "txtCustomerId";
            txtCustomerId.ReadOnly = true;
            txtCustomerId.Size = new Size(268, 23);
            txtCustomerId.TabIndex = 1;
            // 
            // lblCustomerId
            // 
            lblCustomerId.AutoSize = true;
            lblCustomerId.Location = new Point(15, 28);
            lblCustomerId.Name = "lblCustomerId";
            lblCustomerId.Size = new Size(41, 15);
            lblCustomerId.TabIndex = 0;
            lblCustomerId.Text = "Mã ID:";
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(201, 412);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(82, 34);
            btnDelete.TabIndex = 14;
            btnDelete.Text = "Xóa";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(108, 412);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(86, 34);
            btnUpdate.TabIndex = 13;
            btnUpdate.Text = "Cập nhật";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(15, 412);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(86, 34);
            btnAdd.TabIndex = 12;
            btnAdd.Text = "Thêm mới";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblReady });
            statusStrip1.Location = new Point(0, 488);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(894, 22);
            statusStrip1.TabIndex = 3;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblReady
            // 
            lblReady.Name = "lblReady";
            lblReady.Size = new Size(275, 17);
            lblReady.Text = "Sẵn sàng | Quản lý Khách hàng thân thiết (EF Core)";
            // 
            // FormCustomerManagement
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(894, 510);
            Controls.Add(statusStrip1);
            Controls.Add(grpInfo);
            Controls.Add(grpList);
            Controls.Add(grpSearch);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FormCustomerManagement";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Hệ thống quản lý siêu thị - Quản lý Khách hàng (Customers)";
            Load += FormCustomerManagement_Load;
            grpSearch.ResumeLayout(false);
            grpSearch.PerformLayout();
            grpList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).EndInit();
            grpInfo.ResumeLayout(false);
            grpInfo.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.GroupBox grpSearch;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtKeyword;
        private System.Windows.Forms.GroupBox grpList;
        private System.Windows.Forms.DataGridView dgvCustomers;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPhone;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPoints;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRank;
        private System.Windows.Forms.GroupBox grpInfo;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtCustomerId;
        private System.Windows.Forms.Label lblCustomerId;
        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.TextBox txtPhoneNumber;
        private System.Windows.Forms.Label lblPhoneNumber;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox txtRewardPoints;
        private System.Windows.Forms.Label lblRewardPoints;
        private System.Windows.Forms.ComboBox cbMembershipRank;
        private System.Windows.Forms.Label lblMembershipRank;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblReady;
    }
}
