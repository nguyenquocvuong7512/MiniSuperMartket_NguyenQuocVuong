using System.ComponentModel.DataAnnotations;

namespace MiniSupermarket.API.Dto
{
    // DTO trả về thông tin khách hàng
    public class CustomerDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? Address { get; set; }
        public int RewardPoints { get; set; }
        public string MembershipRank { get; set; } = "Chuẩn";
    }

    // DTO tiếp nhận dữ liệu thêm mới khách hàng
    public class CreateCustomerDto
    {
        [Required(ErrorMessage = "Tên khách hàng không được để trống!")]
        [StringLength(100, ErrorMessage = "Tên khách hàng không vượt quá 100 ký tự")]
        public string CustomerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Số điện thoại không được để trống!")]
        [StringLength(15, ErrorMessage = "Số điện thoại không vượt quá 15 ký tự")]
        public string PhoneNumber { get; set; } = string.Empty;

        [StringLength(200, ErrorMessage = "Địa chỉ không vượt quá 200 ký tự")]
        public string? Address { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Điểm tích lũy không được là số âm!")]
        public int RewardPoints { get; set; } = 0;

        [StringLength(50)]
        public string MembershipRank { get; set; } = "Chuẩn";
    }

    // DTO tiếp nhận dữ liệu cập nhật khách hàng
    public class UpdateCustomerDto
    {
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Tên khách hàng không được để trống!")]
        [StringLength(100, ErrorMessage = "Tên khách hàng không vượt quá 100 ký tự")]
        public string CustomerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Số điện thoại không được để trống!")]
        [StringLength(15, ErrorMessage = "Số điện thoại không vượt quá 15 ký tự")]
        public string PhoneNumber { get; set; } = string.Empty;

        [StringLength(200, ErrorMessage = "Địa chỉ không vượt quá 200 ký tự")]
        public string? Address { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Điểm tích lũy không được là số âm!")]
        public int RewardPoints { get; set; } = 0;

        [StringLength(50)]
        public string MembershipRank { get; set; } = "Chuẩn";
    }
}
