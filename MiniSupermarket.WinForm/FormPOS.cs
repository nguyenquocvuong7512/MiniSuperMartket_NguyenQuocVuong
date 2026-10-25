using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using MiniSupermarket.WinForms;
using MiniSupermarket.WinForms.Services;

namespace MiniSupermarket.WinForms.Forms
{
    public partial class FormPOS : Form
    {
        private readonly HttpClient _client;
        private readonly List<CartItem> _cart = new();
        private CustomerDto? _currentCustomer = null;
        private int _lastCompletedOrderId = 0;

        public FormPOS()
        {
            InitializeComponent();

            _client = new HttpClient
            {
                BaseAddress = new Uri(ApiClientService.ApiBaseUrl)
            };

            cboPaymentMethod.SelectedIndex = 0;

            SetupCustomStyles();
            SetupEvents();
            _ = LoadProductsAsync();
        }

        private void SetupCustomStyles()
        {
            Theme.UITheme.ApplyModernDataGridView(dgvProducts);
            Theme.UITheme.ApplyModernDataGridView(dgvCart);
            SetupProductGridColumns();

            Theme.UITheme.StyleButton(btnAdd, Theme.UITheme.ButtonVariant.Primary);
            Theme.UITheme.StyleButton(btnSearch, Theme.UITheme.ButtonVariant.Secondary);
            Theme.UITheme.StyleButton(btnCustSearch, Theme.UITheme.ButtonVariant.Secondary);
            Theme.UITheme.StyleButton(btnPay, Theme.UITheme.ButtonVariant.Primary);
        }

