namespace MvcUniversity.Models;

// Data Transfer Object class, used to bypass navigation properties validation during API calls
public class EnrollmentDTO
{
    public int Id { get; set; }
    public Grade? Grade { get; set; }
    public int StudentId { get; set; }
    public int CourseId { get; set; }
}
