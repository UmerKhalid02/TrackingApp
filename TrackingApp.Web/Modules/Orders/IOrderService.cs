using TrackingApp.Application.DataTransferObjects.OrderDTO;
using TrackingApp.Application.DataTransferObjects.Shared;
using TrackingApp.Application.Parameters;
using TrackingApp.Application.Wrappers;

namespace TrackingApp.Web.Modules.Orders
{
    public interface IOrderService
    {
        Task<Response<PaginationResponseModel>> GetAllActiveOrders(OrderPageParamter request);
        Task<Response<OrderResponseDTO>> GetActiveOrderById(int orderId);
        Task<Response<OrderResponseDTO>> AddOrder(AddOrderRequestDTO request);
        Task<Response<OrderResponseDTO>> UpdateOrder(int orderid, UpdateOrderRequestDTO request);
        Task<Response<OrderResponseDTO>> UpdateOrderStatus(int orderId, string status);
        Task<Response<bool>> DeleteOrder(int orderId);
    }
}
