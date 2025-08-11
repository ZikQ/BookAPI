using BookAPI.DTOs;
using BookAPI.Models;
using BookAPI.Repositories;

namespace BookAPI.Services;

public class BookService(IBookRepository repository) : IBookService
{
    public async Task CreateAsync(CreateBookDto bookdto, CancellationToken ct=default)
    {
        var book = new Book
        {
            Title = bookdto.Title,
            Author = bookdto.Author,
            Genre = bookdto.Genre,
            PublicationYear = bookdto.PublicationYear,
            Created = DateTime.UtcNow
        };
        
        await repository.CreateAsync(book, ct);
    }
}