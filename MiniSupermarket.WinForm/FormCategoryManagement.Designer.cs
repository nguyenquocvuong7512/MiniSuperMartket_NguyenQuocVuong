namespace MiniSupermarket.WinForms
{
    partial class FormCategoryManagement
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
            dgvCategories = new DataGridView();
            CategoryId = new DataGridViewTextBoxColumn();
            CategoryName = new DataGridViewTextBoxColumn();
            Description = new DataGridViewTextBoxColumn();
            panelInfo = new Panel();
            lblInfoTitle = new Label();
            btnDelete = new Button();
            btnUpdate = new Button();
            btnAdd = new Button();
            txtDescription = new TextBox();
            lblDescription = new Label();
            txtCategoryName = new TextBox();
            lblCategoryName = new Label();
            txtId = new TextBox();
            lblId = new Label();
            statusStrip1 = new StatusStrip();
            lblReady = new ToolStripStatusLabel();
            lblApiUrl = new ToolStripStatusLabel();
            panelTop.SuspendLayout();
            panelBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCategories).BeginInit();
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
            btnLoad.Location = new Point(480, 15);
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
            btnSearch.Location = new Point(385, 15);
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
            txtKeyword.PlaceholderText = "Nhập từ khóa tìm kiếm nhóm hàng...";
            txtKeyword.Size = new Size(275, 25);
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
            panelBody.Controls.Add(dgvCategories);
            panelBody.Controls.Add(panelInfo);
            panelBody.Dock = DockStyle.Fill;
            panelBody.Location = new Point(12, 72);
            panelBody.Name = "panelBody";
            panelBody.Padding = new Padding(0, 10, 0, 0);
            panelBody.Size = new Size(1176, 582);
            panelBody.TabIndex = 1;
            // 
            // dgvCategories
            // 
            dgvCategories.AllowUserToAddRows = false;
            dgvCategories.AllowUserToDeleteRows = false;
            dgvCategories.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCategories.BackgroundColor = Color.White;
            dgvCategories.BorderStyle = BorderStyle.None;
            dgvCategories.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCategories.Columns.AddRange(new DataGridViewColumn[] { CategoryId, CategoryName, Description });
            dgvCategories.Dock = DockStyle.Fill;
            dgvCategories.Location = new Point(0, 10);
            dgvCategories.MultiSelect = false;
            dgvCategories.Name = "dgvCategories";
            dgvCategories.ReadOnly = true;
            dgvCategories.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCategories.Size = new Size(816, 572);
            dgvCategories.TabIndex = 0;
            dgvCategories.CellClick += dgvCategories_CellClick;
            dgvCategories.CellContentClick += dgvCategories_CellContentClick;
            // 
            // CategoryId
            // 
            CategoryId.DataPropertyName = "CategoryId";
            CategoryId.FillWeight = 20F;
            CategoryId.HeaderText = "Mã ID";
            CategoryId.Name = "CategoryId";
            CategoryId.ReadOnly = true;
            // 
            // CategoryName
            // 
            CategoryName.DataPropertyName = "CategoryName";
            CategoryName.FillWeight = 40F;
            CategoryName.HeaderText = "Tên Nhóm hàng";
            CategoryName.Name = "CategoryName";
            CategoryName.ReadOnly = true;
            // 
            // Description
            // 
            Description.DataPropertyName = "Description";
            Description.FillWeight = 50F;
            Description.HeaderText = "Mô Tả Chi Tiết";
            Description.Name = "Description";
            Description.ReadOnly = true;
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
            panelInfo.Controls.Add(txtCategoryName);
            panelInfo.Controls.Add(lblCategoryName);
            panelInfo.Controls.Add(txtId);
            panelInfo.Controls.Add(lblId);
            panelInfo.Dock = DockStyle.Right;
            panelInfo.Location = new Point(816, 10);
            panelInfo.Name = "panelInfo";
            panelInfo.Padding = new Padding(22);
            panelInfo.Size = new Size(360, 572);
            panelInfo.TabIndex = 1;
            // 
            // lblInfoTitle
            // 
            lblInfoTitle.AutoSize = true;
            lblInfoTitle.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            lblInfoTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblInfoTitle.Location = new Point(18, 16);
            lblInfoTitle.Name = "lblInfoTitle";
            lblInfoTitle.Size = new Size(236, 21);
            lblInfoTitle.TabIndex = 9;
            lblInfoTitle.Text = "📁 THÔNG TIN NHÓM HÀNG";
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
            txtDescription.PlaceholderText = "Nhập mô tả chi tiết nhóm hàng...";
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
            // txtCategoryName
            // 
            txtCategoryName.BorderStyle = BorderStyle.FixedSingle;
            txtCategoryName.Font = new Font("Segoe UI", 10F);
            txtCategoryName.Location = new Point(20, 137);
            txtCategoryName.Name = "txtCategoryName";
            txtCategoryName.PlaceholderText = "Ví dụ: Rau củ hữu cơ, Trái cây sinh thái...";
            txtCategoryName.Size = new Size(308, 25);
            txtCategoryName.TabIndex = 3;
            // 
            // lblCategoryName
            // 
            lblCategoryName.AutoSize = true;
            lblCategoryName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCategoryName.ForeColor = Color.FromArgb(71, 85, 105);
            lblCategoryName.Location = new Point(18, 115);
            lblCategoryName.Name = "lblCategoryName";
            lblCategoryName.Size = new Size(96, 15);
            lblCategoryName.TabIndex = 2;
            lblCategoryName.Text = "Tên Nhóm hàng:";
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
            // lblApiUrl
            // 
            lblApiUrl.Font = new Font("Segoe UI", 8.5F);
            lblApiUrl.ForeColor = Color.FromArgb(148, 163, 184);
            lblApiUrl.Name = "lblApiUrl";
            lblApiUrl.Size = new Size(130, 17);
            lblApiUrl.Text = "API: /api/categories";
            // 
            // FormCategoryManagement
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(241, 245, 249);
            ClientSize = new Size(1200, 688);
            Controls.Add(panelBody);
            Controls.Add(statusStrip1);
            Controls.Add(panelTop);
            Font = new Font("Segoe UI", 9.75F);
            Name = "FormCategoryManagement";
            Padding = new Padding(12);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý Danh mục Nhóm hàng";
            Load += FormCategoryManagement_Load;
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCategories).EndInit();
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
        private DataGridView dgvCategories;
        private DataGridViewTextBoxColumn CategoryId;
        private DataGridViewTextBoxColumn CategoryName;
        private DataGridViewTextBoxColumn Description;
        private Panel panelInfo;
        private Label lblInfoTitle;
        private Label lblId;
        private TextBox txtId;
        private Label lblCategoryName;
        private TextBox txtCategoryName;
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