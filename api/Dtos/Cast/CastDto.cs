using api.Enums;

namespace api.Dtos.Cast;

public class CastDto
{
    public int Id { get; set; }
    public int MediaId { get; set; }
    public int PersonId { get; set; }
    public CastRole Role { get; set; }
    public PersonDto Person { get; set; }
} 