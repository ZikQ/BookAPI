using System.Security.Claims;
using BookAPI.DTOs;
using BookAPI.Models;
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
    public async Task<IActionResult> CreateReviewAsync([FromBody] CreateReviewDto dto, CancellationToken cancellationToken = default)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        
        await service.AddReviewAsync(userId, dto, cancellationToken);
        return NoContent();
    }
    
    [HttpGet("book/{bookId:int}")]
    public async Task<IActionResult> GetReviews(int bookId, CancellationToken ct)
    {
        var reviews = await service.GetReviewsByBookAsync(bookId, ct);
        return Ok(reviews);
    }
}