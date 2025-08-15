using BookAPI.Models;
using BookAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.Repositories;

public class ReviewRepository(AppDbContext dbContext) : IReviewRepository
{
    public async Task<Review?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await dbContext.Reviews.FirstOrDefaultAsync(x=>x.Id == id, ct);
    }

    public async Task CreateAsync(Review review, CancellationToken cancellationToken = default)
    {
        review.Created = DateTime.UtcNow;
        
        dbContext.Reviews.Add(review);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Review>> GetByBookIdAsync(int bookId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Reviews
            .Where(x => x.BookId == bookId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Review>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Reviews
            .Where(x => x.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(Review review, CancellationToken cancellationToken = default)
    {
        dbContext.Reviews.Update(review);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Review review, CancellationToken cancellationToken = default)
    {
        dbContext.Reviews.Remove(review);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public IQueryable<Review> Query()
    {
        return dbContext.Reviews.AsQueryable();
    }
}