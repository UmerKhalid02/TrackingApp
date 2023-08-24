using System.ComponentModel.DataAnnotations;

namespace TrackingApp.Application.DataTransferObjects.OrderDTO
{
    public class AddOrderRequestDTO
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? OrderName { get; set; } // OrderName is the name of the product
        public string? DesignNo { get; set; }
        public string? Vendor { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime OrderDate { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DeliveryDate { get; set; }
        public string? Description { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be atleast 1")]
        public int Quantity { get; set; }
        public string? OrderTaker { get; set; }
    }
}
