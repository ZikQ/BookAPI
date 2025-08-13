using System.Security.Claims;
using BookAPI.DTOs;
using BookAPI.Extensions;
using BookAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewController(IReviewService service) : ControllerBase
{
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateReviewAsync([FromBody] CreateReviewDto dto, 
        CancellationToken cancellationToken = default)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        
        await service.AddReviewAsync(userId, dto, cancellationToken);
        return NoContent();
    }
    
    [AllowAnonymous]
    [HttpGet("book/{bookId:int}")]
    public async Task<IActionResult> GetReviewsByBook([FromRoute] int bookId, CancellationToken ct)
    {
        var reviews = await service.GetReviewsByBookAsync(bookId, ct);
        return Ok(reviews);
    }
    
    [AllowAnonymous]
    [HttpGet("user/{userId:int}")]
    public async Task<IActionResult> GetReviewsByUser([FromRoute] int userId, CancellationToken ct)
    {
        var reviews = await service.GetReviewsByUserAsync(userId, ct);
        return Ok(reviews);
    }
    
    [Authorize(Policy = AuthorizationPolicies.AuthorOrAdmin)]
    [HttpPatch("{id:int}")]
    public async Task<IActionResult> PatchAsync([FromRoute] int id, [FromBody] UpdateReviewPartialDto dto,
        CancellationToken ct)
    {
        await service.PatchAsync(id, dto, ct);
        return NoContent();
    }

    [Authorize(Policy = AuthorizationPolicies.Admin)]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id, CancellationToken ct)
    {
        await service.DeleteAsync(id, ct);
        return NoContent();
    }
}