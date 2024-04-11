using Domain.Data.Types;
using System.Text.Json;
using static Infrastructure.Data.IntegrationData;
using CTeam = Domain.Data.Entities.Team;
using CDepartment = Domain.Data.Entities.Department;
using CEmployee = Domain.Data.Entities.Employee;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

internal class Migration
{
    public static async Task Migrate(DataContext context)
    {
        var count = await context.Departments.CountAsync();
        if (count > 0)
        {
            return;
        }
        var structure = JsonSerializer.Deserialize<CompanyStructure>(Data.Data.JSON);
        if (structure == null)
        {
            return;
        }

        var ceo = new CEmployee
        {
            Id = int.Parse(structure.CEO.ID),
            JobTitle = structure.CEO.Jobtitle,
            Name = structure.CEO.Name,
            Surname = structure.CEO.Surname,
            Role = Role.Management
        };

        var company = new CDepartment
        {
            Name = "Company",
        };
        company.Employees.Add(ceo);
        MigrateDepartments(structure.Departments, company);
        context.Departments.Add(company);
        await context.SaveChangesAsync();
    }

    public static void MigrateDepartments(
                       Dictionary<string, Department> departments,
                       CDepartment parent)
    {
        foreach (var fromDepartment in departments)
        {
            var toDepartment = new CDepartment
            {
                ParentDepartment = parent,
                Name = fromDepartment.Key,
            };

            var topManger = fromDepartment.Value.TopManager;
            var departmentManger = MigrateDepartmentManager(topManger, toDepartment);
            toDepartment.Employees.Add(departmentManger);

            foreach (var fromTeam in fromDepartment.Value.Teams)
            {
                AddTeam(toDepartment, fromTeam);
            }

            parent.SubDepartments.Add(toDepartment);
        }
    }

    private static void AddTeam(CDepartment toDepartment, KeyValuePair<string, Team> fromTeam)
    {
        var toTeam = new CTeam
        {
            Name = fromTeam.Key,
            Department = toDepartment,
        };

        var manager = fromTeam.Value.TeamManager;
        var teamManager = MigrateTeamManger(toDepartment, manager, toTeam);
        toTeam.Employees.Add(teamManager);
        MigrateTeamEmployees(toDepartment, fromTeam, toTeam);
        toDepartment.Teams.Add(toTeam);
    }

    private static CEmployee MigrateDepartmentManager(
                             TopManager topManager,
                             CDepartment toDepartment)
    {
        var departmentManager = new CEmployee
        {
            Id = int.Parse(topManager.ID),
            JobTitle = topManager.Jobtitle,
            Name = topManager.Name,
            Surname = topManager.Surname,
            Role = Role.Management,
            Department = toDepartment
        };

        return departmentManager;
    }

    private static CEmployee MigrateTeamManger(
                             CDepartment toDepartment,
                             TeamManager teamManager,
                             CTeam toTeam)
    {
        return new CEmployee
        {
            Id = int.Parse(teamManager.ID),
            JobTitle = teamManager.Jobtitle,
            Name = teamManager.Name,
            Surname = teamManager.Surname,
            Department = toDepartment,
            Team = toTeam,
            Role = Role.Management
        };
    }

    private static void MigrateTeamEmployees(
                        CDepartment toDepartment,
                        KeyValuePair<string, Team> fromTeam,
                        CTeam toTeam)
    {
        foreach (var fromEmployee in fromTeam.Value.Employees)
        {
            var toEmployee = new CEmployee
            {
                Id = int.Parse(fromEmployee.ID),
                JobTitle = fromEmployee.Jobtitle,
                Name = fromEmployee.Name,
                Surname = fromEmployee.Surname,
                Role = Role.Regular,
                Department = toDepartment,
                Team = toTeam
            };

            toTeam.Employees.Add(toEmployee);
        }
    }
}
