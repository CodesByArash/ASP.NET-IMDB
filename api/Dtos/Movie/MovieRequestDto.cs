using api.Enums;
using System.ComponentModel.DataAnnotations;

namespace api.Dtos;

public class MovieRequestDto
{
    [Required(ErrorMessage = "IMDB ID is required")]
    [StringLength(20, MinimumLength = 1, ErrorMessage = "IMDB ID must be between 1 and 20 characters")]
    public string ImdbId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Title is required")]
    [StringLength(200, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 200 characters")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Release year is required")]
    public DateTime ReleaseDate { get; set; }

    [Required(ErrorMessage = "Description is required")]
    [StringLength(2000, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 2000 characters")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Duration is required")]
    [Range(1, 600, ErrorMessage = "Duration must be between 1 and 600 minutes")]
    public int Duration { get; set; }

    [MinLength(1, ErrorMessage = "At least one Genre ID is required")]
    public List<int> GenreIds { get; set; } = new List<int>();

    [Required(ErrorMessage = "Poster URL is required")]
    [Url(ErrorMessage = "Invalid URL format for poster")]
    [StringLength(500, ErrorMessage = "Poster URL must not exceed 500 characters")]
    public string PosterUrl { get; set; } = string.Empty;

    [Required(ErrorMessage = "Rate is required")]
    [Range(0, 10, ErrorMessage = "Rate must be between 0 and 10")]
    public double Rate { get; set; }
}