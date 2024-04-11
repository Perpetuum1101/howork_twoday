using Application.Services;
using Application.Services.Contracts;
using Moq;
using Domain.Data.Entities;
using Application.Services.Rules;

namespace Tests;

public class ApprovalServiceTests
{
    private IApprovalService _approvalService;
    private Mock<IEmployeeRepo> _employeeRepo;
    private Mock<IInvoiceRepo> _invoiceRepo;
    private Mock<IDepartmentRepo> _departmentRepo;

    public ApprovalServiceTests()
    {
        _employeeRepo = new Mock<IEmployeeRepo>();
        _employeeRepo
            .Setup(x => x.GetManagerByDepartment("Accounting"))
            .ReturnsAsync(new Employee
            {
                Id = 12,
                DepartmentId = 7
            });
        _invoiceRepo = new Mock<IInvoiceRepo>();
        _departmentRepo = new Mock<IDepartmentRepo>();
        var unitOfWorkRepo = new Mock<IUnitOfWork>();
        unitOfWorkRepo.Setup(x => x.Commit());
        var rules = new List<IRule>
        {
            new ApproveByTeamManager(_employeeRepo.Object),
            new ApproveByDepartmentManger(_employeeRepo.Object),
            new ApproveByAccountingManager(_employeeRepo.Object),
        };
        _approvalService = new ApprovalService(
                               _departmentRepo.Object,
                               _employeeRepo.Object,
                               _invoiceRepo.Object,
                               rules,
                               unitOfWorkRepo.Object);
    }


    [Fact]
    public async void ShouldBeApprovedByDepartmentManagerWhenEmployeeFromSameDepartment()
    {
        const string departmentName = "Test2";
        const int invoiceEmployeeId = 2;
        const int departmentManagerId = 5;
        const int departmentId = 3;

        var invoice = new Invoice
        {
            Department = new Department { Name = departmentName },
            EmployeeId = invoiceEmployeeId
        };

        Setup(departmentName,
              invoiceEmployeeId,
              departmentManagerId,
              departmentId,
              departmentId,
              0);
        await _approvalService.Process(invoice);
        _invoiceRepo.Verify(ms => ms.Add(
                            It.Is<Invoice>(mo => mo.Approvers.Select(x => x.Id)
                                                             .Contains(departmentManagerId))),
                            Times.Once());
    }

    [Fact]
    public async void ShouldNotBeApprovedByDepartmentManagerWhenEmployeeIsManager()
    {
        const string departmentName = "Test2";
        const int invoiceEmployeeId = 5;
        const int departmentManagerId = 5;
        const int departmentId = 3;

        var invoice = new Invoice
        {
            Department = new Department { Name = departmentName },
            EmployeeId = invoiceEmployeeId
        };

        Setup(departmentName,
              invoiceEmployeeId,
              departmentManagerId,
              departmentId,
              departmentId,
              0);
        await _approvalService.Process(invoice);
        _invoiceRepo.Verify(ms => ms.Add(
                            It.Is<Invoice>(mo => !mo.Approvers.Select(x => x.Id)
                                                              .Contains(departmentManagerId))),
                            Times.Once());
    }

    [Fact]
    public async void ShouldBeApprovedByTeamManagerWhenEmployeeIsNotFromDepartment()
    {
        const string departmentName = "Test2";
        const int invoiceEmployeeId = 2;
        const int departmentManagerId = 5;
        const int departmentId = 3;
        const int employeeDepartmentId = 4;
        const int teamManagerId = 33;
        const int teamId = 99;

        var invoice = new Invoice
        {
            Department = new Department { Name = departmentName },
            EmployeeId = invoiceEmployeeId
        };

        Setup(departmentName,
              invoiceEmployeeId,
              departmentManagerId,
              departmentId,
              employeeDepartmentId,
              teamManagerId,
              teamId);

        await _approvalService.Process(invoice);
        _invoiceRepo.Verify(ms => ms.Add(
                            It.Is<Invoice>(mo => mo.Approvers.First().Id == teamManagerId)),
                            Times.Once());
    }

