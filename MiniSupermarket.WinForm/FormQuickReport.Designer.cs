namespace MiniSupermarket.WinForms
{
    partial class FormQuickReport
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
            panelHeader = new Panel();
            btnRunReport = new Button();
            dtpReportDate = new DateTimePicker();
            lblSelectDate = new Label();
            lblReportHeader = new Label();
            panelSummary = new TableLayoutPanel();
            cardOrders = new Panel();
            lblOrdersIcon = new Label();
            lblOrdersTitle = new Label();
            lblTotalOrders = new Label();
            cardRevenue = new Panel();
            lblRevenueIcon = new Label();
            lblRevenueTitle = new Label();
            lblTotalRevenue = new Label();
            cardBestSeller = new Panel();
            lblBestSellerIcon = new Label();
            lblBestSellerTitle = new Label();
            lblBestSeller = new Label();
            panelContent = new Panel();
            panelGridActions = new Panel();
            btnCancelOrder = new Button();
            btnViewOrderDetail = new Button();
            btnRefresh = new Button();
            lblGridTitle = new Label();
            dgvOrders = new DataGridView();
            panelHeader.SuspendLayout();
            panelSummary.SuspendLayout();
            cardOrders.SuspendLayout();
            cardRevenue.SuspendLayout();
            cardBestSeller.SuspendLayout();
            panelContent.SuspendLayout();
            panelGridActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvOrders).BeginInit();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.White;
            panelHeader.Controls.Add(btnRunReport);
            panelHeader.Controls.Add(dtpReportDate);
            panelHeader.Controls.Add(lblSelectDate);
            panelHeader.Controls.Add(lblReportHeader);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(12, 12);
            panelHeader.Name = "panelHeader";
            panelHeader.Padding = new Padding(15, 12, 15, 12);
            panelHeader.Size = new Size(1176, 64);
            panelHeader.TabIndex = 0;
            // 
            // btnRunReport
            // 
            btnRunReport.BackColor = Color.FromArgb(5, 150, 105);
            btnRunReport.Cursor = Cursors.Hand;
            btnRunReport.FlatAppearance.BorderSize = 0;
            btnRunReport.FlatStyle = FlatStyle.Flat;
            btnRunReport.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnRunReport.ForeColor = Color.White;
            btnRunReport.Location = new Point(560, 14);
            btnRunReport.Name = "btnRunReport";
            btnRunReport.Size = new Size(160, 36);
            btnRunReport.TabIndex = 3;
            btnRunReport.Text = "📊 Xem Báo Cáo";
            btnRunReport.UseVisualStyleBackColor = false;
            // 
            // dtpReportDate
            // 
            dtpReportDate.CustomFormat = "dd/MM/yyyy";
            dtpReportDate.Font = new Font("Segoe UI", 10.5F);
            dtpReportDate.Format = DateTimePickerFormat.Custom;
            dtpReportDate.Location = new Point(390, 18);
            dtpReportDate.Name = "dtpReportDate";
            dtpReportDate.Size = new Size(155, 26);
            dtpReportDate.TabIndex = 2;
            // 
            // lblSelectDate
            // 
            lblSelectDate.AutoSize = true;
            lblSelectDate.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblSelectDate.ForeColor = Color.FromArgb(71, 85, 105);
            lblSelectDate.Location = new Point(300, 23);
            lblSelectDate.Name = "lblSelectDate";
            lblSelectDate.Size = new Size(84, 17);
            lblSelectDate.TabIndex = 1;
            lblSelectDate.Text = "Chọn ngày:";
            // 
            // lblReportHeader
            // 
            lblReportHeader.AutoSize = true;
            lblReportHeader.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblReportHeader.ForeColor = Color.FromArgb(15, 23, 42);
            lblReportHeader.Location = new Point(15, 20);
            lblReportHeader.Name = "lblReportHeader";
            lblReportHeader.Size = new Size(247, 21);
            lblReportHeader.TabIndex = 0;
            lblReportHeader.Text = "📈 BÁO CÁO DOANH THU BÁN LẺ";
            // 
            // panelSummary
            // 
            panelSummary.ColumnCount = 3;
            panelSummary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
            panelSummary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
            panelSummary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
            panelSummary.Controls.Add(cardOrders, 0, 0);
            panelSummary.Controls.Add(cardRevenue, 1, 0);
            panelSummary.Controls.Add(cardBestSeller, 2, 0);
            panelSummary.Dock = DockStyle.Top;
            panelSummary.Location = new Point(12, 86);
            panelSummary.Name = "panelSummary";
            panelSummary.RowCount = 1;
            panelSummary.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            panelSummary.Size = new Size(1176, 125);
            panelSummary.TabIndex = 1;
            // 
            // cardOrders
            // 
            cardOrders.BackColor = Color.FromArgb(37, 99, 235);
            cardOrders.Controls.Add(lblOrdersIcon);
            cardOrders.Controls.Add(lblOrdersTitle);
            cardOrders.Controls.Add(lblTotalOrders);
            cardOrders.Dock = DockStyle.Fill;
            cardOrders.Location = new Point(0, 0);
            cardOrders.Margin = new Padding(0, 0, 10, 0);
            cardOrders.Name = "cardOrders";
            cardOrders.Padding = new Padding(18);
            cardOrders.Size = new Size(382, 125);
            cardOrders.TabIndex = 0;
            // 
            // lblOrdersIcon
            // 
            lblOrdersIcon.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblOrdersIcon.AutoSize = true;
            lblOrdersIcon.Font = new Font("Segoe UI", 26F);
            lblOrdersIcon.ForeColor = Color.FromArgb(191, 219, 254);
            lblOrdersIcon.Location = new Point(310, 15);
            lblOrdersIcon.Name = "lblOrdersIcon";
            lblOrdersIcon.Size = new Size(61, 47);
            lblOrdersIcon.TabIndex = 2;
            lblOrdersIcon.Text = "🧾";
            // 
            // lblOrdersTitle
            // 
            lblOrdersTitle.AutoSize = true;
            lblOrdersTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblOrdersTitle.ForeColor = Color.FromArgb(219, 234, 254);
            lblOrdersTitle.Location = new Point(18, 18);
            lblOrdersTitle.Name = "lblOrdersTitle";
            lblOrdersTitle.Size = new Size(137, 17);
            lblOrdersTitle.TabIndex = 0;
            lblOrdersTitle.Text = "TỔNG SỐ HÓA ĐƠN";
            // 
            // lblTotalOrders
            // 
            lblTotalOrders.AutoSize = true;
            lblTotalOrders.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTotalOrders.ForeColor = Color.White;
            lblTotalOrders.Location = new Point(15, 52);
            lblTotalOrders.Name = "lblTotalOrders";
            lblTotalOrders.Size = new Size(114, 45);
            lblTotalOrders.TabIndex = 1;
            lblTotalOrders.Text = "0 đơn";
            // 
            // cardRevenue
            // 
            cardRevenue.BackColor = Color.FromArgb(5, 150, 105);
            cardRevenue.Controls.Add(lblRevenueIcon);
            cardRevenue.Controls.Add(lblRevenueTitle);
            cardRevenue.Controls.Add(lblTotalRevenue);
            cardRevenue.Dock = DockStyle.Fill;
            cardRevenue.Location = new Point(392, 0);
            cardRevenue.Margin = new Padding(0, 0, 10, 0);
            cardRevenue.Name = "cardRevenue";
            cardRevenue.Padding = new Padding(18);
            cardRevenue.Size = new Size(382, 125);
            cardRevenue.TabIndex = 1;
            // 
            // lblRevenueIcon
            // 
            lblRevenueIcon.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblRevenueIcon.AutoSize = true;
            lblRevenueIcon.Font = new Font("Segoe UI", 26F);
            lblRevenueIcon.ForeColor = Color.FromArgb(167, 243, 208);
            lblRevenueIcon.Location = new Point(310, 15);
            lblRevenueIcon.Name = "lblRevenueIcon";
            lblRevenueIcon.Size = new Size(61, 47);
            lblRevenueIcon.TabIndex = 2;
            lblRevenueIcon.Text = "💰";
            // 
            // lblRevenueTitle
            // 
            lblRevenueTitle.AutoSize = true;
            lblRevenueTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblRevenueTitle.ForeColor = Color.FromArgb(209, 250, 229);
            lblRevenueTitle.Location = new Point(18, 18);
            lblRevenueTitle.Name = "lblRevenueTitle";
            lblRevenueTitle.Size = new Size(130, 17);
            lblRevenueTitle.TabIndex = 0;
            lblRevenueTitle.Text = "TỔNG DOANH THU";
            // 
            // lblTotalRevenue
            // 
            lblTotalRevenue.AutoSize = true;
            lblTotalRevenue.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblTotalRevenue.ForeColor = Color.White;
            lblTotalRevenue.Location = new Point(15, 54);
            lblTotalRevenue.Name = "lblTotalRevenue";
            lblTotalRevenue.Size = new Size(70, 41);
            lblTotalRevenue.TabIndex = 1;
            lblTotalRevenue.Text = "0 đ";
            // 
            // cardBestSeller
            // 
            cardBestSeller.BackColor = Color.FromArgb(217, 119, 6);
            cardBestSeller.Controls.Add(lblBestSellerIcon);
            cardBestSeller.Controls.Add(lblBestSellerTitle);
            cardBestSeller.Controls.Add(lblBestSeller);
            cardBestSeller.Dock = DockStyle.Fill;
            cardBestSeller.Location = new Point(784, 0);
            cardBestSeller.Margin = new Padding(0);
            cardBestSeller.Name = "cardBestSeller";
            cardBestSeller.Padding = new Padding(18);
            cardBestSeller.Size = new Size(392, 125);
            cardBestSeller.TabIndex = 2;
            // 
            // lblBestSellerIcon
            // 
            lblBestSellerIcon.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblBestSellerIcon.AutoSize = true;
            lblBestSellerIcon.Font = new Font("Segoe UI", 26F);
            lblBestSellerIcon.ForeColor = Color.FromArgb(254, 243, 199);
            lblBestSellerIcon.Location = new Point(320, 15);
            lblBestSellerIcon.Name = "lblBestSellerIcon";
            lblBestSellerIcon.Size = new Size(61, 47);
            lblBestSellerIcon.TabIndex = 2;
            lblBestSellerIcon.Text = "🔥";
            // 
            // lblBestSellerTitle
            // 
            lblBestSellerTitle.AutoSize = true;
            lblBestSellerTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblBestSellerTitle.ForeColor = Color.FromArgb(254, 243, 199);
            lblBestSellerTitle.Location = new Point(18, 18);
            lblBestSellerTitle.Name = "lblBestSellerTitle";
            lblBestSellerTitle.Size = new Size(187, 17);
            lblBestSellerTitle.TabIndex = 0;
            lblBestSellerTitle.Text = "MẶT HÀNG BÁN CHẠY NHẤT";
            // 
            // lblBestSeller
            // 
            lblBestSeller.AutoEllipsis = true;
            lblBestSeller.Font = new Font("Segoe UI", 13.5F, FontStyle.Bold);
            lblBestSeller.ForeColor = Color.White;
            lblBestSeller.Location = new Point(18, 52);
            lblBestSeller.Name = "lblBestSeller";
            lblBestSeller.Size = new Size(295, 50);
            lblBestSeller.TabIndex = 1;
            lblBestSeller.Text = "Chưa có dữ liệu";
            // 
            // panelContent
            // 
            panelContent.BackColor = Color.White;
            panelContent.Controls.Add(dgvOrders);
            panelContent.Controls.Add(panelGridActions);
            panelContent.Dock = DockStyle.Fill;
            panelContent.Location = new Point(12, 221);
            panelContent.Name = "panelContent";
            panelContent.Padding = new Padding(18, 12, 18, 16);
            panelContent.Size = new Size(1176, 433);
            panelContent.TabIndex = 2;
            // 
            // panelGridActions
            // 
            panelGridActions.Controls.Add(btnCancelOrder);
            panelGridActions.Controls.Add(btnViewOrderDetail);
            panelGridActions.Controls.Add(btnRefresh);
            panelGridActions.Controls.Add(lblGridTitle);
            panelGridActions.Dock = DockStyle.Top;
            panelGridActions.Location = new Point(18, 12);
            panelGridActions.Name = "panelGridActions";
            panelGridActions.Size = new Size(1140, 42);
            panelGridActions.TabIndex = 0;
            // 
            // lblGridTitle
            // 
            lblGridTitle.Dock = DockStyle.Left;
            lblGridTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblGridTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblGridTitle.Location = new Point(0, 0);
            lblGridTitle.Name = "lblGridTitle";
            lblGridTitle.Size = new Size(400, 42);
            lblGridTitle.TabIndex = 0;
            lblGridTitle.Text = "📋 CHI TIẾT HÓA ĐƠN TRONG CA / NGÀY";
            lblGridTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnCancelOrder
            // 
            btnCancelOrder.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCancelOrder.BackColor = Color.FromArgb(220, 38, 38);
            btnCancelOrder.Cursor = Cursors.Hand;
            btnCancelOrder.FlatAppearance.BorderSize = 0;
            btnCancelOrder.FlatStyle = FlatStyle.Flat;
            btnCancelOrder.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnCancelOrder.ForeColor = Color.White;
            btnCancelOrder.Location = new Point(980, 4);
            btnCancelOrder.Name = "btnCancelOrder";
            btnCancelOrder.Size = new Size(160, 34);
            btnCancelOrder.TabIndex = 3;
            btnCancelOrder.Text = "❌ Hủy Đơn Hàng";
            btnCancelOrder.UseVisualStyleBackColor = false;
            // 
            // btnViewOrderDetail
            // 
            btnViewOrderDetail.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnViewOrderDetail.BackColor = Color.FromArgb(37, 99, 235);
            btnViewOrderDetail.Cursor = Cursors.Hand;
            btnViewOrderDetail.FlatAppearance.BorderSize = 0;
            btnViewOrderDetail.FlatStyle = FlatStyle.Flat;
            btnViewOrderDetail.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnViewOrderDetail.ForeColor = Color.White;
            btnViewOrderDetail.Location = new Point(810, 4);
            btnViewOrderDetail.Name = "btnViewOrderDetail";
            btnViewOrderDetail.Size = new Size(160, 34);
            btnViewOrderDetail.TabIndex = 2;
            btnViewOrderDetail.Text = "🔍 Xem Chi Tiết";
            btnViewOrderDetail.UseVisualStyleBackColor = false;
            // 
            // btnRefresh
            // 
            btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRefresh.BackColor = Color.FromArgb(241, 245, 249);
            btnRefresh.Cursor = Cursors.Hand;
            btnRefresh.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.FromArgb(71, 85, 105);
            btnRefresh.Location = new Point(695, 4);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(105, 34);
            btnRefresh.TabIndex = 1;
            btnRefresh.Text = "🔄 Tải lại";
            btnRefresh.UseVisualStyleBackColor = false;
            // 
            // dgvOrders
            // 
            dgvOrders.AllowUserToAddRows = false;
            dgvOrders.AllowUserToDeleteRows = false;
            dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvOrders.BackgroundColor = Color.White;
            dgvOrders.BorderStyle = BorderStyle.None;
            dgvOrders.ColumnHeadersHeight = 38;
            dgvOrders.Dock = DockStyle.Fill;
            dgvOrders.Location = new Point(18, 54);
            dgvOrders.MultiSelect = false;
            dgvOrders.Name = "dgvOrders";
            dgvOrders.ReadOnly = true;
            dgvOrders.RowHeadersVisible = false;
            dgvOrders.RowTemplate.Height = 34;
            dgvOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrders.Size = new Size(1140, 363);
            dgvOrders.TabIndex = 1;
            // 
            // FormQuickReport
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(241, 245, 249);
            ClientSize = new Size(1200, 666);
            Controls.Add(panelContent);
            Controls.Add(panelSummary);
            Controls.Add(panelHeader);
            Font = new Font("Segoe UI", 9.75F);
            Name = "FormQuickReport";
            Padding = new Padding(12);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Báo cáo Doanh thu & Hiệu suất Ca bán";
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelSummary.ResumeLayout(false);
            cardOrders.ResumeLayout(false);
            cardOrders.PerformLayout();
            cardRevenue.ResumeLayout(false);
            cardRevenue.PerformLayout();
            cardBestSeller.ResumeLayout(false);
            cardBestSeller.PerformLayout();
            panelContent.ResumeLayout(false);
            panelGridActions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvOrders).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelHeader;
        private Label lblReportHeader;
        private Label lblSelectDate;
        private DateTimePicker dtpReportDate;
        private Button btnRunReport;
        private TableLayoutPanel panelSummary;
        private Panel cardOrders;
        private Label lblOrdersTitle;
        private Label lblTotalOrders;
        private Label lblOrdersIcon;
        private Panel cardRevenue;
        private Label lblRevenueTitle;
        private Label lblTotalRevenue;
        private Label lblRevenueIcon;
        private Panel cardBestSeller;
        private Label lblBestSellerTitle;
        private Label lblBestSeller;
        private Label lblBestSellerIcon;
        private Panel panelContent;
        private Panel panelGridActions;
        private Label lblGridTitle;
        private Button btnCancelOrder;
        private Button btnViewOrderDetail;
        private Button btnRefresh;
        private DataGridView dgvOrders;
    }
}
