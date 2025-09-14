using api.Data;
using api.Interfaces.IRepositories;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository;

public class EpisodeRepository(ApplicationDbContext context) : Repository<Episode>(context), IEpisodeRepository
{
    public override async Task<Episode?> GetByIdAsync(int id)
    {
        return await _context.Episodes
            .Include(e => e.Season)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public override async Task<List<Episode>> GetAllAsync()
    {
        return await _context.Episodes
            .Include(e => e.Season)
            .ToListAsync();
    }

    public override async Task<Episode?> AddAsync(Episode episode)
    {
        await _context.Episodes.AddAsync(episode);
        await _context.SaveChangesAsync();
        return await _context.Episodes
            .Include(e => e.Season)
            .FirstOrDefaultAsync(e => e.Id == episode.Id);
    }

    public override async Task<Episode?> UpdateAsync(Episode episode)
    {
        _context.Episodes.Update(episode);
        var affected = await _context.SaveChangesAsync();
        var entity = await _context.Episodes
            .Include(e => e.Season)
            .FirstOrDefaultAsync(e => e.Id == episode.Id);
        return affected > 0 ? entity : null;
    }
}
