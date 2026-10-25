using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Windows.Forms;
using MiniSupermarket.WinForms.Services;

namespace MiniSupermarket.WinForms.Forms
{
    public partial class FormQuickAddCustomer : Form
    {
        private readonly HttpClient _client;
        public CustomerDto? CreatedCustomer { get; private set; }

        public FormQuickAddCustomer(string initialPhone = "")
        {
            InitializeComponent();

            _client = new HttpClient
            {
                BaseAddress = new Uri(ApiClientService.ApiBaseUrl)
            };

            if (!string.IsNullOrEmpty(SessionManager.JwtToken))
            {
                _client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", SessionManager.JwtToken);
            }

            txtPhone.Text = initialPhone;

            btnSave.Click += async (s, e) => await SaveCustomerAsync();
            btnCancel.Click += (s, e) => DialogResult = DialogResult.Cancel;

            Load += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtPhone.Text))
                {
                    txtPhone.Focus();
                }
                else
                {
                    txtName.Focus();
                }
            };
        }

        private async System.Threading.Tasks.Task SaveCustomerAsync()
        {
            string phone = txtPhone.Text.Trim();
            string name = txtName.Text.Trim();
            string address = txtAddress.Text.Trim();

            if (string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show(
                    "Vui lòng nhập số điện thoại của khách hàng.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtPhone.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show(
                    "Vui lòng nhập họ và tên khách hàng.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            try
            {
                btnSave.Enabled = false;

                var newCustomer = new
                {
                    CustomerName = name,
                    PhoneNumber = phone,
                    Address = string.IsNullOrWhiteSpace(address) ? null : address,
                    RewardPoints = 0,
                    MembershipRank = "Mới"
                };

                var response = await _client.PostAsJsonAsync("customers", newCustomer);

                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<CustomerDto>();
                    CreatedCustomer = created;

                    MessageBox.Show(
                        $"Đăng ký thành công khách hàng:\n{name} ({phone})",
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    string err = await response.Content.ReadAsStringAsync();
                    MessageBox.Show(
                        "Không thể tạo khách hàng mới:\n" + err,
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi kết nối khi tạo khách hàng:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                btnSave.Enabled = true;
            }
        }
    }
}
