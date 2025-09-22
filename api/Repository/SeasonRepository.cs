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
    public override async Task<bool> DeleteAsync(Season season)
    {
        // باز کردن تراکنش
        await using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            // پیدا کردن اپیزودها
            var episodes = await _context.Episodes
                .Where(e => e.SeasonId == season.Id)
                .ToListAsync();

            if (episodes.Any())
                _context.Episodes.RemoveRange(episodes);

            // پیدا کردن فصل (ممکنه قبلا پاک شده باشه)
            var entity = await _context.Seasons
                .FirstOrDefaultAsync(s => s.Id == season.Id);

            if (entity != null)
                _context.Seasons.Remove(entity);

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
