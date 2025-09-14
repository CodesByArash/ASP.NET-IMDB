using api.Dtos;
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
            ReleaseDate = seriesModel.ReleaseDate,
            Description = seriesModel.Description,
            Genres = seriesModel.Genres.Select(g => g.ToGenreDto()).ToList(),
            PosterUrl = seriesModel.PosterUrl,
            Rate = seriesModel.Rate,
        };
    }
    
    public static Series ToSeriesModel(this SeriesRequestDto seriesDto)
    {
        return new Series
        {
            ImdbId = seriesDto.ImdbId,
            Title = seriesDto.Title,
            ReleaseDate = seriesDto.ReleaseDate,
            Description = seriesDto.Description,
            // Genres = seriesDto.GenreIds,
            PosterUrl = seriesDto.PosterUrl,
            Rate = seriesDto.Rate,
        };
    }

}
