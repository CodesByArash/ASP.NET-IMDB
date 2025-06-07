using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using api.Interfaces;
using api.Repository;
using Microsoft.AspNetCore.Authorization;
using api.Mappers;
using API.Dtos;

namespace api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SeriesController : ControllerBase
{

    private readonly ISeriesRepository _seriesRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly IGenreRepository _genreRepository;

    public SeriesController(ApplicationDBContext context)
    {
        _seriesRepository = new SeriesRepository(context);
        _commentRepository = new CommentRepository(context);
        _genreRepository = new GenreRepository(context);
    }


    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var series = await _seriesRepository.GetAllAsync();
        var seriesDto = series.Select(s => s.ToSeriesDto()).ToList();
        return Ok(seriesDto);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetail([FromRoute] int id)
    {
        var series = await _seriesRepository.GetByIdAsync(id);
        if (series == null)
            return NotFound();
        series.Comments = await _commentRepository.GetContentCommentsAsync(series.Id, Enums.ContentTypeEnum.Series);
        return Ok(series.ToSeriesDetailDto());
    }


    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSeriesRequest seriesDto)
    {
        var genre = await _genreRepository.GetByIdAsync(seriesDto.GenreId);
        if (genre == null)
            return NotFound("Invalid Genre ID");
        var series = seriesDto.ToSeriesModel();
        await _seriesRepository.CreateAsync(series);
        return CreatedAtAction(nameof(GetDetail), new { id = series.Id }, series.ToSeriesDto());
    }


    [Authorize(Roles = "Admin")]
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateSeriesRequest seriesDto)
    {
        var genre = await _genreRepository.GetByIdAsync(seriesDto.GenreId);
        if (genre == null)
            return NotFound("Invalid Genre ID");
        var seriesModel = await _seriesRepository.UpdateAsync(id, seriesDto);
        if (seriesModel == null)
            return NotFound();
        return Ok(seriesModel.ToSeriesDetailDto());
    }


    [Authorize(Roles = "Admin")]
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var seriesModel = await _seriesRepository.DeleteAsync(id);
        if (seriesModel == null)
            return NotFound();
        return Ok(seriesModel.ToSeriesDto());
    }
}

