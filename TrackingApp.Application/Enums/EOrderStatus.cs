namespace TrackingApp.Application.Enums
{
    public enum EOrderStatus
    {
        PREPARATION = 0,
        DYING = 1,
        ADDAWORK = 2,
        STITCHING = 3,
        DISPATCHED = 4,
        COMPLETED = 5,
    }

    public static class EOrderStatusExtensions
    {
        public static string GetOrderStatusStringValue(this EOrderStatus status)
        {
            return status switch
            {
                EOrderStatus.DYING => "DYING",
                EOrderStatus.ADDAWORK => "ADDAWORK",
                EOrderStatus.STITCHING => "STITCHING",
                EOrderStatus.DISPATCHED => "DISPATCHED",
                EOrderStatus.COMPLETED => "COMPLETED",
                _ => "PREPARATION",
            };
        }

        public static int GetOrderStatusEnumValue(this string status)
        {
            if (string.IsNullOrEmpty(status))
                return 0;

            return status.ToUpper() switch
            {
                "DYING" => 1,
                "ADDAWORK" => 2,
                "STITCHING" => 3,
                "DISPATCHED" => 4,
                "COMPLETED" => 5,
                _ => 0,
            };
        }

        public static bool OrderStatusIsInvalid(string orderStatus)
        {
            return !Enum.TryParse(orderStatus, true, out EOrderStatus parsedType) || !Enum.IsDefined(typeof(EOrderStatus), parsedType);
        }
    }
}
