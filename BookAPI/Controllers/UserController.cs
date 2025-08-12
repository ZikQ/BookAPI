using BookAPI.DTOs;
using BookAPI.Models;
using BookAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService service) : ControllerBase
{
    [AllowAnonymous]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUserAsync([FromRoute] int id)
    {
        var user = await service.GetByIdAsync(id);
        return Ok(user);
    }
}