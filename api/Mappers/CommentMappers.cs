using api.Models;
using API.Dtos;

namespace api.Mappers;
public static class CommentMappers
{
    public static CommentDto ToCommentDto(this Comment commentModel)
    {
        return new CommentDto
        {
            Id = commentModel.Id,
            Text = commentModel.Text,
            UserId = commentModel.UserId,
            ContentId = commentModel.ContentId,
            ContentType = commentModel.ContentType,
            UserName = commentModel.User.UserName,
            CreatedOn = commentModel.CreatedOn,
            LastUpdatedOn = commentModel.LastUpdatedOn
        };
    }

    public static Comment ToCommentModel(this CreateCommentRequest commentDto)
    {
        return new Comment
        {
            Text = commentDto.Text,
            CreatedOn = DateTime.Now,
            LastUpdatedOn = DateTime.Now
        };
    }
}