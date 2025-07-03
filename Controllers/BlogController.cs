using BlogApi.Data;
using BlogApi.Models;
using BlogApi.Models.DTOs.Blog;
using BlogApi.Models.Entities;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BlogApi.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class BlogController(AppDbContext context, UserManager<IdentityUser> userManager) : ControllerBase
{
    [HttpPost]
    [Route("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddBlog(BlogCreateDto dto)
    {
        var userId = userManager.GetUserId(User);
        if (userId == null)
        {
            var user = await userManager.FindByIdAsync(userId);
            var newBlog = dto.Adapt<Blog>();
            newBlog.UserId = userId;
            if (user != null && (await userManager.IsInRoleAsync(user, "Admin") ||
                                 await userManager.IsInRoleAsync(user, "Moderator")))
            {
                newBlog.Status = BlogStatus.Approved;
            }

            await context.Blogs.AddAsync(newBlog);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(ViewBlog), new { id = newBlog.Id }, newBlog.Adapt<BlogDto>());
        }

        return BadRequest(new { msg = "User Not Found" });
    }


    [HttpGet]
    [Route("{id:int:min(1)}")]
    [ProducesResponseType<BlogDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ViewBlog(int id)
    {
        var userId = userManager.GetUserId(User);
        var blog = await context.Blogs
            .Include(x => x.Comments)
            .ThenInclude(x => x.Author)
            .Where(x => x.UserId == userId)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
        if (blog == null)
        {
            return NotFound();
        }
        
        return Ok(blog.Adapt<BlogDto>());
    }


    [HttpGet]
    [Route("")]
    [AllowAnonymous]
    [ProducesResponseType<BlogListAllDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ViewBlogs()
    {
        var blogs = await context.Blogs
            .Include(x => x.User)
            .Include(x => x.Comments)
             .ThenInclude(x => x.Author)
            .Where(x => x.Status == BlogStatus.Approved)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
        return Ok(blogs.Adapt<BlogListAllDto>());
    }
}

    