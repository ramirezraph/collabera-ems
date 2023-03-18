using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EMS.Models;

public class NewEmployeeFormModel
{
    public Guid Id { get; set; } = Guid.Empty;

    [Required]
    public string Name { get; set; } = default!;

    [Required(ErrorMessage = "Please select a date.")]
    [DataType(DataType.Date, ErrorMessage = "Please select a date.")]
    public DateOnly DateOfBirth { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; } = default!;

    [Required]
    public string Phone { get; set; } = default!;

    [DisplayName("Department")]
    [Required(ErrorMessage = "Please select a department.")]
    public Guid DepartmentId { get; set; }

    public NewEmployeeFormModel()
    { }

    public NewEmployeeFormModel(string name, DateOnly dateOfBirth, string email, string phone, Guid departmentId)
    {
        Name = name;
        DateOfBirth = dateOfBirth;
        Email = email;
        Phone = phone;
        DepartmentId = departmentId;
    }

    public NewEmployeeFormModel(Employee employeeModel)
    {
        Id = employeeModel.Id;
        Name = employeeModel.Name ?? "";
        DateOfBirth = employeeModel.DateOfBirth;
        Email = employeeModel.Email;
        Phone = employeeModel.Phone;
        DepartmentId = employeeModel.DepartmentId;
    }
}

public class CreateEmployeeViewModel
{
    public List<Department> Departments { get; set; } = new List<Department>();
    public NewEmployeeFormModel NewEmployee { get; set; } = default!;
}