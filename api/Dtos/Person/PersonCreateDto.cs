using System;
using System.ComponentModel.DataAnnotations;

namespace api.Dtos;

public class PersonCreateDto
{
    [Required(ErrorMessage = "imdb id is required")]
    public string ImdbId { get; set; }

    [Required(ErrorMessage = "fullname is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "fullname must be between 2 and 100 characters")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Date of birth is required")]
    [DataType(DataType.Date, ErrorMessage = "data of birth is not valid")]
    public DateTime BirthDate { get; set; }

    [Required(ErrorMessage = "Biography is required")]
    [StringLength(2000, MinimumLength = 10, ErrorMessage = "biography must be between 10 and 2000 characters")]
    public string Bio { get; set; }

    [Required(ErrorMessage = "photo url is required")]
    [Url(ErrorMessage = "Url should be valid")]
    public string PhotoUrl { get; set; }
}