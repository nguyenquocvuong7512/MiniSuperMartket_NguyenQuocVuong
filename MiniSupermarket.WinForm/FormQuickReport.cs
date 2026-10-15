using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormQuickReport : Form
    {
        public FormQuickReport()
        {
            InitializeComponent();
            SetupGridColumns();

            btnRunReport.Click += async (s, e) => await LoadReportDataAsync();
            this.Load += async (s, e) => await LoadReportDataAsync();
        }

        private void SetupGridColumns()
        {
            dgvOrders.AutoGenerateColumns = false;
            dgvOrders.Columns.Clear();

            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "OrderId",
                HeaderText = "Mã HĐ",
                Width = 90
            });

            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "OrderTimeStr",
                HeaderText = "Thời Gian",
                Width = 140
            });

            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Employee",
                HeaderText = "Thu Ngân",
                Width = 160
            });

            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PaymentMethod",
                HeaderText = "Hình Thức",
                Width = 120
            });

            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TotalAmountStr",
                HeaderText = "Tổng Tiền",
                Width = 140
            });

            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ItemsSummary",
                HeaderText = "Chi Tiết Mặt Hàng",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Status",
                HeaderText = "Trạng Thái",
                Width = 110
            });
        }

        private async Task LoadReportDataAsync()
        {
            // Kiểm tra phân quyền cấp Action: Chỉ Admin mới được xem
            if (SessionManager.CurrentRole.Trim().ToUpper() != "ADMIN")
            {
                MessageBox.Show(
                    "Bạn không có quyền xem dữ liệu tài chính của siêu thị!",
                    "Từ chối",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Stop
                );
                return;
            }

            try
            {
                btnRunReport.Enabled = false;
                btnRunReport.Text = "Đang tải...";

                var response = await ApiClientService.Client.GetAsync("orders");
                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Không thể tải dữ liệu đơn hàng từ máy chủ!",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }

                var jsonStr = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(jsonStr);

                if (!doc.RootElement.TryGetProperty("data", out var dataElem))
                {
                    return;
                }

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var allOrders = JsonSerializer.Deserialize<List<OrderReportDto>>(dataElem.GetRawText(), options) ?? new List<OrderReportDto>();

                DateTime selectedDate = dtpReportDate.Value.Date;
                var filtered = allOrders
                    .Where(o => o.OrderDate.Date == selectedDate)
                    .OrderByDescending(o => o.OrderDate)
                    .ToList();

                // 1. Thống kê tổng số đơn
                lblTotalOrders.Text = $"{filtered.Count:N0} đơn";

                // 2. Thống kê tổng doanh thu (chỉ tính đơn hợp lệ, không tính Cancelled nếu có)
                decimal totalRev = filtered
                    .Where(o => !string.Equals(o.Status, "Cancelled", StringComparison.OrdinalIgnoreCase))
                    .Sum(o => o.TotalAmount);
                lblTotalRevenue.Text = $"{totalRev:N0} đ";

                // 3. Tìm mặt hàng bán chạy nhất trong ngày
                var bestSellerItem = filtered
                    .SelectMany(o => o.Items)
                    .GroupBy(i => i.ProductName)
                    .Select(g => new
                    {
                        ProductName = g.Key,
                        TotalQty = g.Sum(x => x.Quantity)
                    })
                    .OrderByDescending(x => x.TotalQty)
                    .FirstOrDefault();

                if (bestSellerItem != null)
                {
                    lblBestSeller.Text = $"{bestSellerItem.ProductName} ({bestSellerItem.TotalQty:N0} cái)";
                }
                else
                {
                    lblBestSeller.Text = "Chưa có giao dịch";
                }

                // 4. Hiển thị bảng chi tiết
                var displayList = filtered.Select(o => new
                {
                    OrderId = $"#{o.OrderId}",
                    OrderTimeStr = o.OrderDate.ToString("dd/MM/yyyy HH:mm"),
                    Employee = o.Employee,
                    PaymentMethod = o.PaymentMethod,
                    TotalAmountStr = $"{o.TotalAmount:N0} đ",
                    ItemsSummary = string.Join(", ", o.Items.Select(i => $"{i.ProductName} (x{i.Quantity})")),
                    Status = o.Status == "Completed" ? "Hoàn thành" : o.Status
                }).ToList();

                dgvOrders.DataSource = displayList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải báo cáo: " + ex.Message,
                    "Lỗi hệ thống",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                btnRunReport.Enabled = true;
                btnRunReport.Text = "📊 Xem Báo Cáo";
            }
        }
    }

    public class OrderReportDto
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;
        public decimal AmountReceived { get; set; }
        public decimal ChangeAmount { get; set; }
        public string Employee { get; set; } = string.Empty;
        public List<OrderItemReportDto> Items { get; set; } = new();
    }

    public class OrderItemReportDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SubTotal { get; set; }
    }
}
