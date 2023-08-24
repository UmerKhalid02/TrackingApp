using Microsoft.EntityFrameworkCore;
using TrackingApp.Data.Entities.OrderEntity;
using TrackingApp.Data.IRepositories.IOrderRepository;

namespace TrackingApp.Data.Repositories.OrderRepository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly EFDataContext _context;
        public OrderRepository(EFDataContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetAllActiveOrders() => 
            await _context.Order.Where(x => x.IsActive == true && x.DeletedAt == null)
            .Include(x => x.User)
            .ToListAsync();

        public async Task<Order> GetActiveOrderById(int orderId) =>
            await _context.Order.Include(x => x.User).FirstOrDefaultAsync(x => x.IsActive && x.OrderId == orderId && x.DeletedAt == null);

        public async Task<Order> AddOrder(Order order)
        {
            await _context.Order.AddAsync(order);
            return order;
        }

        public Order UpdateOrder(Order order)
        {
            _context.Order.Update(order);
            return order;
        }

        public async Task SaveChanges() =>
            await _context.SaveChangesAsync();
    }
}
