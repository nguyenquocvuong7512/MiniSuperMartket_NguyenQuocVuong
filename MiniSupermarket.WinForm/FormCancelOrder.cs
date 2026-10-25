using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using MiniSupermarket.WinForms.Services;

namespace MiniSupermarket.WinForms.Forms
{
    public partial class FormCancelOrder : Form
    {
        public int CancelledOrderId { get; private set; } = 0;
        private bool _isOrderValidForCancel = false;

        public FormCancelOrder(int initialOrderId = 0)
        {
            InitializeComponent();
            cboReason.SelectedIndex = 0;

            btnLookup.Click += async (s, e) => await LookupOrderAsync();
            btnConfirm.Click += async (s, e) => await ConfirmCancelOrderAsync();
            btnClose.Click += (s, e) => { DialogResult = DialogResult.Cancel; this.Close(); };
            txtOrderId.KeyDown += async (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    await LookupOrderAsync();
                }
            };

            this.Load += async (s, e) =>
            {
                if (initialOrderId > 0)
                {
                    txtOrderId.Text = initialOrderId.ToString();
                    await LookupOrderAsync();
                }
                else
                {
                    txtOrderId.Focus();
                }
            };
        }

        private async Task LookupOrderAsync()
        {
            string idText = txtOrderId.Text.Trim();
            if (string.IsNullOrWhiteSpace(idText) || !int.TryParse(idText, out int orderId) || orderId <= 0)
            {
                MessageBox.Show(
                    "Vui lòng nhập Mã hóa đơn (số nguyên dương) hợp lệ!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtOrderId.Focus();
                return;
            }

            try
            {
                btnLookup.Enabled = false;
                btnLookup.Text = "Đang tìm...";

                var response = await ApiClientService.Client.GetAsync($"orders/{orderId}");
                if (!response.IsSuccessStatusCode)
                {
                    ResetPreview($"Không tìm thấy đơn hàng #{orderId} trong hệ thống!");
                    return;
                }

                var jsonStr = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(jsonStr);

                if (!doc.RootElement.TryGetProperty("data", out var dataElem))
                {
                    ResetPreview($"Dữ liệu trả về không hợp lệ cho đơn #{orderId}!");
                    return;
                }

                // Đọc thông tin chi tiết
                string orderDateStr = dataElem.TryGetProperty("orderDate", out var od) ? od.GetString() ?? "" : "";
                DateTime.TryParse(orderDateStr, out DateTime orderDate);
                decimal totalAmount = dataElem.TryGetProperty("totalAmount", out var ta) ? ta.GetDecimal() : 0;
                string status = dataElem.TryGetProperty("status", out var st) ? st.GetString() ?? "" : "";
                string cashier = dataElem.TryGetProperty("employee", out var em) ? em.GetString() ?? "Không rõ" : "Không rõ";

                lblDateValue.Text = orderDate > DateTime.MinValue ? orderDate.ToString("dd/MM/yyyy HH:mm") : orderDateStr;
                lblCashierValue.Text = cashier;
                lblTotalValue.Text = $"{totalAmount:N0} đ";

                lstItems.Items.Clear();
                if (dataElem.TryGetProperty("items", out var itemsElem) && itemsElem.ValueKind == JsonValueKind.Array)
                {
                    foreach (var item in itemsElem.EnumerateArray())
                    {
                        string pName = item.TryGetProperty("productName", out var pn) ? pn.GetString() ?? "Sản phẩm" : "Sản phẩm";
                        int qty = item.TryGetProperty("quantity", out var q) ? q.GetInt32() : 1;
                        decimal price = item.TryGetProperty("unitPrice", out var up) ? up.GetDecimal() : 0;
                        decimal subTotal = item.TryGetProperty("subTotal", out var sub) ? sub.GetDecimal() : (qty * price);

                        lstItems.Items.Add($"• {pName}  x{qty}  ({price:N0} đ) => {subTotal:N0} đ");
                    }
                }

                if (string.Equals(status, "Cancelled", StringComparison.OrdinalIgnoreCase))
                {
                    lblStatusValue.Text = "❌ ĐÃ HỦY (Không thể hủy lại)";
                    lblStatusValue.ForeColor = Color.FromArgb(220, 38, 38);
                    btnConfirm.Enabled = false;
                    _isOrderValidForCancel = false;
                }
                else
                {
                    lblStatusValue.Text = "✅ HỢP LỆ (Có thể hủy & hoàn kho)";
                    lblStatusValue.ForeColor = Color.FromArgb(5, 150, 105);
                    btnConfirm.Enabled = true;
                    _isOrderValidForCancel = true;
                }
            }
            catch (Exception ex)
            {
                ResetPreview("Lỗi tra cứu: " + ex.Message);
            }
            finally
            {
                btnLookup.Enabled = true;
                btnLookup.Text = "🔍 Tra Cứu";
            }
        }

        private void ResetPreview(string message)
        {
            lblDateValue.Text = "---";
            lblCashierValue.Text = "---";
            lblTotalValue.Text = "0 đ";
            lblStatusValue.Text = message;
            lblStatusValue.ForeColor = Color.FromArgb(220, 38, 38);
            lstItems.Items.Clear();
            btnConfirm.Enabled = false;
            _isOrderValidForCancel = false;
        }

        private async Task ConfirmCancelOrderAsync()
        {
            if (!_isOrderValidForCancel || !int.TryParse(txtOrderId.Text.Trim(), out int orderId) || orderId <= 0)
            {
                MessageBox.Show(
                    "Vui lòng tra cứu hóa đơn hợp lệ trước khi xác nhận hủy!",
                    "Chưa sẵn sàng",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            string reason = cboReason.SelectedItem?.ToString() ?? "Khác";

            var confirm = MessageBox.Show(
                $"BẠN CÓ CHẮC CHẮN MUỐN HỦY ĐƠN HÀNG #{orderId}?\n\n" +
                $"• Tổng tiền đơn: {lblTotalValue.Text}\n" +
                $"• Nhân viên: {lblCashierValue.Text}\n" +
                $"• Lý do hủy: {reason}\n\n" +
                $"Hệ thống sẽ:\n" +
                $"1. Chuyển trạng thái đơn sang 'Cancelled' (Đã hủy)\n" +
                $"2. Hoàn lại toàn bộ số lượng sản phẩm vào kho hàng trong CSDL\n\n" +
                $"Nhấn YES để xác nhận hủy ngay bây giờ.",
                "Xác nhận hủy hóa đơn & hoàn kho",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm != DialogResult.Yes) return;

            try
            {
                btnConfirm.Enabled = false;
                btnConfirm.Text = "Đang xử lý hủy...";

                // Gọi API PUT /api/orders/{orderId}/cancel
                var response = await ApiClientService.Client.PutAsync($"orders/{orderId}/cancel", null);
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    CancelledOrderId = orderId;
                    MessageBox.Show(
                        $"✅ ĐÃ HỦY THÀNH CÔNG ĐƠN HÀNG #{orderId}!\n\n" +
                        $"Toàn bộ số lượng sản phẩm đã được tự động hoàn lại tồn kho trong CSDL.",
                        "Hủy đơn thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    string errorMsg = "Không thể hủy đơn hàng!";
                    try
                    {
                        using var doc = JsonDocument.Parse(content);
                        if (doc.RootElement.TryGetProperty("message", out var msgElem))
                        {
                            errorMsg = msgElem.GetString() ?? errorMsg;
                        }
                    }
                    catch { }

                    MessageBox.Show(errorMsg, "Lỗi hủy đơn", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối khi hủy đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnConfirm.Enabled = true;
                btnConfirm.Text = "❌ Xác Nhận Hủy & Hoàn Kho";
            }
        }
    }
}
