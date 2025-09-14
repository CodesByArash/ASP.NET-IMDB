using api.Data;
using api.Interfaces.IRepositories;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository;

public class SeasonRepository(ApplicationDbContext context) : Repository<Season>(context), ISeasonRepository
{
    public override async Task<Season?> GetByIdAsync(int id)
    {
        return await _context.Seasons
            .Include(s => s.Episodes)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public override async Task<List<Season>> GetAllAsync()
    {
        return await _context.Seasons
            .Include(s => s.Episodes)
            .ToListAsync();
    }

    public override async Task<Season?> AddAsync(Season season)
    {
        await _context.Seasons.AddAsync(season);
        await _context.SaveChangesAsync();
        return await _context.Seasons
            .Include(s => s.Episodes)
            .FirstOrDefaultAsync(s => s.Id == season.Id);
    }

    public override async Task<Season?> UpdateAsync(Season season)
    {
        _context.Seasons.Update(season);
        var affected = await _context.SaveChangesAsync();
        var entity = await _context.Seasons
            .Include(s => s.Episodes)
            .FirstOrDefaultAsync(s => s.Id == season.Id);
        return affected > 0 ? entity : null;
    }
}
