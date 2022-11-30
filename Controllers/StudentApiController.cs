using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcUniversity.Data;
using MvcUniversity.Models;

namespace MvcUniversity.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentApiController : ControllerBase
{
    private readonly UniversityContext _context;

    public StudentApiController(UniversityContext context)
    {
        _context = context;
    }

    // GET: api/StudentApi
    public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
    {
        // Get students
        var students = _context.Students
            .OrderBy(s => s.LastName)
            .ThenBy(s => s.FirstName);

        return await students.ToListAsync();
    }

    // GET: api/studentApi/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Student>> GetStudent(int id)
    {
        // Find student and related enrollments
        // SingleAsync() throws an exception if no student is found (which is possible, depending on id)
        // SingleOrDefaultAsync() is a safer choice here
        var student = await _context.Students
            .Where(s => s.Id == id)
            .Include(s => s.Enrollments)
            .SingleOrDefaultAsync();

        if (student == null)
            return NotFound();

        return student;
    }

    // POST: api/StudentApi
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Student>> PostStudent(Student student)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
    }

    // DELETE: api/StudentApi/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null)
            return NotFound();

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
