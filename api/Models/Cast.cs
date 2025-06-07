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
        public int ContentId { get; set; }
        public ContentTypeEnum ContentType { get; set; }
        public int PersonId { get; set; }
        public CastRole Role { get; set; }
        public Person Person { get; set; }
    }
}

