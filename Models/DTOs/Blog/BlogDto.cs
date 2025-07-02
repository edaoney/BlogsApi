using BlogApi.Models.DTOs.Comment;

namespace BlogApi.Models.DTOs.Blog;

public class BlogDto
{
    public required string Title { get; set; } // başlık
    public required string Summary { get; set; }// özet
    public required string Content { get; set; }// içerik
    public required string Status { get; set; }
    public List<CommentBlogDto> Comments { get; set; }
    public DateTime CreatedAt { get; set; }= DateTime.Now;
    public DateTime UpdatedAt { get; set; }
}