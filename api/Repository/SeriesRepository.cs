using api.Data;
using api.Enums;
using api.Interfaces;
using api.Models;
using API.Dtos;
using api.Interfaces.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace api.Repository;

public class SeriesRepository(ApplicationDbContext context) : Repository<Series>(context), ISeriesRepository
{
    // public async Task<List<Series>> GetAllAsync(){
    //     return await _context.Series.Include(m=>m.Genre).ToListAsync();
    //     // Include(c => c.Comments).
    // }

    // public async Task<Series?> GetByIdAsync(int id){
    //     return await _context.Series.Include(m=>m.Genre).FirstOrDefaultAsync(s => s.Id == id);
    //     // .Include(c => c.Comments)
    // }

    // public async Task<Series> CreateAsync(Series series){
    //     await _context.Series.AddAsync(series);
    //     await _context.SaveChangesAsync();
    //     return series;
    // }

    // public async Task<Series?> UpdateAsync(int id, UpdateSeriesRequest seriesDto){
    //     var existingSeries = await _context.Series.FirstOrDefaultAsync(x => x.Id == id);
    //     if (existingSeries == null){
    //         return null;
    //     }
    //     existingSeries.Title = seriesDto.Title;
    //     existingSeries.ImdbId = seriesDto.ImdbId;
    //     existingSeries.ReleaseYear = seriesDto.ReleaseYear;
    //     existingSeries.Description = seriesDto.Description;
    //     existingSeries.GenreId = seriesDto.GenreId;
    //     existingSeries.PosterUrl = seriesDto.PosterUrl;
    //     existingSeries.Rate = seriesDto.Rate;
    //     await _context.SaveChangesAsync();
    //
    //     return existingSeries;
    // }

    public override async Task<bool> DeleteAsync(Series series)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var entity = await _context.Series
                .Include(s => s.Seasons)
                .ThenInclude(se => se.Episodes)
                .FirstOrDefaultAsync(s => s.Id == series.Id);

            if (entity == null) return false;

            var allEpisodes = entity.Seasons.SelectMany(se => se.Episodes).ToList();
            if (allEpisodes.Any()) _context.Episodes.RemoveRange(allEpisodes);

            if (entity.Seasons.Any()) _context.Seasons.RemoveRange(entity.Seasons);

            _context.Series.Remove(entity);

            var affected = await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return affected > 0;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}