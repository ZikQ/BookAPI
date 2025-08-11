using BookAPI.DTOs;
using BookAPI.Models;

namespace BookAPI.Services;

public interface IBookService
{
    Task CreateAsync(CreateBookDto book, CancellationToken ct=default);
    Task<Book> GetByIdAsync(int id, CancellationToken ct=default);
    Task DeleteAsync(int id, CancellationToken ct=default);
    Task UpdateAsync(int id, CreateBookDto update, CancellationToken ct = default);
}