using Domain.Data.Entities.Abstraction;

namespace Domain.Data.Entities;

public class Department : Entity
{
    public string Name { get; set; } = null!;
    public int? ParentDepartmentId { get; set; }

    public virtual Department? ParentDepartment { get; set; }
    public virtual List<Department> SubDepartments { get; set; } = [];
    public virtual List<Employee> Employees { get; set; } = [];
    public virtual List<Team> Teams { get; set; } = [];
}
