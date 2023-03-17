using EMS.Models;

namespace EMS.Persistence.Repositories.Departments;

public class DepartmentsInMemRepository : IDepartmentsRepository
{
    private List<Department> _departments;

    public DepartmentsInMemRepository()
    {
        _departments = new List<Department>()
        {
            new Department { Id = Guid.NewGuid(), Name = "Admin" },
            new Department { Id = Guid.NewGuid(), Name = "IT" },
            new Department { Id = Guid.NewGuid(), Name = "HR" }
        };
    }

    public Department? Add(Department entity)
    {
        entity.Id = Guid.NewGuid();
        _departments.Add(entity);

        return entity;
    }

    public Department? Delete(Guid Id)
    {
        var department = GetById(Id);

        if (department is null)
        {
            return null;
        }

        _departments.Remove(department);
        return department;
    }

    public ICollection<Department> GetAll()
    {
        return _departments.ToList();
    }

    public Department? GetById(Guid Id)
    {
        return _departments.Find(department => department.Id == Id);
    }

    public Department? Update(Guid Id, Department updatedEntity)
    {
        var department = GetById(Id);

        if (department is null)
        {
            return null;
        }

        department.Name = updatedEntity.Name;

        return department;
    }
}