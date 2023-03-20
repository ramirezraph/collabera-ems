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
        var connectionString = _config.GetConnectionString("MSSQL");
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