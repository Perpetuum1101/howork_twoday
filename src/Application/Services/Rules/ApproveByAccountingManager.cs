using Application.Services.Contracts;
using Domain.Data.Entities;

namespace Application.Services.Rules;

public class ApproveByAccountingManager(IEmployeeRepo employeeRepo) : IRule
{
    private readonly IEmployeeRepo _employeeRepo = employeeRepo;

    public async Task Apply(Invoice invoice)
    {
        const string accountingDepartmentName = "Accounting";
        var accountingDepartmentManger = await _employeeRepo.GetManagerByDepartment(
                                                             accountingDepartmentName);
        if (accountingDepartmentManger == null)
        {
            return;
        }
        var employeeIsAccountingManager = accountingDepartmentManger.Id == invoice.EmployeeId;
        if (employeeIsAccountingManager)
        {
            return;
        }

        invoice.Approvers.Add(accountingDepartmentManger);
    }
}
