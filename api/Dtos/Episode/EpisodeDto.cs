using api.Dtos.Season;

namespace API.Dtos
{
    public class EpisodeDto
    {
        public int Id { get; set; }
        public string ImdbId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; } = string.Empty;
        public int EpisodeNumber { get; set; }
        public int? DurationMinutes { get; set; }
        public int SeasonId { get; set; }
        public SeasonDto? Season { get; set; }
        public string PosterUrl { get; set; } = string.Empty;
        public double Rate { get; set; }
    }
}
