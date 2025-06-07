using api.Dtos;
using api.Models;

namespace api.Mappers;

public static class CastMappers
{
    public static CastDto ToCastDto(this Cast castModel)
    {
        return new CastDto
        {
            Id = castModel.Id,
            ContentId = castModel.ContentId,
            ContentType = castModel.ContentType,
            PersonId = castModel.PersonId,
            Role = castModel.Role,
            Person = castModel.Person.ToPersonDisplayDto()
        };
    }

    public static Cast ToCastModel(this CreateCastRequest castDto)
    {
        return new Cast
        {
            ContentId = castDto.ContentId,
            ContentType = castDto.ContentType,
            PersonId = castDto.PersonId,
            Role = castDto.Role
        };
    }
} 