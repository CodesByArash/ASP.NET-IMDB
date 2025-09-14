namespace api.Models;

public class Media : IEntity
{
    public int Id { get; set; }
    public string ImdbId { get; set; }
    public string Title { get; set; }
    public DateTime ReleaseDate { get; set; }
    public  string Description { get; set; }
    public  string PosterUrl { get; set; }
    public double Rate { get; set; }
    public List<Comment> Comments { get; set; }
    public List<Rate> Rates { get; set; }
}