using api.Enums;
using api.Models;

public class Comment
{
    public int Id { get; set; }
    public ContentTypeEnum ContentType { get; set; }
    public int ContentId { get; set; }
    public string UserId { get; set; }
    public required AppUser User { get; set; }
    public string Text {get; set;} = string.Empty;
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public DateTime LastUpdatedOn { get; set;} = DateTime.Now;
}