using api.Data;
using api.Interfaces.IRepositories;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository;

public class Repository<T> : IRepository<T> where T : class, IEntity
{
    protected readonly ApplicationDbContext _context;
    protected Repository(ApplicationDbContext context)
    {
        _context = context;
    }
    public virtual async Task<T?> GetByIdAsync(int id) => await _context.Set<T>().FindAsync(id);
    public virtual async Task<List<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();

    public virtual async Task<List<T>> GetAllByIdsAsync(IEnumerable<int> ids)
    {
        return await _context.Set<T>()
            .Where(g => ids.Contains(g.Id))
            .ToListAsync();
    }

    public virtual async Task<(List<T>, int)> GetAllPaginatedAsync(int page, int pageSize)
    {
        return (await _context.Set<T>()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(),
            _context.Set<T>().Count());
    }
    public virtual async Task<T?> AddAsync(T entity)
    {
        var entry = await _context.Set<T>().AddAsync(entity);
        var affected = await _context.SaveChangesAsync();

        return affected > 0 ? entry.Entity : null;
    }
    public virtual async Task<T?> UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        var affected = await _context.SaveChangesAsync();

        return affected > 0 ? entity : null;
    }
    public virtual async Task<bool> DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        return await _context.SaveChangesAsync() > 0;
    }
}
