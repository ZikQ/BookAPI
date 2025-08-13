using BookAPI.DTOs;
using BookAPI.Extensions;
using BookAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController(IBookService service) : ControllerBase
{
    [Authorize(Policy = AuthorizationPolicies.LibrarianOrAdmin)]
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateBookDto book)
    {
        await service.CreateAsync(book);
        return NoContent();
    }
    
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] BookQueryParameters parameters)
    {
        var result = await service.GetAllAsync(parameters);
        
        return Ok(result);
    }
    
    [AllowAnonymous]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetBookAsync([FromRoute] int id)
    {
        var book = await service.GetByIdAsync(id);
        return Ok(book);
    }
    
    [Authorize(Policy = AuthorizationPolicies.LibrarianOrAdmin)]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] CreateBookDto update)
    {
        await service.UpdateAsync(id, update);
        return NoContent();
    }
    
    [Authorize(Policy = AuthorizationPolicies.LibrarianOrAdmin)]
    [HttpPatch("{id:int}")]
    public async Task<IActionResult> PatchAsync([FromRoute] int id, [FromBody] UpdateBookPartialDto update)
    {
        await service.PatchAsync(id, update);
        return NoContent();
    }
    
    [Authorize(Policy = AuthorizationPolicies.Admin)]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }
    
    [HttpPost("{id}/upload-cover")]
    [Authorize(Policy = AuthorizationPolicies.LibrarianOrAdmin)]
    public async Task<IActionResult> UploadCover([FromRoute] int id, IFormFile file, CancellationToken ct = default)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file provided");

        await service.UploadCoverAsync(id, file, ct);
        return NoContent();
    }

    [HttpPost("{id}/upload-pdf")]
    [Authorize(Policy = AuthorizationPolicies.LibrarianOrAdmin)]
    public async Task<IActionResult> UploadPdf([FromRoute] int id, IFormFile file, CancellationToken ct = default)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file provided");

        await service.UploadPdfAsync(id, file, ct);
        return NoContent();
    }
}