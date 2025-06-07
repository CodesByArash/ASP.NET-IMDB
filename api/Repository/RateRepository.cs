using api.Data;
using api.Enums;
using api.Interfaces;
using api.Models;
using api.Dtos;
using Microsoft.EntityFrameworkCore;
using api.Mappers;

namespace api.Repository;

public class RateRepository : IRateRepository
{
    private readonly ApplicationDBContext _context;

    public RateRepository(ApplicationDBContext context)
    {
        _context = context;
    }

    public async Task<Rate> CreateAsync(Rate rate)
    {
        await _context.Rates.AddAsync(rate);
        await _context.SaveChangesAsync();
        
        return await _context.Rates.Include(r => r.User).FirstOrDefaultAsync(r => r.Id == rate.Id);
    }

    public async Task<Rate?> UpdateAsync(int id, UpdateRateDto rateDto, string userId)
    {
        var existingRate = await _context.Rates.Include(c => c.User).FirstOrDefaultAsync(x =>x.UserId == userId && x.Id == id);
        if (existingRate==null)
            return null;
        existingRate.Score = rateDto.Score;
        await _context.SaveChangesAsync();

        return existingRate;
    }

    public async Task<Rate?> DeleteAsync(int id, string userId)
    {
        var existingRate = await _context.Rates.FirstOrDefaultAsync(x =>x.UserId ==userId && x.Id == id);
        if (existingRate==null)
            return null;
        _context.Rates.Remove(existingRate);
        await _context.SaveChangesAsync();

        return existingRate;
    }

    public async Task<List<Rate>> GetContentRatesAsync(int contentId, ContentTypeEnum contentType)
    {
        return await _context.Rates
            .Where(r => r.ContentId == contentId && r.ContentType == contentType)
            .Include(r => r.User)
            .OrderByDescending(r => r.CreatedOn)
            .ToListAsync();
    }

    public async Task<Rate?> GetByIdAsync(int id)
    {
        return await _context.Rates.Include(r => r.User)
            .FirstOrDefaultAsync(r => r.Id == id);
    }
} 

