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
            ContentId = commentModel.MediaId,
            UserName = commentModel.User?.UserName,
            CreatedOn = commentModel.CreatedOn,
            LastUpdatedOn = commentModel.LastUpdatedOn
        };
    }

    public static Comment ToCommentModel(this CommentRequestDto commentDto)
    {
        return new Comment
        {
            Text = commentDto.Text,
            LastUpdatedOn = DateTime.UtcNow
        };
    }
}