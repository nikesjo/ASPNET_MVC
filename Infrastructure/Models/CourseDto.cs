namespace Infrastructure.Models;

public class CourseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
    public string OriginalPrice { get; set; } = null!;
    public string? DiscountPrice { get; set; }
    public int Hours { get; set; }
    public bool IsBestSeller { get; set; }
    public string? LikesInProcent { get; set; }
    public string? LikesInNumbers { get; set; }
    public string? ImageUrl { get; set; }
    public string? AuthorImageUrl { get; set; }
    public string Category { get; set; } = null!;
}
