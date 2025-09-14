using api.Dtos;
using api.Models;
using API.Dtos;
using api.Dtos.Season;

namespace api.Mappers;

public static class SeasonMappers
{
    public static SeasonDto ToSeasonDto(this Season seasonModel)
    {
        return new SeasonDto
        {
            Id = seasonModel.Id,
            ImdbId = seasonModel.ImdbId,
            Title = seasonModel.Title,
            ReleaseDate = seasonModel.ReleaseDate,
            Description = seasonModel.Description,
            SeasonNumber = seasonModel.SeasonNumber,
            EpisodesCount = seasonModel.EpisodesCount,
            SeriesId = seasonModel.SeriesId,
            PosterUrl = seasonModel.PosterUrl,
            Rate = seasonModel.Rate,
            Episodes = seasonModel.Episodes?.Select(e => e.ToEpisodeDto()).ToList() ?? new List<EpisodeDto>(),
        };
    }
    
    public static Season ToSeasonModel(this SeasonRequestDto seasonDto)
    {
        return new Season
        {
            ImdbId = seasonDto.ImdbId,
            Title = seasonDto.Title,
            ReleaseDate = seasonDto.ReleaseDate,
            Description = seasonDto.Description,
            SeasonNumber = seasonDto.SeasonNumber,
            EpisodesCount = seasonDto.EpisodesCount,
            SeriesId = seasonDto.SeriesId,
            PosterUrl = seasonDto.PosterUrl,
            Rate = seasonDto.Rate,
        };
    }
}
