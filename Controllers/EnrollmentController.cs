using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcUniversity.Data;
using MvcUniversity.Models;

namespace MvcUniversity.Controllers;

public class EnrollmentController : Controller
{
    private readonly UniversityContext _context;

    public EnrollmentController(UniversityContext context)
    {
        _context = context;
    }

    // GET: Enrollment/Create?studentId=5
    public async Task<IActionResult> Create(int? studentId)
    {
        if (studentId == null)
        {
            return NotFound();
        }

        // Lookup student by id
        var student = await _context.Students.FindAsync(studentId);
        if (student == null)
        {
            return NotFound();
        }

        // Retrieve list of courses where student is already enrolled
        var studentCoursesQuery = from c in _context.Courses
                                  from e in c.Enrollments.Where(e => e.Student.Id == studentId)
                                  select c;

        // Retrieve courses not in the previous list
        // https://web.archive.org/web/20120321161927/https://www.programminglinq.com/blogs/marcorusso/archive/2008/01/14/the-not-in-clause-in-linq-to-sql.aspx
        var availableCoursesQuery = from c in _context.Courses
                                    where !(from c2 in studentCoursesQuery
                                            select c2.Id)
                                     .Contains(c.Id)
                                    select c;
        var availableCourses = availableCoursesQuery.ToList();

        ViewData["Student"] = student;
        ViewData["CourseId"] = new SelectList(availableCourses, "Id", "Title");
        return View();
    }

    // POST: Enrollment/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,CourseId,StudentId")] Enrollment enrollment)
    {
        // Lookup student and course
        var student = _context.Students.Find(enrollment.StudentId);
        var course = _context.Courses.Find(enrollment.CourseId);

        // Define student and course for new enrollment
        enrollment.Student = student!;
        enrollment.Course = course!;

        // Create new enrollment in DB
        _context.Add(enrollment);
        await _context.SaveChangesAsync();

        // Redirect to student details
        return RedirectToAction("Details", "Student", new RouteValueDictionary { { "id", enrollment.StudentId } });
    }
}
