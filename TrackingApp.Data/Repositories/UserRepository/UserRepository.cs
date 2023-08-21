using Microsoft.EntityFrameworkCore;
using TrackingApp.Application.Enums;
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
            await _context.User.Where(u => u.IsActive && u.UserRole.Role.RoleName != RolesKey.AD).ToListAsync();

        public async Task<User> GetUserById(Guid userId) =>
            await _context.User.FirstOrDefaultAsync(u => u.UserId == userId && u.IsActive && u.UserRole.Role.RoleName != RolesKey.AD);

        public async Task<User> AddUser(User user)
        {
            await _context.User.AddAsync(user);
            return user;
        }

        public User UpdateUser(User user)
        {
            _context.User.Update(user);
            return user;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UserWithUsernameExists(string username) =>
            await _context.User.AnyAsync(u => u.UserName == username && u.IsActive);

        public async Task<bool> UserWithEmailExists(string email) =>
            await _context.User.AnyAsync(u => u.Email == email && u.IsActive);
    }
}
