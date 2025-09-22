using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Enums;

namespace api.Models
{
    public class Cast : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int MediaId { get; set; }

        [Required]
        public int PersonId { get; set; }

        [Required]
        public CastRole Role { get; set; }

        public Person? Person { get; set; }
    }

}

