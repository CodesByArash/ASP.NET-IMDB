using api.Dtos;
using api.Dtos.Cast;
using api.Interfaces;
using api.Models;

namespace api.Mappers;

public static class CastMappers
{
    public static CastDto ToDto(this Cast castModel)
    {
        return new CastDto
        {
            Id = castModel.Id,
            MediaId = castModel.MediaId,
            PersonId = castModel.PersonId,
            Role = castModel.Role,
            Person = castModel.Person.ToPersonDisplayDto()
        };
    }

    public static Cast ToModel(this CastRequestDto castDto)
    {
        return new Cast
        {
            MediaId = castDto.MediaId,
            PersonId = castDto.PersonId,
            Role = castDto.Role
        };
    }
} 