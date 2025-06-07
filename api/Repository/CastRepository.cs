using api.Data;
using api.Enums;
using api.Interfaces;
using api.Models;
using api.Dtos;
using Microsoft.EntityFrameworkCore;
using api.Mappers;

namespace api.Repository;

public class CastRepository : ICastRepository
{
    private readonly ApplicationDBContext _context;

    public CastRepository(ApplicationDBContext context)
    {
        _context = context;
    }

    public async Task<Cast> CreateAsync(Cast cast)
    {
        await _context.Cast.AddAsync(cast);
        await _context.SaveChangesAsync();
        
        return await _context.Cast.Include(r => r.Person).FirstOrDefaultAsync(r => r.Id == cast.Id);
    }

    public async Task<Cast?> UpdateAsync(int id, UpdateCastRequest castDto)
    {
        var existingCast = await _context.Cast.FirstOrDefaultAsync(x => x.Id == id);
        if (existingCast==null)
            return null;
        existingCast.Role = castDto.Role;
        existingCast.ContentId = castDto.ContentId;
        existingCast.ContentType = castDto.ContentType;
        existingCast.PersonId = castDto.PersonId;
        
        await _context.SaveChangesAsync();

        return existingCast;
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

    public async Task<List<Cast>> GetContentCastAsync(int contentId, ContentTypeEnum contentType)
    {
        return await _context.Cast
            .Where(c => c.ContentId == contentId && c.ContentType == contentType)
            .Include(c => c.Person)
            .ToListAsync();
    }

    public async Task<Cast?> GetByIdAsync(int id)
    {
        return await _context.Cast.Include(c => c.Person)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
} 

