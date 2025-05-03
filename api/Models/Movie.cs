using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public required string ImdbId { get; set; }
        public required string Title { get; set; }
        public int ReleaseYear { get; set; }
        public  required string Description { get; set; }
        public int Duration { get; set; }
        public  required string Genre { get; set; }
        public required string PosterUrl { get; set; }
        public double Rate { get; set; }
        [NotMapped]
        public ICollection<Cast> Cast { get; set; }
        [NotMapped]
        public ICollection<Comment> Comments { get; set; }
        [NotMapped]
        public ICollection<Rate> Rates { get; set; }

    }
}