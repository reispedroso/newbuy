using BCrypt.Net;

namespace newbuy.App.Services;
public class PasswordHash
{
    public string HashPassword(string password)
    {
        if (password == null)
        {
            throw new Exception("Password is null");
        }
        string salt = BCrypt.Net.BCrypt.GenerateSalt();

        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);

        return hashedPassword;
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        if (password == null || hashedPassword == null)
        {
            throw new Exception("Password or hashed password is null");
        }
        bool passwordMatches = BCrypt.Net.BCrypt.Verify(password, hashedPassword);

        return passwordMatches;
    }
}