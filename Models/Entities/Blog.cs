using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BlogApi.Models.Entities;

public class Blog
{
    public int Id { get; set; }
    [Required, MaxLength(50)]
    public string Title { get; set; }
    [Required, MaxLength(100)]
    public string Summary { get; set; }
    [Required, MaxLength(350)]
    public string Content { get; set; }
    public string UserId { get; set; }
    public IdentityUser User { get; set; }
    public ICollection<Comment> Comments { get; set; }
    public BlogStatus Status { get; set; } = BlogStatus.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; }
}