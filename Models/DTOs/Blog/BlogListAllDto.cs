using BlogApi.Models.DTOs.Comment;

namespace BlogApi.Models.DTOs.Blog;

public class BlogListAllDto
{
    public string AuthorName { get; set; }
    public string Title { get; set; }
    public string Summary { get; set; }
    public string Content { get; set; }
    public string Status { get; set; }
    public List<CommentBlogDto> Comments { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
