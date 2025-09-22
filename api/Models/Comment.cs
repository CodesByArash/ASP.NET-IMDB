using System.ComponentModel.DataAnnotations;
using api.Enums;
using api.Models;

namespace api.Models;
public class Comment : IEntity
{
    [Key]
    public int Id { get; set; }
    public string Text {get; set;} = string.Empty;
    public int MediaId { get; set; }
    public string UserId { get; set; } = string.Empty;
    public AppUser User { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public DateTime LastUpdatedOn { get; set;} = DateTime.UtcNow;
    public Media Media { get; set; }
}


