using Application.Services;
using Domain.Data.Entities;
using Infrastructure.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;
internal class DepartmentRepository(DataContext context) : Repository<Department>(context),
                                                           IDepartmentRepo
{
    
    public async Task<Department?> GetByName(string name)
    {
        var result = await _context.Set<Department>()
                                   .Where(x => x.Name == name)
                                   .FirstOrDefaultAsync();
        return result;
    }
}
