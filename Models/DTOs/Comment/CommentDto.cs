using BlogApi.Models.DTOs.Report;

namespace BlogApi.Models.DTOs.Comment;

public class CommentDto
{
    public string AuthorName { get; set; } // yorumu yazan kişi
    public string Content { get; set; } // yorum 
    public List<ReportCommentDto> ReportComments { get; set; } //  birden fazla raporlanmış yorumu tutan listedir.
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}