namespace api.Models;


    public class Person
    {
        public int Id { get; set; }
        public string ImdbId { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Bio { get; set; }
        public string PhotoUrl { get; set; }

        public ICollection<Cast> Casts { get; set; }
    }
