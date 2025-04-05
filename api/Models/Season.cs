namespace api.Models;

public class Season
{
    public int Id { get; set; }
    public int SeriesId { get; set; }
    public int SeasonNumber { get; set; }
    public int ReleaseYear { get; set; }

    public double Rating { get; set; }

    public Series Series { get; set; }
    public ICollection<Episode> Episodes { get; set; }
    public ICollection<UserRating> UserRatings { get; set; }
}