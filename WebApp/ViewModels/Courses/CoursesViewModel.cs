using Infrastructure.Models;

namespace WebApp.ViewModels.Courses;

public class CoursesViewModel
{
    public IEnumerable<CategoryDto>? Categories { get; set; }
    public IEnumerable<CourseDto>? Courses { get; set; }
}
