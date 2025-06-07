using api.Models;
using api.Enums;

public class Rate
{
    public int Id { get; set; }
    public int ContentId {get; set;}
    public  ContentTypeEnum ContentType {get; set;}
    public string UserId {get; set;}
    public AppUser User {get; set;}
    public ScoreEnum Score {get; set;}
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public DateTime LastUpdatedOn { get; set;} = DateTime.Now;
}
