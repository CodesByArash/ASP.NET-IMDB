using api.Models;
using API.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace api.Interfaces;

public interface ISeriesRepository{
    
    public Task<List<Series>> GetAllAsync();

    public Task<Series?> GetByIdAsync(int id);

    public Task<Series> CreateAsync(Series series);

    public Task<Series?> UpdateAsync(int id, UpdateSeriesRequest seriesDto);

    public Task<Series?> DeleteAsync(int id);

}