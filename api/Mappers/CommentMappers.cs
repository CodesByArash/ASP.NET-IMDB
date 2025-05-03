using api.Models;
using API.Dtos;

public static class CommentMappers
{
    public static CommentDto ToCommentDto(this Comment commentModel){
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

    public static Comment ToCmmentModel(this CreateCommentRequest commentDto){
        return new Comment
        {
            Text = commentDto.Text,
            UserId = commentDto.UserId,
            ContentId = commentDto.ContentId,
            ContentType = commentDto.ContentType,
            User = commentDto.User,
            CreatedOn = commentDto.CreatedOn,
            LastUpdatedOn = commentDto.LastUpdatedOn
        };
    }
}