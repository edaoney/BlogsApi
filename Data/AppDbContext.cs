using BlogApi.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Data;

public class  AppDbContext (DbContextOptions<AppDbContext> options) : IdentityDbContext<IdentityUser>(options)
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Report> Reports { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); // Identity tabanlı yapı varsa bunu koru

        // Yabancı anahtar ilişkisi burada tanımlanır
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Blog)
            .WithMany(b => b.Comments)
            .HasForeignKey(c => c.BlogId)
            .OnDelete(DeleteBehavior.Restrict);
    }
    
    
}

