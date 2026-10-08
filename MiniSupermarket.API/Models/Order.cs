using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniSupermarket.API.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        // Nhân viên thực hiện đơn hàng
        [Required]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User? User { get; set; }

        // Ngày giờ tạo đơn
        public DateTime OrderDate { get; set; } = DateTime.Now;

        // Tổng tiền đơn hàng
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        // Trạng thái đơn hàng
        // Completed: đã thanh toán
        // Cancelled: đã hủy
        public string Status { get; set; } = "Completed";

        // Phương thức thanh toán
        // Cash / Transfer
        public string PaymentMethod { get; set; } = "Cash";

        // Số tiền khách đưa
        [Column(TypeName = "decimal(18,2)")]
        public decimal AmountReceived { get; set; }

        // Tiền thừa trả khách
        [Column(TypeName = "decimal(18,2)")]
        public decimal ChangeAmount { get; set; }

        // Danh sách sản phẩm trong đơn
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
            = new List<OrderDetail>();
    }
}