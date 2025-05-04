using System.ComponentModel.DataAnnotations.Schema;
using api.Enums;

namespace api.Models;

    public class Series
    {
        public int Id { get; set; }
        public required string ImdbId { get; set; }
        public required string Title { get; set; }
        public int ReleaseYear { get; set; }
        public required string Description { get; set; }
        public required string PosterUrl { get; set; }
        public GenreEnum Genre { get; set; }
        public double Rate { get; set; }
        [NotMapped]
        public ICollection<Cast> Cast { get; set; }
        [NotMapped]
        public ICollection<Comment> Comments { get; set; }
    }
