/*
 * Họ và tên sinh viên: Nguyễn Quốc Vương
 * DTO: Nghiệp vụ thanh toán giỏ hàng POS
 */
namespace MiniSupermarket.API.Dto
{
    // DTO biểu diễn từng dòng sản phẩm trong giỏ hàng
    public class CartItemCheckoutDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

    // DTO nhận yêu cầu thanh toán tổng thể từ máy POS
    public class CheckoutRequestDto
    {
        // Tài khoản thu ngân đang đứng ca trực (ví dụ: cashier01, cashier02...)
        public string CashierUsername { get; set; } = string.Empty;

        // Mã định danh khách hàng (nếu là khách vãng lai, truyền CustomerId = 15)
        public int CustomerId { get; set; }

        // Phương thức thanh toán (Cash / Transfer)
        public string PaymentMethod { get; set; } = "Cash";

        // Danh sách các món hàng cần thanh toán
        public List<CartItemCheckoutDto> Items { get; set; } = new();
    }
}
