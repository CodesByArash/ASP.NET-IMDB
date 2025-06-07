using System.ComponentModel.DataAnnotations;
using api.Enums;

namespace api.Dtos;

public class CreateRateDto
{
    [Required(ErrorMessage = "ContentId is required")]
    public int ContentId { get; set; }

    [Required(ErrorMessage = "ContentType is required")]
    public ContentTypeEnum ContentType { get; set; }

    [Required(ErrorMessage = "Score is required")]
    [Range(0, 5, ErrorMessage = "Score must be between 0 and 5")]
    public ScoreEnum Score { get; set; }
} 