using api.Data;
using api.Enums;
using api.Interfaces;
using api.Models;
using API.Dtos;
using Microsoft.EntityFrameworkCore;

namespace api.Repository;

public class CommentRepository : ICommentRepository
{
    private readonly ApplicationDBContext _context;

    public CommentRepository(ApplicationDBContext context)
    {
        _context = context;
    }

    public async Task<Comment> CreateAsync(Comment comment)
    {
        await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();
        
        return await _context.Comments.Include(c => c.User).FirstOrDefaultAsync(c => c.Id == comment.Id);
    }

    public async Task<Comment?> UpdateAsync(int id, UpdateCommentRequest commentDto, string userId)
    {
        var existingComment = await _context.Comments.Include(c => c.User).FirstOrDefaultAsync(x =>x.UserId == userId && x.Id == id);
        if (existingComment==null)
            return null;
        existingComment.Text = commentDto.Text;
        existingComment.LastUpdatedOn = DateTime.Now;
        await _context.SaveChangesAsync();

        return existingComment;
    }

    public async Task<Comment?> DeleteAsync(int id, string userId)
    {
        var existingComment = await _context.Comments.FirstOrDefaultAsync(x =>x.UserId ==userId && x.Id == id);
        if (existingComment==null)
            return null;
        _context.Comments.Remove(existingComment);
        await _context.SaveChangesAsync();

        return existingComment;
    }

    public async Task<List<Comment>> GetContentCommentsAsync(int contentId, ContentTypeEnum contentType)
    {
        return await _context.Comments
            .Where(c => c.ContentId == contentId && c.ContentType == contentType)
            .Include(c => c.User)
            .OrderByDescending(c => c.CreatedOn)
            .ToListAsync();
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
        return await _context.Comments.Include(c => c.User)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
} 

