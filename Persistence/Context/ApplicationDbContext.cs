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