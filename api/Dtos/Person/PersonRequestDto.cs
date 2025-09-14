using System;
using System.ComponentModel.DataAnnotations;

namespace api.Dtos;

public class PersonRequestDto
{
    [Required(ErrorMessage = "imdb id is required")]
    public string ImdbId { get; set; }

    [Required(ErrorMessage = "full name is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "full name must be between 2 and 100 characters")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "date of birth is required")]
    [DataType(DataType.Date, ErrorMessage = "birth date is not valid")]
    public DateTime BirthDate { get; set; }

    [Required(ErrorMessage = "Biography is required")]
    [StringLength(2000, MinimumLength = 10, ErrorMessage = "Biography must be between 10 and 2000 characters")]
    public string Bio { get; set; }

    [Required(ErrorMessage = "Photo url is required")]
    [Url(ErrorMessage = "Photo Url should be valid")]
    public string PhotoUrl { get; set; }
} 