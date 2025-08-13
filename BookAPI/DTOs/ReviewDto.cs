using System.ComponentModel.DataAnnotations;

namespace BookAPI.DTOs;

public class GetReviewDto
{
    public int BookId { get; set; }
    
    public int UserId { get; set; }
    
    public int Rating { get; set; }
    public string Comment { get; set; } = null!;
    
    public DateTime Created { get; set; }
}

public class CreateReviewDto
{
    [Required]
    public int BookId { get; set; }
    [Required]
    [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5")]
    public int Rating { get; set; }
    [StringLength(maximumLength: 1000, ErrorMessage = "Comment must be less than 1000 characters")]
    public string Comment { get; set; } = null!;
}

public class UpdateReviewPartialDto
{
    [Required]
    [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5")]
    public int? Rating { get; set; }
    [StringLength(maximumLength: 1000, ErrorMessage = "Comment must be less than 1000 characters")]
    public string? Comment { get; set; }
}
