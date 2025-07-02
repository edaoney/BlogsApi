using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BlogApi.Models.Entities;

public class Blog
{
    public int Id { get; set; }
    [Required, MaxLength(50)] 
    public string Title { get; set; } // başlık
    [Required, MaxLength(150)] 
    public string Summary { get; set; } // özet
    [Required, MaxLength(500)] 
    public string Content { get; set; } // içerik
    public string UserId { get; set; }
    public IdentityUser User { get; set; }
    public ICollection<Comment> Comments { get; set; }
    public BlogStatus Status { get; set; } = BlogStatus.Pending;
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime Updated { get; set; }
}
    
   