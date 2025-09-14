using api.Enums;
using System.ComponentModel.DataAnnotations;

namespace api.Dtos;

public class CastRequestDto
{
    [Required(ErrorMessage = "Content ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Invalid Content ID")]
    public int MediaId { get; set; }

    [Required(ErrorMessage = "Person ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Invalid Person ID")]
    public int PersonId { get; set; }

    [Required(ErrorMessage = "Role is required")]
    public CastRole Role { get; set; }
}