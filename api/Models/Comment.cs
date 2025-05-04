using api.Enums;
using api.Models;

namespace api.Models;
public class Comment
{
    public int Id { get; set; }
    public string Text {get; set;} = string.Empty;
    public int ContentId { get; set; }
    public ContentTypeEnum ContentType { get; set; }
    public string UserId { get; set; } = string.Empty;
    public AppUser User { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public DateTime LastUpdatedOn { get; set;} = DateTime.Now;
}