using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormUserManagement : Form
    {
        private int _selectedUserId = 0;

        public FormUserManagement()
        {
            InitializeComponent();
            cboRole.Items.AddRange(new object[] { "Admin", "Cashier", "Warehouse" });
            cboRole.SelectedIndex = 1;

            SetupUserGrid();

            this.Load += async (s, e) => await LoadUsersAsync();
            btnRefresh.Click += async (s, e) => await LoadUsersAsync();
            btnAddUser.Click += async (s, e) => await AddUserAsync();
            btnResetPassword.Click += async (s, e) => await ResetPasswordAsync();
            btnDeleteUser.Click += async (s, e) => await DeleteUserAsync();
            dgvUsers.CellClick += DgvUsers_CellClick;
        }

        private void SetupUserGrid()
        {
            dgvUsers.AutoGenerateColumns = false;
            dgvUsers.Columns.Clear();

            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Id",
                HeaderText = "ID",
                Width = 60
            });

            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Username",
                HeaderText = "Tên Đăng Nhập",
                Width = 140
            });

            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "FullName",
                HeaderText = "Họ Và Tên",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Role",
                HeaderText = "Vai Trò",
                Width = 110
            });

            dgvUsers.Columns.Add(new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "IsActive",
                HeaderText = "Hoạt Động",
                Width = 90
            });
        }

        private async Task LoadUsersAsync()
        {
            if (SessionManager.CurrentRole.Trim().ToUpper() != "ADMIN")
            {
                MessageBox.Show(
                    "Bạn không có quyền quản trị người dùng!",
                    "Từ chối",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Stop
                );
                return;
            }

            try
            {
                var users = await ApiClientService.Client.GetFromJsonAsync<List<UserDto>>("users");
                dgvUsers.DataSource = users;

                int count = users?.Count ?? 0;
                lblUserCount.Text = $"Tổng: {count} người dùng";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi lấy danh sách tài khoản: " + ex.Message,
                    "Lỗi kết nối",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void DgvUsers_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvUsers.Rows[e.RowIndex].DataBoundItem is UserDto user)
            {
                _selectedUserId = user.Id;
                txtUsername.Text = user.Username;
                txtFullName.Text = user.FullName;
                txtPassword.Clear();

                for (int i = 0; i < cboRole.Items.Count; i++)
                {
                    if (string.Equals(cboRole.Items[i]?.ToString(), user.Role, StringComparison.OrdinalIgnoreCase))
                    {
                        cboRole.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private async Task AddUserAsync()
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string fullName = txtFullName.Text.Trim();
            string role = cboRole.SelectedItem?.ToString() ?? "Cashier";

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show(
                    "Tên đăng nhập và mật khẩu không được để trống!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            var newUser = new
            {
                Username = username,
                Password = password,
                FullName = fullName,
                Role = role
            };

            try
            {
                var res = await ApiClientService.Client.PostAsJsonAsync("users", newUser);
                if (res.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Tạo tài khoản mới thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    ClearInputs();
                    await LoadUsersAsync();
                }
                else
                {
                    string err = await res.Content.ReadAsStringAsync();
                    MessageBox.Show(
                        "Không thể tạo tài khoản (có thể tên đăng nhập đã tồn tại)!\n" + err,
                        "Thất bại",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task ResetPasswordAsync()
        {
            if (_selectedUserId <= 0)
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần đặt lại mật khẩu từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show(
                $"Bạn có chắc chắn muốn đặt lại mật khẩu của tài khoản '{txtUsername.Text}' về '123456'?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm != DialogResult.Yes) return;

            try
            {
                var res = await ApiClientService.Client.PutAsync($"users/{_selectedUserId}/reset-password", null);
                if (res.IsSuccessStatusCode)
                {
                    MessageBox.Show("Mật khẩu đã được đặt lại thành công về: 123456", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Lỗi khi đặt lại mật khẩu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task DeleteUserAsync()
        {
            if (_selectedUserId <= 0)
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (txtUsername.Text.Trim().ToLower() == "admin01")
            {
                MessageBox.Show("Không thể xóa tài khoản Quản trị viên gốc (admin01)!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa tài khoản '{txtUsername.Text}' (ID={_selectedUserId})?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirm != DialogResult.Yes) return;

            try
            {
                var res = await ApiClientService.Client.DeleteAsync($"users/{_selectedUserId}");
                if (res.IsSuccessStatusCode)
                {
                    MessageBox.Show("Đã xóa tài khoản!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputs();
                    await LoadUsersAsync();
                }
                else
                {
                    MessageBox.Show("Không thể xóa tài khoản này!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputs()
        {
            _selectedUserId = 0;
            txtUsername.Clear();
            txtPassword.Clear();
            txtFullName.Clear();
            cboRole.SelectedIndex = 1;
        }
    }

    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}
