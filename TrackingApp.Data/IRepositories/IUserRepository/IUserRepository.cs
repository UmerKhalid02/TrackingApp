using TrackingApp.Data.Entities.UserEntity;

namespace TrackingApp.Data.IRepositories.IUserRepository
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsers();
        Task<User> GetUserById(Guid userId);
        Task<User> AddUser(User user);
        Task SaveChanges();
    }
}
