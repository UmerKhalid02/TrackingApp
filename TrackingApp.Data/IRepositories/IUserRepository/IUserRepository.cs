using TrackingApp.Data.Entities.UserEntity;

namespace TrackingApp.Data.IRepositories.IUserRepository
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsers();
        Task<User> GetUserById(Guid userId);
        Task<User> AddUser(User user);
        User UpdateUser(User user);
        Task SaveChanges();
        Task<bool> UserWithUsernameExists(string username);
        Task<bool> UserWithEmailExists(string email);
        Task<User> GetUserByUsername(string username);
    }
}
