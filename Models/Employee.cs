using System.ComponentModel.DataAnnotations;
using EMS.Models.Interfaces;

namespace EMS.Models;

public class Employee : IEntity
{
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = default!;

    [Required]
    public DateOnly DateOfBirth { get; set; }

    [Required]
    public string Email { get; set; } = default!;

    [Required]
    public string Phone { get; set; } = default!;

    public Department Department { get; set; } = default!;
}