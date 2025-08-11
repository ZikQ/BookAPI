using BookAPI.DTOs;

namespace BookAPI.Services;

public interface IBookService
{
    Task CreateAsync(CreateBookDto book, CancellationToken ct=default);
}