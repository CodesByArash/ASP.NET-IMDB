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
public class CastController : ControllerBase
{
    private readonly ICastRepository _castRepository;
    private readonly IMovieRepository _movieRepository;
    private readonly ISeriesRepository _seriesRepository;

    public CastController(ApplicationDBContext context)
    {
        _castRepository = new CastRepository(context);
        _movieRepository = new MovieRepository(context);
        _seriesRepository = new SeriesRepository(context);
    }

    [Authorize]
    [HttpPost("movie/{movieId}")]
    public async Task<IActionResult> AddMovieCast([FromRoute] int movieId, [FromBody] CreateCastRequest castDto)
    {
        var movie = await _movieRepository.GetByIdAsync(movieId);
        if (movie == null)
            return NotFound("Movie Not Found");
        var cast = CastMappers.ToCastModel(castDto);
        cast.PersonId = castDto.PersonId;
        cast.ContentId = movieId;
        cast.ContentType = ContentTypeEnum.Movie;
        var createdCast = await _castRepository.CreateAsync(cast);

        return Ok(createdCast.ToCastDto());
    }

    [Authorize]
    [HttpPost("series/{seriesId}")]
    public async Task<IActionResult> AddSeriesCast([FromRoute] int seriesId, [FromBody] CreateCastRequest castDto)
    {
        var series = await _seriesRepository.GetByIdAsync(seriesId);
        if (series == null)
            return NotFound("Series Not Found");
        var cast = CastMappers.ToCastModel(castDto);
        cast.PersonId = castDto.PersonId;
        cast.ContentId = seriesId;
        cast.ContentType = ContentTypeEnum.Series;
        var createdCast = await _castRepository.CreateAsync(cast);
        
        return Ok(createdCast.ToCastDto());
    }

    [HttpGet("movie/{movieId}")]
    public async Task<IActionResult> GetMovieCast([FromRoute] int movieId)
    {
        var casts = await _castRepository.GetContentCastAsync(movieId, ContentTypeEnum.Movie);
        var castDtos = casts.Select(c => c.ToCastDto());
        return Ok(castDtos);
    }

    [HttpGet("series/{seriesId}")]
    public async Task<IActionResult> GetSeriesCast([FromRoute]  int seriesId)
    {
        var casts = await _castRepository.GetContentCastAsync(seriesId, ContentTypeEnum.Series);
        var castDtos = casts.Select(c => c.ToCastDto());
        return Ok(castDtos);
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCast(int id, [FromBody] UpdateCastRequest castDto)
    {
        var cast = await _castRepository.UpdateAsync(id, castDto);
        if (cast == null)
            return NotFound("cast Not Found");

        return Ok(cast.ToCastDto());
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCast(int id)
    {
        var cast =  await _castRepository.DeleteAsync(id);
        if (cast == null)
            return NotFound("cast Not Found");
        return Ok(cast);
    }
}

