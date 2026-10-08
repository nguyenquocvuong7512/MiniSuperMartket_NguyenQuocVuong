using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms.Forms
{
    public partial class FormPOS : Form
    {
        private readonly HttpClient _client;
        private readonly List<CartItem> _cart = new();

        public FormPOS()
        {
            InitializeComponent();

            _client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7292/api/")
            };

            cboPaymentMethod.SelectedIndex = 0;

            SetupEvents();
            _ = LoadProductsAsync();
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

            var confirm = MessageBox.Show(
                $"Tổng tiền: {total:N0} đ\nPhương thức: {paymentMethod}\n\nXác nhận thanh toán?",
                "Xác nhận",
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

                MessageBox.Show(
                    $"THANH TOÁN THÀNH CÔNG!\n\nMã đơn: #{result.Data?.OrderId}\nTổng tiền: {total:N0} đ\nTiền khách đưa: {amountReceived:N0} đ\nTiền thừa: {change:N0} đ",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                _cart.Clear();
                txtAmountReceived.Clear();
                RefreshCart();

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

        // =========================================================
        // AUTHORIZATION
        // =========================================================

        private void AddAuthorizationHeader()
        {
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", SessionManager.JwtToken);
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
    }
}