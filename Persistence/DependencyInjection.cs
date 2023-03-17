using EMS.Persistence.Context;
using EMS.Persistence.Repositories.Departments;
using EMS.Persistence.Repositories.Employees;

namespace EMS.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>();

        // In Memory Repositories
        services.AddSingleton<IDepartmentsRepository, DepartmentsInMemRepository>();
        services.AddSingleton<IEmployeesRepository, EmployeesInMemRepository>();

        return services;
    }
}