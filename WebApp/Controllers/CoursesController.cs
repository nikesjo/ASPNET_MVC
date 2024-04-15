using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Security.Claims;
using WebApp.ViewModels.Courses;
using static System.Net.WebRequestMethods;

namespace WebApp.Controllers;

[Authorize]
public class CoursesController(CategoryService categoryService, CourseService courseService) : Controller
{
    private readonly CategoryService _categoryService = categoryService;
    private readonly CourseService _courseService = courseService;

    [HttpGet]
    [Route("/courses")]
    public async Task<IActionResult> Courses(string category = "", string searchQuery = "", int pageNumber = 1, int pageSize = 6)
    {
        var viewModel = new CoursesViewModel();
        try
        {
            var courseResult = await _courseService.GetCoursesAsync(HttpContext, category, searchQuery, pageNumber, pageSize);

            viewModel = new CoursesViewModel
            {
                Categories = await _categoryService.GetCategoriesAsync(HttpContext),
                Courses = courseResult.Courses,
                Pagination = new PaginationModel
                {
                    PageSize = pageSize,
                    CurrentPage = pageNumber,
                    TotalPages = courseResult.TotalPages,
                    TotalItems = courseResult.TotalItems
                }
            };

            var token = HttpContext.Session.GetString("token");

            return View(viewModel);


        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }

        return View(viewModel);
    }

    //[HttpPost]
    //public async Task<IActionResult> AddCourse(CourseDto dto)
    //{
    //    var course = new SavedCourseModel
    //    {
    //        CourseId = dto.Id,
    //        UserId = HttpContext.User.Id,
    //    };
    //    var userId = (HttpContext.Current.User.Identity as ClaimsIdentity).Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)
    //    //var userId = HttpContext.User.Identity;
    //    return View();
    //}

    [HttpGet("{id}")]
    [Route("/courses/{id}")]
    public async Task<IActionResult> SingleCourse(CourseDto courseDto, string id)
    {
        var viewModel = new SingleCourseViewModel();
        try
        {
            var courseResult = await _courseService.GetCourseAsync(id, HttpContext);

            viewModel = new SingleCourseViewModel
            {
                Course = courseDto,
            };


            return View(viewModel);


        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }

        return View(viewModel);
    }
}
