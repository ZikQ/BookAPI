using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BookAPI;

public class AuthOptions
{
    public string Secret { get; set; } = null!;
    public string Issuer { get; set; } = "BookAPI";
    public string Audience { get; set; } = "BookAPIClients";
    public int ExpirationMinutes { get; set; } = 60;
    
    public SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
    }
}