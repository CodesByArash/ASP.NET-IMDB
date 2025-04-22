namespace API.Dtos;

public class CreateMovieRequest{
        public string ImdbId { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public string Genre { get; set; }
        public string PosterUrl { get; set; }
        public double Rating { get; set; }
        // public List<CastDto> Casts { get; set; }
        // public List<UserRatingDto> UserRatings { get; set; }

}