using System.ComponentModel.DataAnnotations;

namespace MvcUniversity.Models;

public class Department
{
    public int Id { get; set; }

    [StringLength(50, MinimumLength = 3)]
    public string Name { get; set; } = null!;

    public int? InstructorId { get; set; }
    public Instructor? Administrator { get; set; }

    public List<Course> Courses { get; set; } = new();
}
