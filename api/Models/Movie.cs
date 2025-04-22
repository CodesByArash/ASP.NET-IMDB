using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string ImdbId { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public string Genre { get; set; }
        public string PosterUrl { get; set; }

        public double Rating { get; set; }

        public ICollection<Cast> Casts { get; set; } 

        public ICollection<UserRating> UserRatings { get; set; }
    }
}