using BookAPI.Models;
using BookAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.Repositories;

public class BookRepository(AppDbContext dbContext) : IBookRepository
{
    public async Task CreateAsync(Book book, CancellationToken ct=default)
    {
        book.Created = DateTime.UtcNow;
        
        await dbContext.Books.AddAsync(book, ct);
        await dbContext.SaveChangesAsync(ct);
    }
    public IQueryable<Book> Query()
    {
        return dbContext.Books.AsQueryable();
    }
    
    public async Task<Book?> GetByIdAsync(int id, CancellationToken ct=default)
    {
        return await dbContext.Books.FirstOrDefaultAsync(x=>x.Id == id, ct);
    }

    public async Task<List<Book>> GetAllAsync(CancellationToken ct=default)
    {
        return await dbContext.Books.ToListAsync(ct);
    }

    public async Task UpdateAsync(Book book, CancellationToken ct=default)
    {
        dbContext.Books.Update(book);
        await dbContext.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Book book, CancellationToken ct=default)
    {
        dbContext.Books.Remove(book);
        await dbContext.SaveChangesAsync(ct);
    }
}