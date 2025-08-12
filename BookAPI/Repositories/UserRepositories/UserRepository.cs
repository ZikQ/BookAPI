using BookAPI.DTOs;
using BookAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.Repositories;

public class UserRepository(AppContext context) : IUserRepository
{
    public async Task<User?> GetByLoginAsync(string login, CancellationToken ct = default) =>
        await context.Users.FirstOrDefaultAsync(u => u.Login == login, ct);

    public async Task CreateAsync(User user, CancellationToken ct = default)
    {
        await context.Users.AddAsync(user, ct);
        await context.SaveChangesAsync(ct);
    }

    public async Task<User?> GetByIdAsync(int id, CancellationToken ct = default) =>
        await context.Users.FindAsync(new object[]{ id }, ct);
}