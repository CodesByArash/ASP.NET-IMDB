using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Enums;

namespace api.Models
{
    public class Cast
    {
        public int Id { get; set; }
        public int? MovieId { get; set; } 
        public int? SeriesId { get; set; } 
        public int? SeasonId { get; set; }
        public int? EpisodeId { get; set; } 

        public int PersonId { get; set; } 
        public string Role { get; set; }  
        public string CharacterName { get; set; } 

        public Movie Movie { get; set; }
        public Series Series { get; set; }
        public Season Season { get; set; }
        public Episode Episode { get; set; }
        public Person Person { get; set; }
    }
}