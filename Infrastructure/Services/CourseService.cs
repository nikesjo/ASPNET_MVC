using Infrastructure.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Infrastructure.Services;

public class CourseService(HttpClient http, IConfiguration configuration)
{
    private readonly HttpClient _http = http;
    private readonly IConfiguration _configuration = configuration;

    public async Task<IEnumerable<CourseModel>> GetCoursesAsync()
    {
        try
        {
            var response = await _http.GetAsync(_configuration["ApiUris:Courses"]);
            if (response.IsSuccessStatusCode)
            {
                var courses = JsonConvert.DeserializeObject<IEnumerable<CourseModel>>(await response.Content.ReadAsStringAsync());
                return courses ??= null!;
            }
        }
        catch (Exception ex) { }

        return null!;
    }
}
