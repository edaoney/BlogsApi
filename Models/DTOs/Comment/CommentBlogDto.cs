namespace BlogApi.Models.DTOs.Comment;

public class CommentBlogDto
{
    public string AuthorName { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }= DateTime.Now;
}