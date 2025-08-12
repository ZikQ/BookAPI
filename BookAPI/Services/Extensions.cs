using BookAPI.Services.Interfaces;

namespace BookAPI.Services;

public static class Extensions
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IReviewService, ReviewService>();
        
        return services;
    }
}