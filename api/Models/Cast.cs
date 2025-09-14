using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Enums;

namespace api.Models
{
    public class Cast : IEntity
    {
        public int Id { get; set; }
        public int MediaId { get; set; }
        public int PersonId { get; set; }
        public CastRole Role { get; set; }
        public Person Person { get; set; }
    }
}

