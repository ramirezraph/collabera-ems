using EMS.Persistence.Context;
using EMS.Persistence.Repositories.Departments;
using EMS.Persistence.Repositories.Departments.Db;
using EMS.Persistence.Repositories.Employees;
using EMS.Persistence.Repositories.Employees.Db;

namespace EMS.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>();

        // In Memory Repositories
        // services.AddSingleton<IDepartmentsRepository, DepartmentsInMemRepository>();
        // services.AddSingleton<IEmployeesRepository, EmployeesInMemRepository>();

        // MSSQL Database Repositories
        services.AddScoped<IDepartmentsRepository, DepartmentsDbRepository>();
        services.AddScoped<IEmployeesRepository, EmployeesDbRepository>();

        return services;
    }
}