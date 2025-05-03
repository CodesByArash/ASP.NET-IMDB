using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace api.Models
{
    public class AppUser: IdentityUser
    {
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Rate> Rates { get; set; }
    }
}

