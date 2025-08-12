using BookAPI.DTOs;
using BookAPI.Models;

namespace BookAPI.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<User?> GetByLoginAsync(string login, CancellationToken ct = default);
    Task CreateAsync(User user, CancellationToken ct = default);
}