
namespace api.Models;

public class Episode : Media
{
    public int EpisodeNumber { get; set; }
    public int? DurationMinutes { get; set; }
    public int SeasonId { get; set; }
    public Season Season { get; set; }
}

