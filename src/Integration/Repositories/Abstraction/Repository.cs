using Domain.Data.Entities.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Abstraction;

internal abstract class Repository<T>(DataContext context) where T : Entity
{
    protected readonly DataContext _context = context;
    public void Add(T entity)
    {
        _context.Add(entity);
    }

    public async Task<T?> Get(int id)
    {
        var result = await _context.Set<T>().FindAsync(id);
        return result;
    }

    public virtual async Task<List<T>> GetAll()
    {
        var result = await _context.Set<T>().ToListAsync();
        return result;
    }
}
