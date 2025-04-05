namespace api.Models;


public class Episode
{
        public int Id { get; set; }
        public int SeasonId { get; set; }
        public int EpisodeNumber { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
        public DateTime ReleaseDate { get; set; }

        public double Rating { get; set; }

        public Season Season { get; set; }
        
        public ICollection<Cast> Casts { get; set; } 

        public ICollection<UserRating> UserRatings { get; set; }
}