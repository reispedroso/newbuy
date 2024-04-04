using Microsoft.EntityFrameworkCore;
using newbuy.Domain.Interfaces;
using newbuy.Domain.Models;
using newbuy.Infrastructure.Data;

namespace newbuy.Infrastructure.Repositories;

public class UserRepository(AppDbContext context) : UserInterface 
{
    private readonly AppDbContext _context = context;

    public async Task CreateUser(User user)
    {
       if (user == null)
        {
            throw new ArgumentException("Object not found", nameof(user));
        }
        
        try
        {
            User newUser = new()
            {
                Id = Guid.NewGuid(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password!,
                CreatedAt = DateTime.UtcNow,
                UserTypeId = user.UserTypeId
            };
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new Exception($"Erro ao adicionar usuario no banco: {ex.Message}");
        }
    }

    public Task<IEnumerable<User>> GetAllUsers()
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUserById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateUser(Guid id, User user)
    {
        throw new NotImplementedException();
    }

    public Task DeleteUser(Guid id)
    {
        throw new NotImplementedException();
    }
    public Task<User> AuthenticateUser(string email, string password)
    {
        throw new NotImplementedException();
    }
}