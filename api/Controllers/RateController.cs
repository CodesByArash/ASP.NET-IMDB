using System.Net.Mime;
using System.Security.Claims;
using api.Data;
using api.Dtos;
using api.Enums;
using api.Interfaces;
using api.Mappers;
using api.Models;
using api.Repository;
using api.Interfaces.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using api.Helpers;

[ApiController]
[Route("api/[controller]")]
public class RateController : ControllerBase
{
    private readonly IRateRepository _rateRepository;
    private readonly IMediaRepository _mediaRepository;
    public RateController(IRateRepository rateRepository, IMediaRepository mediaRepository)
    {
        _rateRepository = rateRepository;
        _mediaRepository = mediaRepository;
    }
    [HttpGet("media/{mediaId}")]
    public async Task<IActionResult> GetMovieRates([FromRoute] int mediaId)
    {
        try
        {
            var rates = await _rateRepository.GetMediaRatesAsync(mediaId);
            var rateDtos = rates.Select(r => r.ToRateDto());
            return ApiResponse.Success(rateDtos, "Rates retrieved successfully");
        }
        catch (Exception ex)
        {
            return ApiResponse.Error("Failed to retrieve rates", 500);
        }
    }

    [Authorize]
    [HttpPost("media/{mediaId}")]
    public async Task<IActionResult> AddMediaRate([FromRoute] int mediaId, [FromBody] RateRequestDto rateDto)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(userId == null)
                return ApiResponse.BadRequest("Invalid Credentials");
            
            var media = await _mediaRepository.GetByIdAsync(mediaId);
            if (media == null)
                return ApiResponse.NotFound("Media Not Found");
            
            var rate = rateDto.ToRateModel();
            rate.UserId = userId;
            rate.MediaId = mediaId;
            var createdRate = await _rateRepository.AddAsync(rate);
            if (createdRate == null)
            {
                return ApiResponse.Error("Failed to create rate", 500);
            }
            return ApiResponse.Success(createdRate.ToRateDto(), "Rate added successfully");
        }
        catch (Exception ex)
        {
            return ApiResponse.Error("Failed to add rate", 500);
        }
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRate(int id, [FromBody] RateRequestDto rateDto)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(userId == null)
                return ApiResponse.Unauthorized("Invalid Credentials");
            
            var rate = await _rateRepository.GetByIdAsync(id);
            if(rate == null)
                return ApiResponse.NotFound("Rate Not Found");
            
            var isAllowed = rate.UserId == userId;
            if (!isAllowed)
                return ApiResponse.Unauthorized("Invalid Credentials");
            
            rate.Score = rateDto.Score;
            rate.LastUpdatedOn = DateTime.UtcNow;
            var updatedRate = await _rateRepository.UpdateAsync(rate);
            if (updatedRate == null)
                return ApiResponse.NotFound("Rate Not Found");
            
            return ApiResponse.Success(rate.ToRateDto(), "Rate updated successfully");
        }
        catch (Exception ex)
        {
            return ApiResponse.Error("Failed to update rate", 500);
        }
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRate(int id)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(userId == null)
                return ApiResponse.BadRequest("Invalid Credentials");
            
            var rate = new Rate() { Id = id };
            var isRemoved = await _rateRepository.DeleteAsync(rate);
            if (!isRemoved)
                return ApiResponse.NotFound("Rate Not Found or Could not be Updated");
            
            return ApiResponse.Success(isRemoved, "Rate deleted successfully");
        }
        catch (Exception ex)
        {
            return ApiResponse.Error("Failed to delete rate", 500);
        }
    }
}

