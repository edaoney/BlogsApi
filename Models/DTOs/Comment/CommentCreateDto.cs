namespace BlogApi.Models.DTOs.Comment;

public class CommentCreateDto
{
    public string Content { get; set; } // yorum 
    
    public DateTime Created { get; set; } = DateTime.Now;
}