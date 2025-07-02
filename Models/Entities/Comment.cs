using Microsoft.AspNetCore.Identity;

namespace BlogApi.Models.Entities;

public class Comment
{
    public int Id { get; set; }
    public string Content { get; set; } // yorum 
    public string AuthorId { get; set; } // yorumu yapan kişi
    public IdentityUser Author { get; set; }
    public int BlogId { get; set; }// blog yazısı ıd
    public Blog Blog { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime Updated { get; set; } 
    
   
    

}