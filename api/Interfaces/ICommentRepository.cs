using api.Enums;
using api.Models;
using API.Dtos;

namespace api.Interfaces;

public interface ICommentRepository
{
    public Task<List<Comment>> GetContentCommentsAsync(int contentId, ContentTypeEnum contentTypeEnum);
    public Task<Comment?> GetByIdAsync(int id);
    public Task<Comment> CreateAsync(Comment comment);
    public Task<Comment?> UpdateAsync(int id, UpdateCommentRequest commentDto, string userId);
    public Task<Comment?> DeleteAsync(int id, string userId);
}
