namespace BookAPI.Models;

public enum UserRole
{
    User,
    Librarian,
    Admin
}

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Login { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public DateTime CreatedAt { get; set; }

    public UserRole Role { get; set; } = UserRole.User;
}