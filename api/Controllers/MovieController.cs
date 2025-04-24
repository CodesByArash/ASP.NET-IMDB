using Microsoft.AspNetCore.Mvc;
using api.Data;
using API.Dtos;


namespace api.Controllers;

[Route("api/Movie")]
[ApiController]
public class MovieController : ControllerBase
{

    private readonly MovieDatabaseContext _context;
    public MovieController(MovieDatabaseContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public IActionResult GetList()
    {
        var movies = _context.Movies.Select(movie => movie.ToMovieDto()).ToList();
        return Ok(movies);
    }

    [HttpGet("{id}")]
    public IActionResult GetDetail([FromRoute] int id)
    {
        var movie = _context.Movies.Find(id)?.ToMovieDto();
        if(movie == null)
            return NotFound();
        return Ok(movie);
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateMovieRequest movieDto){
        var movie = movieDto.ToMovieModel();
        _context.Movies.Add(movie);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetDetail), new { id = movie.Id }, movie.ToMovieDto());
    }

    [HttpPut]
    [Route("{id}")]
    public IActionResult Update([FromRoute] int id, [FromBody] UpdateMovieRequest movieDto){
        var movieModel = _context.Movies.FirstOrDefault(x => x.Id == id);
        if(movieModel == null)
            return NotFound();
        movieModel.Title = movieDto.Title;
        movieModel.ReleaseYear = movieDto.ReleaseYear;
        movieModel.Description = movieDto.Description;
        movieModel.Duration = movieDto.Duration;
        movieModel.Genre = movieDto.Genre;
        movieModel.PosterUrl = movieDto.PosterUrl;
        movieModel.Rating = movieDto.Rating;
        _context.SaveChanges();
        return Ok(movieModel.ToMovieDto());
    }
}

   