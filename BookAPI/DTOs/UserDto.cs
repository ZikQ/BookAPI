namespace BookAPI.DTOs;

public class CreateUserDto
{
    public string Login { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class LoginDto
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class AuthResultDto
{
    public string Token { get; set; } = null!;
    public DateTime ExpiresAt { get; set; }
}