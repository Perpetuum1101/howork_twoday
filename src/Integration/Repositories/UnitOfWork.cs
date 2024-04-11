using Application.Services;

namespace Infrastructure.Repositories;

internal class UnitOfWork(DataContext context) : IUnitOfWork
{
    private readonly DataContext _context = context;

    public async Task Commit()
    {
        await _context.SaveChangesAsync();
    }
}
