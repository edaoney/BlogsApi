using Microsoft.AspNetCore.Identity;

namespace BlogApi.Models.Entities;

public class Report
{
    public int Id { get; set; }
    public string ReporterId { get; set; }
    public IdentityUser Reporter { get; set; }
    public string Reason { get; set; }
    public int CommentId { get; set; }
    public Comment Comment { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; }
}