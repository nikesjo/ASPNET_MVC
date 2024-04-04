using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using WebApp.ViewModels.Courses;

namespace WebApp.Controllers;

[Authorize]
public class CoursesController(HttpClient http, CategoryService categoryService, CourseService courseService, IConfiguration configuration) : Controller
{
    private readonly HttpClient _http = http;
    private readonly CategoryService _categoryService = categoryService;
    private readonly CourseService _courseService = courseService;
    private readonly IConfiguration _configuration = configuration;

    [HttpGet]
    [Route("/courses")]
    public async Task<IActionResult> Courses(string category = "", string searchQuery = "")
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
                HttpContext.Session.SetString("token", await tokenResponse.Content.ReadAsStringAsync());
            }


            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

            var viewModel = new CoursesViewModel
            {
                Categories = await _categoryService.GetCategoriesAsync(),
                Courses = await _courseService.GetCoursesAsync(category, searchQuery)
            };

            return View(viewModel);

            //var response = await _http.GetAsync(_configuration["ApiUris:Courses"]);
            //if (response.IsSuccessStatusCode)
            //{
            //    return View(viewModel);
            //}
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
