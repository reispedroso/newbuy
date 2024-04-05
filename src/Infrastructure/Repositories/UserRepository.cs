using Microsoft.EntityFrameworkCore;
using newbuy.App.Services;
using newbuy.Domain.Interfaces;
using newbuy.Domain.Models;
using newbuy.Infrastructure.Data;

namespace newbuy.Infrastructure.Repositories;

public class UserRepository(AppDbContext context) : IUserInterface 
{
    private readonly AppDbContext _context = context;
    private readonly PasswordHash _passwordHash = new();
    private readonly DateTimeCorrection _timeCorrection = new();

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
                Password = _passwordHash.HashPassword(user.Password),
                CreatedAt = _timeCorrection.GetCorrectedDateTime(DateTime.UtcNow),
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

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        List<User> users = await _context.Users.ToListAsync();

        if (users == null)
        {
            throw new Exception("Users not found");
        }

        return users;
    }

    public async Task<User> GetUserById(Guid id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

        if (user == null)
        {
            throw new Exception("User not found");
        }

        return user;
    }

    public async Task UpdateUser(Guid id, User user)
    {
          User userToUpdate = GetUserById(id).Result;

        userToUpdate.FirstName = user.FirstName;
        userToUpdate.LastName = user.LastName;
        userToUpdate.Email = user.Email;
        userToUpdate.Password = _passwordHash.HashPassword(user.Password!);
        userToUpdate.UpdatedAt = _timeCorrection.GetCorrectedDateTime(DateTime.UtcNow);
        userToUpdate.UserTypeId = user.UserTypeId;
        
        _context.Users.Update(userToUpdate);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUser(Guid id)
    {
       
        User userToDelete = GetUserById(id).Result;

        userToDelete.DeletedAt = _timeCorrection.GetCorrectedDateTime(DateTime.UtcNow);
        await _context.SaveChangesAsync();
    }
    public async Task<User> AuthenticateUser(string email, string password)
    {
        
        var user =  await _context.Users.Include(u => u.UserType).FirstOrDefaultAsync(u => u.Email == email) ?? throw new Exception("Usuário não encontrado");
        if (!_passwordHash.VerifyPassword(password, user.Password!))
        {
            throw new Exception("Incorrect password");
        }

        return user;
    }
}