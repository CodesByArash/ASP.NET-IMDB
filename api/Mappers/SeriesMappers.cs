using api.Models;
using API.Dtos;


namespace api.Mappers;

public static class SeriesMappers
{
    public static SeriesDto ToSeriesDto(this Series seriesModel)
    {
        return new SeriesDto
        {
            Id = seriesModel.Id,
            ImdbId = seriesModel.ImdbId,
            Title = seriesModel.Title,
            ReleaseYear = seriesModel.ReleaseYear,
            Description = seriesModel.Description,
            Genre = seriesModel.Genre.ToGenreDto(),
            PosterUrl = seriesModel.PosterUrl,
            Rate = seriesModel.Rate,
        };
    }

    public static SeriesDetailDto ToSeriesDetailDto(this Series seriesModel)
    {
        return new SeriesDetailDto
        {
            Id = seriesModel.Id,
            ImdbId = seriesModel.ImdbId,
            Title = seriesModel.Title,
            ReleaseYear = seriesModel.ReleaseYear,
            Description = seriesModel.Description,
            Genre = seriesModel.Genre.ToGenreDto(),
            PosterUrl = seriesModel.PosterUrl,
            Rate = seriesModel.Rate,
            Comments = seriesModel.Comments.Select(c => c.ToCommentDto()).ToList(),
        };
    }

    public static Series ToSeriesModel(this CreateSeriesRequest seriesDto)
    {
        return new Series
        {
            ImdbId = seriesDto.ImdbId,
            Title = seriesDto.Title,
            ReleaseYear = seriesDto.ReleaseYear,
            Description = seriesDto.Description,
            GenreId = seriesDto.GenreId,
            PosterUrl = seriesDto.PosterUrl,
            Rate = seriesDto.Rate,
        };
    }
}
