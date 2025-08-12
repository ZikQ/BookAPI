using BookAPI.DTOs;
using BookAPI.Models;
using BookAPI.Repositories.Interfaces;
using BookAPI.Services.Interfaces;

namespace BookAPI.Services;

public class ReviewService(IReviewRepository repository) : IReviewService 
{
    public async Task AddReviewAsync(int userId, CreateReviewDto dto, CancellationToken ct = default)
    {
        var review = new Review
        {
            BookId = dto.BookId,
            UserId = userId,
            Rating = dto.Rating,
            Comment = dto.Comment,
            Created = DateTime.UtcNow
        };

        await repository.CreateAsync(review, ct);
    }

    public async Task<IEnumerable<Review>> GetReviewsByBookAsync(int bookId, CancellationToken ct = default)
    {
        return await repository.GetByBookIdAsync(bookId, ct);
    }
}