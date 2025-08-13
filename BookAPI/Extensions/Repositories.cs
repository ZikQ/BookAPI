using BookAPI.Repositories;
using BookAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.Extensions;

public static class Repositories
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        
        services.AddDbContext<AppDbContext>(x =>
        {
            x.UseNpgsql("Host=localhost;Database=books;Username=postgres;Password=admin");
        });
        
        return services;
    }
}