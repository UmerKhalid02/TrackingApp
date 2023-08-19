using TrackingApp.Application.Enums;

namespace TrackingApp.Application.Parameters
{
    public class RequestPageParamter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SearchBy { get; set; }
        public string? OrderBy { get; set; }
        public string OrderType { get; set; } = PaginationOrder.Descending;
    }
}
