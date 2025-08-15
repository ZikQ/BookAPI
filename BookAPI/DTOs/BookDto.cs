using System.ComponentModel.DataAnnotations;

namespace BookAPI.DTOs;

public class CreateBookDto
{
    [Required]
    public string Title { get; set; } = null!;
    
    [Required]
    public string Author { get; set; } = null!;
    
    [Required]
    public string Genre { get; set; } = null!;
    
    public string PdfPath { get; set; } = null!;
    public string CoverPath { get; set; } = null!;
    
    [Required]
    public int PublicationYear { get; set; }
}

public class UpdateBookPartialDto
{
    public string? Title { get; set; }
    public string? Author { get; set; }
    public string? Genre { get; set; }
    public int? PublicationYear { get; set; }
}

public class BookQueryParameters
{
    public string? Search { get; set; }
    public string? Sort { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class PagedResult<T>
{
    public IEnumerable<T> Items { get; set; } = [];
    public int TotalItems { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}