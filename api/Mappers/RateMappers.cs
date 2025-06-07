using api.Models;
using api.Dtos;

namespace api.Mappers;

public static class RateMappers
{
    public static RateDto ToRateDto(this Rate rateModel)
    {
        return new RateDto
        {
            Id = rateModel.Id,
            ContentId = rateModel.ContentId,
            ContentType = rateModel.ContentType,
            UserId = rateModel.UserId,
            UserName = rateModel.User.UserName,
            Score = rateModel.Score
        };
    }

    public static Rate ToRateModel(this CreateRateDto rateDto)
    {
        return new Rate
        {
            ContentId = rateDto.ContentId,
            ContentType = rateDto.ContentType,
            Score = rateDto.Score,
        };
    }

    public static void UpdateRateModel(this Rate rateModel, UpdateRateDto rateDto)
    {
        rateModel.Score = rateDto.Score;
    }
} 