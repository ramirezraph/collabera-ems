using EMS.Models;

namespace EMS.Persistence.Repositories.Employees;

public interface IEmployeesRepository : IRepository<Employee>
{
    void ChangeDepartment(Guid employeeId, Guid departmentId);
}