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

    public async Task<Book> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var book = await repository.GetByIdAsync(id, ct);
        
        if (book is null)
            throw new Exception("Book not found");

        return book;
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var book = await GetByIdAsync(id, ct);
        await repository.DeleteAsync(book, ct);
    }

    public async Task UpdateAsync(int id, CreateBookDto update, CancellationToken ct = default)
    {
        var book = await GetByIdAsync(id, ct);
        
        book.Title = update.Title;
        book.Author = update.Author;
        book.Genre = update.Genre;
        book.PublicationYear = update.PublicationYear;
        book.Updated = DateTime.UtcNow;
        
        await repository.UpdateAsync(book, ct);
        
    }

}