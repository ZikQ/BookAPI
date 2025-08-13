namespace BookAPI.Services.Interfaces;

public interface IFileService
{
    Task<string> UploadCoverAsync(int bookId, IFormFile file, CancellationToken ct = default);
    Task<string> UploadPdfAsync(int bookId, IFormFile file, CancellationToken ct = default);
}