
using newbuy.Domain.Models;

namespace newbuy.Domain.Interfaces;
public interface IUserRepository
{
    Task CreateUser(User user);
    Task<User> GetUserById(Guid id);
    Task<IEnumerable<User>> GetAllUsers();
    Task UpdateUser(Guid id, User user);
    Task DeleteUser(Guid id);
    Task<User> AuthenticateUser(string email, string password);
}