namespace MiniSupermarket.WinForms
{
    partial class FormRoleManagement
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
            panelBody = new Panel();
            dgvRoles = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colRoleName = new DataGridViewTextBoxColumn();
            colDesc = new DataGridViewTextBoxColumn();
            panelInfo = new Panel();
            lblInfoTitle = new Label();
            btnDelete = new Button();
            btnUpdate = new Button();
            btnAdd = new Button();
            txtDescription = new TextBox();
            lblDescription = new Label();
            txtRoleName = new TextBox();
            lblRoleName = new Label();
            txtId = new TextBox();
            lblId = new Label();
            statusStrip1 = new StatusStrip();
            lblReady = new ToolStripStatusLabel();
            lblApiUrl = new ToolStripStatusLabel();
            panelBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRoles).BeginInit();
            panelInfo.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // panelBody
            // 
            panelBody.Controls.Add(dgvRoles);
            panelBody.Controls.Add(panelInfo);
            panelBody.Dock = DockStyle.Fill;
            panelBody.Location = new Point(12, 12);
            panelBody.Name = "panelBody";
            panelBody.Padding = new Padding(0, 5, 0, 0);
            panelBody.Size = new Size(1176, 642);
            panelBody.TabIndex = 0;
            // 
            // dgvRoles
            // 
            dgvRoles.AllowUserToAddRows = false;
            dgvRoles.AllowUserToDeleteRows = false;
            dgvRoles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRoles.BackgroundColor = Color.White;
            dgvRoles.BorderStyle = BorderStyle.None;
            dgvRoles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRoles.Columns.AddRange(new DataGridViewColumn[] { colId, colRoleName, colDesc });
            dgvRoles.Dock = DockStyle.Fill;
            dgvRoles.Location = new Point(0, 5);
            dgvRoles.MultiSelect = false;
            dgvRoles.Name = "dgvRoles";
            dgvRoles.ReadOnly = true;
            dgvRoles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRoles.Size = new Size(816, 637);
            dgvRoles.TabIndex = 0;
            dgvRoles.CellClick += dgvRoles_CellClick;
            // 
            // colId
            // 
            colId.DataPropertyName = "Id";
            colId.FillWeight = 20F;
            colId.HeaderText = "Mã ID";
            colId.Name = "colId";
            colId.ReadOnly = true;
            // 
            // colRoleName
            // 
            colRoleName.DataPropertyName = "RoleName";
            colRoleName.FillWeight = 40F;
            colRoleName.HeaderText = "Tên Vai trò";
            colRoleName.Name = "colRoleName";
            colRoleName.ReadOnly = true;
            // 
            // colDesc
            // 
            colDesc.DataPropertyName = "Description";
            colDesc.FillWeight = 50F;
            colDesc.HeaderText = "Mô Tả Vai Trò";
            colDesc.Name = "colDesc";
            colDesc.ReadOnly = true;
            // 
            // panelInfo
            // 
            panelInfo.BackColor = Color.White;
            panelInfo.Controls.Add(lblInfoTitle);
            panelInfo.Controls.Add(btnDelete);
            panelInfo.Controls.Add(btnUpdate);
            panelInfo.Controls.Add(btnAdd);
            panelInfo.Controls.Add(txtDescription);
            panelInfo.Controls.Add(lblDescription);
            panelInfo.Controls.Add(txtRoleName);
            panelInfo.Controls.Add(lblRoleName);
            panelInfo.Controls.Add(txtId);
            panelInfo.Controls.Add(lblId);
            panelInfo.Dock = DockStyle.Right;
            panelInfo.Location = new Point(816, 5);
            panelInfo.Name = "panelInfo";
            panelInfo.Padding = new Padding(22);
            panelInfo.Size = new Size(360, 637);
            panelInfo.TabIndex = 1;
            // 
            // lblInfoTitle
            // 
            lblInfoTitle.AutoSize = true;
            lblInfoTitle.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            lblInfoTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblInfoTitle.Location = new Point(18, 16);
            lblInfoTitle.Name = "lblInfoTitle";
            lblInfoTitle.Size = new Size(183, 21);
            lblInfoTitle.TabIndex = 9;
            lblInfoTitle.Text = "🛡️ THÔNG TIN VAI TRÒ";
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.FromArgb(220, 38, 38);
            btnDelete.Cursor = Cursors.Hand;
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnDelete.ForeColor = Color.White;
            btnDelete.Location = new Point(230, 420);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(98, 40);
            btnDelete.TabIndex = 8;
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
            btnUpdate.Location = new Point(125, 420);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(98, 40);
            btnUpdate.TabIndex = 7;
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
            btnAdd.Location = new Point(20, 420);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(98, 40);
            btnAdd.TabIndex = 6;
            btnAdd.Text = "➕ Thêm";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // txtDescription
            // 
            txtDescription.BorderStyle = BorderStyle.FixedSingle;
            txtDescription.Font = new Font("Segoe UI", 10F);
            txtDescription.Location = new Point(20, 205);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.PlaceholderText = "Nhập mô tả quyền hạn vai trò...";
            txtDescription.ScrollBars = ScrollBars.Vertical;
            txtDescription.Size = new Size(308, 185);
            txtDescription.TabIndex = 5;
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDescription.ForeColor = Color.FromArgb(71, 85, 105);
            lblDescription.Location = new Point(18, 183);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(43, 15);
            lblDescription.TabIndex = 4;
            lblDescription.Text = "Mô Tả:";
            // 
            // txtRoleName
            // 
            txtRoleName.BorderStyle = BorderStyle.FixedSingle;
            txtRoleName.Font = new Font("Segoe UI", 10F);
            txtRoleName.Location = new Point(20, 137);
            txtRoleName.Name = "txtRoleName";
            txtRoleName.PlaceholderText = "Ví dụ: ADMIN, CASHIER...";
            txtRoleName.Size = new Size(308, 25);
            txtRoleName.TabIndex = 3;
            // 
            // lblRoleName
            // 
            lblRoleName.AutoSize = true;
            lblRoleName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblRoleName.ForeColor = Color.FromArgb(71, 85, 105);
            lblRoleName.Location = new Point(18, 115);
            lblRoleName.Name = "lblRoleName";
            lblRoleName.Size = new Size(71, 15);
            lblRoleName.TabIndex = 2;
            lblRoleName.Text = "Tên Vai trò:";
            // 
            // txtId
            // 
            txtId.BackColor = Color.FromArgb(241, 245, 249);
            txtId.BorderStyle = BorderStyle.FixedSingle;
            txtId.Font = new Font("Segoe UI", 10F);
            txtId.Location = new Point(20, 72);
            txtId.Name = "txtId";
            txtId.ReadOnly = true;
            txtId.Size = new Size(308, 25);
            txtId.TabIndex = 1;
            // 
            // lblId
            // 
            lblId.AutoSize = true;
            lblId.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblId.ForeColor = Color.FromArgb(71, 85, 105);
            lblId.Location = new Point(18, 50);
            lblId.Name = "lblId";
            lblId.Size = new Size(46, 15);
            lblId.TabIndex = 0;
            lblId.Text = "Mã ID:";
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = Color.FromArgb(241, 245, 249);
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblReady, lblApiUrl });
            statusStrip1.Location = new Point(12, 654);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1176, 22);
            statusStrip1.TabIndex = 1;
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
            // lblApiUrl
            // 
            lblApiUrl.Font = new Font("Segoe UI", 8.5F);
            lblApiUrl.ForeColor = Color.FromArgb(148, 163, 184);
            lblApiUrl.Name = "lblApiUrl";
            lblApiUrl.Size = new Size(101, 17);
            lblApiUrl.Text = "API: /api/roles";
            // 
            // FormRoleManagement
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(241, 245, 249);
            ClientSize = new Size(1200, 688);
            Controls.Add(panelBody);
            Controls.Add(statusStrip1);
            Font = new Font("Segoe UI", 9.75F);
            Name = "FormRoleManagement";
            Padding = new Padding(12);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý Vai trò Hệ thống";
            Load += FormRoleManagement_Load;
            panelBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvRoles).EndInit();
            panelInfo.ResumeLayout(false);
            panelInfo.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panelBody;
        private DataGridView dgvRoles;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colRoleName;
        private DataGridViewTextBoxColumn colDesc;
        private Panel panelInfo;
        private Label lblInfoTitle;
        private Label lblId;
        private TextBox txtId;
        private Label lblRoleName;
        private TextBox txtRoleName;
        private Label lblDescription;
        private TextBox txtDescription;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblReady;
        private ToolStripStatusLabel lblApiUrl;
    }
}