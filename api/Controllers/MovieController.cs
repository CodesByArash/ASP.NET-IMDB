using Microsoft.AspNetCore.Mvc;
using api.Data;
using API.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using api.Interfaces;
using api.Repository;
using api.Models;

namespace api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MovieController : ControllerBase
{

    private readonly IMovieRepository _movieRepository;
    private readonly ICommentRepository _commentRepository;

    private readonly IGenreRepository _genreRepository;
    public MovieController(ApplicationDBContext context)
    {
        _movieRepository = new MovieRepository(context);
        _commentRepository = new CommentRepository(context);
        _genreRepository = new GenreRepository(context);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var movies = await _movieRepository.GetAllAsync();
        var movieDto = movies.Select(movie => movie.ToMovieDto()).ToList();
        return Ok(movieDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetail([FromRoute] int id)
    {
        var movie = await _movieRepository.GetByIdAsync(id);
        if(movie == null)
            return NotFound();
        movie.Comments = await _commentRepository.GetContentCommentsAsync(movie.Id, Enums.ContentTypeEnum.Movie);
        return Ok(movie.ToMovieDetailDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMovieRequest movieDto){
        var genre =await _genreRepository.GetByIdAsync(movieDto.GenreId);
        if (genre == null)
            return NotFound("Invalid Genre ID");
        var movie = movieDto.ToMovieModel();
        await _movieRepository.CreateAsync(movie);
        return CreatedAtAction(nameof(GetDetail), new { id = movie.Id }, movie.ToMovieDto());
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateMovieRequest movieDto){
        var genre =await _genreRepository.GetByIdAsync(movieDto.GenreId);
        if (genre == null)
            return NotFound("Invalid Genre ID");
        var movieModel = await _movieRepository.UpdateAsync(id, movieDto);
        if(movieModel == null)
            return NotFound();
        return Ok(movieModel.ToMovieDto());
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id ){
        var movieModel = await _movieRepository.DeleteAsync(id);
        if (movieModel == null)
            return NotFound();
        return Ok(movieModel.ToMovieDto());
    }
}