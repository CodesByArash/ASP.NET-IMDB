using api.Enums;
using api.Models;
using api.Dtos;

namespace api.Interfaces;

public interface IRateRepository
{
    public Task<List<Rate>> GetContentRatesAsync(int contentId, ContentTypeEnum contentTypeEnum);
    public Task<Rate?> GetByIdAsync(int id);
    public Task<Rate> CreateAsync(Rate rate);
    public Task<Rate?> UpdateAsync(int id, UpdateRateDto rateDto, string userId);
    public Task<Rate?> DeleteAsync(int id, string userId);
}
