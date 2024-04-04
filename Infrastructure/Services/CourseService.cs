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
                var result = JsonConvert.DeserializeObject<CourseResult>(await response.Content.ReadAsStringAsync());
                if (result != null && result.Succeeded)
                    return result.Courses ??= null!;
            }
        }
        catch (Exception ex) { }

        return null!;
    }
}
