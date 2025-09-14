using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using api.Interfaces;
using api.Repository;
using Microsoft.AspNetCore.Authorization;
using api.Mappers;
using api.Interfaces.IRepositories;
using api.Models;
using api.Helpers;

namespace api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SeriesController : ControllerBase
{
    private readonly ISeriesRepository _seriesRepository;
    private readonly IGenreRepository _genreRepository;
    
    public SeriesController(ApplicationDbContext context, ISeriesRepository seriesRepository, IGenreRepository genreRepository)
    {
        _seriesRepository = seriesRepository;
        _genreRepository = genreRepository;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        try
        {
            var series = await _seriesRepository.GetAllAsync();
            var seriesDto = series.Select(s => s.ToSeriesDto()).ToList();
            return ApiResponse.Success(seriesDto, "Series retrieved successfully");
        }
        catch (Exception ex)
        {
            return ApiResponse.Error("Failed to retrieve series", 500);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetail([FromRoute] int id)
    {
        try
        {
            var series = await _seriesRepository.GetByIdAsync(id);
            if (series == null)
                return ApiResponse.NotFound("Series not found");

            return ApiResponse.Success(series.ToSeriesDto(), "Series retrieved successfully");
        }
        catch (Exception ex)
        {
            return ApiResponse.Error("Failed to retrieve series", 500);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SeriesRequestDto seriesDto)
    {
        try
        {
            var genres = await _genreRepository.GetAllByIdsAsync(seriesDto.GenreIds);
            if (genres.Count != seriesDto.GenreIds.Count)
                return ApiResponse.NotFound("Invalid Genre ID");

            var series = seriesDto.ToSeriesModel();
            series.Genres = genres;
            var addedSeries = await _seriesRepository.AddAsync(series);

            if (addedSeries == null)
                return ApiResponse.Error("Failed to create the series", 500);

            return ApiResponse.Success(addedSeries.ToSeriesDto(), "Series created successfully");
        }
        catch (Exception ex)
        {
            return ApiResponse.Error("Failed to create series", 500);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] SeriesRequestDto seriesDto)
    {
        try
        {
            var genres = await _genreRepository.GetAllByIdsAsync(seriesDto.GenreIds);
            if (genres.Count != seriesDto.GenreIds.Count)
                return ApiResponse.NotFound("Invalid Genre ID");

            var seriesToUpdate = seriesDto.ToSeriesModel();
            seriesToUpdate.Id = id;
            seriesToUpdate.Genres = genres;
            var updatedSeries = await _seriesRepository.UpdateAsync(seriesToUpdate); 
            if (updatedSeries == null) 
                return ApiResponse.NotFound("Series not found");

            return ApiResponse.Success(updatedSeries.ToSeriesDto(), "Series updated successfully");
        }
        catch (Exception ex)
        {
            return ApiResponse.Error("Failed to update series", 500);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            var series = new Series() { Id = id };
            var deletedSeries = await _seriesRepository.DeleteAsync(series); 

            if (!deletedSeries)
                return ApiResponse.NotFound("Series not found");

            return ApiResponse.Success(deletedSeries, "Series has been deleted");
        }
        catch (Exception ex)
        {
            return ApiResponse.Error("Failed to delete series", 500);
        }
    }
}
