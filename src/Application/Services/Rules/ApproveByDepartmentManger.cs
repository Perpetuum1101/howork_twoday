using Application.Services.Contracts;
using Domain.Data.Entities;

namespace Application.Services.Rules;

public class ApproveByDepartmentManger(IEmployeeRepo employeeRepo) : IRule
{
    private readonly IEmployeeRepo _employeeRepo = employeeRepo;

    public async Task Apply(Invoice invoice)
    {
        var manger = await _employeeRepo.GetManagerByDepartment(invoice.DepartmentId);
        if (manger == null)
        {
            return;
        }

        var employeeIsManager = manger.Id == invoice.EmployeeId;
        if (employeeIsManager)
        {
            return;
        }

        invoice.Approvers.Add(manger);
    }
}
