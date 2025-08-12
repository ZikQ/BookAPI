namespace BookAPI.Models;

public class Review
{
    public int Id { get; set; }
    
    public int BookId { get; set; }
    public Book Book { get; set; } = null!;
    
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    
    public int Rating { get; set; }
    public string Comment { get; set; } = null!;
    
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}