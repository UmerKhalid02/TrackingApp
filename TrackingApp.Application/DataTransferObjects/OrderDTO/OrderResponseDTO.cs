namespace TrackingApp.Application.DataTransferObjects.OrderDTO
{
    public class OrderResponseDTO
    {
        public int OrderId { get; set; }
        public string? OrderName { get; set; } 
        public string? DesignNo { get; set; }
        public string? Vendor { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string? Description { get; set; }
        public string? OrderStatus { get; set; }
        public int? Quantity { get; set; }
        public string? OrderTaker { get; set; }
        public string? OrderImagePath { get; set; }
        public string? StitchingImagePath { get; set; }
        public Customer Customer { get; set; }
    }

    public class Customer
    {
        public Guid UserId { get; set; }
        public string? UserName { get; set; }
    }
}
