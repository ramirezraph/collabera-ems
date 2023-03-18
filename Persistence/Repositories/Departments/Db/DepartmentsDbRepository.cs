using EMS.Models;
using EMS.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EMS.Persistence.Repositories.Departments.Db;

public class DepartmentsDbRepository : IDepartmentsRepository
{

    private ApplicationDbContext _dbContext;

    public DepartmentsDbRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Department? Add(Department entity)
    {
        entity.Id = Guid.NewGuid();
        _dbContext.Departments.Add(entity);
        _dbContext.SaveChanges();

        return entity;
    }

    public Department? Delete(Guid Id)
    {
        var department = GetById(Id);

        if (department is null)
        {
            return null;
        }

        _dbContext.Departments.Remove(department);
        _dbContext.SaveChanges();
        return department;
    }

    public IEnumerable<Department> GetAll()
    {
        return _dbContext.Departments
            .AsNoTracking()
            .ToList();
    }

    public Department? GetById(Guid Id)
    {
        return _dbContext.Departments
            .FirstOrDefault(department => department.Id == Id);
    }

    public Department? Update(Guid Id, Department updatedEntity)
    {
        var department = GetById(Id);

        if (department is null)
        {
            return null;
        }

        department.Name = updatedEntity.Name;
        _dbContext.SaveChanges();

        return department;
    }
}