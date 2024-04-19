using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace Infrastructure.Services;

public class CourseService(HttpClient http, IConfiguration configuration, UserManager<UserEntity> userManager, DataContext context)
{
    private readonly HttpClient _http = http;
    private readonly IConfiguration _configuration = configuration;
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly DataContext _context = context;

    public async Task<CourseResult> GetCoursesAsync(HttpContext httpContext, string category = "", string searchQuery = "", int pageNumber = 1, int pageSize = 10)
    {
        try
        {
            var tokenResponse = await _http.SendAsync(new HttpRequestMessage
            {
                RequestUri = new Uri(_configuration["ApiUris:Auth"]!),
                Method = HttpMethod.Post
            });

            if (tokenResponse.IsSuccessStatusCode)
            {
                httpContext.Session.SetString("token", await tokenResponse.Content.ReadAsStringAsync());
            }

            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", httpContext.Session.GetString("token"));
            var response = await _http.GetAsync($"{_configuration["ApiUris:Courses"]}&category={Uri.UnescapeDataString(category)}&searchQuery={Uri.UnescapeDataString(searchQuery)}&pageNumber={pageNumber}&pageSize={pageSize}");
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<CourseResult>(await response.Content.ReadAsStringAsync());
                if (result != null && result.Succeeded)
                    return result;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }

        return null!;
    }

    public async Task<CourseDto> GetCourseAsync(int id)
    {
        var response = await _http.GetAsync($"{_configuration["ApiUris:SingleCourse"]}{id}?key={_configuration["ApiKey"]}");
        if (response.IsSuccessStatusCode)
        {
            var result = JsonConvert.DeserializeObject<CourseDto>(await response.Content.ReadAsStringAsync());
            if (result != null)
                return result;
        }

        return null!;
    }

    public async Task SaveCourseForUserAsync(int courseId, string userId)
    {
        try
        {
            // Hämta användaren
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                // Ta reda på om användaren redan har sparat kursen
                var savedCourse = await _context.SavedCourses.FirstOrDefaultAsync(x => x.UserId == userId && x.CourseId == courseId);

                if (savedCourse == null)
                {
                    // Om inte, skapa en ny sparad kurs och lägg till i databasen
                    savedCourse = new SavedCourseEntity
                    {
                        UserId = userId,
                        CourseId = courseId
                    };
                    _context.SavedCourses.Add(savedCourse);
                    await _context.SaveChangesAsync();
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error saving courseId {courseId} for userId {userId}: {ex.Message}");
        }
    }

    public async Task<List<CourseDto>> GetCoursesByIdsAsync(string userId)
    {
        var courses = new List<CourseDto>();

        try
        {
            var savedCourses = await _context.SavedCourses.Where(x => x.UserId == userId).ToListAsync();

            var courseIds = savedCourses.Select(x => x.CourseId).ToList();

            foreach (var id in courseIds)
            {
                var response = await _http.GetAsync($"{_configuration["ApiUris:SingleCourse"]}{id}?key={_configuration["ApiKey"]}");
                if (response.IsSuccessStatusCode)
                {
                    var courseJson = await response.Content.ReadAsStringAsync();
                    var course = JsonConvert.DeserializeObject<CourseDto>(courseJson);

                    if (course != null)
                    {
                        courses.Add(course);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error fetching saved courses for userId {userId}: {ex.Message}");
        }

        return courses;
    }

    //public async Task<CourseDto> GetCourseAsync(string id, HttpContext httpContext)
    //{
    //    if (httpContext.Request.Cookies.TryGetValue("AccessToken", out var token))
    //    {
    //        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

    //        var response = await _http.GetAsync($"{_configuration["ApiUris:Courses"]}{id}?key={_configuration["ApiKey"]}");
    //        if (response.IsSuccessStatusCode)
    //        {
    //            var result = JsonConvert.DeserializeObject<CourseDto>(await response.Content.ReadAsStringAsync());
    //            if (result != null)
    //                return result;
    //        }
    //    }

    //    return null!;
    //}

    //public async Task<SavedCourseModel> SaveCourseAsync(SavedCourseModel savedCourseModel)
    //{
    //    var savedCourses = await _context.SavedCourses.FindAsync(x => x.UserId == savedCourseModel.UserId, x => x.CourseId == savedCourseModel.CourseId);
    //    //httpContext.User.Identity.Id = userId;
    //    //httpContext.User.Identities.Equals(new[] { userId });
    //    //var user = await httpContext.User.FindFirst(userId);
    //    //var user = await _userManager.FindByIdAsync(userId);
    //    return null!;
    //}

    //public async Task<IEnumerable<CourseDto>> GetSavedCourseAsync(SavedCourseModel savedCourseModel)
    //{
    //    var userId = await _userManager.Users.AnyAsync(x => x.Id == savedCourseModel.UserId);
    //    var query = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)
    //    //var query = _context.SavedCourses.Include(x => x.UserId).AsQueryable();
    //    //query = query.OrderByDescending(o => o.CourseId);
    //    //IQueryable<SavedCourseModel> courses = await _context.SavedCourseModel;
    //    //var savedCourses = await _context.SavedCourses.FindAsync(x => x.UserId == savedCourseModel.UserId, x => x.CourseId == savedCourseModel.CourseId);
    //    //httpContext.User.Identity.Id = userId;
    //    //httpContext.User.Identities.Equals(new[] { userId });
    //    //var user = await httpContext.User.FindFirst(userId);
    //    //var user = await _userManager.FindByIdAsync(userId);
    //    return null!;
    //}
}
