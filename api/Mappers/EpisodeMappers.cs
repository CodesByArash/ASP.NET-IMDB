using api.Dtos;
using api.Models;
using API.Dtos;

namespace api.Mappers;

public static class EpisodeMappers
{
    public static EpisodeDto ToEpisodeDto(this Episode episodeModel)
    {
        return new EpisodeDto
        {
            Id = episodeModel.Id,
            ImdbId = episodeModel.ImdbId,
            Title = episodeModel.Title,
            ReleaseDate = episodeModel.ReleaseDate,
            Description = episodeModel.Description,
            EpisodeNumber = episodeModel.EpisodeNumber,
            DurationMinutes = episodeModel.DurationMinutes,
            SeasonId = episodeModel.SeasonId,
            Season = episodeModel.Season?.ToSeasonDto(),
            PosterUrl = episodeModel.PosterUrl,
            Rate = episodeModel.Rate,
        };
    }
    
    public static Episode ToEpisodeModel(this EpisodeRequestDto episodeDto)
    {
        return new Episode
        {
            ImdbId = episodeDto.ImdbId,
            Title = episodeDto.Title,
            ReleaseDate = episodeDto.ReleaseDate,
            Description = episodeDto.Description,
            EpisodeNumber = episodeDto.EpisodeNumber,
            DurationMinutes = episodeDto.DurationMinutes,
            SeasonId = episodeDto.SeasonId,
            PosterUrl = episodeDto.PosterUrl,
            Rate = episodeDto.Rate,
        };
    }
}
