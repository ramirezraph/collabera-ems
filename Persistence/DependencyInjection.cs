using EMS.Persistence.Context;
using EMS.Persistence.Repositories.Departments;
using EMS.Persistence.Repositories.Departments.Db;
using EMS.Persistence.Repositories.Employees;
using EMS.Persistence.Repositories.Employees.Db;
using Microsoft.EntityFrameworkCore;

namespace EMS.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var databaseName = configuration.GetConnectionString("Database");
        var server = configuration.GetConnectionString("Server");
        var username = configuration.GetConnectionString("Username");
        var password = configuration.GetConnectionString("Password");
        var connectionString = $@"Server={server};Database={databaseName};User Id={username};Password={password};Integrated Security=false;TrustServerCertificate=true";

        if (connectionString is null)
        {
            throw new ArgumentNullException(nameof(connectionString), "Connection string cannot be null");
        }

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        // In Memory Repositories
        // services.AddSingleton<IDepartmentsRepository, DepartmentsInMemRepository>();
        // services.AddSingleton<IEmployeesRepository, EmployeesInMemRepository>();

        // MSSQL Database Repositories
        // Dependency Inversion
        services.AddScoped<IDepartmentsRepository, DepartmentsDbRepository>();
        services.AddScoped<IEmployeesRepository, EmployeesDbRepository>();

        return services;
    }

    public static IServiceProvider UseAutoMigration(this IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            db.Database.Migrate();
        }

        return serviceProvider;
    }
}