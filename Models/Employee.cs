using System.ComponentModel.DataAnnotations;
using EMS.Models.Interfaces;

namespace EMS.Models;

public class Employee : IEntity
{
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = default!;

    [Required]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; } = default!;

    [Required]
    public string Phone { get; set; } = default!;

    public Guid DepartmentId { get; set; }

    public Department Department { get; set; } = default!;
}