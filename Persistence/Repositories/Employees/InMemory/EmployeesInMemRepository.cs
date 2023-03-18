using System.Linq;
using EMS.Models;
using EMS.Persistence.Repositories.Departments;

namespace EMS.Persistence.Repositories.Employees;

public class EmployeesInMemRepository : IEmployeesRepository
{
    private static List<Employee> _employees = new List<Employee>();
    private IDepartmentsRepository _departmentsRepository;

    public EmployeesInMemRepository(IDepartmentsRepository departmentsRepository)
    {
        _departmentsRepository = departmentsRepository;

        _employees.Add(new Employee
        {
            Id = Guid.NewGuid(),
            Name = "Raphael Ramirez",
            DateOfBirth = DateOnly.FromDateTime(new DateTime(1999, 5, 19)),
            Department = _departmentsRepository.GetAll().First(),
            Email = "raphaelisiah.ramirez@gmail.com",
            Phone = "09923355642"
        });
    }

    public Employee? Add(Employee entity)
    {
        entity.Id = Guid.NewGuid();
        _employees.Add(entity);

        return entity;
    }

    public Employee? Delete(Guid Id)
    {
        var employee = GetById(Id);
        if (employee is null)
        {
            return null;
        }

        _employees.Remove(employee);

        return employee;
    }

    public ICollection<Employee> GetAll()
    {
        return _employees.ToList();
    }

    public Employee? GetById(Guid Id)
    {
        return _employees.Find(employee => employee.Id == Id);
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
    }

}