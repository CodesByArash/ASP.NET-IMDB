using api.Enums;
using System.ComponentModel.DataAnnotations;

namespace api.Dtos;

public class UpdateCastRequest
{
    [Required(ErrorMessage = "Content ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Invalid Content ID")]
    public int ContentId { get; set; }

    [Required(ErrorMessage = "Content Type is required")]
    public ContentTypeEnum ContentType { get; set; }

    [Required(ErrorMessage = "Person ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Invalid Person ID")]
    public int PersonId { get; set; }

    [Required(ErrorMessage = "Role is required")]
    public CastRole Role { get; set; }
} 