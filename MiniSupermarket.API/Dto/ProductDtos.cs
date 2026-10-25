using System.ComponentModel.DataAnnotations;

namespace MiniSupermarket.API.Dto
{
    // DTO trả về thông tin sản phẩm
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string? Unit { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }

    // DTO tiếp nhận dữ liệu thêm mới sản phẩm
    public class CreateProductDto
    {
        [Required(ErrorMessage = "Mã vạch sản phẩm không được để trống!")]
        [StringLength(50, ErrorMessage = "Mã vạch không vượt quá 50 ký tự")]
        public string Barcode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tên sản phẩm không được để trống!")]
        [StringLength(150, ErrorMessage = "Tên sản phẩm không vượt quá 150 ký tự")]
        public string ProductName { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = "Giá bán sản phẩm phải lớn hơn 0!")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Số lượng tồn kho không được là số âm!")]
        public int StockQuantity { get; set; }

        [StringLength(50)]
        public string? Unit { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn danh mục hợp lệ!")]
        public int CategoryId { get; set; }
    }

    // DTO tiếp nhận dữ liệu cập nhật sản phẩm
    public class UpdateProductDto
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Mã vạch sản phẩm không được để trống!")]
        [StringLength(50, ErrorMessage = "Mã vạch không vượt quá 50 ký tự")]
        public string Barcode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tên sản phẩm không được để trống!")]
        [StringLength(150, ErrorMessage = "Tên sản phẩm không vượt quá 150 ký tự")]
        public string ProductName { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = "Giá bán sản phẩm phải lớn hơn 0!")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Số lượng tồn kho không được là số âm!")]
        public int StockQuantity { get; set; }

        [StringLength(50)]
        public string? Unit { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn danh mục hợp lệ!")]
        public int CategoryId { get; set; }
    }
}
