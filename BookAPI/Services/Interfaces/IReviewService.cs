using BookAPI.DTOs;
using BookAPI.Models;

namespace BookAPI.Services.Interfaces;

public interface IReviewService
{
    Task AddReviewAsync(int userId, CreateReviewDto dto, CancellationToken ct = default);
    Task<IEnumerable<GetReviewDto>> GetReviewsByBookAsync(int bookId, CancellationToken ct = default);
    Task<IEnumerable<GetReviewDto>> GetReviewsByUserAsync(int userId, CancellationToken ct = default);
    Task PatchAsync(int id, UpdateReviewPartialDto update, CancellationToken ct = default);
    Task DeleteAsync(int id, CancellationToken ct = default);
}