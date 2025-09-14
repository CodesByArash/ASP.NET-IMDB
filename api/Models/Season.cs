using System.Collections.Generic;

namespace api.Models;

public class Season : Media
{
    public int SeasonNumber { get; set; }
    public int EpisodesCount { get; set; }
    public int SeriesId { get; set; }
    public Series Series { get; set; }
    public List<Episode> Episodes { get; set; }
}
