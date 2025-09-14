using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using API.Dtos;
using api.Interfaces;
using api.Interfaces.IRepositories;
using api.Repository;
using Microsoft.AspNetCore.Authorization;
using api.Mappers;
using api.Models;
using api.Helpers;

namespace api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenreController : ControllerBase
{

    private readonly IGenreRepository _genreRepository;
    public GenreController(ApplicationDbContext context)
    {
        _genreRepository = new GenreRepository(context);
    }
    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        try
        {
            var genres = await _genreRepository.GetAllAsync();
            var genreDto = genres.Select(genre => genre.ToGenreDto()).ToList();
            return ApiResponse.Success(genreDto, "Genres retrieved successfully");
        }
        catch (Exception ex)
        {
            return ApiResponse.Error("Failed to retrieve genres", 500);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetail([FromRoute] int id)
    {
        try
        {
            var genre = await _genreRepository.GetByIdAsync(id);
            if(genre == null)
                return ApiResponse.NotFound("Genre not found");
            
            return ApiResponse.Success(genre.ToGenreDto(), "Genre retrieved successfully");
        }
        catch (Exception ex)
        {
            return ApiResponse.Error("Failed to retrieve genre", 500);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] GenreRequestDto genreDto)
    {
        try
        {
            var genre = genreDto.ToGenreModel();
            genre = await _genreRepository.AddAsync(genre);
            if (genre == null)
                return ApiResponse.Error("Failed to create genre", 500);
            
            return ApiResponse.Success(genre.ToGenreDto(), "Genre created successfully");
        }
        catch (Exception ex)
        {
            return ApiResponse.Error("Failed to create genre", 500);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] GenreRequestDto genreDto)
    {
        try
        {
            var genre = new Genre()
            {
                Id = id,
                Title = genreDto.Title
            };

            genre = await _genreRepository.UpdateAsync(genre);
            if (genre == null)
                return ApiResponse.NotFound("Genre not found");

            return ApiResponse.Success(genre.ToGenreDto(), "Genre updated successfully");
        }
        catch (Exception ex)
        {
            return ApiResponse.Error("Failed to update genre", 500);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            var genre = new Genre() { Id = id };
            var isRemoved = await _genreRepository.DeleteAsync(genre);
            if (!isRemoved)
                return ApiResponse.NotFound("Genre not found or could not be deleted");
            
            return ApiResponse.Success(isRemoved, "Genre has been deleted successfully");
        }
        catch (Exception ex)
        {
            return ApiResponse.Error("Failed to delete genre", 500);
        }
    }
}