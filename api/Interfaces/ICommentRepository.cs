using api.Models;
using API.Dtos;

namespace api.Interfaces;

public interface ICommentRepository
{
    public Task<List<Comment>> GetAllAsync();
    public Task<Comment?> GetByIdAsync(int id);
    public Task<Comment> CreateAsync(Comment comment);
    public Task<Comment?> UpdateAsync(int id, CreateCommentRequest commentDto);
    public Task<Comment?> DeleteAsync(int id);
} 