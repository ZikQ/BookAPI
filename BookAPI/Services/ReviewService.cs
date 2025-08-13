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

    public async Task<IEnumerable<GetReviewDto>> GetReviewsByBookAsync(int bookId, CancellationToken ct = default)
    {
        var reviews = await repository.GetByBookIdAsync(bookId, ct);

        if (reviews is null || !reviews.Any())
            return Enumerable.Empty<GetReviewDto>();
        
        return reviews.Select(r => new GetReviewDto
        {
            UserId = r.UserId,
            BookId = r.BookId,
            Rating = r.Rating,
            Created = r.Created,
            Comment = r.Comment
        });
    }

    public async Task<IEnumerable<GetReviewDto>> GetReviewsByUserAsync(int userId, CancellationToken ct = default)
    {
        var reviews = await repository.GetByUserIdAsync(userId, ct);

        if (reviews is null || !reviews.Any())
            return Enumerable.Empty<GetReviewDto>();
        
        return reviews.Select(r => new GetReviewDto
        {
            UserId = r.UserId,
            BookId = r.BookId,
            Rating = r.Rating,
            Created = r.Created,
            Comment = r.Comment
        });
    }

    public async Task PatchAsync(int id, UpdateReviewPartialDto update, CancellationToken ct = default)
    {
        var review = await repository.GetByIdAsync(id, ct);

        if (review is null)
        {  
            throw new NotFoundException($"Review with id {id} not found");
        }

        if (update.Comment is not null) review.Comment = update.Comment;
        if (update.Rating.HasValue) review.Rating = update.Rating.Value;

        review.Updated = DateTime.UtcNow;

        await repository.UpdateAsync(review, ct);
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var review = await repository.GetByIdAsync(id, ct);

        if (review is null)
        {
            throw new NotFoundException($"Review with id {id} not found");
        }

        await repository.DeleteAsync(review, ct);
    }
}