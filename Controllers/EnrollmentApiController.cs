using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcUniversity.Data;
using MvcUniversity.Models;

namespace MvcUniversity.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EnrollmentApiController : ControllerBase
{
    private readonly UniversityContext _context;

    public EnrollmentApiController(UniversityContext context)
    {
        _context = context;
    }

    // POST: api/EnrollmentApi
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Student>> PostEnrollment(EnrollmentDTO enrollmentDTO)
    {
        // Grade is deserialized as an int (0 => A, 1 => B, etc)
        Enrollment enrollment = new Enrollment(enrollmentDTO);

        // Lookup student and course
        var student = _context.Students.Find(enrollment.StudentId);
        var course = _context.Courses.Find(enrollment.CourseId);

        // Define student and course for new enrollment
        enrollment.Student = student!;
        enrollment.Course = course!;

        // Create new enrollment in DB
        _context.Enrollments.Add(enrollment);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(PostEnrollment), new { id = enrollment.Id }, enrollment);
    }

    // PUT: api/EnrollmentApi/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutEnrollment(int id, EnrollmentDTO enrollmentDTO)
    {
        if (id != enrollmentDTO.Id)
            return BadRequest();

        Enrollment enrollment = new Enrollment(enrollmentDTO);

        // Lookup student and course
        var student = _context.Students.Find(enrollment.StudentId);
        var course = _context.Courses.Find(enrollment.CourseId);

        // Define student and course for updated enrollment
        enrollment.Student = student!;
        enrollment.Course = course!;

        _context.Entry(enrollment).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Enrollments.Any(m => m.Id == id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }
}
