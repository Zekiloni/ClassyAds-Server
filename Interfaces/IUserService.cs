using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetUserById(int userId);
        Task<User?> GetUserByUsername(string username);
        Task<IEnumerable<User>> GetAllUsers();
        Task CreateUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(User user);
    }
}
