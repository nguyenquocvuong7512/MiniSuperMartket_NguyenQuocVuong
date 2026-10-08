namespace MiniSupermarket.API.Models
{
    // Lớp biểu diễn thực thể Vai trò nhân viên trong siêu thị mini
    public class Role
    {
        // Mã định danh vai trò (Khóa chính)
        public int Id { get; set; }

        // Tên vai trò
        public string RoleName { get; set; } = string.Empty;

        // Mô tả chi tiết chức năng của vai trò
        public string? Description { get; set; }

        // Danh sách nhân viên thuộc vai trò này
        public virtual ICollection<User> Users { get; set; }
            = new List<User>();
    }
}