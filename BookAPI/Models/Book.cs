namespace BookAPI.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
    public string Genre { get; set; } = null!;
    public int PublicationYear { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}