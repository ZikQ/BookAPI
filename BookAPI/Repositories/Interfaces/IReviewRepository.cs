using BookAPI.Models;

namespace BookAPI.Repositories.Interfaces;

public interface IReviewRepository
{
    Task CreateAsync(Review review, CancellationToken cancellationToken = default);
    Task<Review> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Review>> GetByBookIdAsync(int bookId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Review>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default);
    Task UpdateAsync(Review review, CancellationToken cancellationToken = default);
    Task DeleteAsync(Review review, CancellationToken cancellationToken = default);
}