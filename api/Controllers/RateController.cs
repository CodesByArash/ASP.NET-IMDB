using System.Net.Mime;
using System.Security.Claims;
using api.Data;
using api.Dtos;
using api.Enums;
using api.Interfaces;
using api.Mappers;
using api.Models;
using api.Repository;
using API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class RateController : ControllerBase
{
    private readonly IRateRepository _rateRepository;
    private readonly IMovieRepository _movieRepository;
    private readonly ISeriesRepository _seriesRepository;

    public RateController(ApplicationDBContext context)
    {
        _rateRepository = new RateRepository(context);
        _movieRepository = new MovieRepository(context);
        _seriesRepository = new SeriesRepository(context);
    }

    [Authorize]
    [HttpPost("movie/{movieId}")]
    public async Task<IActionResult> AddMovieRate([FromRoute] int movieId, [FromBody] CreateRateDto rateDto)
    {      
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if(userId == null)
            return BadRequest("Invalid Credentials");
        var movie = await _movieRepository.GetByIdAsync(movieId);
        if (movie == null)
            return NotFound("Movie Not Found");
        var rate = RateMappers.ToRateModel(rateDto);
        rate.UserId = userId;
        rate.ContentId = movieId;
        rate.ContentType = ContentTypeEnum.Movie;
        var createdRate = await _rateRepository.CreateAsync(rate);

        return Ok(createdRate.ToRateDto());
    }

    [Authorize]
    [HttpPost("series/{seriesId}")]
    public async Task<IActionResult> AddSeriesRate([FromRoute] int seriesId, [FromBody] CreateRateDto rateDto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if(userId == null)
            return BadRequest("Invalid Credentials");
        var series = await _seriesRepository.GetByIdAsync(seriesId);
        if (series == null)
            return NotFound("Series Not Found");
        var rate = RateMappers.ToRateModel(rateDto);
        rate.UserId = userId;
        rate.ContentId = seriesId;
        rate.ContentType = ContentTypeEnum.Series;
        var createdRate = await _rateRepository.CreateAsync(rate);
        
        return Ok(createdRate.ToRateDto());
    }

    [HttpGet("movie/{movieId}")]
    public async Task<IActionResult> GetMovieRates([FromRoute] int movieId)
    {
        var rates = await _rateRepository.GetContentRatesAsync(movieId, api.Enums.ContentTypeEnum.Movie);
        var rateDtos = rates.Select(r => r.ToRateDto());
        return Ok(rateDtos);
    }

    [HttpGet("series/{seriesId}")]
    public async Task<IActionResult> GetSeriesRates([FromRoute]  int seriesId)
    {
        var rates = await _rateRepository.GetContentRatesAsync(seriesId, api.Enums.ContentTypeEnum.Series);
        var rateDtos = rates.Select(r => r.ToRateDto());
        return Ok(rateDtos);
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRate(int id, [FromBody] UpdateRateDto rateDto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if(userId == null)
            return BadRequest("Invalid Credentials");

        var rate = await _rateRepository.UpdateAsync(id, rateDto, userId);
        if (rate == null)
            return NotFound("Rate Not Found");

        return Ok(rate.ToRateDto());
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRate(int id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if(userId == null)
            return BadRequest("Invalid Credentials");

        var rate =  await _rateRepository.DeleteAsync(id, userId);
        if (rate == null)
            return NotFound("rate Not Found");
        return Ok(rate);
    }
}