        private void SetupProductGridColumns()
        {
            dgvProducts.AutoGenerateColumns = false;
            dgvProducts.Columns.Clear();

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Barcode",
                HeaderText = "Mã vạch",
                Width = 125
            });

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ProductName",
                HeaderText = "Tên sản phẩm",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Price",
                HeaderText = "Đơn giá",
                Width = 95,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N0",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "StockQuantity",
                HeaderText = "Tồn",
                Width = 65,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Unit",
                HeaderText = "ĐVT",
                Width = 65,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });
        }

        // =========================================================
        // EVENTS
        // =========================================================

        private void SetupEvents()
        {
            btnAdd.Click += BtnAdd_Click;

            btnSearch.Click += async (s, e) =>
            {
                await SearchProductsAsync();
            };

            dgvProducts.CellDoubleClick += DgvProducts_CellDoubleClick;

            btnRemove.Click += BtnRemove_Click;
            btnCancelOrder.Click += async (s, e) => await OpenCancelOrderDialogAsync();

            btnClear.Click += (s, e) =>
            {
                _cart.Clear();
                RefreshCart();
            };

            txtAmountReceived.TextChanged += TxtAmountReceived_TextChanged;

            cboPaymentMethod.SelectedIndexChanged += CboPaymentMethod_SelectedIndexChanged;

            btnPay.Click += async (s, e) =>
            {
                await ProcessPaymentAsync();
            };

            txtBarcode.KeyDown += TxtBarcode_KeyDown;

            // Khách hàng & Tích điểm
            btnCustSearch.Click += async (s, e) => await SearchCustomerByPhoneAsync();
            txtCustPhone.KeyDown += async (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    await SearchCustomerByPhoneAsync();
                }
            };
            btnCustAdd.Click += (s, e) => OpenQuickAddCustomer();
            btnCustClear.Click += (s, e) => ResetCustomer();
            btnSelectGuest.Click += (s, e) => ResetCustomer();
        }

        // =========================================================
        // LOAD PRODUCTS
        // =========================================================

        private async Task LoadProductsAsync()
        {
            try
            {
                AddAuthorizationHeader();

                var response = await _client.GetAsync("products");

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Không thể tải danh sách sản phẩm.",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                var products = await response.Content.ReadFromJsonAsync<List<ProductDto>>();
                dgvProducts.DataSource = products;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải sản phẩm:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // SEARCH PRODUCT
        // =========================================================

        private async Task SearchProductsAsync()
        {
            try
            {
                string keyword = txtSearch.Text.Trim();

                if (string.IsNullOrWhiteSpace(keyword))
                {
                    await LoadProductsAsync();
                    return;
                }

                AddAuthorizationHeader();

                var response = await _client.GetAsync($"products/search?keyword={Uri.EscapeDataString(keyword)}");

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Không tìm thấy sản phẩm.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    return;
                }

                var products = await response.Content.ReadFromJsonAsync<List<ProductDto>>();
                dgvProducts.DataSource = products;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tìm kiếm:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // THÊM SẢN PHẨM
        // =========================================================

        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            AddSelectedProduct();
        }

        private void DgvProducts_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                AddSelectedProduct();
            }
        }

        private void TxtBarcode_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                _ = AddProductByBarcodeAsync();
            }
        }

        private async Task AddProductByBarcodeAsync()
        {
            string barcode = txtBarcode.Text.Trim();

            if (string.IsNullOrWhiteSpace(barcode))
                return;

            try
            {
                AddAuthorizationHeader();

                var response = await _client.GetAsync($"products/search?keyword={Uri.EscapeDataString(barcode)}");

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Không tìm thấy sản phẩm có mã vạch này.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    return;
                }

                var products = await response.Content.ReadFromJsonAsync<List<ProductDto>>();
                var product = products?.FirstOrDefault(p => p.Barcode == barcode);

                if (product == null)
                {
                    MessageBox.Show(
                        "Không tìm thấy mã vạch.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    return;
                }

                AddProductToCart(product);

                txtBarcode.Clear();
                txtBarcode.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tìm sản phẩm:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void AddSelectedProduct()
        {
            if (dgvProducts.CurrentRow == null)
            {
                MessageBox.Show(
                    "Hãy chọn sản phẩm.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            if (dgvProducts.CurrentRow.DataBoundItem is not ProductDto product)
            {
                return;
            }

            AddProductToCart(product);
        }

        // =========================================================
        // THÊM VÀO CART
        // =========================================================

        private void AddProductToCart(ProductDto product)
        {
            int quantity = (int)nudQuantity.Value;

            if (quantity <= 0)
                return;

            if (product.StockQuantity <= 0)
            {
                MessageBox.Show(
                    "Sản phẩm đã hết hàng.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            var existing = _cart.FirstOrDefault(x => x.ProductId == product.ProductId);
            int currentQuantity = existing?.Quantity ?? 0;

            if (currentQuantity + quantity > product.StockQuantity)
            {
                MessageBox.Show(
                    $"Không đủ tồn kho.\nTồn kho: {product.StockQuantity}",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (existing == null)
            {
                _cart.Add(new CartItem
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Quantity = quantity,
                    UnitPrice = product.Price
                });
            }
            else
            {
                existing.Quantity += quantity;
            }

            RefreshCart();
            nudQuantity.Value = 1;
        }

        // =========================================================
        // REFRESH CART
        // =========================================================

        private void RefreshCart()
        {
            foreach (var item in _cart)
            {
                item.SubTotal = item.Quantity * item.UnitPrice;
            }

            dgvCart.DataSource = null;
            dgvCart.DataSource = _cart.ToList();

            decimal total = _cart.Sum(x => x.SubTotal);
            lblTotal.Text = $"{total:N0} đ";

            CalculateChange();
            UpdatePointsCalculation();
        }

        // =========================================================
        // XÓA SẢN PHẨM
        // =========================================================

        private void BtnRemove_Click(object? sender, EventArgs e)
        {
            if (dgvCart.CurrentRow == null)
                return;

            if (dgvCart.CurrentRow.DataBoundItem is CartItem item)
            {
                _cart.Remove(item);
                RefreshCart();
            }
        }

        // =========================================================
        // TÍNH TIỀN THỪA
        // =========================================================

        private void TxtAmountReceived_TextChanged(object? sender, EventArgs e)
        {
            CalculateChange();
        }

        private void CboPaymentMethod_SelectedIndexChanged(object? sender, EventArgs e)
        {
            bool isCash = cboPaymentMethod.SelectedItem?.ToString() == "Cash";

            txtAmountReceived.Enabled = isCash;

            if (!isCash)
            {
                txtAmountReceived.Text = "";
            }

            CalculateChange();
        }

        private void CalculateChange()
        {
            decimal total = _cart.Sum(x => x.SubTotal);

            if (cboPaymentMethod?.SelectedItem?.ToString() != "Cash")
            {
                lblChange.Text = "0 đ";
                return;
            }

            if (decimal.TryParse(txtAmountReceived?.Text, out decimal received))
            {
                decimal change = received - total;

                if (change < 0)
                    change = 0;

                lblChange.Text = $"{change:N0} đ";
            }
            else
            {
                lblChange.Text = "0 đ";
            }
        }

        // =========================================================
        // THANH TOÁN
        // =========================================================

        private async Task ProcessPaymentAsync()
        {
            if (_cart.Count == 0)
            {
                MessageBox.Show(
                    "Giỏ hàng đang trống.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            decimal total = _cart.Sum(x => x.SubTotal);
            string paymentMethod = cboPaymentMethod.SelectedItem?.ToString() ?? "Cash";
            decimal amountReceived = 0;

            if (paymentMethod == "Cash")
            {
                if (!decimal.TryParse(txtAmountReceived.Text, out amountReceived))
                {
                    MessageBox.Show(
                        "Vui lòng nhập số tiền khách đưa.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtAmountReceived.Focus();
                    return;
                }

                if (amountReceived < total)
                {
                    MessageBox.Show(
                        $"Tiền khách đưa chưa đủ.\n\nTổng tiền: {total:N0} đ",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }
            }
            else
            {
                amountReceived = total;
            }

            int pointsToEarn = (int)(total / 10000);
            string custInfo = _currentCustomer != null
                ? $"Khách hàng: {_currentCustomer.CustomerName} ({_currentCustomer.PhoneNumber})\nĐiểm hiện có: {_currentCustomer.RewardPoints:N0} | Tích thêm: +{pointsToEarn:N0} điểm\n\n"
                : "Khách hàng: Khách vãng lai (Không tích điểm)\n\n";

            var confirm = MessageBox.Show(
                $"{custInfo}Tổng tiền: {total:N0} đ\nPhương thức: {paymentMethod}\n\nXác nhận thanh toán?",
                "Xác nhận thanh toán",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                btnPay.Enabled = false;
                AddAuthorizationHeader();

                var request = new CreateOrderRequest
                {
                    PaymentMethod = paymentMethod,
                    AmountReceived = amountReceived,
                    CustomerId = _currentCustomer?.CustomerId,
                    CustomerPhone = _currentCustomer?.PhoneNumber,
                    Items = _cart.Select(x => new CreateOrderItemRequest
                    {
                        ProductId = x.ProductId,
                        Quantity = x.Quantity
                    }).ToList()
                };

                var response = await _client.PostAsJsonAsync("orders", request);
                var result = await response.Content.ReadFromJsonAsync<OrderResponse>();

                if (!response.IsSuccessStatusCode || result == null || !result.Success)
                {
                    MessageBox.Show(
                        result?.Message ?? "Thanh toán thất bại.",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                decimal change = result.Data?.ChangeAmount ?? 0;
                string pointsMsg = "";
                if (result.Data != null && result.Data.RewardPointsAdded > 0)
                {
                    pointsMsg = $"\n--- TÍCH ĐIỂM HỘI VIÊN ---\n" +
                                $"Khách hàng: {result.Data.CustomerName}\n" +
                                $"Điểm cộng thêm: +{result.Data.RewardPointsAdded:N0} điểm\n" +
                                $"Tổng điểm tích lũy: {result.Data.TotalRewardPoints:N0} điểm\n" +
                                $"Hạng thẻ: {result.Data.MembershipRank}\n";
                }

                MessageBox.Show(
                    $"THANH TOÁN THÀNH CÔNG!\n\n" +
                    $"Mã đơn: #{result.Data?.OrderId}\n" +
                    $"Tổng tiền: {total:N0} đ\n" +
                    $"Tiền khách đưa: {amountReceived:N0} đ\n" +
                    $"Tiền thừa: {change:N0} đ\n" +
                    pointsMsg,
                    "Thanh toán thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                _lastCompletedOrderId = result.Data?.OrderId ?? 0;

                _cart.Clear();
                txtAmountReceived.Clear();
                RefreshCart();
                ResetCustomer();

                await LoadProductsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi thanh toán:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                btnPay.Enabled = true;
            }
        }

        private async Task OpenCancelOrderDialogAsync()
        {
            using var dialog = new FormCancelOrder(_lastCompletedOrderId);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                if (_lastCompletedOrderId == dialog.CancelledOrderId)
                {
                    _lastCompletedOrderId = 0;
                }

                // Cập nhật lại tồn kho sản phẩm hiển thị trên quầy
                await LoadProductsAsync();
            }
        }

        // =========================================================
        // NGHIỆP VỤ KHÁCH HÀNG & TÍCH ĐIỂM (LOYALTY POINTS)
        // =========================================================

        private async Task SearchCustomerByPhoneAsync()
        {
            string phone = txtCustPhone.Text.Trim();

            if (string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show(
                    "Vui lòng nhập số điện thoại để tra cứu hội viên.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                txtCustPhone.Focus();
                return;
            }

            try
            {
                AddAuthorizationHeader();

                // 1. Thử gọi API tra cứu chính xác theo SĐT
                var response = await _client.GetAsync($"customers/by-phone/{Uri.EscapeDataString(phone)}");

                CustomerDto? customer = null;

                if (response.IsSuccessStatusCode)
                {
                    customer = await response.Content.ReadFromJsonAsync<CustomerDto>();
                }
                else
                {
                    // Fallback sang endpoint search nếu chưa có
                    var searchResponse = await _client.GetAsync($"customers/search?keyword={Uri.EscapeDataString(phone)}");
                    if (searchResponse.IsSuccessStatusCode)
                    {
                        var list = await searchResponse.Content.ReadFromJsonAsync<List<CustomerDto>>();
                        customer = list?.FirstOrDefault(c => c.PhoneNumber == phone || c.PhoneNumber.Replace(" ", "") == phone.Replace(" ", ""));
                    }
                }

                if (customer != null)
                {
                    SetCustomer(customer);
                }
                else
                {
                    var ask = MessageBox.Show(
                        $"Không tìm thấy khách hàng có số điện thoại: '{phone}'.\n\nBạn có muốn đăng ký tài khoản hội viên mới ngay không?",
                        "Khách hàng mới",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (ask == DialogResult.Yes)
                    {
                        OpenQuickAddCustomer(phone);
                    }
                    else
                    {
                        ResetCustomer();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi khi tra cứu khách hàng:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void SetCustomer(CustomerDto customer)
        {
            _currentCustomer = customer;
            txtCustPhone.Text = customer.PhoneNumber;
            lblCustName.Text = customer.CustomerName;
            lblCustRank.Text = string.IsNullOrWhiteSpace(customer.MembershipRank) ? "Mới" : customer.MembershipRank;
            lblCustPoints.Text = $"{customer.RewardPoints:N0} điểm";

            lblCustStatus.Text = $"Đã nhận diện: {customer.CustomerName} ({customer.PhoneNumber}) - Điểm sẽ được cộng khi thanh toán.";
            lblCustStatus.ForeColor = Color.FromArgb(0, 100, 0);

            UpdatePointsCalculation();
        }

        private void ResetCustomer()
        {
            _currentCustomer = null;
            txtCustPhone.Clear();
            lblCustName.Text = "Khách vãng lai";
            lblCustRank.Text = "-";
            lblCustPoints.Text = "0 điểm";
            lblCustStatus.Text = "Khách vãng lai (Không tích điểm). Nhập SĐT để tích điểm.";
            lblCustStatus.ForeColor = Color.Gray;

            UpdatePointsCalculation();
        }

        private void OpenQuickAddCustomer(string initialPhone = "")
        {
            string phoneToUse = string.IsNullOrWhiteSpace(initialPhone) ? txtCustPhone.Text.Trim() : initialPhone;
            using var formAdd = new FormQuickAddCustomer(phoneToUse);
            if (formAdd.ShowDialog(this) == DialogResult.OK && formAdd.CreatedCustomer != null)
            {
                SetCustomer(formAdd.CreatedCustomer);
            }
        }

        private void UpdatePointsCalculation()
        {
            decimal total = _cart.Sum(x => x.SubTotal);
            int pointsToEarn = (int)(total / 10000); // 10.000 VNĐ = 1 điểm

            if (_currentCustomer != null)
            {
                lblCustEarn.Text = $"+{pointsToEarn:N0} điểm (Tỷ lệ: 10.000đ = 1 điểm)";
                lblCustEarn.ForeColor = Color.Crimson;

                lblPointsSummary.Text = $"+{pointsToEarn:N0} điểm";
                lblPointsSummary.ForeColor = Color.Crimson;

                lblCustomerSummary.Text = $"Khách hàng: {_currentCustomer.CustomerName} ({_currentCustomer.PhoneNumber})";
                lblCustomerSummary.ForeColor = Color.FromArgb(20, 60, 130);
            }
            else
            {
                lblCustEarn.Text = "+0 điểm (Khách vãng lai)";
                lblCustEarn.ForeColor = Color.Gray;

                lblPointsSummary.Text = "+0 điểm";
                lblPointsSummary.ForeColor = Color.Gray;

                lblCustomerSummary.Text = "Khách hàng: Khách vãng lai";
                lblCustomerSummary.ForeColor = Color.DimGray;
            }
        }

        // =========================================================
        // AUTHORIZATION
        // =========================================================

        private void AddAuthorizationHeader()
        {
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", SessionManager.JwtToken);
        }

        private void cboPaymentMethod_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }

    // =============================================================
    // DTO CLASSES
    // =============================================================

    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SubTotal { get; set; }
    }

    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string Unit { get; set; } = string.Empty;
        public int CategoryId { get; set; }
    }

    public class CreateOrderRequest
    {
        public string PaymentMethod { get; set; } = "Cash";
        public decimal AmountReceived { get; set; }
        public int? CustomerId { get; set; }
        public string? CustomerPhone { get; set; }
        public List<CreateOrderItemRequest> Items { get; set; } = new();
    }

    public class CreateOrderItemRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class OrderResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public OrderResponseData? Data { get; set; }
    }

    public class OrderResponseData
    {
        public int OrderId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal AmountReceived { get; set; }
        public decimal ChangeAmount { get; set; }
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public int RewardPointsAdded { get; set; }
        public int TotalRewardPoints { get; set; }
        public string MembershipRank { get; set; } = string.Empty;
    }
}