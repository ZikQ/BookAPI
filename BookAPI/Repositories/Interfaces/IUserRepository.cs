using BookAPI.DTOs;
using BookAPI.Models;

namespace BookAPI.Repositories.Interfaces;

public interface IUserRepository
{
    Task CreateAsync(User user, CancellationToken ct = default);
    Task<User?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<User?> GetByLoginAsync(string login, CancellationToken ct = default);
    Task UpdateAsync(User user, CancellationToken ct = default);
    Task DeleteAsync(User user, CancellationToken ct = default);
    
}