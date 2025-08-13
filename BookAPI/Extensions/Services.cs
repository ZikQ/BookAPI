using BookAPI.Services;
using BookAPI.Services.Interfaces;

namespace BookAPI.Extensions;

public static class Services
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<IBookService, BookService>(); 
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IReviewService, ReviewService>();
        
        return services;
    }
}