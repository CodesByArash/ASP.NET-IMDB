using api.Enums;

namespace api.Dtos;

public class CastDto
{
    public int Id { get; set; }
    public int ContentId { get; set; }
    public ContentTypeEnum ContentType { get; set; }
    public int PersonId { get; set; }
    public CastRole Role { get; set; }
    public PersonDisplayDto Person { get; set; }
} 