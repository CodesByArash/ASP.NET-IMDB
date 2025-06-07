using api.Models;
using api.Dtos;

namespace api.Mappers;

public static class PersonMapper
{
    public static PersonDisplayDto ToPersonDisplayDto(this Person person)
    {
        return new PersonDisplayDto
        {
            Id = person.Id,
            ImdbId = person.ImdbId,
            FullName = person.FullName,
            BirthDate = person.BirthDate,
            PhotoUrl = person.PhotoUrl
        };
    }

    public static PersonDetailDto ToPersonDetailDto(this Person person)
    {
        return new PersonDetailDto
        {
            Id = person.Id,
            ImdbId = person.ImdbId,
            FullName = person.FullName,
            BirthDate = person.BirthDate,
            Bio = person.Bio,
            PhotoUrl = person.PhotoUrl
        };
    }

    public static Person ToPersonModel(this PersonCreateDto dto)
    {
        return new Person
        {
            ImdbId = dto.ImdbId,
            FullName = dto.FullName,
            BirthDate = dto.BirthDate,
            Bio = dto.Bio,
            PhotoUrl = dto.PhotoUrl
        };
    }
} 