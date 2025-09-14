using api.Dtos;
using api.Models;
using API.Dtos;


namespace api.Mappers;

public static class MovieMappers
{
    public static MovieDto ToMovieDto(this Movie movieModel)
    {
        return new MovieDto
        {
            Id = movieModel.Id,
            ImdbId = movieModel.ImdbId,
            Title = movieModel.Title,
            ReleaseDate = movieModel.ReleaseDate,
            Description = movieModel.Description,
            Duration = movieModel.Duration,
            Genres = movieModel.Genres.Select(g => g.ToGenreDto()).ToList(),
            PosterUrl = movieModel.PosterUrl,
            Rate = movieModel.Rate,
        };
    }
    public static Movie ToMovieModel(this MovieRequestDto movieDto)
    {
        return new Movie
        {
            ImdbId = movieDto.ImdbId,
            Title = movieDto.Title,
            ReleaseDate = movieDto.ReleaseDate,
            // Genres = movieDto.GenreIds,
            Description = movieDto.Description,
            Duration = movieDto.Duration,
            PosterUrl = movieDto.PosterUrl,
            Rate = movieDto.Rate,
        };
    }
}
