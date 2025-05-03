using api.Enums;
using api.Models;

namespace API.Dtos;

public class CreateCommentRequest{
    public int Id { get; set; }
    public required string Text { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime LastUpdatedOn { get; set; }
    public required string UserId { get; set; }
    public AppUser User { get; set; }
    public int ContentId { get; set; }
    public ContentTypeEnum ContentType { get; set; }
}