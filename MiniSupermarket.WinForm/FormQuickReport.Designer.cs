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
            lblOrdersTitle = new Label();
            lblTotalOrders = new Label();
            cardRevenue = new Panel();
            lblRevenueTitle = new Label();
            lblTotalRevenue = new Label();
            cardBestSeller = new Panel();
            lblBestSellerTitle = new Label();
            lblBestSeller = new Label();
            panelContent = new Panel();
            dgvOrders = new DataGridView();
            lblGridTitle = new Label();
            panelHeader.SuspendLayout();
            panelSummary.SuspendLayout();
            cardOrders.SuspendLayout();
            cardRevenue.SuspendLayout();
            cardBestSeller.SuspendLayout();
            panelContent.SuspendLayout();
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
            panelHeader.Location = new Point(15, 15);
            panelHeader.Name = "panelHeader";
            panelHeader.Padding = new Padding(15);
            panelHeader.Size = new Size(1020, 75);
            panelHeader.TabIndex = 0;
            // 
            // btnRunReport
            // 
            btnRunReport.BackColor = Color.FromArgb(24, 119, 242);
            btnRunReport.Cursor = Cursors.Hand;
            btnRunReport.FlatAppearance.BorderSize = 0;
            btnRunReport.FlatStyle = FlatStyle.Flat;
            btnRunReport.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnRunReport.ForeColor = Color.White;
            btnRunReport.Location = new Point(480, 20);
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
            dtpReportDate.Location = new Point(310, 24);
            dtpReportDate.Name = "dtpReportDate";
            dtpReportDate.Size = new Size(150, 26);
            dtpReportDate.TabIndex = 2;
            // 
            // lblSelectDate
            // 
            lblSelectDate.AutoSize = true;
            lblSelectDate.Font = new Font("Segoe UI", 10F);
            lblSelectDate.ForeColor = Color.FromArgb(64, 64, 64);
            lblSelectDate.Location = new Point(220, 28);
            lblSelectDate.Name = "lblSelectDate";
            lblSelectDate.Size = new Size(84, 19);
            lblSelectDate.TabIndex = 1;
            lblSelectDate.Text = "Chọn ngày:";
            // 
            // lblReportHeader
            // 
            lblReportHeader.AutoSize = true;
            lblReportHeader.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblReportHeader.ForeColor = Color.FromArgb(24, 30, 48);
            lblReportHeader.Location = new Point(15, 26);
            lblReportHeader.Name = "lblReportHeader";
            lblReportHeader.Size = new Size(185, 21);
            lblReportHeader.TabIndex = 0;
            lblReportHeader.Text = "BÁO CÁO DOANH THU";
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
            panelSummary.Location = new Point(15, 95);
            panelSummary.Name = "panelSummary";
            panelSummary.RowCount = 1;
            panelSummary.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            panelSummary.Size = new Size(1020, 115);
            panelSummary.TabIndex = 1;
            // 
            // cardOrders
            // 
            cardOrders.BackColor = Color.FromArgb(21, 101, 192);
            cardOrders.Controls.Add(lblOrdersTitle);
            cardOrders.Controls.Add(lblTotalOrders);
            cardOrders.Dock = DockStyle.Fill;
            cardOrders.Location = new Point(0, 0);
            cardOrders.Margin = new Padding(0, 0, 10, 0);
            cardOrders.Name = "cardOrders";
            cardOrders.Padding = new Padding(15);
            cardOrders.Size = new Size(330, 115);
            cardOrders.TabIndex = 0;
            // 
            // lblOrdersTitle
            // 
            lblOrdersTitle.AutoSize = true;
            lblOrdersTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblOrdersTitle.ForeColor = Color.WhiteSmoke;
            lblOrdersTitle.Location = new Point(15, 15);
            lblOrdersTitle.Name = "lblOrdersTitle";
            lblOrdersTitle.Size = new Size(137, 19);
            lblOrdersTitle.TabIndex = 0;
            lblOrdersTitle.Text = "TỔNG SỐ HÓA ĐƠN";
            // 
            // lblTotalOrders
            // 
            lblTotalOrders.AutoSize = true;
            lblTotalOrders.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblTotalOrders.ForeColor = Color.White;
            lblTotalOrders.Location = new Point(15, 45);
            lblTotalOrders.Name = "lblTotalOrders";
            lblTotalOrders.Size = new Size(101, 41);
            lblTotalOrders.TabIndex = 1;
            lblTotalOrders.Text = "0 đơn";
            // 
            // cardRevenue
            // 
            cardRevenue.BackColor = Color.FromArgb(46, 125, 50);
            cardRevenue.Controls.Add(lblRevenueTitle);
            cardRevenue.Controls.Add(lblTotalRevenue);
            cardRevenue.Dock = DockStyle.Fill;
            cardRevenue.Location = new Point(340, 0);
            cardRevenue.Margin = new Padding(0, 0, 10, 0);
            cardRevenue.Name = "cardRevenue";
            cardRevenue.Padding = new Padding(15);
            cardRevenue.Size = new Size(330, 115);
            cardRevenue.TabIndex = 1;
            // 
            // lblRevenueTitle
            // 
            lblRevenueTitle.AutoSize = true;
            lblRevenueTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblRevenueTitle.ForeColor = Color.WhiteSmoke;
            lblRevenueTitle.Location = new Point(15, 15);
            lblRevenueTitle.Name = "lblRevenueTitle";
            lblRevenueTitle.Size = new Size(133, 19);
            lblRevenueTitle.TabIndex = 0;
            lblRevenueTitle.Text = "TỔNG DOANH THU";
            // 
            // lblTotalRevenue
            // 
            lblTotalRevenue.AutoSize = true;
            lblTotalRevenue.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTotalRevenue.ForeColor = Color.White;
            lblTotalRevenue.Location = new Point(15, 47);
            lblTotalRevenue.Name = "lblTotalRevenue";
            lblTotalRevenue.Size = new Size(65, 37);
            lblTotalRevenue.TabIndex = 1;
            lblTotalRevenue.Text = "0 đ";
            // 
            // cardBestSeller
            // 
            cardBestSeller.BackColor = Color.FromArgb(230, 81, 0);
            cardBestSeller.Controls.Add(lblBestSellerTitle);
            cardBestSeller.Controls.Add(lblBestSeller);
            cardBestSeller.Dock = DockStyle.Fill;
            cardBestSeller.Location = new Point(680, 0);
            cardBestSeller.Margin = new Padding(0);
            cardBestSeller.Name = "cardBestSeller";
            cardBestSeller.Padding = new Padding(15);
            cardBestSeller.Size = new Size(340, 115);
            cardBestSeller.TabIndex = 2;
            // 
            // lblBestSellerTitle
            // 
            lblBestSellerTitle.AutoSize = true;
            lblBestSellerTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblBestSellerTitle.ForeColor = Color.WhiteSmoke;
            lblBestSellerTitle.Location = new Point(15, 15);
            lblBestSellerTitle.Name = "lblBestSellerTitle";
            lblBestSellerTitle.Size = new Size(191, 19);
            lblBestSellerTitle.TabIndex = 0;
            lblBestSellerTitle.Text = "MẶT HÀNG BÁN CHẠY NHẤT";
            // 
            // lblBestSeller
            // 
            lblBestSeller.AutoEllipsis = true;
            lblBestSeller.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblBestSeller.ForeColor = Color.White;
            lblBestSeller.Location = new Point(15, 48);
            lblBestSeller.Name = "lblBestSeller";
            lblBestSeller.Size = new Size(310, 52);
            lblBestSeller.TabIndex = 1;
            lblBestSeller.Text = "Chưa có dữ liệu";
            // 
            // panelContent
            // 
            panelContent.BackColor = Color.White;
            panelContent.Controls.Add(dgvOrders);
            panelContent.Controls.Add(lblGridTitle);
            panelContent.Dock = DockStyle.Fill;
            panelContent.Location = new Point(15, 215);
            panelContent.Name = "panelContent";
            panelContent.Padding = new Padding(15);
            panelContent.Size = new Size(1020, 390);
            panelContent.TabIndex = 2;
            // 
            // dgvOrders
            // 
            dgvOrders.AllowUserToAddRows = false;
            dgvOrders.AllowUserToDeleteRows = false;
            dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvOrders.BackgroundColor = Color.White;
            dgvOrders.BorderStyle = BorderStyle.None;
            dgvOrders.ColumnHeadersHeight = 35;
            dgvOrders.Dock = DockStyle.Fill;
            dgvOrders.Location = new Point(15, 45);
            dgvOrders.MultiSelect = false;
            dgvOrders.Name = "dgvOrders";
            dgvOrders.ReadOnly = true;
            dgvOrders.RowHeadersVisible = false;
            dgvOrders.RowTemplate.Height = 30;
            dgvOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrders.Size = new Size(990, 330);
            dgvOrders.TabIndex = 1;
            // 
            // lblGridTitle
            // 
            lblGridTitle.AutoSize = true;
            lblGridTitle.Dock = DockStyle.Top;
            lblGridTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblGridTitle.ForeColor = Color.FromArgb(64, 64, 64);
            lblGridTitle.Location = new Point(15, 15);
            lblGridTitle.Name = "lblGridTitle";
            lblGridTitle.Padding = new Padding(0, 0, 0, 10);
            lblGridTitle.Size = new Size(207, 30);
            lblGridTitle.TabIndex = 0;
            lblGridTitle.Text = "Danh Sách Hóa Đơn Trong Ngày";
            // 
            // FormQuickReport
            // 
            BackColor = Color.FromArgb(244, 245, 247);
            ClientSize = new Size(1050, 620);
            Controls.Add(panelContent);
            Controls.Add(panelSummary);
            Controls.Add(panelHeader);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormQuickReport";
            Padding = new Padding(15);
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelSummary.ResumeLayout(false);
            cardOrders.ResumeLayout(false);
            cardOrders.PerformLayout();
            cardRevenue.ResumeLayout(false);
            cardRevenue.PerformLayout();
            cardBestSeller.ResumeLayout(false);
            panelContent.ResumeLayout(false);
            panelContent.PerformLayout();
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
        private Panel cardRevenue;
        private Label lblRevenueTitle;
        private Label lblTotalRevenue;
        private Panel cardBestSeller;
        private Label lblBestSellerTitle;
        private Label lblBestSeller;
        private Panel panelContent;
        private Label lblGridTitle;
        private DataGridView dgvOrders;
    }
}
