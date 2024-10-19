using System.Security.Cryptography;
using System.Text;

namespace JuiceWorld.Utils;

public static class AuthUtils
{
    public static string GenerateSalt(int bytesCount = 16)
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(bytesCount));
    }

    public static string HashPassword(string password, string salt)
    {
        var hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(password + salt));
        return Convert.ToBase64String(hashedBytes);
    }
}
