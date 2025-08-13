using BookAPI.Models;

namespace BookAPI.Extensions;


public static class AuthorizationPolicies
{
    public const string Admin = "Admin";
    public const string AuthorOrAdmin = "AuthorOrAdmin";
    public const string LibrarianOrAdmin = "LibrarianOrAdmin";
    public static void AddPolices(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy =>
            {
                policy.RequireRole(UserRole.Admin.ToString());
            });
            
            options.AddPolicy("AuthorOrAdmin", policy =>
            {
                policy.RequireAssertion(context =>
                {
                    var userId = int.Parse(context.User.FindFirst("sub")?.Value);
                    var review = context.Resource as Review;
                    
                    return review != null && (review.UserId == userId || context.User.IsInRole(UserRole.Admin.ToString()));
                });
            });
            
            options.AddPolicy("LibrarianOrAdmin", policy =>
            {
                policy.RequireRole(UserRole.Librarian.ToString());
            });
        });
    }
}
