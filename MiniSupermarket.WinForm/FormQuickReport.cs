using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

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
            Theme.UITheme.StyleButton(btnExportReport, Theme.UITheme.ButtonVariant.Success);

            // Khởi tạo khoảng thời gian mặc định: 7 ngày gần nhất
            dtpReportDate.Value = DateTime.Today.AddDays(-6);
            dtpToDate.Value = DateTime.Today;

            SetupGridColumns();

            btnRunReport.Click += async (s, e) => await LoadReportDataAsync();
            btnRefresh.Click += async (s, e) => await LoadReportDataAsync();
            btnCancelOrder.Click += async (s, e) => await OpenCancelOrderDialogAsync();
            btnViewOrderDetail.Click += (s, e) => ShowSelectedOrderDetail();
            btnExportReport.Click += (s, e) => ExportReportToCsv();
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

            var sb = new StringBuilder();
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

        // =========================================================
        // TẢI DỮ LIỆU BÁO CÁO & DASHBOARD
        // =========================================================
        private async Task LoadReportDataAsync()
        {
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

                // 1. Tiêu chí 44: Thẻ tổng kết Dashboard (Dashboard KPI Summary Cards)
                await LoadDashboardSummaryAsync();

                // 2. Tiêu chí 45: Biểu đồ cột Doanh thu theo thời gian (Column Chart - Group By Ngày)
                await LoadDailyRevenueChartAsync();

                // 3. Tải danh sách đơn hàng chi tiết theo khoảng ngày chọn
                DateTime fromDate = dtpReportDate.Value.Date;
                DateTime toDate = dtpToDate.Value.Date.AddDays(1).AddTicks(-1);

                var response = await ApiClientService.Client.GetAsync("orders");
                if (response.IsSuccessStatusCode)
                {
                    var jsonStr = await response.Content.ReadAsStringAsync();
                    using var doc = JsonDocument.Parse(jsonStr);

                    if (doc.RootElement.TryGetProperty("data", out var dataElem))
                    {
                        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                        var allOrders = JsonSerializer.Deserialize<List<OrderReportDto>>(dataElem.GetRawText(), options) ?? new List<OrderReportDto>();

                        var filtered = allOrders
                            .Where(o => o.OrderDate >= fromDate && o.OrderDate <= toDate)
                            .OrderByDescending(o => o.OrderDate)
                            .ToList();

                        _cachedFilteredOrders = filtered;

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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải dữ liệu báo cáo: " + ex.Message,
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

        // =========================================================
        // TIÊU CHÍ 44: TẢI 4 THẺ TỔNG KẾT DASHBOARD (SUMMARY CARDS)
        // =========================================================
        private async Task LoadDashboardSummaryAsync()
        {
            try
            {
                var response = await ApiClientService.Client.GetAsync("reports/dashboard-summary");
                if (response.IsSuccessStatusCode)
                {
                    var jsonStr = await response.Content.ReadAsStringAsync();
                    using var doc = JsonDocument.Parse(jsonStr);

                    if (doc.RootElement.TryGetProperty("data", out var dataElem))
                    {
                        decimal todayRevenue = dataElem.TryGetProperty("todayRevenue", out var pRev) ? pRev.GetDecimal() : 0;
                        int totalOrders = dataElem.TryGetProperty("totalOrders", out var pOrd) ? pOrd.GetInt32() : 0;
                        int totalCustomers = dataElem.TryGetProperty("totalCustomers", out var pCust) ? pCust.GetInt32() : 0;
                        int lowStockCount = 0;
                        if (dataElem.TryGetProperty("lowStockProductsCount", out var pStockCount))
                            lowStockCount = pStockCount.GetInt32();
                        else if (dataElem.TryGetProperty("lowStockCount", out var pStockAlt))
                            lowStockCount = pStockAlt.GetInt32();

                        lblTotalRevenue.Text = $"{todayRevenue:N0} đ";
                        lblTotalOrders.Text = $"{totalOrders:N0} đơn";
                        lblTotalCustomers.Text = $"{totalCustomers:N0} khách";
                        lblLowStock.Text = $"{lowStockCount:N0} sp yếu";
                    }
                }
            }
            catch
            {
                // Fallback nếu lỗi kết nối
            }
        }

        // =========================================================
        // TIÊU CHÍ 45: TẢI VÀ VẼ BIỂU ĐỒ CỘT DOANH THU THEO NGÀY
        // =========================================================
        private async Task LoadDailyRevenueChartAsync()
        {
            try
            {
                DateTime from = dtpReportDate.Value.Date;
                DateTime to = dtpToDate.Value.Date;

                string url = $"reports/daily-revenue?fromDate={from:yyyy-MM-dd}&toDate={to:yyyy-MM-dd}";
                var response = await ApiClientService.Client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                    return;

                var jsonStr = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(jsonStr);

                if (doc.RootElement.TryGetProperty("data", out var dataElem))
                {
                    chartRevenue.Series["DoanhThu"].Points.Clear();

                    foreach (var item in dataElem.EnumerateArray())
                    {
                        DateTime date = item.GetProperty("date").GetDateTime();
                        decimal revenue = item.GetProperty("totalRevenue").GetDecimal();
                        int orderCount = item.GetProperty("orderCount").GetInt32();

                        string label = date.ToString("dd/MM");
                        int pointIndex = chartRevenue.Series["DoanhThu"].Points.AddXY(label, (double)revenue);
                        chartRevenue.Series["DoanhThu"].Points[pointIndex].ToolTip = $"{date:dd/MM/yyyy}: {revenue:N0} đ ({orderCount} đơn)";
                    }
                }
            }
            catch
            {
                // Bỏ qua lỗi chart nếu chưa có dữ liệu
            }
        }

        // =========================================================
        // TIÊU CHÍ 48: XUẤT BÁO CÁO HÓA ĐƠN RA CSV/EXCEL (UTF-8 WITH BOM)
        // =========================================================
        private void ExportReportToCsv()
        {
            if (_cachedFilteredOrders == null || _cachedFilteredOrders.Count == 0)
            {
                MessageBox.Show(
                    "Không có dữ liệu hóa đơn nào để xuất báo cáo!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            using var sfd = new SaveFileDialog
            {
                Title = "Xuất Báo Cáo Hóa Đơn Ra Excel/CSV",
                Filter = "Tệp CSV (Excel hỗ trợ) (*.csv)|*.csv",
                FileName = $"BaoCao_HoaDon_{DateTime.Now:yyyyMMdd_HHmm}.csv"
            };

            if (sfd.ShowDialog(this) != DialogResult.OK)
                return;

            try
            {
                var sb = new StringBuilder();

                // Tiêu đề cột
                sb.AppendLine("Mã Đơn,Thời Gian,Thu Ngân,Phương Thức,Tổng Tiền (VNĐ),Tiền Khách Đưa (VNĐ),Tiền Thối (VNĐ),Trạng Thái,Chi Tiết Mặt Hàng");

                foreach (var o in _cachedFilteredOrders)
                {
                    string itemsStr = string.Join(" | ", o.Items.Select(i => $"{i.ProductName} (x{i.Quantity})"));
                    string escapedItems = $"\"{itemsStr.Replace("\"", "\"\"")}\"";
                    string escapedEmployee = $"\"{o.Employee.Replace("\"", "\"\"")}\"";
                    string status = string.Equals(o.Status, "Cancelled", StringComparison.OrdinalIgnoreCase) ? "Đã hủy" : "Hoàn thành";

                    sb.AppendLine($"#{o.OrderId},{o.OrderDate:dd/MM/yyyy HH:mm},{escapedEmployee},{o.PaymentMethod},{o.TotalAmount},{o.AmountReceived},{o.ChangeAmount},{status},{escapedItems}");
                }

                // Ghi file với UTF-8 kèm BOM để Microsoft Excel tự động hiển thị đúng tiếng Việt có dấu
                var utf8WithBom = new UTF8Encoding(encoderShouldEmitUTF8Identifier: true);
                File.WriteAllText(sfd.FileName, sb.ToString(), utf8WithBom);

                MessageBox.Show(
                    $"Xuất báo cáo thành công!\n\nĐã lưu tại:\n{sfd.FileName}\n\n(Tệp được mã hóa UTF-8 with BOM hiển thị sắc nét tiếng Việt trên Excel)",
                    "Xuất Báo Cáo Thành Công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi khi xuất file Excel/CSV: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
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
