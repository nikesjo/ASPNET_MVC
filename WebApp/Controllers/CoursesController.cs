using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;
using WebApp.ViewModels.Courses;

namespace WebApp.Controllers;

[Authorize]
public class CoursesController(CategoryService categoryService, CourseService courseService) : Controller
{
    private readonly CategoryService _categoryService = categoryService;
    private readonly CourseService _courseService = courseService;

    [HttpGet]
    [Route("/courses")]
    public async Task<IActionResult> Courses(string category = "", string searchQuery = "")
    {
        var viewModel = new CoursesViewModel();
        try
        {
            viewModel = new CoursesViewModel
            {
                Categories = await _categoryService.GetCategoriesAsync(HttpContext),
                Courses = await _courseService.GetCoursesAsync(HttpContext, category, searchQuery)
            };

            var token = HttpContext.Session.GetString("token");

            return View(viewModel);


        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return View(viewModel);
    }

    [HttpGet]
    [Route("/courses/singlecourse")]
    public IActionResult SingleCourse()
    {
        return View();
    }
}
