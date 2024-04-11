using Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class DataContext : DbContext
{
    public DbSet<Department> Departments { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Invoice> Invoices { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
       => options.EnableSensitiveDataLogging().UseSqlite($"Data Source=data.db");

public DataContext() { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Department>()
                    .HasMany(e => e.SubDepartments)
                    .WithOne(e => e.ParentDepartment)
                    .HasForeignKey(e => e.ParentDepartmentId);

        modelBuilder.Entity<Department>()
                    .HasMany(e => e.Teams)
                    .WithOne(e => e.Department)
                    .HasForeignKey(e => e.DepartmentId)
                    .IsRequired();

        modelBuilder.Entity<Department>()
                    .HasMany(e => e.Employees)
                    .WithOne(e => e.Department)
                    .HasForeignKey(e => e.DepartmentId);

        modelBuilder.Entity<Team>()
                    .HasMany(e => e.Employees)
                    .WithOne(e => e.Team)
                    .HasForeignKey(e => e.TeamId);

        modelBuilder.Entity<Invoice>()
                    .HasMany(e => e.Approvers)
                    .WithMany(e => e.ApprovedInvoices)
                    .UsingEntity<InvoiceApprovers>(
                       l => l.HasOne<Employee>().WithMany().HasForeignKey(e => e.ApproverId),
                       r => r.HasOne<Invoice>().WithMany().HasForeignKey(e => e.InvoiceId));

        modelBuilder.Entity<Employee>()
                    .HasMany(e => e.Invoices)
                    .WithOne(e => e.Employee)
                    .HasForeignKey(e => e.EmployeeId);
    }
}
