using Microsoft.EntityFrameworkCore;
using TrackingApp.Data.Entities.UserEntity;
using TrackingApp.Data.IRepositories.IUserRepository;

namespace TrackingApp.Data.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly EFDataContext _context;
        public UserRepository(EFDataContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUsers() =>
            await _context.User.Where(u => u.IsActive).ToListAsync();

        public async Task<User> GetUserById(Guid userId) =>
            await _context.User.FirstOrDefaultAsync(u => u.UserId == userId && u.IsActive);

        public async Task<User> AddUser(User user)
        {
            await _context.User.AddAsync(user);
            return user;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
