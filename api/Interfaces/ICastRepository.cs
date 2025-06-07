using api.Enums;
using api.Models;
using api.Dtos;

namespace api.Interfaces;

public interface ICastRepository
{
    public Task<List<Cast>> GetContentCastAsync(int contentId, ContentTypeEnum contentTypeEnum);
    public Task<Cast?> GetByIdAsync(int id);
    public Task<Cast> CreateAsync(Cast cast);
    public Task<Cast?> UpdateAsync(int id, UpdateCastRequest castDto);
    public Task<Cast?> DeleteAsync(int id);
}
