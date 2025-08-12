using BookAPI.DTOs;
using BookAPI.Models;

namespace BookAPI.Services;

public interface IUserService
{
    Task<User> GetByIdAsync(int id, CancellationToken ct = default);
    
    Task<User> RegisterAsync(CreateUserDto dto, CancellationToken ct = default);
    
    Task<AuthResultDto> AuthenticateAsync(LoginDto dto, CancellationToken ct = default);
}