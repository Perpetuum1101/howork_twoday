using Application.Services;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class Setup
{
    public static void SetupRepos(this IServiceCollection services)
    {
        services.AddDbContext<DataContext>();
        services.AddTransient<IEmployeeRepo, EmployeeRepository>();
        services.AddTransient<IDepartmentRepo, DepartmentRepository>();
        services.AddTransient<IInvoiceRepo, InvoiceRepository>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
    }

    public static async Task InitData(this IServiceProvider serviceProvider)
    {
        using (var serviceScope = serviceProvider.GetService<IServiceScopeFactory>()?.CreateScope())
        {
            var context = serviceScope?.ServiceProvider.GetRequiredService<DataContext>();
            context?.Database.EnsureCreated();
            if (context != null)
            {
                await Migration.Migrate(context);
            }
        }
    }
}
