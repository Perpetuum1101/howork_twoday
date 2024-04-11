using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data;
internal class IntegrationData
{
    public class CompanyStructure
    {
        public CEO CEO { get; set; }
        public Dictionary<string, Department> Departments { get; set; }
    }

    public class CEO
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Jobtitle { get; set; }
    }

    public class Department
    {
        public TopManager TopManager { get; set; }
        public Dictionary<string, Team> Teams { get; set; }
    }

    public class TopManager
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Jobtitle { get; set; }
    }

    public class Team
    {
        public TeamManager TeamManager { get; set; }
        public List<Employee> Employees { get; set; }
    }

    public class TeamManager
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Jobtitle { get; set; }
    }

    public class Employee
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Jobtitle { get; set; }
    }
}
