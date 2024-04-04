using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using WebApp.ViewModels.Courses;

namespace WebApp.Controllers;

[Authorize]
public class CoursesController(HttpClient http, CategoryService categoryService, CourseService courseService) : Controller
{
    private readonly HttpClient _http = http;
    private readonly CategoryService _categoryService = categoryService;
    private readonly CourseService _courseService = courseService;

    [HttpGet]
    [Route("/courses")]
    public async Task<IActionResult> Courses()
    {
        try
        {
            var tokenResponse = await _http.SendAsync(new HttpRequestMessage
            {
                RequestUri = new Uri("https://localhost:7091/api/auth"),
                Method = HttpMethod.Post
            });

            if (tokenResponse.IsSuccessStatusCode)
            {
                HttpContext.Session.SetString("token", await tokenResponse.Content.ReadAsStringAsync());
            }


            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

            var viewModel = new CoursesViewModel
            {
                Categories = await _categoryService.GetCategoriesAsync()
            };

            var response = await _http.GetAsync("https://localhost:7091/api/courses");
            if (response.IsSuccessStatusCode)
            {
               viewModel.Courses = JsonConvert.DeserializeObject<IEnumerable<CourseModel>>(await response.Content.ReadAsStringAsync())!;
                return View(viewModel);
            }
        }
        catch (Exception ex) { }
        return View();
    }

    [HttpGet]
    [Route("/courses/singlecourse")]
    public IActionResult SingleCourse()
    {
        return View();
    }
}
