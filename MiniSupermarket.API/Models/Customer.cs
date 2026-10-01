using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniSupermarket.API.Models
{
    [Table("Customers")]
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("CustomerId")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Tên khách hàng không được để trống")]
        [StringLength(100)]
        [Column("CustomerName", TypeName = "nvarchar(100)")]
        public string CustomerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [StringLength(15)]
        [Column("PhoneNumber", TypeName = "varchar(15)")]
        public string PhoneNumber { get; set; } = string.Empty;

        [StringLength(200)]
        [Column("Address", TypeName = "nvarchar(200)")]
        public string? Address { get; set; }

        [Column("RewardPoints")]
        public int RewardPoints { get; set; } = 0;

        [StringLength(50)]
        [Column("MembershipRank", TypeName = "nvarchar(50)")]
        public string MembershipRank { get; set; } = "Chuẩn";
    }
}