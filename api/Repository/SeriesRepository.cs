using api.Data;
using api.Interfaces;
using api.Models;
using API.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace api.Repository;

public class SeriesRepository : ISeriesRepository{

    private readonly ApplicationDBContext _context;

    public SeriesRepository(ApplicationDBContext context){
        _context = context;
    }

    public async Task<List<Series>> GetAllAsync(){
        return await _context.Series.ToListAsync();
        // Include(c => c.Comments).
    }

    public async Task<Series?> GetByIdAsync(int id){
        return await _context.Series.FirstOrDefaultAsync(s => s.Id == id);
        // .Include(c => c.Comments)
    }

    public async Task<Series> CreateAsync(Series series){
        await _context.Series.AddAsync(series);
        await _context.SaveChangesAsync();
        return series;
    }

    public async Task<Series?> UpdateAsync(int id, UpdateSeriesRequest seriesDto){
        var existingSeries = await _context.Series.FirstOrDefaultAsync(x => x.Id == id);
        if (existingSeries == null){
            return null;
        }
        existingSeries.Title = seriesDto.Title;
        existingSeries.ImdbId = seriesDto.ImdbId;
        existingSeries.ReleaseYear = seriesDto.ReleaseYear;
        existingSeries.Description = seriesDto.Description;
        existingSeries.Genre = seriesDto.Genre;
        existingSeries.PosterUrl = seriesDto.PosterUrl;
        existingSeries.Rate = seriesDto.Rate;
        await _context.SaveChangesAsync();

        return existingSeries;
    }

    public async Task<Series?> DeleteAsync(int id ){
        var series = await _context.Series.FirstOrDefaultAsync(s => s.Id == id);
        if(series == null){
            return null;
        }
        _context.Series.Remove(series);
        await _context.SaveChangesAsync();

        return series;
    }

}