using BookAPI.Models;

namespace BookAPI.Repositories.Interfaces;

public interface IReviewRepository
{
    Task<Review> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task CreateAsync(Review review, CancellationToken cancellationToken = default);
    Task<IEnumerable<Review>> GetByBookIdAsync(int bookId, CancellationToken cancellationToken = default);
}