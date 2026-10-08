using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormProductManagement : Form
    {
        public FormProductManagement()
        {
            InitializeComponent();

            // Gắn event bằng code
            this.Load += FormProductManagement_Load;

            btnSearch.Click += btnSearch_Click;
            btnLoad.Click += btnLoad_Click;
            btnAdd.Click += btnAdd_Click;
            btnUpdate.Click += btnUpdate_Click;
            btnDelete.Click += btnDelete_Click;
            btnClear.Click += btnClear_Click;

            dgvProducts.CellClick += dgvProducts_CellClick;
        }

        // =====================================================
        // HTTP CLIENT
        // =====================================================

        private HttpClient GetAuthenticatedClient()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7292/api/")
            };

            if (!string.IsNullOrEmpty(SessionManager.JwtToken))
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(
                        "Bearer",
                        SessionManager.JwtToken
                    );
            }

            return client;
        }

        // =====================================================
        // LOAD FORM
        // =====================================================

        private async void FormProductManagement_Load(
            object? sender,
            EventArgs e)
        {
            string role = SessionManager.CurrentRole.Trim().ToUpper();

            // Chỉ Admin và Warehouse được mở màn hình này
            if (role != "ADMIN" && role != "WAREHOUSE")
            {
                MessageBox.Show(
                    "Bạn không có quyền truy cập quản lý sản phẩm!",
                    "Từ chối truy cập",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                this.Close();
                return;
            }

            // Warehouse chỉ được xem
            if (role == "WAREHOUSE")
            {
                btnAdd.Enabled = false;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
            }

            lblReady.Text =
                $"Sẵn sàng | Quyền: {SessionManager.CurrentRole} | Quản lý sản phẩm";

            await LoadCategoriesAsync();
            await LoadProductsAsync();
        }

        // =====================================================
        // LOAD CATEGORY
        // =====================================================

        private async Task LoadCategoriesAsync()
        {
            try
            {
                using var client = GetAuthenticatedClient();

                var categories =
                    await client.GetFromJsonAsync<List<CategoryDto>>(
                        "categories"
                    );

                if (categories == null)
                    return;

                cbCategory.DataSource = null;
                cbCategory.DataSource = categories;
                cbCategory.DisplayMember = "CategoryName";
                cbCategory.ValueMember = "CategoryId";

                if (categories.Count > 0)
                    cbCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể tải danh mục: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =====================================================
        // LOAD PRODUCTS
        // =====================================================

        private async Task LoadProductsAsync()
        {
            try
            {
                using var client = GetAuthenticatedClient();

                var products =
                    await client.GetFromJsonAsync<List<ProductDto>>(
                        "products"
                    );

                dgvProducts.DataSource = null;
                dgvProducts.DataSource = products;

                FormatDataGridView();

                lblReady.Text =
                    $"Đã tải {products?.Count ?? 0} sản phẩm | Quyền: {SessionManager.CurrentRole}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải danh sách sản phẩm: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =====================================================
        // FORMAT DATAGRIDVIEW
        // =====================================================

        private void FormatDataGridView()
        {
            if (dgvProducts.Columns.Count == 0)
                return;

            if (dgvProducts.Columns["ProductId"] != null)
                dgvProducts.Columns["ProductId"].HeaderText = "ID";

            if (dgvProducts.Columns["Barcode"] != null)
                dgvProducts.Columns["Barcode"].HeaderText = "Mã vạch";

            if (dgvProducts.Columns["ProductName"] != null)
                dgvProducts.Columns["ProductName"].HeaderText = "Tên sản phẩm";

            if (dgvProducts.Columns["Price"] != null)
                dgvProducts.Columns["Price"].HeaderText = "Giá";

            if (dgvProducts.Columns["StockQuantity"] != null)
                dgvProducts.Columns["StockQuantity"].HeaderText = "Tồn kho";

            if (dgvProducts.Columns["Unit"] != null)
                dgvProducts.Columns["Unit"].HeaderText = "Đơn vị";

            if (dgvProducts.Columns["CategoryId"] != null)
                dgvProducts.Columns["CategoryId"].HeaderText = "ID danh mục";

            if (dgvProducts.Columns["Category"] != null)
                dgvProducts.Columns["Category"].Visible = false;
        }

        // =====================================================
        // CLICK DATAGRIDVIEW
        // =====================================================

        private void dgvProducts_CellClick(
            object? sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (dgvProducts.Rows[e.RowIndex].DataBoundItem
                is not ProductDto product)
                return;

            txtProductId.Text = product.ProductId.ToString();
            txtBarcode.Text = product.Barcode;
            txtProductName.Text = product.ProductName;
            txtPrice.Text = product.Price.ToString("0.##");
            txtStockQuantity.Text = product.StockQuantity.ToString();
            txtUnit.Text = product.Unit ?? "";

            if (cbCategory.Items.Count > 0)
            {
                cbCategory.SelectedValue = product.CategoryId;
            }
        }

        // =====================================================
        // ADD
        // =====================================================

        private async void btnAdd_Click(
            object? sender,
            EventArgs e)
        {
            if (!ValidateInput())
                return;

            if (!TryGetInput(
                    out string barcode,
                    out string productName,
                    out decimal price,
                    out int stock,
                    out string unit,
                    out int categoryId))
            {
                return;
            }

            var newProduct = new
            {
                Barcode = barcode,
                ProductName = productName,
                Price = price,
                StockQuantity = stock,
                Unit = unit,
                CategoryId = categoryId
            };

            try
            {
                using var client = GetAuthenticatedClient();

                var response =
                    await client.PostAsJsonAsync(
                        "products",
                        newProduct
                    );

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Thêm sản phẩm thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    await LoadProductsAsync();
                    ClearInputs();
                }
                else
                {
                    string error =
                        await response.Content.ReadAsStringAsync();

                    MessageBox.Show(
                        "Thêm sản phẩm thất bại:\n" + error,
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi kết nối: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =====================================================
        // UPDATE
        // =====================================================

        private async void btnUpdate_Click(
            object? sender,
            EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProductId.Text))
            {
                MessageBox.Show(
                    "Vui lòng chọn sản phẩm cần sửa!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            if (!ValidateInput())
                return;

            if (!int.TryParse(
                    txtProductId.Text,
                    out int productId))
            {
                MessageBox.Show("ID sản phẩm không hợp lệ!");
                return;
            }

            if (!TryGetInput(
                    out string barcode,
                    out string productName,
                    out decimal price,
                    out int stock,
                    out string unit,
                    out int categoryId))
            {
                return;
            }

            var updateProduct = new
            {
                ProductId = productId,
                Barcode = barcode,
                ProductName = productName,
                Price = price,
                StockQuantity = stock,
                Unit = unit,
                CategoryId = categoryId
            };

            try
            {
                using var client = GetAuthenticatedClient();

                var response =
                    await client.PutAsJsonAsync(
                        $"products/{productId}",
                        updateProduct
                    );

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Cập nhật sản phẩm thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    await LoadProductsAsync();
                    ClearInputs();
                }
                else
                {
                    string error =
                        await response.Content.ReadAsStringAsync();

                    MessageBox.Show(
                        "Cập nhật thất bại:\n" + error,
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi kết nối: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =====================================================
        // DELETE
        // =====================================================

        private async void btnDelete_Click(
            object? sender,
            EventArgs e)
        {
            if (!int.TryParse(
                    txtProductId.Text,
                    out int productId))
            {
                MessageBox.Show(
                    "Vui lòng chọn sản phẩm cần xóa!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc muốn xóa sản phẩm ID = {productId}?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result != DialogResult.Yes)
                return;

            try
            {
                using var client = GetAuthenticatedClient();

                var response =
                    await client.DeleteAsync(
                        $"products/{productId}"
                    );

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Xóa sản phẩm thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    await LoadProductsAsync();
                    ClearInputs();
                }
                else
                {
                    string error =
                        await response.Content.ReadAsStringAsync();

                    MessageBox.Show(
                        "Xóa thất bại:\n" + error,
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi kết nối: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =====================================================
        // SEARCH
        // =====================================================

        private async void btnSearch_Click(
            object? sender,
            EventArgs e)
        {
            string keyword = txtKeyword.Text.Trim();

            if (string.IsNullOrWhiteSpace(keyword))
            {
                await LoadProductsAsync();
                return;
            }

            try
            {
                using var client = GetAuthenticatedClient();

                string url =
                    $"products/search?keyword={Uri.EscapeDataString(keyword)}";

                var products =
                    await client.GetFromJsonAsync<List<ProductDto>>(url);

                dgvProducts.DataSource = null;
                dgvProducts.DataSource = products;

                FormatDataGridView();

                lblReady.Text =
                    $"Tìm thấy {products?.Count ?? 0} sản phẩm.";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tìm kiếm: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =====================================================
        // LOAD BUTTON
        // =====================================================

        private async void btnLoad_Click(
            object? sender,
            EventArgs e)
        {
            txtKeyword.Clear();

            await LoadCategoriesAsync();
            await LoadProductsAsync();

            ClearInputs();
        }

        // =====================================================
        // CLEAR
        // =====================================================

        private void btnClear_Click(
            object? sender,
            EventArgs e)
        {
            ClearInputs();
        }

        private void ClearInputs()
        {
            txtProductId.Clear();
            txtBarcode.Clear();
            txtProductName.Clear();
            txtPrice.Clear();
            txtStockQuantity.Clear();
            txtUnit.Clear();

            if (cbCategory.Items.Count > 0)
                cbCategory.SelectedIndex = 0;

            dgvProducts.ClearSelection();
        }

        // =====================================================
        // VALIDATE
        // =====================================================

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtBarcode.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập mã vạch!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtBarcode.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập tên sản phẩm!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtProductName.Focus();
                return false;
            }

            if (!decimal.TryParse(
                    txtPrice.Text.Trim(),
                    NumberStyles.Number,
                    CultureInfo.CurrentCulture,
                    out decimal price) || price < 0)
            {
                MessageBox.Show(
                    "Giá sản phẩm không hợp lệ!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtPrice.Focus();
                return false;
            }

            if (!int.TryParse(
                    txtStockQuantity.Text.Trim(),
                    out int stock) || stock < 0)
            {
                MessageBox.Show(
                    "Số lượng tồn kho không hợp lệ!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtStockQuantity.Focus();
                return false;
            }

            if (cbCategory.SelectedValue == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn danh mục!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return false;
            }

            return true;
        }

        // =====================================================
        // GET INPUT
        // =====================================================

        private bool TryGetInput(
            out string barcode,
            out string productName,
            out decimal price,
            out int stock,
            out string unit,
            out int categoryId)
        {
            barcode = txtBarcode.Text.Trim();
            productName = txtProductName.Text.Trim();
            unit = txtUnit.Text.Trim();

            price = 0;
            stock = 0;
            categoryId = 0;

            if (!decimal.TryParse(
                    txtPrice.Text.Trim(),
                    NumberStyles.Number,
                    CultureInfo.CurrentCulture,
                    out price))
            {
                MessageBox.Show("Giá không hợp lệ!");
                return false;
            }

            if (!int.TryParse(
                    txtStockQuantity.Text.Trim(),
                    out stock))
            {
                MessageBox.Show("Số lượng tồn kho không hợp lệ!");
                return false;
            }

            if (cbCategory.SelectedValue == null ||
                !int.TryParse(
                    cbCategory.SelectedValue.ToString(),
                    out categoryId))
            {
                MessageBox.Show("Danh mục không hợp lệ!");
                return false;
            }

            return true;
        }

        private void panelInputs_Paint(object sender, PaintEventArgs e)
        {

        }
    }

    // =========================================================
    // PRODUCT DTO
    // =========================================================

    public class ProductDto
    {
        public int ProductId { get; set; }

        public string Barcode { get; set; } = string.Empty;

        public string ProductName { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public string? Unit { get; set; }

        public int CategoryId { get; set; }

        public CategoryDto? Category { get; set; }
    }
}