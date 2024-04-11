using Application.Services.Contracts;
using Domain.Data.Entities;

namespace Application.Services.Rules;

public class ApproveByTeamManager(IEmployeeRepo employeeRepo) : IRule
{
    private readonly IEmployeeRepo _employeeRepo = employeeRepo;

    public async Task Apply(Invoice invoice)
    {
        var invoiceIsFromEmployeeDepartment = invoice.Employee.DepartmentId == invoice.DepartmentId;
        if (invoiceIsFromEmployeeDepartment)
        {
            return;
        }
        var employeeIsTeamMember = invoice.Employee.TeamId != null;
        if (!employeeIsTeamMember)
        {
            return;
        }
        var teamManager = await _employeeRepo.GetTeamManager(invoice.EmployeeId);
        if (teamManager == null)
        {
            return;
        }
        if (teamManager.Id != invoice.EmployeeId)
        {
            invoice.Approvers.Add(teamManager);
        }
    }
}
