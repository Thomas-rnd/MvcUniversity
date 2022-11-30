using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcUniversity.Models;

public class Instructor
{
    public int Id { get; set; }

    [Display(Name = "Last Name")]
    public string LastName { get; set; } = null!;

    [Display(Name = "First Name")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Hire Date"), DataType(DataType.Date)]
    public DateTime HireDate { get; set; }

    public List<Department> AdministeredDepartments { get; set; } = new();
    public List<Course> Courses { get; set; } = new();
}
