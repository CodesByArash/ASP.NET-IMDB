using api.Models;
using api.Enums;

public class Rate
{
    public int Id { get; set; }
    public int ContentId {get; set;}
    public  ContentTypeEnum ContentType {get; set;}
    public string UserId {get; set;}
    public required AppUser User {get; set;}
    public ScoreEnum Score {get; set;}
}