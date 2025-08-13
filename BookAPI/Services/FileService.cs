using BookAPI.Services.Interfaces;

namespace BookAPI.Services;

public class FileService(IWebHostEnvironment environment) : IFileService
{
    public async Task<string> UploadCoverAsync(int bookId, IFormFile file, CancellationToken ct = default)
    {
        return await UploadFileAsync(bookId, file, "covers", new[] { ".jpg", ".png", ".jpeg" }, 5 * 1024 * 1024, ct);
    }

    public async Task<string> UploadPdfAsync(int bookId, IFormFile file, CancellationToken ct = default)
    {
        return await UploadFileAsync(bookId, file, "pdfs", new[] { ".pdf" }, 50 * 1024 * 1024, ct);
    }
    
    private async Task<string> UploadFileAsync(int bookId, 
        IFormFile file, 
        string folder, 
        string[] allowedExtensions, 
        long maxSizeBytes, 
        CancellationToken ct)
    {
        if (file is null || file.Length == 0)
            throw new ArgumentException("File is empty");
        
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!allowedExtensions.Contains(extension))
            throw new ArgumentException($"File type {extension} is not supported");
        
        if (file.Length > maxSizeBytes)
            throw new ArgumentException($"File too large");
        
        var fileName = $"{bookId}_{Guid.NewGuid()}{extension}";
        var folderPath = Path.Combine(environment.WebRootPath, folder);
        
        var filePath = Path.Combine(folderPath, fileName);
        
        await using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream, ct);
        }

        return $"/{folder}/{fileName}";
    }
    
}