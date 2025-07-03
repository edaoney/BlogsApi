using BlogApi.Models.DTOs.Blog;
using BlogApi.Models.DTOs.Comment;
using BlogApi.Models.DTOs.Report;
using BlogApi.Models.Entities;
using Mapster;

namespace BlogApi.Configs;

public static class MappingConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<Blog, BlogDto>
            .NewConfig()
            .Map(dest => dest.Status, src => src.Status.ToString());
        TypeAdapterConfig<Comment, CommentBlogDto>.NewConfig()
            .Map(dest => dest.AuthorName, src => src.Author.UserName);
        TypeAdapterConfig<Blog, BlogListAllDto>
            .NewConfig()
            .Map(dest => dest.AuthorName, src => src.User.UserName);
        TypeAdapterConfig<Blog, BlogStatusList>
            .NewConfig()
            .Map(dest => dest.AuthorName, src => src.User.UserName);
        TypeAdapterConfig<Report, ReportDto>
            .NewConfig()
            .Map(dest => dest.Comment, src => src.Comment.Content)
            .Map(dest => dest.Reporter, src => src.Reporter.UserName);
        TypeAdapterConfig<Comment, CommentDto>.NewConfig()
            .Map(dest => dest.AuthorName, src => src.Author.UserName)
            .Map(dest => dest.ReportComments, src => src.Reports.Select(r => new ReportCommentDto
            {
                Reporter = r.Reporter.UserName,
                Reason = r.Reason
            }).ToList());

    }
}