using System.ComponentModel.DataAnnotations;
using api.Models;
using api.Enums;

public class Rate : IEntity
{
    [Key]
    public int Id { get; set; }
    public int MediaId {get; set;}
    // public  ContentTypeEnum ContentType {get; set;}
    public string UserId {get; set;}
    public AppUser User {get; set;}
    public ScoreEnum Score {get; set;}
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public DateTime LastUpdatedOn { get; set;} = DateTime.UtcNow;
    public Media Media { get; set; }
}
