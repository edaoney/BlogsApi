using BlogApi.Data;
using BlogApi.Models;
using BlogApi.Models.DTOs.Blog;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Controllers;

[ApiController]
[Route("Moderator")]
[Authorize(Roles = "Admin, Moderator")]
public class ModeratorController(AppDbContext context) : ControllerBase
{
    [HttpPut]
    [Route("{id:int:min(1)}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async  Task<IActionResult> SetStatusBlog(int id, BlogStatus status)
    {
        var blogs = await context.Blogs
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();
        if (blogs == null)
        {
            return BadRequest(new { msg = "Blog not found." });
        }
        blogs.Status = status;
        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete]
    [Route("deletecomment/{id:int:min(1)}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteComment(int id)
    {
        var comment = await context.Comments.FindAsync(id);
        if (comment == null)
        {
            return NotFound();
        }
        
        context.Comments.Remove(comment);
        await context.SaveChangesAsync();
        return Ok(new { msg = "Comment deleted." });
    }

    [HttpGet]
    [Route("posts/")]
    [ProducesResponseType<BlogDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAuthorBlogs(string id)
    {
        var blogs = await context.Blogs
            .Include(x => x.User)
            .Include(x => x.Comments)
            .ThenInclude(x => x.Author)
            .Where(x => x.UserId == id)
            .ToListAsync();
        
        return Ok(blogs.Adapt<BlogDto[]>());
    }

    [HttpGet]
    [Route("blogs/all")]
    [ProducesResponseType<BlogDto[]>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetStatusBlogs(string filter)
    {
        if (!Enum.TryParse<BlogStatus>(filter, true, out var parsedStatus))
        {
            return BadRequest(new { msg = "Invalid status value." });
        }
        
        var blogs = await context.Blogs
            .Include(x => x.User)
            .Include(x => x.Comments)
            .ThenInclude(x => x.Author)
            .Where(x => x.Status == parsedStatus)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
        
        return Ok(blogs.Adapt<BlogStatusList[]>());
    }
}