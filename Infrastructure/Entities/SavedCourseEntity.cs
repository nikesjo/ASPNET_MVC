using Infrastructure.Models;

namespace Infrastructure.Entities;

public class SavedCourseEntity
{
    public string UserId { get; set; } = null!;
    public int CourseId { get; set; }

    public static implicit operator SavedCourseEntity(SavedCourseModel model)
    {
        return new SavedCourseEntity
        {
            UserId = model.UserId!,
            CourseId = model.CourseId
        };
    }
}
