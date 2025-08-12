using BookAPI.DTOs;
using BookAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService service) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] CreateUserDto dto, CancellationToken ct)
    {
        try
        {
            var user = await service.RegisterAsync(dto, ct);

            return CreatedAtAction(nameof(GetUserAsync), new { id = user.Id }, 
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
    
    [AllowAnonymous]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUserAsync([FromRoute] int id)
    {
        var user = await service.GetByIdAsync(id);
        return Ok(user);
    }
}