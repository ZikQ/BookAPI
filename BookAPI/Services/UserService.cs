using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookAPI.DTOs;
using BookAPI.Models;
using BookAPI.Repositories;
using BookAPI.Repositories.Interfaces;
using BookAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BookAPI.Services;

public class UserService(IUserRepository repository, IOptions<AuthOptions> authOptions, ITokenService tokenService)
    : IUserService
{
    private readonly AuthOptions _authOptions = authOptions.Value;
    
    private readonly PasswordHasher<User> _passwordHasher = new();

    public async Task<GetUserDto> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var user = await repository.GetByIdAsync(id, ct);
        
        if (user is null)
            throw new NotFoundException($"User {id} not found");

        var userDto = new GetUserDto
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.Name,
            Role = user.Role
        };
        
        return userDto;
    }

    public async Task<User> RegisterAsync(CreateUserDto dto, CancellationToken ct = default)
    {
        var existing = await repository.GetByLoginAsync(dto.Login, ct);
        if (existing is not null)
            throw new AlreadyRegisteredException($"User {dto.Login} already registered");

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
            throw new ConflictException("Invalid login or password");
        
        var verify = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
        if (verify == PasswordVerificationResult.Failed)
            throw new ConflictException("Invalid login or password");
        
        var (tokenString, tokenDescriptor) = tokenService.GenerateToken(user);

        return new AuthResultDto
        {
            Token = tokenString,
            ExpiresAt = tokenDescriptor.Expires!.Value,
        };
    }
}