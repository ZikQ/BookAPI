using BookAPI.DTOs;
using BookAPI.Models;
using BookAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.Repositories;

public class UserRepository(AppDbContext dbContext) : IUserRepository
{
    public async Task<User?> GetByLoginAsync(string login, CancellationToken ct = default) =>
        await dbContext.Users.FirstOrDefaultAsync(u => u.Login == login, ct);

    public async Task CreateAsync(User user, CancellationToken ct = default)
    {
        await dbContext.Users.AddAsync(user, ct);
        await dbContext.SaveChangesAsync(ct);
    }

    public async Task<User?> GetByIdAsync(int id, CancellationToken ct = default) =>
        await dbContext.Users.FindAsync(new object[]{ id }, ct);
}