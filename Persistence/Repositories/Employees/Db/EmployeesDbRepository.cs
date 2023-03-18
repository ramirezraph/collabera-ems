using EMS.Models;
using EMS.Persistence.Context;
using EMS.Persistence.Repositories.Departments;
using Microsoft.EntityFrameworkCore;

namespace EMS.Persistence.Repositories.Employees.Db;

public class EmployeesDbRepository : IEmployeesRepository
{
    private ApplicationDbContext _dbContext;
    private IDepartmentsRepository _departmentsRepository;

    public EmployeesDbRepository(ApplicationDbContext dbContext, IDepartmentsRepository departmentsRepository)
    {
        _dbContext = dbContext;
        _departmentsRepository = departmentsRepository;
    }

    public Employee? Add(Employee entity)
    {
        entity.Id = Guid.NewGuid();
        _dbContext.Employees.Add(entity);
        _dbContext.SaveChanges();
        return entity;
    }

    public Employee? Delete(Guid Id)
    {
        var employee = GetById(Id);
        if (employee is null)
        {
            return null;
        }

        _dbContext.Employees.Remove(employee);
        _dbContext.SaveChanges();
        return employee;
    }

    public IEnumerable<Employee> GetAll()
    {
        return _dbContext.Employees
            .AsNoTracking()
            .Include(e => e.Department)
            .ToList();
    }

    public Employee? GetById(Guid Id)
    {
        return _dbContext.Employees
            .Include(e => e.Department)
            .FirstOrDefault(employee => employee.Id == Id);
    }

    public Employee? Update(Guid Id, Employee updatedEntity)
    {
        var employee = GetById(Id);
        if (employee is null)
        {
            return null;
        }

        employee.Name = updatedEntity.Name;
        employee.DateOfBirth = updatedEntity.DateOfBirth;
        employee.Email = updatedEntity.Email;
        employee.Phone = updatedEntity.Phone;

        employee.Department = updatedEntity.Department;
        employee.DepartmentId = updatedEntity.DepartmentId;

        _dbContext.SaveChanges();

        return employee;
    }

    public void ChangeDepartment(Guid employeeId, Guid departmentId)
    {
        var employee = GetById(employeeId);
        if (employee is null)
        {
            throw new Exception("Employee does not exists.");
        }

        var department = _departmentsRepository.GetById(departmentId);
        if (department is null)
        {
            throw new Exception("Department does not exists.");
        }

        employee.Department = department;

        _dbContext.SaveChanges();
    }
}