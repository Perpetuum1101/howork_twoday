using Domain.Data.Entities.Abstraction;

namespace Domain.Data.Entities;

public class Invoice : Entity
{
    public int EmployeeId { get; set; }
    public string Number { get; set; } = null!;
    public int DepartmentId { get; set; }
    public DateTime Date { get; set; }
    public float TotalAmount { get; set; }
    public DateTime PaymentDeadline { get; set; }
    public string DocumentLink { get; set; } = null!;
    public float VatRate { get; set; }
    public float VatAmount { get; set; }

    public virtual Department Department { get; set; } = null!;
    public virtual Employee Employee { get; set; } = null!;
    public virtual HashSet<Employee> Approvers { get; set; } = [];
}
