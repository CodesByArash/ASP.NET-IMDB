using api.Enums;
using api.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class CreateCommentRequest
{
    [Required(ErrorMessage = "Comment text is required")]
    [StringLength(1000, MinimumLength = 1, ErrorMessage = "Comment must be between 1 and 1000 characters")]
    public required string Text { get; set; }
}