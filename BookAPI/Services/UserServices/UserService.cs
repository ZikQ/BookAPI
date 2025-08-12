using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookAPI.DTOs;
using BookAPI.Models;
using BookAPI.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BookAPI.Services;

public class UserService(IUserRepository repository, IOptions<AuthOptions> authOptions)
    : IUserService
{
    private readonly AuthOptions _authOptions = authOptions.Value;
    
    private readonly PasswordHasher<User> _passwordHasher = new();

    public async Task<User> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var user = await repository.GetByIdAsync(id, ct);
        
        if (user is null)
            throw new Exception($"User {id} not found");
        
        return user;
    }

    public async Task<User> RegisterAsync(CreateUserDto dto, CancellationToken ct = default)
    {
        var existing = await repository.GetByLoginAsync(dto.Login, ct);
        if (existing is not null)
            throw new Exception($"User {dto.Login} already registered");

        var user = new User
        {
            Login = dto.Login,
            Email = dto.Email,
            Name = dto.Name,
            Role = UserRole.User,
            CreatedAt = DateTime.UtcNow
        };
        
        user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);
        
        await repository.CreateAsync(user, ct);
        
        return user;
    }

    public async Task<AuthResultDto> AuthenticateAsync(LoginDto dto, CancellationToken ct = default)
    {
        var user = await repository.GetByLoginAsync(dto.Login, ct);
        if (user is null)
            throw new Exception("Invalid login or password");
        
        var verify = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
        if (verify == PasswordVerificationResult.Failed)
            throw new Exception("Invalid login or password");
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_authOptions.Secret);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Login),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_authOptions.ExpirationMinutes),
            Issuer = _authOptions.Issuer,
            Audience = _authOptions.Audience,
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString =  tokenHandler.WriteToken(token);

        return new AuthResultDto
        {
            Token = tokenString,
            ExpiresAt = tokenDescriptor.Expires!.Value,
        };
    }
}