    [Fact]
    public async void ShouldNotBeApprovedByTeamManagerWhenEmployeeIsTeamManager()
    {
        const string departmentName = "Test2";
        const int invoiceEmployeeId = 2;
        const int departmentManagerId = 5;
        const int departmentId = 3;
        const int employeeDepartmentId = 4;
        const int teamManagerId = 2;

        var invoice = new Invoice
        {
            Department = new Department { Name = departmentName },
            EmployeeId = invoiceEmployeeId
        };

        Setup(departmentName,
              invoiceEmployeeId,
              departmentManagerId,
              departmentId,
              employeeDepartmentId,
              teamManagerId);

        await _approvalService.Process(invoice);
        _invoiceRepo.Verify(ms => ms.Add(
                            It.Is<Invoice>(mo => mo.Approvers.First().Id != teamManagerId)),
                            Times.Once());
    }

    [Fact]
    public async void ShouldBeApprovedByAccountingManager()
    {
        const string departmentName = "Test2";
        const int invoiceEmployeeId = 2;
        const int departmentManagerId = 5;
        const int departmentId = 3;
        const int employeeDepartmentId = 4;
        const int teamManagerId = 2;

        var invoice = new Invoice
        {
            Department = new Department { Name = departmentName },
            EmployeeId = invoiceEmployeeId
        };

        Setup(departmentName,
              invoiceEmployeeId,
              departmentManagerId,
              departmentId,
              employeeDepartmentId,
              teamManagerId);

        await _approvalService.Process(invoice);
        _invoiceRepo.Verify(ms => ms.Add(
                            It.Is<Invoice>(mo => mo.Approvers.Last().Id == 12)),
                            Times.Once());
    }

    [Fact]
    public async void ShouldNotBeApprovedByAccountingManagerIfEmployeeIsAccountingManger()
    {
        const string departmentName = "Test2";
        const int invoiceEmployeeId = 12;
        const int departmentManagerId = 5;
        const int departmentId = 3;
        const int employeeDepartmentId = 7;
        const int teamManagerId = 2;

        var invoice = new Invoice
        {
            Department = new Department { Name = departmentName },
            EmployeeId = invoiceEmployeeId
        };

        Setup(departmentName,
              invoiceEmployeeId,
              departmentManagerId,
              departmentId,
              employeeDepartmentId,
              teamManagerId);

        await _approvalService.Process(invoice);
        _invoiceRepo.Verify(ms => ms.Add(
                            It.Is<Invoice>(mo => mo.Approvers.Last().Id != 12)),
                            Times.Once());
    }

    [Fact]
    public async void ShouldNotBeApprovedByTeamManagerIfEmployeeIsAccountingManger()
    {
        const string departmentName = "Test2";
        const int invoiceEmployeeId = 2;
        const int departmentManagerId = 5;
        const int departmentId = 3;
        const int employeeDepartmentId = 7;
        const int teamManagerId = 8;

        var invoice = new Invoice
        {
            Department = new Department { Name = departmentName },
            EmployeeId = invoiceEmployeeId
        };

        Setup(departmentName,
              invoiceEmployeeId,
              departmentManagerId,
              departmentId,
              employeeDepartmentId,
              teamManagerId);

        await _approvalService.Process(invoice);
        _invoiceRepo.Verify(ms => ms.Add(
                            It.Is<Invoice>(mo => !mo.Approvers.Select(x => x.Id)
                                                              .Contains(teamManagerId))),
                            Times.Once());
    }


    private void Setup(string departmentName,
                       int invoiceEmployeeId,
                       int departmentManagerId,
                       int departmentId,
                       int employeeDepartmentId,
                       int teemManagerId,
                       int? teamId = null)
    {
        _employeeRepo.Setup(x => x.Get(invoiceEmployeeId)).ReturnsAsync(new Employee
        {
            Id = invoiceEmployeeId,
            Name = "TestEmployee",
            DepartmentId = employeeDepartmentId,
            TeamId = teamId
        });
        _employeeRepo.Setup(x => x.GetManagerByDepartment(departmentId))
                     .ReturnsAsync(new Employee
                     {
                         Id = departmentManagerId,
                         DepartmentId = departmentId
                     });
        _employeeRepo.Setup(x => x.GetManagerByDepartment(departmentName))
                     .ReturnsAsync(new Employee
                     {
                         Id = departmentManagerId,
                         DepartmentId = departmentId
                     });
        _employeeRepo.Setup(x => x.GetTeamManager(invoiceEmployeeId)).ReturnsAsync(new Employee
        {
            Id = teemManagerId,
            DepartmentId = employeeDepartmentId,
            Name = "TeamManager"
        });
        _departmentRepo.Setup(x => x.GetByName(departmentName)).ReturnsAsync(new Department
        {
            Id = departmentId,
            Name = departmentName,
        });
    }
}