using API.Dtos;

namespace api.Dtos.Season
{
    public class SeasonDto
    {
        public int Id { get; set; }
        public string ImdbId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; } = string.Empty;
        public int SeasonNumber { get; set; }
        public int EpisodesCount { get; set; }
        public int SeriesId { get; set; }
        public string PosterUrl { get; set; } = string.Empty;
        public double Rate { get; set; }
        public List<EpisodeDto> Episodes { get; set; } = new List<EpisodeDto>();
    }
}
