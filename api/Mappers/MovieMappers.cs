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
            ReleaseYear = movieModel.ReleaseYear,
            Description = movieModel.Description,
            Duration = movieModel.Duration,
            Genre = movieModel.Genre.ToGenreDto(),
            PosterUrl = movieModel.PosterUrl,
            Rate = movieModel.Rate,
        };
    }

    public static MovieDetailDto ToMovieDetailDto(this Movie movieModel)
    {
        return new MovieDetailDto
        {
            Id = movieModel.Id,
            ImdbId = movieModel.ImdbId,
            Title = movieModel.Title,
            ReleaseYear = movieModel.ReleaseYear,
            Description = movieModel.Description,
            Duration = movieModel.Duration,
            Genre = movieModel.Genre.ToGenreDto(),
            PosterUrl = movieModel.PosterUrl,
            Rate = movieModel.Rate,
            Comments = movieModel.Comments.Select(c => c.ToCommentDto()).ToList()
        };
    }

    public static Movie ToMovieModel(this CreateMovieRequest movieDto)
    {
        return new Movie
        {
            ImdbId = movieDto.ImdbId,
            Title = movieDto.Title,
            ReleaseYear = movieDto.ReleaseYear,
            GenreId = movieDto.GenreId,
            Description = movieDto.Description,
            Duration = movieDto.Duration,
            PosterUrl = movieDto.PosterUrl,
            Rate = movieDto.Rate,
        };
    }
}
