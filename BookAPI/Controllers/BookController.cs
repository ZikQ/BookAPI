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
}