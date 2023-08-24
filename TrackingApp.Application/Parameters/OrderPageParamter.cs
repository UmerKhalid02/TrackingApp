namespace TrackingApp.Application.Parameters
{
    public class OrderPageParamter : RequestPageParamter
    {
        public string? OrderName { get; set; }
        public int? Quantity { get; set; }
        public string? OrderStatus { get; set; }
    }
}
