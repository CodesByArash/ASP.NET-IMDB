using api.Enums;

namespace API.Dtos;

public class CommentDto
{
    public int Id { get; set; }
    public required string Text { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime LastUpdatedOn { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public int ContentId { get; set; }
    public ContentTypeEnum ContentType { get; set; }
}