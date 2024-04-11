using Domain.Data.Entities.Abstraction;

namespace Domain.Data.Entities;

public class Team : Entity
{
    public string Name { get; set; } = null!;
    public int DepartmentId { get; set; }

    public virtual Department Department { get; set; } = null!;
    public virtual List<Employee> Employees { get; set; } = [];
}
