using api.Enums;

namespace API.Dtos;

public class CreateMovieRequest
{
        public string ImdbId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public int ReleaseYear { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Duration { get; set; }
        public int GenreId {get; set;}
        public string PosterUrl { get; set; } = string.Empty;
        public double Rate { get; set; }

}