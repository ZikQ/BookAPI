using BookAPI.DTOs;
using BookAPI.Models;

namespace BookAPI.Services.Interfaces;

public interface IReviewService
{
    Task AddReviewAsync(int userId, CreateReviewDto dto, CancellationToken ct = default);
    Task<IEnumerable<Review>> GetReviewsByBookAsync(int bookId, CancellationToken ct = default);
}