using BookAPI.DTOs;
using BookAPI.Models;
using BookAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookAPI.Controllers;

[ApiController]
[Route("api/books")]
public class BookController(IBookService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBookDto book)
    {
        await service.CreateAsync(book);
        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetBookAsync([FromHeader] int id)
    {
        var book = await service.GetByIdAsync(id);
        return Ok(book);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync([FromHeader] int id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromHeader] int id, [FromBody] CreateBookDto update)
    {
        await service.UpdateAsync(id, update);
        return NoContent();
    }
}