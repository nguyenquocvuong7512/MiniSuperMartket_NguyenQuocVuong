using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormCustomerManagement : Form
    {
        // === Cấu hình ===
        private static readonly HttpClient _httpClient = new HttpClient();
        private const string ApiBase = "https://localhost:7123/api/customers";
        private string _jwtToken = string.Empty;

        // === Lớp DTO nhận dữ liệu ===
        public class CustomerDto
        {
            public int CustomerId { get; set; }
            public string CustomerName { get; set; } = string.Empty;
            public string PhoneNumber { get; set; } = string.Empty;
            public string? Address { get; set; }
            public int RewardPoints { get; set; }
            public string MembershipRank { get; set; } = "Chuẩn";
        }

        // === Constructor ===
        public FormCustomerManagement(string jwtToken)
        {
            _jwtToken = jwtToken;
            InitializeComponent(); // Gọi hàm thiết kế từ file khác
            SetupHttpClient();
            BindEvents();
        }

        // === Cấu hình HttpClient ===
        private void SetupHttpClient()
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_jwtToken}");
        }

        // === Gắn sự kiện cho các nút bấm ===
        private void BindEvents()
        {
            btnLoad.Click += BtnLoad_Click;
            btnAdd.Click += BtnAdd_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnDelete.Click += BtnDelete_Click;
            btnSearch.Click += BtnSearch_Click;
            dgvCustomers.CellClick += DgvCustomers_CellClick;
        }

        // === 1. Tải danh sách ===
        private async void BtnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                var list = await _httpClient.GetFromJsonAsync<List<CustomerDto>>(ApiBase);
                dgvCustomers.DataSource = null;
                dgvCustomers.DataSource = list;
                FormatDataGridView();
                ClearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "❌ Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // === 2. Chọn dòng trên lưới → Điền vào ô nhập ===
        private void DgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvCustomers.Rows[e.RowIndex];
            txtCustomerId.Text = row.Cells["CustomerId"].Value.ToString();
            txtCustomerName.Text = row.Cells["CustomerName"].Value?.ToString() ?? "";
            txtPhoneNumber.Text = row.Cells["PhoneNumber"].Value?.ToString() ?? "";
            txtAddress.Text = row.Cells["Address"].Value?.ToString() ?? "";
            txtRewardPoints.Text = row.Cells["RewardPoints"].Value?.ToString() ?? "0";
            txtMembershipRank.Text = row.Cells["MembershipRank"].Value?.ToString() ?? "Chuẩn";
        }

        // === 3. Thêm khách hàng mới ===
        private async void BtnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCustomerName.Text) ||
                string.IsNullOrWhiteSpace(txtPhoneNumber.Text))
            {
                MessageBox.Show("Tên và Số điện thoại không được bỏ trống!",
                    "⚠️ Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var newCustomer = new
            {
                CustomerName = txtCustomerName.Text.Trim(),
                PhoneNumber = txtPhoneNumber.Text.Trim(),
                Address = txtAddress.Text.Trim(),
                RewardPoints = int.TryParse(txtRewardPoints.Text, out var p) ? p : 0,
                MembershipRank = string.IsNullOrWhiteSpace(txtMembershipRank.Text)
                                    ? "Chuẩn"
                                    : txtMembershipRank.Text.Trim()
            };

            try
            {
                var response = await _httpClient.PostAsJsonAsync(ApiBase, newCustomer);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("✅ Thêm khách hàng thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BtnLoad_Click(sender, e);
                }
                else
                {
                    var errorMsg = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"❌ Lỗi: {errorMsg}", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi kết nối: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // === 4. Cập nhật thông tin ===
        private async void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCustomerId.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần sửa!",
                    "⚠️ Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCustomerName.Text) ||
                string.IsNullOrWhiteSpace(txtPhoneNumber.Text))
            {
                MessageBox.Show("Tên và Số điện thoại không được bỏ trống!",
                    "⚠️ Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = int.Parse(txtCustomerId.Text);
            var updateCustomer = new
            {
                CustomerId = id,
                CustomerName = txtCustomerName.Text.Trim(),
                PhoneNumber = txtPhoneNumber.Text.Trim(),
                Address = txtAddress.Text.Trim(),
                RewardPoints = int.TryParse(txtRewardPoints.Text, out var p) ? p : 0,
                MembershipRank = string.IsNullOrWhiteSpace(txtMembershipRank.Text)
                                    ? "Chuẩn"
                                    : txtMembershipRank.Text.Trim()
            };

            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{ApiBase}/{id}", updateCustomer);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("✅ Cập nhật thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BtnLoad_Click(sender, e);
                }
                else
                {
                    var errorMsg = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"❌ Lỗi: {errorMsg}", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi kết nối: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // === 5. Xóa khách hàng ===
        private async void BtnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCustomerId.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa!",
                    "⚠️ Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn xóa?", "❓ Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            int id = int.Parse(txtCustomerId.Text);

            try
            {
                var response = await _httpClient.DeleteAsync($"{ApiBase}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("✅ Xóa thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BtnLoad_Click(sender, e);
                }
                else
                {
                    var errorMsg = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"❌ Lỗi: {errorMsg}", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi kết nối: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // === 6. Tìm kiếm ===
        private async void BtnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();

            if (string.IsNullOrWhiteSpace(keyword))
            {
                BtnLoad_Click(sender, e);
                return;
            }

            try
            {
                var list = await _httpClient.GetFromJsonAsync<List<CustomerDto>>($"{ApiBase}/search?keyword={keyword}");

                dgvCustomers.DataSource = null;
                dgvCustomers.DataSource = list;
                FormatDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tìm kiếm: {ex.Message}", "❌ Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // === Công cụ: Xóa trắng ô nhập ===
        private void ClearInputFields()
        {
            txtCustomerId.Clear();
            txtCustomerName.Clear();
            txtPhoneNumber.Clear();
            txtAddress.Clear();
            txtRewardPoints.Text = "0";
            txtMembershipRank.Text = "Chuẩn";
        }

        // === Công cụ: Định dạng cột lưới ===
        private void FormatDataGridView()
        {
            dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCustomers.Columns["CustomerId"].HeaderText = "Mã KH";
            dgvCustomers.Columns["CustomerName"].HeaderText = "Tên Khách Hàng";
            dgvCustomers.Columns["PhoneNumber"].HeaderText = "Số Điện Thoại";
            dgvCustomers.Columns["Address"].HeaderText = "Địa Chỉ";
            dgvCustomers.Columns["RewardPoints"].HeaderText = "Điểm Tích Lũy";
            dgvCustomers.Columns["MembershipRank"].HeaderText = "Hạng Thẻ";
        }
    }
}