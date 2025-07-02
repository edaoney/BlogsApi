using BlogApi.Models.DTOs.Report;

namespace BlogApi.Models.DTOs.Comment;

public class CommentDto
{
    public string AuthorName { get; set; }
    public string Content { get; set; } // yorum 
    public List<ReportCommentDto> ReportComments { get; set; }
    public DateTime CreatedAt { get; set; }
}