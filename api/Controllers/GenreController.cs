using Microsoft.AspNetCore.Mvc;
using api.Data;
using API.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using api.Interfaces;
using api.Repository;
using Microsoft.AspNetCore.Authorization;
using api.Mappers;

namespace api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenreController : ControllerBase
{

    private readonly IGenreRepository _genreRepository;
    public GenreController(ApplicationDBContext context)
    {
        _genreRepository = new GenreRepository(context);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var genres = await _genreRepository.GetAllAsync();
        var genreDto = genres.Select(genre => genre.ToGenreDto()).ToList();
        return Ok(genreDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetail([FromRoute] int id)
    {
        var genre = await _genreRepository.GetByIdAsync(id);
        if(genre == null)
            return NotFound();
        return Ok(genre.ToGenreDto());
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateGenreRequest genreDto){
        var genre = genreDto.ToGenreModel();
        await _genreRepository.CreateAsync(genre);
        return CreatedAtAction(nameof(GetDetail), new { id = genre.Id }, genre.ToGenreDto());
    }

    [Authorize(Roles = "Admin")]
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateGenreRequest genreDto){
        var genreModel = await _genreRepository.UpdateAsync(id, genreDto);
        if(genreModel == null)
            return NotFound();
        return Ok(genreModel.ToGenreDto());
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id ){
        var genreModel = await _genreRepository.DeleteAsync(id);
        if (genreModel == null)
            return NotFound();
        return Ok(genreModel.ToGenreDto());
    }
}