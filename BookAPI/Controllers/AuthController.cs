using BookAPI.DTOs;
using BookAPI.Models;
using BookAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IUserService service) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] CreateUserDto dto, CancellationToken ct)
    {
        try
        {
            var user = await service.RegisterAsync(dto, ct);

            return CreatedAtAction(nameof(RegisterAsync), new { id = user.Id },
                new { user.Id, user.Login, user.Email });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginDto dto, CancellationToken ct)
    {
        try
        {
            var auth = await service.AuthenticateAsync(dto, ct);
            return Ok(auth);
        }
        catch (Exception e)
        {
            return Unauthorized(new { message = e.Message });
        }
    }
}