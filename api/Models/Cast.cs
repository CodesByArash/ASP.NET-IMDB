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
        public int PersonId { get; set; } 
        public CastRole Role { get; set; }  
        public Movie? Movie { get; set; }
        public Series? Series { get; set; }
        public Person Person { get; set; }
    }
}