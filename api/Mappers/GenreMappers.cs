using api.Models;
using API.Dtos;

namespace api.Mappers;

public static class GenreMappers
{
    public static GenreDto ToGenreDto(this Genre genreModel)
    {
        return new GenreDto
        {
            Id = genreModel.Id,
            Title = genreModel.Title,
        };
    }
    public static Genre ToGenreModel(this GenreRequestDto genreDto)
    {
        return new Genre
        {
            Title = genreDto.Title,
        };
    }
}
