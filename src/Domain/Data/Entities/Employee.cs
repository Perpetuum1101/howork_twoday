using Domain.Data.Entities.Abstraction;
using Domain.Data.Types;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Data.Entities;

public class Employee : Entity
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public override int Id { get => base.Id; set => base.Id = value; }
    public string JobTitle { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public int? TeamId { get; set; }
    public int DepartmentId { get; set; }
    public Role Role { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is Employee employee && Id == employee.Id;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id);
    }

    public virtual Team? Team { get; set; }
    public virtual Department Department { get; set; } = null!;
    public virtual List<Invoice> Invoices { get; set; } = [];
    public virtual List<Invoice> ApprovedInvoices { get; set; } = [];
}
