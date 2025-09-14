using api.Data;
using api.Enums;
using api.Interfaces;
using api.Models;
using api.Dtos;
using api.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using api.Mappers;

namespace api.Repository;

public class RateRepository :Repository<Rate>, IRateRepository
{
    public RateRepository(ApplicationDbContext context):base(context)
    {
    }
    public async Task<List<Rate>> GetMediaRatesAsync(int mediaId)
    {
        return await _context.Rates
            .Where(c => c.MediaId == mediaId)
            .Include(c => c.User)
            .OrderByDescending(c => c.CreatedOn)
            .ToListAsync();
    }
    public override async Task<Rate?> AddAsync(Rate rate)
    {
        await _context.Rates.AddAsync(rate);
        var affected = await _context.SaveChangesAsync();
        var entity = await _context.Rates.Include(c => c.User).FirstOrDefaultAsync(c => c.Id == rate.Id);
        return affected > 0 ? entity : null;
    }
    public override async Task<Rate?> UpdateAsync(Rate rate)
    {
        _context.Rates.Update(rate);
        var affected = await _context.SaveChangesAsync();
        var entity = await _context.Comments.Include(c => c.User).FirstOrDefaultAsync(c => c.Id == rate.Id);
        return affected>0 ? rate : null;
    }
} 

