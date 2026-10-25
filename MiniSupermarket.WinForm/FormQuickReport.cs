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
        private List<OrderReportDto> _cachedFilteredOrders = new();

        public FormQuickReport()
        {
            InitializeComponent();
            Theme.UITheme.ApplyModernDataGridView(dgvOrders);
            Theme.UITheme.StyleButton(btnRunReport, Theme.UITheme.ButtonVariant.Primary);
            Theme.UITheme.StyleButton(btnCancelOrder, Theme.UITheme.ButtonVariant.Danger);
            Theme.UITheme.StyleButton(btnViewOrderDetail, Theme.UITheme.ButtonVariant.Secondary);
            Theme.UITheme.StyleButton(btnRefresh, Theme.UITheme.ButtonVariant.Neutral);
            SetupGridColumns();

            btnRunReport.Click += async (s, e) => await LoadReportDataAsync();
            btnRefresh.Click += async (s, e) => await LoadReportDataAsync();
            btnCancelOrder.Click += async (s, e) => await OpenCancelOrderDialogAsync();
            btnViewOrderDetail.Click += (s, e) => ShowSelectedOrderDetail();
            dgvOrders.CellDoubleClick += (s, e) => ShowSelectedOrderDetail();
            dgvOrders.RowPrePaint += DgvOrders_RowPrePaint;

            this.Load += async (s, e) => await LoadReportDataAsync();
        }

        private void DgvOrders_RowPrePaint(object? sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvOrders.Rows.Count)
            {
                var row = dgvOrders.Rows[e.RowIndex];
                var statusCell = row.Cells["colStatus"] ?? (row.Cells.Count > 6 ? row.Cells[6] : null);
                if (statusCell?.Value != null)
                {
                    string status = statusCell.Value.ToString() ?? "";
                    if (status.Contains("hủy", StringComparison.OrdinalIgnoreCase) || status.Contains("cancel", StringComparison.OrdinalIgnoreCase))
                    {
                        row.DefaultCellStyle.ForeColor = Color.FromArgb(148, 163, 184); // Xám nhạt
                        row.DefaultCellStyle.SelectionForeColor = Color.FromArgb(239, 68, 68); // Đỏ khi chọn
                    }
                }
            }
        }

        private void SetupGridColumns()
        {
            dgvOrders.AutoGenerateColumns = false;
            dgvOrders.Columns.Clear();

            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colOrderId",
                DataPropertyName = "OrderId",
                HeaderText = "Mã HĐ",
                Width = 90
            });

            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTime",
                DataPropertyName = "OrderTimeStr",
                HeaderText = "Thời Gian",
                Width = 140
            });

            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colEmployee",
                DataPropertyName = "Employee",
                HeaderText = "Thu Ngân",
                Width = 150
            });

            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colMethod",
                DataPropertyName = "PaymentMethod",
                HeaderText = "Hình Thức",
                Width = 110
            });

            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTotal",
                DataPropertyName = "TotalAmountStr",
                HeaderText = "Tổng Tiền",
                Width = 130
            });

            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colItems",
                DataPropertyName = "ItemsSummary",
                HeaderText = "Chi Tiết Mặt Hàng",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colStatus",
                DataPropertyName = "Status",
                HeaderText = "Trạng Thái",
                Width = 120
            });
        }

        private async Task OpenCancelOrderDialogAsync()
        {
            int selectedOrderId = 0;
            if (dgvOrders.CurrentRow != null && dgvOrders.CurrentRow.DataBoundItem != null)
            {
                var item = dgvOrders.CurrentRow.DataBoundItem;
                var propId = item.GetType().GetProperty("RawOrderId");
                if (propId != null)
                {
                    selectedOrderId = (int)propId.GetValue(item)!;
                }
            }

            using var dialog = new Forms.FormCancelOrder(selectedOrderId);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                await LoadReportDataAsync();
            }
        }

        private void ShowSelectedOrderDetail()
        {
            if (dgvOrders.CurrentRow == null || dgvOrders.CurrentRow.DataBoundItem == null)
            {
                MessageBox.Show("Vui lòng chọn 1 đơn hàng trên bảng để xem chi tiết!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var item = dgvOrders.CurrentRow.DataBoundItem;
            var propId = item.GetType().GetProperty("RawOrderId");
            int orderId = propId != null ? (int)propId.GetValue(item)! : 0;

            var order = _cachedFilteredOrders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
            {
                MessageBox.Show("Không tìm thấy thông tin đơn hàng chi tiết!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var sb = new System.Text.StringBuilder();
            sb.AppendLine($"==========================================");
            sb.AppendLine($"           CHI TIẾT HÓA ĐƠN #{order.OrderId}");
            sb.AppendLine($"==========================================");
            sb.AppendLine($"• Thời gian: {order.OrderDate:dd/MM/yyyy HH:mm:ss}");
            sb.AppendLine($"• Thu ngân: {order.Employee}");
            sb.AppendLine($"• Phương thức: {order.PaymentMethod}");
            sb.AppendLine($"• Trạng thái: {(string.Equals(order.Status, "Cancelled", StringComparison.OrdinalIgnoreCase) ? "❌ ĐÃ HỦY" : "✅ Hoàn thành")}");
            sb.AppendLine($"------------------------------------------");
            sb.AppendLine($"DANH SÁCH MÓN HÀNG:");
            int idx = 1;
            foreach (var p in order.Items)
            {
                sb.AppendLine($" {idx++}. {p.ProductName}");
                sb.AppendLine($"    SL: {p.Quantity}  x  {p.UnitPrice:N0} đ  =>  {p.SubTotal:N0} đ");
            }
            sb.AppendLine($"------------------------------------------");
            sb.AppendLine($"TỔNG TIỀN PHẢI THU: {order.TotalAmount:N0} đ");
            if (order.AmountReceived > 0)
            {
                sb.AppendLine($"Tiền khách đưa: {order.AmountReceived:N0} đ | Tiền thừa: {order.ChangeAmount:N0} đ");
            }
            sb.AppendLine($"==========================================");

            MessageBox.Show(sb.ToString(), $"Chi tiết đơn #{order.OrderId}", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                _cachedFilteredOrders = filtered;

                // 1. Thống kê tổng số đơn
                lblTotalOrders.Text = $"{filtered.Count:N0} đơn";

                // 2. Thống kê tổng doanh thu (chỉ tính đơn hợp lệ, không tính Cancelled nếu có)
                decimal totalRev = filtered
                    .Where(o => !string.Equals(o.Status, "Cancelled", StringComparison.OrdinalIgnoreCase))
                    .Sum(o => o.TotalAmount);
                lblTotalRevenue.Text = $"{totalRev:N0} đ";

                // 3. Tìm mặt hàng bán chạy nhất trong ngày (không tính đơn hủy)
                var bestSellerItem = filtered
                    .Where(o => !string.Equals(o.Status, "Cancelled", StringComparison.OrdinalIgnoreCase))
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
                    RawOrderId = o.OrderId,
                    RawStatus = o.Status,
                    OrderId = $"#{o.OrderId}",
                    OrderTimeStr = o.OrderDate.ToString("dd/MM/yyyy HH:mm"),
                    Employee = o.Employee,
                    PaymentMethod = o.PaymentMethod,
                    TotalAmountStr = $"{o.TotalAmount:N0} đ",
                    ItemsSummary = string.Join(", ", o.Items.Select(i => $"{i.ProductName} (x{i.Quantity})")),
                    Status = string.Equals(o.Status, "Cancelled", StringComparison.OrdinalIgnoreCase)
                        ? "❌ Đã hủy"
                        : (o.Status == "Completed" ? "Hoàn thành" : o.Status)
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
