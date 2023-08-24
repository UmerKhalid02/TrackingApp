using TrackingApp.Data.Entities.OrderEntity;

namespace TrackingApp.Data.IRepositories.IOrderRepository
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllActiveOrders();
        Task<Order> GetActiveOrderById(int orderId);
        Task<Order> AddOrder(Order order);
        Order UpdateOrder(Order order);
        Task SaveChanges();
    }
}
