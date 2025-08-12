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
}