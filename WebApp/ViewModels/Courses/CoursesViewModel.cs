using Infrastructure.Models;

namespace WebApp.ViewModels.Courses;

public class CoursesViewModel
{
    public IEnumerable<CategoryModel>? Categories { get; set; }
    public IEnumerable<CourseModel>? Courses { get; set; }
}
