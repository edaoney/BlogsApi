using System.ComponentModel.DataAnnotations;

namespace BlogApi.Models.DTOs.Blog;

public class BlogCreateDto
{
    [Required, MaxLength(50)]
    public required string Title { get; set; } // başlık
    [Required, MaxLength(100)]
    public required string Summary { get; set; }// özet 
    [Required, MaxLength(500)]
    public required string Content { get; set; }// içerik
    public DateTime Created { get; set; }= DateTime.Now;
}