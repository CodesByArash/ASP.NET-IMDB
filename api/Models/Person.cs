using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models;


    public class Person
    {
        public int Id { get; set; }
        public required string ImdbId { get; set; }
        public required string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public required string Bio { get; set; }
        public required string PhotoUrl { get; set; }
        [NotMapped]
        public ICollection<Cast> Cast { get; set; }
    }
