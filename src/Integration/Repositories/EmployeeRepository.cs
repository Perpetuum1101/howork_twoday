using Application.Services;
using Domain.Data.Entities;
using Domain.Data.Types;
using Infrastructure.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal class EmployeeRepository(DataContext context) : Repository<Employee>(context), 
                                                         IEmployeeRepo
{
    

    public async Task<Employee?> GetManagerByDepartment(string departmentName)
    {
        var result = await _context.Employees
            .Where(x => x.TeamId == null)
            .Where(x => x.Role == Role.Management)
            .Where(x => x.DepartmentId == _context.Departments
                                                  .Where(x => x.Name == departmentName)
                                                  .Select(x => x.Id)
                                                  .FirstOrDefault())
            .FirstOrDefaultAsync();

        return result;
    }

    public async Task<Employee?> GetManagerByDepartment(int departmentId)
    {
        var result = await _context.Employees
            .Where(x => x.TeamId == null)
            .Where(x => x.Role == Role.Management)
            .Where(x => x.DepartmentId == departmentId)
            .FirstOrDefaultAsync();

        return result;
    }

    public async Task<Employee?> GetTeamManager(int id)
    {
        var result = await _context.Employees
            .Where(e => e.Role == Role.Management)
            .Where(e => e.TeamId == context.Employees.Where(x => x.Id == id)
                                                     .Select(x => x.TeamId)
                                                     .FirstOrDefault())
            .FirstOrDefaultAsync();

        return result;
    }
}
