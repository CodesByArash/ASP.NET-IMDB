using System;

namespace api.Dtos;

public class PersonDisplayDto
{
    public int Id { get; set; }
    public string ImdbId { get; set; }
    public string FullName { get; set; }
    public DateTime BirthDate { get; set; }
    public string PhotoUrl { get; set; }
} 