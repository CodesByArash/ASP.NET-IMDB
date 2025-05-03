using api.Models;
using API.Dtos;

public static class MovieMappers
{
    public static MovieDto ToMovieDto(this Movie movieModel){
        return new MovieDto
        {
            Id = movieModel.Id,
            ImdbId = movieModel.ImdbId,
            Title = movieModel.Title,
            ReleaseYear = movieModel.ReleaseYear,
            Description = movieModel.Description,
            Duration = movieModel.Duration,
            Genre = movieModel.Genre,
            PosterUrl = movieModel.PosterUrl,
            Rate = movieModel.Rate,
            Comments = movieModel.Comments.Select(c => c.ToCommentDto()).ToList(),
        };
    }

    public static Movie ToMovieModel(this CreateMovieRequest movieDto){
        return new Movie
        {
            ImdbId = movieDto.ImdbId,
            Title = movieDto.Title,
            ReleaseYear = movieDto.ReleaseYear,
            Description = movieDto.Description,
            Duration = movieDto.Duration,
            Genre = movieDto.Genre,
            PosterUrl = movieDto.PosterUrl,
            Rate = movieDto.Rate,
            // Comments = movieDto.Comments
        };
    }
}
