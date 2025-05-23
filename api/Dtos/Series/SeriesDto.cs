using api.Enums;
using api.Models;

namespace API.Dtos
{
    public class SeriesDto
    {
        public int Id { get; set; }
        public string ImdbId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public int ReleaseYear { get; set; }
        public string Description { get; set; } = string.Empty;
        public int GenreId {get; set;}
        public GenreDto Genre { get; set; }
        public string PosterUrl { get; set; } = string.Empty;
        public double Rate { get; set; }
    }
}