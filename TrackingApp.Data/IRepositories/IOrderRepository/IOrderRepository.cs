using TrackingApp.Data.Entities.OrderEntity;

namespace TrackingApp.Data.IRepositories.IOrderRepository
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllActiveOrders();
        Task<List<Order>> GetAllCompletedOrders();
        Task<Order> GetActiveOrderById(int orderId);
        Task<Order> AddOrder(Order order);
        Order UpdateOrder(Order order);
        Task SaveChanges();
        Task<List<Order>> GetAllUserActiveOrders(Guid userId);
        Task<Order> GetUserActiveOrderById(Guid userId, int orderId);
    }
}
