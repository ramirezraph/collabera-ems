using EMS.Models;
using EMS.Persistence.Repositories.Departments;
using Microsoft.AspNetCore.Mvc;

namespace EMS.Controllers;

public class DepartmentsController : Controller
{
    private IDepartmentsRepository _departmentsRepository;

    public DepartmentsController(IDepartmentsRepository departmentsRepository)
    {
        _departmentsRepository = departmentsRepository;
    }

    public IActionResult Index()
    {
        var departments = _departmentsRepository.GetAll();
        return View(departments);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Department newDepartment)
    {
        if (ModelState.IsValid)
        {
            _ = _departmentsRepository.Add(newDepartment);
            return RedirectToAction("Index");
        }
        return View();
    }

    public IActionResult Details(Guid departmentId)
    {
        var department = _departmentsRepository.GetById(departmentId);
        return View(department);
    }
    public IActionResult Delete(Guid departmentId)
    {
        _ = _departmentsRepository.Delete(departmentId);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Update(Guid departmentId)
    {
        var department = _departmentsRepository.GetById(departmentId);
        return View(department);
    }

    [HttpPost]
    public IActionResult Update(Department updatedDepartment)
    {
        _ = _departmentsRepository.Update(updatedDepartment.Id, updatedDepartment);
        return RedirectToAction("Index");
    }
}