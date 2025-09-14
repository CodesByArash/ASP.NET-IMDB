using api.Data;
using api.Enums;
using api.Interfaces;
using api.Models;
using api.Dtos;
using api.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using api.Mappers;

namespace api.Repository;

public class CastRepository :Repository<Cast>, ICastRepository
{
    public CastRepository(ApplicationDbContext context):base(context)
    {
    }
    public async Task<List<Cast>> GetMediaCastAsync(int mediaId)
    {
        return await _context.Cast
            .Where(c => c.MediaId == mediaId)
            .Include(c => c.Person)
            .ToListAsync();
    }
    public async Task<Cast?> GetByIdAsync(int id)
    {
        return await _context.Cast.Include(c => c.Person)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public override async Task<Cast?> AddAsync(Cast cast)
    {
        await _context.Cast.AddAsync(cast);
        await _context.SaveChangesAsync();
        return await _context.Cast.Include(r => r.Person).FirstOrDefaultAsync(r => r.Id == cast.Id);
    }

    public async Task<Cast?> UpdateAsync(Cast cast)
    {
        _context.Cast.Update(cast);
        var affected = await _context.SaveChangesAsync();
        var entity = await _context.Cast.Include(r => r.Person).FirstOrDefaultAsync(r => r.Id == cast.Id);
        return affected>0 ? cast : null;
    }

    public async Task<Cast?> DeleteAsync(int id)
    {
        var existingCast = await _context.Cast.FirstOrDefaultAsync(x => x.Id == id);
        if (existingCast==null)
            return null;
        _context.Cast.Remove(existingCast);
        await _context.SaveChangesAsync();

        return existingCast;
    }

} 

