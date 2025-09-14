using api.Enums;
using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class GenreRequestDto
{
    [Required(ErrorMessage = "Genre title is required")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Genre title must be between 2 and 50 characters")]
    public string Title { get; set; } = string.Empty;
}