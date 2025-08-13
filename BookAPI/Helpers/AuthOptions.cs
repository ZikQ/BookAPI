using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BookAPI;

public class AuthOptions
{
    private string? _secret;

    public string Secret
    {
        get => _secret ??= GenerateSecret();
        set => _secret = value;
    }

    public string Issuer { get; set; } = "BookAPI";
    public string Audience { get; set; } = "BookAPIClients";
    public int ExpirationMinutes { get; set; } = 60;

    public SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret));
    }

    private static string GenerateSecret()
    {
        byte[] key = RandomNumberGenerator.GetBytes(32);
        return Convert.ToBase64String(key);
    }
}