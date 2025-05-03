using api.Models;
using API.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace api.Interfaces;

public interface IMovieRepository{
    
    public Task<List<Movie>> GetAllAsync();

    public Task<Movie?> GetByIdAsync(int id);

    public Task<Movie> CreateAsync(Movie movie);

    public Task<Movie?> UpdateAsync(int id, UpdateMovieRequest movieDto);

    public Task<Movie?> DeleteAsync(int id);

}