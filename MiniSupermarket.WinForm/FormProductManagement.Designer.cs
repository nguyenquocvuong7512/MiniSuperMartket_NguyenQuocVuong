namespace MiniSupermarket.WinForms
{
    partial class FormProductManagement
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
            btnSearch = new Button();
            btnLoad = new Button();
            txtKeyword = new TextBox();
            lblSearch = new Label();
            panelInputs = new Panel();
            btnClear = new Button();
            btnDelete = new Button();
            btnUpdate = new Button();
            btnAdd = new Button();
            cbCategory = new ComboBox();
            lblCategory = new Label();
            txtUnit = new TextBox();
            lblUnit = new Label();
            txtStockQuantity = new TextBox();
            lblStock = new Label();
            txtPrice = new TextBox();
            lblPrice = new Label();
            txtProductName = new TextBox();
            lblProductName = new Label();
            txtBarcode = new TextBox();
            lblBarcode = new Label();
            txtProductId = new TextBox();
            lblProductId = new Label();
            statusStrip = new StatusStrip();
            lblReady = new ToolStripStatusLabel();
            dgvProducts = new DataGridView();
            panelTop.SuspendLayout();
            panelInputs.SuspendLayout();
            statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.White;
            panelTop.Controls.Add(btnSearch);
            panelTop.Controls.Add(btnLoad);
            panelTop.Controls.Add(txtKeyword);
            panelTop.Controls.Add(lblSearch);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(1050, 50);
            panelTop.TabIndex = 0;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(342, 11);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(85, 29);
            btnSearch.TabIndex = 1;
            btnSearch.Text = "🔍 Tìm";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // btnLoad
            // 
            btnLoad.Location = new Point(433, 11);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(100, 29);
            btnLoad.TabIndex = 2;
            btnLoad.Text = "🔄 Tải lại";
            btnLoad.UseVisualStyleBackColor = true;
            // 
            // txtKeyword
            // 
            txtKeyword.Location = new Point(86, 13);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.Size = new Size(250, 25);
            txtKeyword.TabIndex = 0;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Location = new Point(15, 16);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(67, 19);
            lblSearch.TabIndex = 3;
            lblSearch.Text = "Tìm kiếm:";
            // 
            // panelInputs
            // 
            panelInputs.BackColor = Color.White;
            panelInputs.Controls.Add(btnClear);
            panelInputs.Controls.Add(btnDelete);
            panelInputs.Controls.Add(btnUpdate);
            panelInputs.Controls.Add(btnAdd);
            panelInputs.Controls.Add(cbCategory);
            panelInputs.Controls.Add(lblCategory);
            panelInputs.Controls.Add(txtUnit);
            panelInputs.Controls.Add(lblUnit);
            panelInputs.Controls.Add(txtStockQuantity);
            panelInputs.Controls.Add(lblStock);
            panelInputs.Controls.Add(txtPrice);
            panelInputs.Controls.Add(lblPrice);
            panelInputs.Controls.Add(txtProductName);
            panelInputs.Controls.Add(lblProductName);
            panelInputs.Controls.Add(txtBarcode);
            panelInputs.Controls.Add(lblBarcode);
            panelInputs.Controls.Add(txtProductId);
            panelInputs.Controls.Add(lblProductId);
            panelInputs.Dock = DockStyle.Right;
            panelInputs.Location = new Point(700, 50);
            panelInputs.Name = "panelInputs";
            panelInputs.Size = new Size(350, 588);
            panelInputs.TabIndex = 1;
            panelInputs.Paint += panelInputs_Paint;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(185, 365);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(140, 38);
            btnClear.TabIndex = 10;
            btnClear.Text = "\U0001f9f9 Nhập lại";
            btnClear.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(24, 365);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(140, 38);
            btnDelete.TabIndex = 9;
            btnDelete.Text = "❌ Xóa";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(185, 315);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(140, 38);
            btnUpdate.TabIndex = 8;
            btnUpdate.Text = "✏️ Sửa";
            btnUpdate.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(24, 315);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(140, 38);
            btnAdd.TabIndex = 7;
            btnAdd.Text = "➕ Thêm";
            btnAdd.UseVisualStyleBackColor = true;
            // 
            // cbCategory
            // 
            cbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cbCategory.FormattingEnabled = true;
            cbCategory.Location = new Point(110, 257);
            cbCategory.Name = "cbCategory";
            cbCategory.Size = new Size(215, 25);
            cbCategory.TabIndex = 6;
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.Location = new Point(20, 260);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(75, 19);
            lblCategory.TabIndex = 11;
            lblCategory.Text = "Danh mục:";
            // 
            // txtUnit
            // 
            txtUnit.Location = new Point(110, 217);
            txtUnit.Name = "txtUnit";
            txtUnit.Size = new Size(215, 25);
            txtUnit.TabIndex = 5;
            // 
            // lblUnit
            // 
            lblUnit.AutoSize = true;
            lblUnit.Location = new Point(20, 220);
            lblUnit.Name = "lblUnit";
            lblUnit.Size = new Size(52, 19);
            lblUnit.TabIndex = 12;
            lblUnit.Text = "Đơn vị:";
            // 
            // txtStockQuantity
            // 
            txtStockQuantity.Location = new Point(110, 177);
            txtStockQuantity.Name = "txtStockQuantity";
            txtStockQuantity.Size = new Size(215, 25);
            txtStockQuantity.TabIndex = 4;
            // 
            // lblStock
            // 
            lblStock.AutoSize = true;
            lblStock.Location = new Point(20, 180);
            lblStock.Name = "lblStock";
            lblStock.Size = new Size(62, 19);
            lblStock.TabIndex = 13;
            lblStock.Text = "Tồn kho:";
            // 
            // txtPrice
            // 
            txtPrice.Location = new Point(110, 137);
            txtPrice.Name = "txtPrice";
            txtPrice.Size = new Size(215, 25);
            txtPrice.TabIndex = 3;
            // 
            // lblPrice
            // 
            lblPrice.AutoSize = true;
            lblPrice.Location = new Point(20, 140);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(32, 19);
            lblPrice.TabIndex = 14;
            lblPrice.Text = "Giá:";
            // 
            // txtProductName
            // 
            txtProductName.Location = new Point(110, 97);
            txtProductName.Name = "txtProductName";
            txtProductName.Size = new Size(215, 25);
            txtProductName.TabIndex = 2;
            // 
            // lblProductName
            // 
            lblProductName.AutoSize = true;
            lblProductName.Location = new Point(20, 100);
            lblProductName.Name = "lblProductName";
            lblProductName.Size = new Size(52, 19);
            lblProductName.TabIndex = 15;
            lblProductName.Text = "Tên SP:";
            // 
            // txtBarcode
            // 
            txtBarcode.Location = new Point(110, 57);
            txtBarcode.Name = "txtBarcode";
            txtBarcode.Size = new Size(215, 25);
            txtBarcode.TabIndex = 1;
            // 
            // lblBarcode
            // 
            lblBarcode.AutoSize = true;
            lblBarcode.Location = new Point(20, 60);
            lblBarcode.Name = "lblBarcode";
            lblBarcode.Size = new Size(64, 19);
            lblBarcode.TabIndex = 16;
            lblBarcode.Text = "Mã vạch:";
            // 
            // txtProductId
            // 
            txtProductId.Location = new Point(110, 17);
            txtProductId.Name = "txtProductId";
            txtProductId.ReadOnly = true;
            txtProductId.Size = new Size(215, 25);
            txtProductId.TabIndex = 0;
            // 
            // lblProductId
            // 
            lblProductId.AutoSize = true;
            lblProductId.Location = new Point(20, 20);
            lblProductId.Name = "lblProductId";
            lblProductId.Size = new Size(26, 19);
            lblProductId.TabIndex = 17;
            lblProductId.Text = "ID:";
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { lblReady });
            statusStrip.Location = new Point(0, 638);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(1050, 22);
            statusStrip.TabIndex = 2;
            // 
            // lblReady
            // 
            lblReady.Name = "lblReady";
            lblReady.Size = new Size(54, 17);
            lblReady.Text = "Sẵn sàng";
            // 
            // dgvProducts
            // 
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.AllowUserToDeleteRows = false;
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProducts.Dock = DockStyle.Fill;
            dgvProducts.Location = new Point(0, 50);
            dgvProducts.MultiSelect = false;
            dgvProducts.Name = "dgvProducts";
            dgvProducts.ReadOnly = true;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.Size = new Size(700, 588);
            dgvProducts.TabIndex = 3;
            // 
            // FormProductManagement
            // 
            ClientSize = new Size(1050, 660);
            Controls.Add(dgvProducts);
            Controls.Add(panelInputs);
            Controls.Add(panelTop);
            Controls.Add(statusStrip);
            Font = new Font("Segoe UI", 10F);
            Name = "FormProductManagement";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "QUẢN LÝ SẢN PHẨM";
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelInputs.ResumeLayout(false);
            panelInputs.PerformLayout();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtKeyword;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnLoad;

        private System.Windows.Forms.Panel panelInputs;
        private System.Windows.Forms.Label lblProductId;
        private System.Windows.Forms.TextBox txtProductId;
        private System.Windows.Forms.Label lblBarcode;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label lblStock;
        private System.Windows.Forms.TextBox txtStockQuantity;
        private System.Windows.Forms.Label lblUnit;
        private System.Windows.Forms.TextBox txtUnit;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cbCategory;

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblReady;
        private System.Windows.Forms.DataGridView dgvProducts;
    }
}