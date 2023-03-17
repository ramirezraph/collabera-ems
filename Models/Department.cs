using System.ComponentModel.DataAnnotations;
using EMS.Models.Interfaces;

namespace EMS.Models;

public class Department : IEntity
{
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = default!;
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}