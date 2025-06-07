using System;
using System.Collections.Generic;

namespace api.Dtos;

public class PersonDetailDto
{
    public int Id { get; set; }
    public string ImdbId { get; set; }
    public string FullName { get; set; }
    public DateTime BirthDate { get; set; }
    public string Bio { get; set; }
    public string PhotoUrl { get; set; }
    
    // public ICollection<CastDto> cast  { get; set; }
    
}