using api.Enums;
using api.Models;

namespace API.Dtos;

public class UpdateCommentRequest{
    public required string Text { get; set; }
    public DateTime LastUpdatedOn { get; set; }
    public ContentTypeEnum ContentType { get; set; }
}