using EMS.Models;
using Microsoft.EntityFrameworkCore;

namespace EMS.Persistence.Context;

public class ApplicationDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        string connectionString = @"Server=DESKTOP-DP6EMGM;Database=EMSDatabase;Integrated Security=True;";
        optionsBuilder.UseSqlServer(connectionString);

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