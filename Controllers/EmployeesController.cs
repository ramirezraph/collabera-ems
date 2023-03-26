using EMS.Models;
using EMS.Persistence.Repositories.Departments;
using EMS.Persistence.Repositories.Employees;
using Microsoft.AspNetCore.Mvc;

namespace EMS.Controllers;

public class EmployeesController : Controller
{

    // Dependency Injection
    private readonly IEmployeesRepository _employeesRepository;
    private readonly IDepartmentsRepository _departmentsRepository;

    public EmployeesController(IEmployeesRepository employeesRepository, IDepartmentsRepository departmentsRepository)
    {
        _employeesRepository = employeesRepository;
        _departmentsRepository = departmentsRepository;
    }

    public IActionResult Index()
    {
        var employees = _employeesRepository.GetAll();
        return View(employees);
    }

    [HttpGet]
    public IActionResult Create()
    {
        CreateEmployeeViewModel viewModel = new CreateEmployeeViewModel
        {
            Departments = _departmentsRepository.GetAll().ToList()
        };

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Create(CreateEmployeeViewModel viewModel)
    {
        viewModel.Departments = _departmentsRepository.GetAll().ToList();

        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        var selectedDepartment = _departmentsRepository.GetById(viewModel.NewEmployee.DepartmentId);
        if (selectedDepartment is null)
        {
            ViewBag.Error = "The selected department cannot be found";
            return View(viewModel);
        }

        var newEmployee = new Employee
        {
            Id = Guid.NewGuid(),
            Name = viewModel.NewEmployee.Name,
            DateOfBirth = viewModel.NewEmployee.DateOfBirth,
            Email = viewModel.NewEmployee.Email,
            Phone = viewModel.NewEmployee.Phone,
            DepartmentId = selectedDepartment.Id,
            Department = selectedDepartment
        };

        _employeesRepository.Add(newEmployee);

        return RedirectToAction("Index");
    }

    public IActionResult Details(Guid employeeId)
    {
        var employee = _employeesRepository.GetById(employeeId);
        return View(employee);
    }
    public IActionResult Delete(Guid employeeId)
    {
        _ = _employeesRepository.Delete(employeeId);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Update(Guid employeeId)
    {
        CreateEmployeeViewModel viewModel = new CreateEmployeeViewModel
        {
            Departments = _departmentsRepository.GetAll().ToList(),
            NewEmployee = new NewEmployeeFormModel(_employeesRepository.GetById(employeeId) ?? new Employee())
        };

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Update(CreateEmployeeViewModel viewModel)
    {
        viewModel.Departments = _departmentsRepository.GetAll().ToList();

        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        var selectedDepartment = _departmentsRepository.GetById(viewModel.NewEmployee.DepartmentId);
        if (selectedDepartment is null)
        {
            ViewBag.Error = "The selected department cannot be found";
            return View(viewModel);
        }

        var updatedEmployee = new Employee
        {
            Name = viewModel.NewEmployee.Name,
            DateOfBirth = viewModel.NewEmployee.DateOfBirth,
            Email = viewModel.NewEmployee.Email,
            Phone = viewModel.NewEmployee.Phone,
            Department = selectedDepartment,
            DepartmentId = selectedDepartment.Id
        };

        _ = _employeesRepository.Update(viewModel.NewEmployee.Id, updatedEmployee);

        return RedirectToAction("Index");
    }
}