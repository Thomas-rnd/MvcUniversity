namespace MvcUniversity.Models;

// Grade obtained in an enrollment
public enum Grade
{
    A, B, C, D, F
}

public class Enrollment
{
    public int Id { get; set; }

    public Grade? Grade { get; set; }

    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public Student Student { get; set; } = null!;
    public Course Course { get; set; } = null!;

    // Default (empty) constructor
    public Enrollment() { }

    // Copy constructor
    public Enrollment(EnrollmentDTO dto)
    {
        // Copy DTO field values
        Id = dto.Id;
        StudentId = dto.StudentId;
        CourseId = dto.CourseId;
        Grade = dto.Grade;
    }

    public override string ToString()
    {
        return $"Id: {Id}, Grade: {Grade}";
    }
}
