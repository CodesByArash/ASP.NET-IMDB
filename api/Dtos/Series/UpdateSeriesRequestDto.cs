using api.Enums;
using api.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class UpdateSeriesRequest
{
    [Required(ErrorMessage = "IMDB ID is required")]
    [StringLength(20, MinimumLength = 1, ErrorMessage = "IMDB ID must be between 1 and 20 characters")]
    public string ImdbId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Title is required")]
    [StringLength(200, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 200 characters")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Release year is required")]
    [Range(1888, 2100, ErrorMessage = "Release year must be between 1888 and 2100")]
    public int ReleaseYear { get; set; }

    [Required(ErrorMessage = "Description is required")]
    [StringLength(2000, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 2000 characters")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Genre ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Invalid Genre ID")]
    public int GenreId { get; set; }

    [Required(ErrorMessage = "Poster URL is required")]
    [Url(ErrorMessage = "Invalid URL format for poster")]
    [StringLength(500, ErrorMessage = "Poster URL must not exceed 500 characters")]
    public string PosterUrl { get; set; } = string.Empty;

    [Required(ErrorMessage = "Rate is required")]
    [Range(0, 10, ErrorMessage = "Rate must be between 0 and 10")]
    public double Rate { get; set; }
}