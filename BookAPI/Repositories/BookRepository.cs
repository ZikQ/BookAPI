using BookAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.Repositories;

public class BookRepository(AppContext context) : IBookRepository
{
    public async Task CreateAsync(Book book, CancellationToken ct=default)
    {
        book.Created = DateTime.UtcNow;
        
        await context.Books.AddAsync(book, ct);
        await context.SaveChangesAsync(ct);
    }

    public async Task<Book?> GetByIdAsync(int id, CancellationToken ct=default)
    {
        return await context.Books.FirstOrDefaultAsync(x=>x.Id == id, ct);
    }

    public async Task<List<Book>> GetAllAsync(CancellationToken ct=default)
    {
        return await context.Books.ToListAsync(ct);
    }

    public async Task UpdateAsync(Book book, CancellationToken ct=default)
    {
        context.Books.Update(book);
        await context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Book book, CancellationToken ct=default)
    {
        context.Books.Remove(book);
        await context.SaveChangesAsync(ct);
    }
}