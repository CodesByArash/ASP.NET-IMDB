using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Dtos;
using api.Interfaces.IRepositories;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using api.Mappers;
using api.Helpers;

namespace api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EpisodeController : ControllerBase
{
    private readonly IEpisodeRepository _episodeRepository;
    private readonly ISeasonRepository _seasonRepository;
    
    public EpisodeController(ApplicationDbContext context, IEpisodeRepository episodeRepository, ISeasonRepository seasonRepository)
    {
        _episodeRepository = episodeRepository;
        _seasonRepository = seasonRepository;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        try
        {
            var episodes = await _episodeRepository.GetAllAsync();
            var episodeDto = episodes.Select(episode => episode.ToEpisodeDto()).ToList();
            return ApiResponse.Success(episodeDto, "Episodes retrieved successfully");
        }
        catch (Exception ex)
        {
            return ApiResponse.Error("Failed to retrieve episodes", 500);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetail([FromRoute] int id)
    {
        try
        {
            var episode = await _episodeRepository.GetByIdAsync(id);
            if (episode == null)
                return ApiResponse.NotFound("Episode not found");

            return ApiResponse.Success(episode.ToEpisodeDto(), "Episode retrieved successfully");
        }
        catch (Exception ex)
        {
            return ApiResponse.Error("Failed to retrieve episode", 500);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] EpisodeRequestDto episodeDto)
    {
        try
        {
            var season = await _seasonRepository.GetByIdAsync(episodeDto.SeasonId);
            if (season == null)
                return ApiResponse.NotFound("Invalid Season ID");

            var episode = episodeDto.ToEpisodeModel();
            var addedEpisode = await _episodeRepository.AddAsync(episode);

            if (addedEpisode == null)
                return ApiResponse.Error("Failed to create the episode", 500);

            return ApiResponse.Success(addedEpisode.ToEpisodeDto(), "Episode created successfully");
        }
        catch (Exception ex)
        {
            return ApiResponse.Error("Failed to create episode", 500);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] EpisodeRequestDto episodeDto)
    {
        try
        {
            var season = await _seasonRepository.GetByIdAsync(episodeDto.SeasonId);
            if (season == null)
                return ApiResponse.NotFound("Invalid Season ID");

            var episodeToUpdate = episodeDto.ToEpisodeModel();
            episodeToUpdate.Id = id;

            var updatedEpisode = await _episodeRepository.UpdateAsync(episodeToUpdate);
            if (updatedEpisode == null)
                return ApiResponse.NotFound("Episode not found");

            return ApiResponse.Success(updatedEpisode.ToEpisodeDto(), "Episode updated successfully");
        }
        catch (Exception ex)
        {
            return ApiResponse.Error("Failed to update episode", 500);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            var episode = new Episode() { Id = id };
            var deletedEpisode = await _episodeRepository.DeleteAsync(episode);

            if (!deletedEpisode)
                return ApiResponse.NotFound("Episode not found");

            return ApiResponse.Success(deletedEpisode, "Episode has been deleted");
        }
        catch (Exception ex)
        {
            return ApiResponse.Error("Failed to delete episode", 500);
        }
    }
}
