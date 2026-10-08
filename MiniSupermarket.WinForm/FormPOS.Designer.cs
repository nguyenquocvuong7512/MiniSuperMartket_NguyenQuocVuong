namespace MiniSupermarket.WinForms.Forms
{
    partial class FormPOS
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblBarcode = new System.Windows.Forms.Label();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dgvProducts = new System.Windows.Forms.DataGridView();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.nudQuantity = new System.Windows.Forms.NumericUpDown();
            this.lblCart = new System.Windows.Forms.Label();
            this.dgvCart = new System.Windows.Forms.DataGridView();
            this.colProductId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUnitPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSubTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblPayment = new System.Windows.Forms.Label();
            this.lblTotalTitle = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblMethod = new System.Windows.Forms.Label();
            this.cboPaymentMethod = new System.Windows.Forms.ComboBox();
            this.lblReceived = new System.Windows.Forms.Label();
            this.txtAmountReceived = new System.Windows.Forms.TextBox();
            this.lblChangeTitle = new System.Windows.Forms.Label();
            this.lblChange = new System.Windows.Forms.Label();
            this.btnPay = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).BeginInit();
            this.SuspendLayout();

            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(25, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(332, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "HỆ THỐNG BÁN HÀNG - POS";

            // 
            // lblBarcode
            // 
            this.lblBarcode.AutoSize = true;
            this.lblBarcode.Location = new System.Drawing.Point(25, 75);
            this.lblBarcode.Name = "lblBarcode";
            this.lblBarcode.Size = new System.Drawing.Size(64, 19);
            this.lblBarcode.TabIndex = 1;
            this.lblBarcode.Text = "Mã vạch:";

            // 
            // txtBarcode
            // 
            this.txtBarcode.Location = new System.Drawing.Point(100, 70);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(220, 25);
            this.txtBarcode.TabIndex = 2;

            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(330, 68);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(130, 30);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Thêm sản phẩm";
            this.btnAdd.UseVisualStyleBackColor = true;

            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(25, 120);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(65, 19);
            this.lblSearch.TabIndex = 4;
            this.lblSearch.Text = "Tìm kiếm:";

            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(100, 115);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(220, 25);
            this.txtSearch.TabIndex = 5;

            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(330, 113);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(80, 30);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "Tìm";
            this.btnSearch.UseVisualStyleBackColor = true;

            // 
            // dgvProducts
            // 
            this.dgvProducts.AllowUserToAddRows = false;
            this.dgvProducts.AllowUserToDeleteRows = false;
            this.dgvProducts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProducts.Location = new System.Drawing.Point(25, 160);
            this.dgvProducts.Name = "dgvProducts";
            this.dgvProducts.ReadOnly = true;
            this.dgvProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProducts.Size = new System.Drawing.Size(520, 200);
            this.dgvProducts.TabIndex = 7;

            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Location = new System.Drawing.Point(25, 375);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(66, 19);
            this.lblQuantity.TabIndex = 8;
            this.lblQuantity.Text = "Số lượng:";

            // 
            // nudQuantity
            // 
            this.nudQuantity.Location = new System.Drawing.Point(100, 370);
            this.nudQuantity.Maximum = new decimal(new int[] { 999, 0, 0, 0 });
            this.nudQuantity.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudQuantity.Name = "nudQuantity";
            this.nudQuantity.Size = new System.Drawing.Size(100, 25);
            this.nudQuantity.TabIndex = 9;
            this.nudQuantity.Value = new decimal(new int[] { 1, 0, 0, 0 });

            // 
            // lblCart
            // 
            this.lblCart.AutoSize = true;
            this.lblCart.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCart.Location = new System.Drawing.Point(580, 70);
            this.lblCart.Name = "lblCart";
            this.lblCart.Size = new System.Drawing.Size(91, 21);
            this.lblCart.TabIndex = 10;
            this.lblCart.Text = "GIỎ HÀNG";

            // 
            // dgvCart
            // 
            this.dgvCart.AllowUserToAddRows = false;
            this.dgvCart.AllowUserToDeleteRows = false;
            this.dgvCart.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCart.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCart.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colProductId,
            this.colProductName,
            this.colQuantity,
            this.colUnitPrice,
            this.colSubTotal});
            this.dgvCart.Location = new System.Drawing.Point(580, 105);
            this.dgvCart.Name = "dgvCart";
            this.dgvCart.ReadOnly = true;
            this.dgvCart.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCart.Size = new System.Drawing.Size(570, 255);
            this.dgvCart.TabIndex = 11;

            // 
            // colProductId
            // 
            this.colProductId.DataPropertyName = "ProductId";
            this.colProductId.HeaderText = "Mã";
            this.colProductId.Name = "colProductId";
            this.colProductId.ReadOnly = true;

            // 
            // colProductName
            // 
            this.colProductName.DataPropertyName = "ProductName";
            this.colProductName.HeaderText = "Sản phẩm";
            this.colProductName.Name = "colProductName";
            this.colProductName.ReadOnly = true;

            // 
            // colQuantity
            // 
            this.colQuantity.DataPropertyName = "Quantity";
            this.colQuantity.HeaderText = "SL";
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.ReadOnly = true;

            // 
            // colUnitPrice
            // 
            this.colUnitPrice.DataPropertyName = "UnitPrice";
            this.colUnitPrice.DefaultCellStyle.Format = "N0";
            this.colUnitPrice.HeaderText = "Đơn giá";
            this.colUnitPrice.Name = "colUnitPrice";
            this.colUnitPrice.ReadOnly = true;

            // 
            // colSubTotal
            // 
            this.colSubTotal.DataPropertyName = "SubTotal";
            this.colSubTotal.DefaultCellStyle.Format = "N0";
            this.colSubTotal.HeaderText = "Thành tiền";
            this.colSubTotal.Name = "colSubTotal";
            this.colSubTotal.ReadOnly = true;

            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(580, 375);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(120, 32);
            this.btnRemove.TabIndex = 12;
            this.btnRemove.Text = "Xóa sản phẩm";
            this.btnRemove.UseVisualStyleBackColor = true;

            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(710, 375);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(120, 32);
            this.btnClear.TabIndex = 13;
            this.btnClear.Text = "Xóa giỏ hàng";
            this.btnClear.UseVisualStyleBackColor = true;

            // 
            // lblPayment
            // 
            this.lblPayment.AutoSize = true;
            this.lblPayment.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblPayment.Location = new System.Drawing.Point(580, 430);
            this.lblPayment.Name = "lblPayment";
            this.lblPayment.Size = new System.Drawing.Size(112, 21);
            this.lblPayment.TabIndex = 14;
            this.lblPayment.Text = "THANH TOÁN";

            // 
            // lblTotalTitle
            // 
            this.lblTotalTitle.AutoSize = true;
            this.lblTotalTitle.Location = new System.Drawing.Point(580, 470);
            this.lblTotalTitle.Name = "lblTotalTitle";
            this.lblTotalTitle.Size = new System.Drawing.Size(70, 19);
            this.lblTotalTitle.TabIndex = 15;
            this.lblTotalTitle.Text = "Tổng tiền:";

            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTotal.Location = new System.Drawing.Point(700, 465);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(43, 25);
            this.lblTotal.TabIndex = 16;
            this.lblTotal.Text = "0 đ";

            // 
            // lblMethod
            // 
            this.lblMethod.AutoSize = true;
            this.lblMethod.Location = new System.Drawing.Point(580, 510);
            this.lblMethod.Name = "lblMethod";
            this.lblMethod.Size = new System.Drawing.Size(82, 19);
            this.lblMethod.TabIndex = 17;
            this.lblMethod.Text = "Thanh toán:";

            // 
            // cboPaymentMethod
            // 
            this.cboPaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPaymentMethod.FormattingEnabled = true;
            this.cboPaymentMethod.Items.AddRange(new object[] { "Cash", "Transfer" });
            this.cboPaymentMethod.Location = new System.Drawing.Point(700, 505);
            this.cboPaymentMethod.Name = "cboPaymentMethod";
            this.cboPaymentMethod.Size = new System.Drawing.Size(180, 25);
            this.cboPaymentMethod.TabIndex = 18;

            // 
            // lblReceived
            // 
            this.lblReceived.AutoSize = true;
            this.lblReceived.Location = new System.Drawing.Point(580, 550);
            this.lblReceived.Name = "lblReceived";
            this.lblReceived.Size = new System.Drawing.Size(76, 19);
            this.lblReceived.TabIndex = 19;
            this.lblReceived.Text = "Khách đưa:";

            // 
            // txtAmountReceived
            // 
            this.txtAmountReceived.Location = new System.Drawing.Point(700, 545);
            this.txtAmountReceived.Name = "txtAmountReceived";
            this.txtAmountReceived.Size = new System.Drawing.Size(180, 25);
            this.txtAmountReceived.TabIndex = 20;

            // 
            // lblChangeTitle
            // 
            this.lblChangeTitle.AutoSize = true;
            this.lblChangeTitle.Location = new System.Drawing.Point(580, 590);
            this.lblChangeTitle.Name = "lblChangeTitle";
            this.lblChangeTitle.Size = new System.Drawing.Size(69, 19);
            this.lblChangeTitle.TabIndex = 21;
            this.lblChangeTitle.Text = "Tiền thừa:";

            // 
            // lblChange
            // 
            this.lblChange.AutoSize = true;
            this.lblChange.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblChange.Location = new System.Drawing.Point(700, 585);
            this.lblChange.Name = "lblChange";
            this.lblChange.Size = new System.Drawing.Size(39, 25);
            this.lblChange.TabIndex = 22;
            this.lblChange.Text = "0 đ";

            // 
            // btnPay
            // 
            this.btnPay.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.btnPay.Location = new System.Drawing.Point(900, 500);
            this.btnPay.Name = "btnPay";
            this.btnPay.Size = new System.Drawing.Size(220, 90);
            this.btnPay.TabIndex = 23;
            this.btnPay.Text = "THANH TOÁN";
            this.btnPay.UseVisualStyleBackColor = true;

            // 
            // FormPOS
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.btnPay);
            this.Controls.Add(this.lblChange);
            this.Controls.Add(this.lblChangeTitle);
            this.Controls.Add(this.txtAmountReceived);
            this.Controls.Add(this.lblReceived);
            this.Controls.Add(this.cboPaymentMethod);
            this.Controls.Add(this.lblMethod);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblTotalTitle);
            this.Controls.Add(this.lblPayment);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.dgvCart);
            this.Controls.Add(this.lblCart);
            this.Controls.Add(this.nudQuantity);
            this.Controls.Add(this.lblQuantity);
            this.Controls.Add(this.dgvProducts);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtBarcode);
            this.Controls.Add(this.lblBarcode);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "FormPOS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "POS - Bán hàng";

            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblBarcode;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dgvProducts;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.NumericUpDown nudQuantity;
        private System.Windows.Forms.Label lblCart;
        private System.Windows.Forms.DataGridView dgvCart;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnitPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubTotal;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblPayment;
        private System.Windows.Forms.Label lblTotalTitle;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblMethod;
        private System.Windows.Forms.ComboBox cboPaymentMethod;
        private System.Windows.Forms.Label lblReceived;
        private System.Windows.Forms.TextBox txtAmountReceived;
        private System.Windows.Forms.Label lblChangeTitle;
        private System.Windows.Forms.Label lblChange;
        private System.Windows.Forms.Button btnPay;
    }
}