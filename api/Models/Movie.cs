using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using api.Enums;

namespace api.Models
{
    public class Movie:Media
    {
        public int Duration { get; set; }
        public ICollection<Genre> Genres { get; set; }
    }
}