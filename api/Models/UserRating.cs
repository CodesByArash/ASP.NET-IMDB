using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Enums;


namespace api.Models
{
    public class UserRating
    {
        public int Id { get; set; }
        
        public int? MovieId { get; set; }
        public int? SeriesId { get; set; }

        public int UserId { get; set; }

        public double Rating { get; set; } 
        public string Review { get; set; } 

        public Movie Movie { get; set; }
        public Series Series { get; set; }
        public User User { get; set; }
    }

}