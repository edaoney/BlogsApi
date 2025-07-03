using BlogApi.Models.DTOs.Comment;

namespace BlogApi.Models.DTOs.Blog;

public class BlogStatusList
{
    public string AuthorName { get; set; }
    public required string Title { get; set; }
    public required string Summary { get; set; }
    public required string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}