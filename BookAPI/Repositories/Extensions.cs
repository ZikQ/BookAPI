using Microsoft.EntityFrameworkCore;

namespace BookAPI.Repositories;

public static class Extensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        
        services.AddDbContext<AppContext>(x =>
        {
            x.UseNpgsql("Host=localhost;Database=books;Username=postgres;Password=admin");
        });
        
        return services;
    }
}