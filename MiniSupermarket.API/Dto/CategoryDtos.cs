using System.ComponentModel.DataAnnotations;

namespace MiniSupermarket.API.Dto
{
    // DTO trả về thông tin nhóm hàng
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string? Description { get; set; }
    }

    // DTO tiếp nhận dữ liệu thêm mới nhóm hàng
    public class CreateCategoryDto
    {
        [Required(ErrorMessage = "Tên nhóm hàng không được để trống!")]
        [StringLength(100, ErrorMessage = "Tên nhóm hàng không vượt quá 100 ký tự")]
        public string CategoryName { get; set; } = string.Empty;

        [StringLength(255, ErrorMessage = "Mô tả không vượt quá 255 ký tự")]
        public string? Description { get; set; }
    }

    // DTO tiếp nhận dữ liệu cập nhật nhóm hàng
    public class UpdateCategoryDto
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Tên nhóm hàng không được để trống!")]
        [StringLength(100, ErrorMessage = "Tên nhóm hàng không vượt quá 100 ký tự")]
        public string CategoryName { get; set; } = string.Empty;

        [StringLength(255, ErrorMessage = "Mô tả không vượt quá 255 ký tự")]
        public string? Description { get; set; }
    }
}
