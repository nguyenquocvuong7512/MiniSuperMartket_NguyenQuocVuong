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
            Theme.UITheme.StyleButton(btnAllOrders, Theme.UITheme.ButtonVariant.Secondary);
            Theme.UITheme.StyleButton(btnCancelOrder, Theme.UITheme.ButtonVariant.Danger);
            Theme.UITheme.StyleButton(btnViewOrderDetail, Theme.UITheme.ButtonVariant.Secondary);
            Theme.UITheme.StyleButton(btnRefresh, Theme.UITheme.ButtonVariant.Neutral);
            Theme.UITheme.StyleButton(btnExportReport, Theme.UITheme.ButtonVariant.Success);

            // Khởi tạo khoảng thời gian mặc định: Từ đầu tháng hiện tại đến hôm nay
            dtpReportDate.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpToDate.Value = DateTime.Today;

            SetupGridColumns();

            btnRunReport.Click += async (s, e) => await LoadReportDataAsync();
            btnAllOrders.Click += async (s, e) => await LoadReportDataAsync(loadAllTime: true);
            btnRefresh.Click += async (s, e) => await LoadReportDataAsync();
            btnCancelOrder.Click += async (s, e) => await OpenCancelOrderDialogAsync();
            btnViewOrderDetail.Click += (s, e) => ShowSelectedOrderDetail();
            btnExportReport.Click += (s, e) => ExportReportToCsv();
            dgvOrders.CellDoubleClick += (s, e) => ShowSelectedOrderDetail();
            dgvOrders.RowPrePaint += DgvOrders_RowPrePaint;

            // Xử lý sự kiện click trên biểu đồ doanh thu và chuyển tab
            chartRevenue.MouseClick += ChartRevenue_MouseClick;
            tabReports.SelectedIndexChanged += async (s, e) =>
            {
                if (tabReports.SelectedTab == tabChart)
                {
                    await LoadDailyRevenueChartAsync();
                }
            };

            this.Load += async (s, e) => await LoadReportDataAsync();
        }

        private void ChartRevenue_MouseClick(object? sender, MouseEventArgs e)
        {
            try
            {
                var hit = chartRevenue.HitTest(e.X, e.Y);
                if (hit != null && hit.ChartElementType == ChartElementType.DataPoint && hit.PointIndex >= 0)
                {
                    if (chartRevenue.Series.IndexOf("DoanhThu") >= 0 &&
                        hit.PointIndex < chartRevenue.Series["DoanhThu"].Points.Count)
                    {
                        var dp = chartRevenue.Series["DoanhThu"].Points[hit.PointIndex];
                        string detailText = !string.IsNullOrEmpty(dp.ToolTip)
                            ? dp.ToolTip
                            : $"Ngày: {dp.AxisLabel} - Doanh thu: {dp.YValues[0]:N0} đ";

                        MessageBox.Show(detailText, "Chi tiết doanh thu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Chart Click error: " + ex.Message);
            }
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
        private async Task LoadReportDataAsync(bool loadAllTime = false)
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
                btnAllOrders.Enabled = false;
                btnRunReport.Text = "Đang tải...";

                if (loadAllTime)
                {
                    dtpReportDate.Value = new DateTime(2020, 1, 1);
                    dtpToDate.Value = DateTime.Today;
                }

                DateTime fromDate = dtpReportDate.Value.Date;
                DateTime toDate = dtpToDate.Value.Date.AddDays(1).AddTicks(-1);

                // 1. Tiêu chí 44: Thẻ tổng kết Dashboard
                await LoadDashboardSummaryAsync(fromDate, toDate);

                // 2. Tiêu chí 45: Biểu đồ cột Doanh thu theo thời gian (Column Chart - Group By Ngày)
                await LoadDailyRevenueChartAsync();

                // 3. Tải danh sách đơn hàng chi tiết theo khoảng ngày chọn
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

                        // Đồng bộ thẻ số lượng và doanh thu đúng khớp 100% với danh sách đang hiển thị
                        decimal periodRevenue = filtered
                            .Where(o => string.Equals(o.Status, "Completed", StringComparison.OrdinalIgnoreCase))
                            .Sum(o => o.TotalAmount);
                        int completedOrders = filtered
                            .Count(o => string.Equals(o.Status, "Completed", StringComparison.OrdinalIgnoreCase));
                        int cancelledOrders = filtered
                            .Count(o => string.Equals(o.Status, "Cancelled", StringComparison.OrdinalIgnoreCase));

                        bool isAllTime = (allOrders.Count == filtered.Count);
                        bool isOnlyToday = (!isAllTime && dtpReportDate.Value.Date == DateTime.Today && dtpToDate.Value.Date == DateTime.Today);

                        if (isOnlyToday)
                        {
                            lblRevenueTitle.Text = "DOANH THU HÔM NAY";
                            lblOrdersTitle.Text = "HÓA ĐƠN HÔM NAY";
                        }
                        else if (isAllTime)
                        {
                            lblRevenueTitle.Text = "TỔNG DOANH THU TOÀN BỘ";
                            lblOrdersTitle.Text = "TỔNG SỐ HÓA ĐƠN";
                        }
                        else
                        {
                            lblRevenueTitle.Text = $"DOANH THU ({dtpReportDate.Value:dd/MM} - {dtpToDate.Value:dd/MM})";
                            lblOrdersTitle.Text = $"HÓA ĐƠN ({dtpReportDate.Value:dd/MM} - {dtpToDate.Value:dd/MM})";
                        }

                        lblTotalRevenue.Text = $"{periodRevenue:N0} đ";
                        lblTotalOrders.Text = $"{filtered.Count:N0} đơn";

                        tabOrders.Text = $"📋 Danh Sách Đơn Hàng ({filtered.Count} đơn)";
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
                btnAllOrders.Enabled = true;
                btnRunReport.Text = "📊 Xem Báo Cáo";
            }
        }

        // =========================================================
        // TIÊU CHÍ 44: TẢI 4 THẺ TỔNG KẾT DASHBOARD (SUMMARY CARDS)
        // =========================================================
        private async Task LoadDashboardSummaryAsync(DateTime? fromDate = null, DateTime? toDate = null)
        {
            try
            {
                string url = "reports/dashboard-summary";
                if (fromDate.HasValue && toDate.HasValue)
                {
                    url += $"?fromDate={fromDate.Value:yyyy-MM-dd}&toDate={toDate.Value:yyyy-MM-dd}";
                }

                var response = await ApiClientService.Client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var jsonStr = await response.Content.ReadAsStringAsync();
                    using var doc = JsonDocument.Parse(jsonStr);

                    if (doc.RootElement.TryGetProperty("data", out var dataElem))
                    {
                        int totalCustomers = dataElem.TryGetProperty("totalCustomers", out var pCust) ? pCust.GetInt32() : 0;
                        int lowStockCount = 0;
                        if (dataElem.TryGetProperty("lowStockProductsCount", out var pStockCount))
                            lowStockCount = pStockCount.GetInt32();
                        else if (dataElem.TryGetProperty("lowStockCount", out var pStockAlt))
                            lowStockCount = pStockAlt.GetInt32();

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
                    if (chartRevenue.Series.IndexOf("DoanhThu") >= 0)
                    {
                        chartRevenue.Series["DoanhThu"].Points.Clear();

                        foreach (var item in dataElem.EnumerateArray())
                        {
                            DateTime date = DateTime.MinValue;
                            if (item.TryGetProperty("date", out var dProp))
                            {
                                if (dProp.ValueKind == JsonValueKind.String)
                                {
                                    DateTime.TryParse(dProp.GetString(), out date);
                                }
                                else
                                {
                                    try { date = dProp.GetDateTime(); } catch { }
                                }
                            }

                            decimal revenue = 0;
                            if (item.TryGetProperty("revenue", out var rProp))
                            {
                                revenue = rProp.GetDecimal();
                            }
                            else if (item.TryGetProperty("totalRevenue", out var trProp))
                            {
                                revenue = trProp.GetDecimal();
                            }

                            int orderCount = 0;
                            if (item.TryGetProperty("orderCount", out var ocProp))
                            {
                                orderCount = ocProp.GetInt32();
                            }

                            string label = date != DateTime.MinValue ? date.ToString("dd/MM") : "N/A";
                            int pointIndex = chartRevenue.Series["DoanhThu"].Points.AddXY(label, (double)revenue);
                            string dateFormatted = date != DateTime.MinValue ? date.ToString("dd/MM/yyyy") : label;
                            chartRevenue.Series["DoanhThu"].Points[pointIndex].ToolTip = $"{dateFormatted}: {revenue:N0} đ ({orderCount} đơn)";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Lỗi chart: " + ex.Message);
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
