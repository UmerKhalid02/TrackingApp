using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using TrackingApp.Data.Entities.UserEntity;

namespace TrackingApp.Data.Entities.OrderEntity
{
    [Table("Order")]
    public class Order : BaseEntity
    {
        [Key]
        [Column("OrderID")]
        public int OrderId { get; set; }

        [Column("UserID")]
        [ForeignKey("UserId")]
        public Guid? UserId { get; set; }
        public virtual User User { get; set; }
        
        [Required]
        public string? OrderName { get; set; } // OrderName is the name of the product
        public string? DesignNo { get; set; }

        public string? Vendor { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public DateTime DeliveryDate { get; set; }
        
        public string? Description { get; set; }
        [Required]
        public string? OrderStatus { get; set; }
        [Required]
        public int Quantity { get; set; }
        public string? OrderTaker{ get; set; }
        public string? OrderImagePath { get; set; }
        public string? StitchingImagePath { get; set; }
    }
}
