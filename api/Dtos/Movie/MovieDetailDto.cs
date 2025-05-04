using api.Enums;

namespace API.Dtos
{
    public class MovieDetailDto
    {
        public int Id { get; set; }
        public string ImdbId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public int ReleaseYear { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Duration { get; set; }
        public GenreEnum Genre { get; set; }
        public string PosterUrl { get; set; } = string.Empty;
        public double Rate { get; set; }
        public List<CommentDto> Comments { get; set; }
    }
}

