using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models;


    public class Person : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string ImdbId { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Bio { get; set; }
        public string PhotoUrl { get; set; }
        public ICollection<Cast> Cast { get; set; }
    }
