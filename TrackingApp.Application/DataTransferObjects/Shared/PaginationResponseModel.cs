namespace TrackingApp.Application.DataTransferObjects.Shared
{
    public class PaginationResponseModel
    {
        public Pagination Pagination { get; set; }
        public dynamic? Items { get; set; }
    }

    public class Pagination
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
    }
}
