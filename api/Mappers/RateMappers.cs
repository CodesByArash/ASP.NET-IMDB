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
            MediaId = rateModel.MediaId,
            UserId = rateModel.UserId,
            UserName = rateModel.User.UserName,
            Score = rateModel.Score
        };
    }

    public static Rate ToRateModel(this RateRequestDto rateDto)
    {
        return new Rate
        {
            Score = rateDto.Score,
        };
    }
} 