using System.ComponentModel.DataAnnotations;
using BookAPI.Models;

namespace BookAPI.DTOs;

public class CreateUserDto
{
    [Required]
    [StringLength(maximumLength: 20, ErrorMessage = "Login must be less than 20 characters")]
    public string Login { get; set; } = null!;
    [EmailAddress]
    public string Email { get; set; } = null!;
    [StringLength(maximumLength: 20, ErrorMessage = "Name must be less than 20 characters")]
    public string Name { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
}

public class GetUserDto
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public string Name { get; set; } = null!;
    public UserRole Role { get; set; }
}

public class LoginDto
{
    [Required]
    [StringLength(maximumLength: 20, ErrorMessage = "Login must be less than 20 characters")]
    public string Login { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
}

public class AuthResultDto
{
    public string Token { get; set; } = null!;
    public DateTime ExpiresAt { get; set; }
}