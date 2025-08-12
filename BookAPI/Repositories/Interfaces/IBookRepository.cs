using BookAPI.Models;

namespace BookAPI.Repositories.Interfaces;

public interface IBookRepository
{
    Task CreateAsync(Book book, CancellationToken ct);
    Task<Book?> GetByIdAsync(int id, CancellationToken ct);
    Task<List<Book>> GetAllAsync(CancellationToken ct);
    Task UpdateAsync(Book book, CancellationToken ct);
    Task DeleteAsync(Book book, CancellationToken ct);
    IQueryable<Book> Query();
}