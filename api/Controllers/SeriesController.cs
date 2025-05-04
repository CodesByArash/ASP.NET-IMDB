using Microsoft.AspNetCore.Mvc;
using api.Data;
using API.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using api.Interfaces;
using api.Repository;

namespace api.Controllers;

[Route("api/Series")]
[ApiController]
public class SeriesController : ControllerBase
{

    private readonly ISeriesRepository _seriesRepository;
    private readonly ApplicationDBContext _context;
    public SeriesController(ApplicationDBContext context)
    {
        _seriesRepository = new SeriesRepository(context);
        _context = context;
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
        if(series == null)
            return NotFound();
        return Ok(series.ToSeriesDetailDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSeriesRequest seriesDto){
        var series = seriesDto.ToSeriesModel();
        await _seriesRepository.CreateAsync(series);
        return CreatedAtAction(nameof(GetDetail), new { id = series.Id }, series.ToSeriesDetailDto());
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateSeriesRequest seriesDto){
        var seriesModel = await _seriesRepository.UpdateAsync(id, seriesDto);
        if(seriesModel == null)
            return NotFound();
        return Ok(seriesModel.ToSeriesDetailDto());
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id ){
        var seriesModel = await _seriesRepository.DeleteAsync(id);
        if (seriesModel == null)
            return NotFound();
        return Ok(seriesModel.ToSeriesDto());
    }
}