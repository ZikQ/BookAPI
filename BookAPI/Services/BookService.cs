using BookAPI.DTOs;
using BookAPI.Models;
using BookAPI.Repositories;
using BookAPI.Repositories.Interfaces;
using BookAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            throw new NotFoundException("Book not found");

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
    
    public async Task PatchAsync(int id, UpdateBookPartialDto update, CancellationToken ct = default)
    {
        var book = await GetByIdAsync(id, ct);

        if (update.Title is not null) book.Title = update.Title;
        if (update.Author is not null) book.Author = update.Author;
        if (update.Genre is not null) book.Genre = update.Genre;
        if (update.PublicationYear.HasValue) book.PublicationYear = update.PublicationYear.Value;

        book.Updated = DateTime.UtcNow;

        await repository.UpdateAsync(book, ct);
    }

    public async Task<PagedResult<Book>> GetAllAsync(BookQueryParameters parameters, CancellationToken ct = default)
    {
        var query = repository.Query();

        if (!string.IsNullOrEmpty(parameters.Search))
        {
            var search = parameters.Search.Trim().ToLower();

            query = query.Where(x =>
                x.Title.ToLower().Contains(search) ||
                x.Author.ToLower().Contains(search) ||
                (x.Genre != null && x.Genre.ToLower().Contains(search))
            );
        }
        
        query = parameters?.Sort?.ToLower() switch
        {
            "title_asc" => query.OrderBy(x => x.Title),
            "title_desc" => query.OrderByDescending(x => x.Title),
            "year_asc" => query.OrderBy(x => x.PublicationYear),
            "year_desc" => query.OrderByDescending(x => x.PublicationYear),

            _ => query.OrderBy(x => x.Id),
        };

        var totalItems = await query.CountAsync(ct);
        
        var items = await query
            .Skip((parameters.Page - 1) * parameters.PageSize)
            .Take(parameters.PageSize)
            .ToListAsync(ct);

        return new PagedResult<Book>
        {
            Items = items,
            TotalItems = totalItems,
            Page = parameters.Page,
            PageSize = parameters.PageSize,
        };
    }
}