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

    public async Task<List<Comment>> GetAllAsync()
    {
        return await _context.Comments
            .Include(c => c.User)
            .ToListAsync();
    }

    public async Task<List<Comment>> GetAllBySeriesIdAsync(int seriesId)
    {
        return await _context.Comments
            .Include(c => c.User).Where(c => c.ContentType == ContentTypeEnum.Series && c.ContentId == seriesId)
            .ToListAsync();
    }

        public async Task<List<Comment>> GetAllByMovieIdAsync(int movieId)
    {
        return await _context.Comments
            .Include(c => c.User).Where(c => c.ContentType == ContentTypeEnum.Movie && c.ContentId == movieId)
            .ToListAsync();
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
        return await _context.Comments
            .Include(c => c.User)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Comment> CreateAsync(Comment comment)
    {
        await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();
        return comment;
    }

    public async Task<Comment?> UpdateAsync(int id, CreateCommentRequest commentDto)
    {
        var existingComment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
        if (existingComment == null)
        {
            return null;
        }

        existingComment.Text = commentDto.Text;
        existingComment.LastUpdatedOn = DateTime.Now;
        await _context.SaveChangesAsync();

        return existingComment;
    }

    public async Task<Comment?> DeleteAsync(int id)
    {
        var comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
        if (comment == null)
        {
            return null;
        }

        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();

        return comment;
    }
} 