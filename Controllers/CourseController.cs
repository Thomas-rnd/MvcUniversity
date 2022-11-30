using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcUniversity.Data;

namespace MvcUniversity.Controllers;

public class CourseController : Controller
{
    private readonly UniversityContext _context;

    public CourseController(UniversityContext context)
    {
        _context = context;
    }

    // GET: Course
    public async Task<IActionResult> Index()
    {
        var courses = await _context.Courses
            .Include(c => c.Department)
            .OrderBy(c => c.Id)
            .ToListAsync();

        return View(courses);
    }
}
