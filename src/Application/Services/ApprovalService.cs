using Application.Services.Contracts;
using Domain.Data.Entities;

namespace Application.Services;

public class ApprovalService(
             IDepartmentRepo departmentRepo,
             IEmployeeRepo employeeRepo,
             IInvoiceRepo invoiceRepo,
             IEnumerable<IRule> rules,
             IUnitOfWork unitOfWork) : IApprovalService
{
    private readonly IInvoiceRepo _invoiceRepo = invoiceRepo;
    private readonly IDepartmentRepo _departmentRepo = departmentRepo;
    private readonly IEmployeeRepo _employeeRepo = employeeRepo;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IEnumerable<IRule> _rules = rules;

    public async Task Process(Invoice invoice)
    {
        var employee = await _employeeRepo.Get(invoice.EmployeeId);
        if (employee == null)
        {
            return;
        }

        invoice.Employee = employee;
        var invoiceDepartment = await _departmentRepo.GetByName(invoice.Department.Name);
        if (invoiceDepartment == null)
        {
            return;
        }
        invoice.DepartmentId = invoiceDepartment.Id;
        invoice.Department = invoiceDepartment;
        foreach(var rule in _rules)
        {
            await rule.Apply(invoice);
        }

        _invoiceRepo.Add(invoice);
        await _unitOfWork.Commit();
    }

    public async Task<List<string>> GetAll()
    {
        var invoices = await _invoiceRepo.GetAll();
        var result = new List<string>();
        foreach(var invoice in invoices)
        {
            var approversStrings = invoice.Approvers.Select(x => $"{x.Name} {x.Surname} ({x.JobTitle})");
            var approversList = string.Join(", ", approversStrings);
            var record = $"Invoice No[{invoice.Number}] pending approval by: {approversList}";
            result.Add(record);
        }

        return result;
    }
}

public interface IDepartmentRepo : IRepo<Department>
{
    Task<Department?> GetByName(string name);
}

public interface IEmployeeRepo : IRepo<Employee>
{
    Task<Employee?> GetManagerByDepartment(string departmentName);
    Task<Employee?> GetManagerByDepartment(int departmentId);
    Task<Employee?> GetTeamManager(int id);
}

public interface IInvoiceRepo : IRepo<Invoice>
{

}

public interface IUnitOfWork
{
    Task Commit();
}

public interface IRepo<T>
{
    Task<T?> Get(int id);
    Task<List<T>> GetAll();
    void Add(T entity);
}
