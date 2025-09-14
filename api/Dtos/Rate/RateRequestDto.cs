using System.ComponentModel.DataAnnotations;
using api.Enums;

namespace api.Dtos;

public class RateRequestDto
{
    [Required(ErrorMessage = "Score is required")]
    [Range(0, 5, ErrorMessage = "Score must be between 0 and 5")]
    public ScoreEnum Score { get; set; }
} 