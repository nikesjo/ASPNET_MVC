namespace Infrastructure.Models;

public class CourseResult
{
    public bool Succeeded { get; set; }
    public IEnumerable<CourseModel>? Courses { get; set; }
}
