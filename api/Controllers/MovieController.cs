using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using api.Interfaces;
using api.Interfaces.IRepositories;
using api.Repository;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using api.Mappers;
using api.Helpers;

namespace api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MovieController : ControllerBase
{
    private readonly IMovieRepository _movieRepository;
    private readonly IGenreRepository _genreRepository;
    
    public MovieController(ApplicationDbContext context, IMovieRepository movieRepository, IGenreRepository genreRepository)
    {
        _movieRepository = movieRepository;
        _genreRepository = genreRepository;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        try
        {
            var movies = await _movieRepository.GetAllAsync();
            var movieDto = movies.Select(movie => movie.ToMovieDto()).ToList();
            return ApiResponse.Success(movieDto, "Movies retrieved successfully");
        }
        catch (Exception ex)
        {
            return ApiResponse.Error("Failed to retrieve movies", 500);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetail([FromRoute] int id)
    {
        try
        {
            var movie = await _movieRepository.GetByIdAsync(id);
            if (movie == null)
                return ApiResponse.NotFound("Movie not found");

            return ApiResponse.Success(movie.ToMovieDto(), "Movie retrieved successfully");
        }
        catch (Exception ex)
        {
            return ApiResponse.Error("Failed to retrieve movie", 500);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MovieRequestDto movieDto)
    {
        try
        {
            var genres = await _genreRepository.GetAllByIdsAsync(movieDto.GenreIds);
            if (genres.Count != movieDto.GenreIds.Count)
                return ApiResponse.NotFound("Invalid Genre ID");

            var movie = movieDto.ToMovieModel();
            movie.Genres = genres;
            var addedMovie = await _movieRepository.AddAsync(movie);
            
            if (addedMovie == null)
                return ApiResponse.Error("Failed to create the movie", 500);

            return ApiResponse.Success(addedMovie.ToMovieDto(), "Movie created successfully");
        }
        catch (Exception ex)
        {
            return ApiResponse.Error("Failed to create movie", 500);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] MovieRequestDto movieDto)
    {
        try
        {
            var genres = await _genreRepository.GetAllByIdsAsync(movieDto.GenreIds);
            if (genres.Count != movieDto.GenreIds.Count)
                return ApiResponse.NotFound("Invalid Genre ID");

            var movieToUpdate = movieDto.ToMovieModel();
            movieToUpdate.Id = id;
            movieToUpdate.Genres = genres;
            var updatedMovie = await _movieRepository.UpdateAsync(movieToUpdate);
            if (updatedMovie == null)
                return ApiResponse.NotFound("Movie not found");

            return ApiResponse.Success(updatedMovie.ToMovieDto(), "Movie updated successfully");
        }
        catch (Exception ex)
        {
            return ApiResponse.Error("Failed to update movie", 500);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            var movie = new Movie() { Id = id };
            var deletedMovie = await _movieRepository.DeleteAsync(movie); 

            if (!deletedMovie)
                return ApiResponse.NotFound("Movie not found");

            // return ApiResponse.Success(null, "Movie has been deleted");
            return ApiResponse.Success(deletedMovie, "Movie has been deleted");
        }
        catch (Exception ex)
        {
            return ApiResponse.Error("Failed to delete movie", 500);
        }
    }
}