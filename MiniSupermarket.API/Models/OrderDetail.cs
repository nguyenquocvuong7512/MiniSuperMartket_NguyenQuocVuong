using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniSupermarket.API.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailId { get; set; }

        // Khóa ngoại đến Order
        [Required]
        public int OrderId { get; set; }

        [ForeignKey(nameof(OrderId))]
        public virtual Order? Order { get; set; }

        // Khóa ngoại đến Product
        [Required]
        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual Product? Product { get; set; }

        // Số lượng mua
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        // Đơn giá tại thời điểm bán
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Đơn giá phải lớn hơn 0!")]
        public decimal UnitPrice { get; set; }

        // Thành tiền
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "Thành tiền không được âm!")]
        public decimal SubTotal { get; set; }
    }
}