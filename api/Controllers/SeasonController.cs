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
public class SeasonController : ControllerBase
{
    private readonly ISeasonRepository _seasonRepository;
    private readonly ISeriesRepository _seriesRepository;
    
    public SeasonController(ApplicationDbContext context, ISeasonRepository seasonRepository, ISeriesRepository seriesRepository)
    {
        _seasonRepository = seasonRepository;
        _seriesRepository = seriesRepository;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        try
        {
            var seasons = await _seasonRepository.GetAllAsync();
            var seasonDto = seasons.Select(season => season.ToSeasonDto()).ToList();
            return ApiResponse.Success(seasonDto, "Seasons retrieved successfully");
        }
        catch (Exception ex)
        {
            return ApiResponse.Error("Failed to retrieve seasons", 500);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetail([FromRoute] int id)
    {
        try
        {
            var season = await _seasonRepository.GetByIdAsync(id);
            if (season == null)
                return ApiResponse.NotFound("Season not found");

            return ApiResponse.Success(season.ToSeasonDto(), "Season retrieved successfully");
        }
        catch (Exception ex)
        {
            return ApiResponse.Error("Failed to retrieve season", 500);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SeasonRequestDto seasonDto)
    {
        try
        {
            var series = await _seriesRepository.GetByIdAsync(seasonDto.SeriesId);
            if (series == null)
                return ApiResponse.NotFound("Invalid Series ID");

            var season = seasonDto.ToSeasonModel();
            var addedSeason = await _seasonRepository.AddAsync(season);

            if (addedSeason == null)
                return ApiResponse.Error("Failed to create the season", 500);

            return ApiResponse.Success(addedSeason.ToSeasonDto(), "Season created successfully");
        }
        catch (Exception ex)
        {
            return ApiResponse.Error("Failed to create season", 500);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] SeasonRequestDto seasonDto)
    {
        try
        {
            var series = await _seriesRepository.GetByIdAsync(seasonDto.SeriesId);
            if (series == null)
                return ApiResponse.NotFound("Invalid Series ID");

            var seasonToUpdate = seasonDto.ToSeasonModel();
            seasonToUpdate.Id = id;

            var updatedSeason = await _seasonRepository.UpdateAsync(seasonToUpdate);
            if (updatedSeason == null)
                return ApiResponse.NotFound("Season not found");

            return ApiResponse.Success(updatedSeason.ToSeasonDto(), "Season updated successfully");
        }
        catch (Exception ex)
        {
            return ApiResponse.Error("Failed to update season", 500);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            var season = new Season() { Id = id };
            var deletedSeason = await _seasonRepository.DeleteAsync(season);

            if (!deletedSeason)
                return ApiResponse.NotFound("Season not found");

            return ApiResponse.Success(deletedSeason, "Season has been deleted");
        }
        catch (Exception ex)
        {
            return ApiResponse.Error("Failed to delete season", 500);
        }
    }
}
