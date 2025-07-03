using BlogApi.Data;
using BlogApi.Models.DTOs.Report;
using BlogApi.Models.Entities;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Controllers;

[ApiController]
[Route("reports")]
[Authorize]
public class ReportController(AppDbContext context, UserManager<IdentityUser> userManager) : ControllerBase
{
    [HttpPost]
    [Route("{id:int:min(1)}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddReport(int id, ReportCreateDto reportCreateDto)
    {
        var userId = userManager.GetUserId(User);
        var comment = context.Comments.FirstOrDefault(x => x.Id == id);
        if (comment == null)
        {
            return BadRequest(new { Error = "Comment not found." });
        }

        var report = new Report
        {
            Reason = reportCreateDto.Reason,
            ReporterId = userId,
            CommentId = id,
        };
        context.Reports.Add(report);
        await context.SaveChangesAsync();
        var user = await userManager.FindByIdAsync(userId);
        report.Reporter = user;
        return CreatedAtAction(nameof(GetReport), new { id = report.Id }, report.Adapt<ReportDto>());
    }

    [HttpGet]
    [Route("{id:int:min(1)}")]
    [ProducesResponseType<ReportDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetReport(int id)
    {
        var userId = userManager.GetUserId(User);
        var report = context.Reports
            .Include(x => x.Comment)
            .Include(x => x.Reporter)
            .Where(x => x.ReporterId == userId)
            .FirstOrDefault(x => x.Id == id);
        if (report == null)
        {
            return BadRequest(new { Error = "Report not found." });
        }
        
        return Ok(report.Adapt<ReportDto>());
    }
}