using api.Data;
using api.Enums;
using api.Interfaces;
using api.Models;
using API.Dtos;
using api.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace api.Repository;

public class CommentRepository(ApplicationDbContext context) : Repository<Comment>(context), ICommentRepository
{
    public async Task<List<Comment>> GetMediaCommentsAsync(int mediaId)
    {
        return await _context.Comments
            .Where(c => c.MediaId == mediaId)
            .Include(c => c.User)
            .OrderByDescending(c => c.CreatedOn)
            .ToListAsync();
    }
    public override async Task<Comment?> AddAsync(Comment comment)
    {
        await _context.Comments.AddAsync(comment);
        var affected = await _context.SaveChangesAsync();
        var entity = await _context.Comments.Include(c => c.User).FirstOrDefaultAsync(c => c.Id == comment.Id);
        return affected > 0 ? entity : null;
    }
    public override async Task<Comment?> UpdateAsync(Comment comment)
    {
        _context.Comments.Update(comment);
        var affected = await _context.SaveChangesAsync();
        var entity = await _context.Comments.Include(c => c.User).FirstOrDefaultAsync(c => c.Id == comment.Id);
        return affected>0 ? comment : null;
    }
}
