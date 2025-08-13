namespace BookAPI.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
    public string Genre { get; set; } = null!;
    public int PublicationYear { get; set; }
    
    public string? CoverPath { get; set; } = null!;
    public string? PdfPath { get; set; } = null!;
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public double AverageRating => Reviews.Any() ? Reviews.Average(r => r.Rating) : 0;
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}