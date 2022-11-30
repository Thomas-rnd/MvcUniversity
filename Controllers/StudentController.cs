using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcUniversity.Data;
using MvcUniversity.Models;

namespace MvcUniversity.Controllers;

public class StudentController : Controller
{
    private readonly UniversityContext _context;

    public StudentController(UniversityContext context)
    {
        _context = context;
    }

    // GET: Student
    public async Task<IActionResult> Index()
    {
        var students = await _context.Students
            .OrderBy(s => s.LastName)
            .ThenBy(s => s.FirstName)
            .ToListAsync();

        return View(students);
    }

    // GET: Student/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        // Lookup student and associated enrollments
        var student = await _context.Students
            .Include(s => s.Enrollments)
            .ThenInclude(e => e.Course)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (student == null)
        {
            return NotFound();
        }

        return View(student);
    }
}
