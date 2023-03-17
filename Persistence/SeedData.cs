using EMS.Models;
using Microsoft.EntityFrameworkCore;

namespace EMS.Persistence;

public static class SeedData
{
    public static void SeedAdminData(this ModelBuilder modelBuilder)
    {
        var adminDepartment = new Department { Id = Guid.NewGuid(), Name = "Admin" };
        modelBuilder.Entity<Department>().HasData(adminDepartment);

        modelBuilder.Entity<Employee>().HasData(
            new Employee
            {
                Id = Guid.NewGuid(),
                Name = "Raphael Ramirez",
                DateOfBirth = DateOnly.FromDateTime(new DateTime(1999, 5, 19)),
                Department = adminDepartment,
                Email = "raphaelisiah.ramirez@gmail.com",
                Phone = "09923355642"
            }
        );
    }
}