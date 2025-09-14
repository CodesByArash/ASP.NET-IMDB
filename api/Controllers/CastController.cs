using api.Dtos;
using api.Interfaces.IRepositories;
using api.Mappers;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using api.Helpers;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CastController : ControllerBase
{
    private readonly ICastRepository _castRepository;
    private readonly IMediaRepository _mediaRepository;
    private readonly IPersonRepository _personRepository;

    public CastController(CastRepository castRepository, IMediaRepository mediaRepository, IPersonRepository personRepository)
    {
        _castRepository = castRepository;
        _mediaRepository = mediaRepository;
        _personRepository = personRepository;
    }

    [HttpGet("media/{mediaId}")]
    public async Task<IActionResult> GetMediaCast([FromRoute] int mediaId)
    {
        try
        {
            var casts = await _castRepository.GetMediaCastAsync(mediaId);
            var castDtoList = casts.Select(c => c.ToDto());
            return ApiResponse.Success(castDtoList, "Cast retrieved successfully");
        }
        catch (Exception ex)
        {
            return ApiResponse.Error("Failed to retrieve cast", 500);
        }
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddMediaCast([FromBody] CastRequestDto castDto)
    {
        try
        {
            var media = await _mediaRepository.GetByIdAsync(castDto.MediaId);
            if (media == null)
                return ApiResponse.NotFound("Media Not Found");
            
            var person = await _personRepository.GetByIdAsync(castDto.PersonId);
            if (person == null)
                return ApiResponse.NotFound("Person Not Found");
            
            var cast = castDto.ToModel();
            var createdCast = await _castRepository.AddAsync(cast);
            if (createdCast == null)
                return ApiResponse.Error("Could Not Create Cast", 500);
            
            return ApiResponse.Success(createdCast.ToDto(), "Cast added successfully");
        }
        catch (Exception ex)
        {
            return ApiResponse.Error("Failed to add cast", 500);
        }
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCast([FromRoute] int id, [FromBody] CastRequestDto castDto)
    {
        try
        {
            var cast = castDto.ToModel();
            cast.Id = id;
            var updatedCast = await _castRepository.UpdateAsync(cast);
            if (updatedCast == null)
                return ApiResponse.NotFound("Cast Not Found");

            return ApiResponse.Success(cast.ToDto(), "Cast updated successfully");
        }
        catch (Exception ex)
        {
            return ApiResponse.Error("Failed to update cast", 500);
        }
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCast(int id)
    {
        try
        {
            var cast = new Cast() {Id = id};
            var isRemoved = await _castRepository.DeleteAsync(cast);
            if (!isRemoved)
                return ApiResponse.NotFound("Cast Not Found");
            
            return ApiResponse.Success(isRemoved, "Cast deleted successfully");
        }
        catch (Exception ex)
        {
            return ApiResponse.Error("Failed to delete cast", 500);
        }
    }
}