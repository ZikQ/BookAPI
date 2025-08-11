using BookAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookAPI;

public class AppContext(DbContextOptions<AppContext> options) : DbContext(options)
{
    public DbSet<Book> Books { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(builder =>
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(15);
            builder.Property(x => x.Email).HasMaxLength(15);
            builder.Property(x => x.Password).HasMaxLength(20).IsRequired();
            builder.Property(x => x.Login).HasMaxLength(20).IsRequired();
            builder.Property(x=> x.Role).IsRequired();
        });

        modelBuilder.Entity<Book>(builder =>
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Author).IsRequired();
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x=>x.Genre).IsRequired();
            builder.Property(x=>x.Created).IsRequired();
            builder.Property(x=>x.PublicationYear).IsRequired();
        });
        
        base.OnModelCreating(modelBuilder);
    }
}