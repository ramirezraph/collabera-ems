using EMS.Models;
using Microsoft.EntityFrameworkCore;

namespace EMS.Persistence.Context;

public class ApplicationDbContext : DbContext
{
    private IConfiguration _config;

    public ApplicationDbContext(IConfiguration config)
    {
        _config = config;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var databaseName = _config.GetConnectionString("Database");
        var server = _config.GetConnectionString("Server");
        var username = _config.GetConnectionString("Username");
        var password = _config.GetConnectionString("Password");
        var connectionString = $@"Server={server};Database={databaseName};User Id={username};Password={password};Integrated Security=false;TrustServerCertificate=true";
        if (connectionString is not null)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<Department> Departments { get; set; } = null!;
    public DbSet<Employee> Employees { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.SeedAdminData();

        base.OnModelCreating(modelBuilder);
    }
}