using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MiniSupermarket.API.Models
{
    // Lớp biểu diễn thực thể Nhóm hàng hóa trong siêu thị mini
    [Table("Categories")] // Đặt tên bảng rõ ràng trong CSDL
    public class Category
    {
        // Mã định danh nhóm hàng (Khóa chính, tự động tăng)
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }

        // Tên nhóm hàng (Bắt buộc, giới hạn độ dài)
        [Required(ErrorMessage = "Tên nhóm hàng không được để trống!")]
        [StringLength(100, ErrorMessage = "Tên nhóm hàng không vượt quá 100 ký tự")]
        public string CategoryName { get; set; } = string.Empty;

        // Mô tả chi tiết về nhóm hàng (Có thể để trống, giới hạn 255 ký tự)
        [StringLength(255)]
        public string? Description { get; set; }

        // Quan hệ 1 - N: Một danh mục chứa nhiều sản phẩm
        [JsonIgnore] // Ngăn lỗi lặp vòng khi trả dữ liệu JSON
        public virtual ICollection<Product>? Products { get; set; }
    }
}