using api.Models;
using API.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace api.Interfaces;

public interface IGenreRepository{
    public Task<List<Genre>> GetAllAsync();
    public Task<Genre?> GetByIdAsync(int id);
    public Task<Genre> CreateAsync(Genre genre);
    public Task<Genre?> UpdateAsync(int id, UpdateGenreRequest genreDto);
    public Task<Genre?> DeleteAsync(int id);
}