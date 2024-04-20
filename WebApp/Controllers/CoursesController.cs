using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using System.Text;
using WebApp.ViewModels.Courses;

namespace WebApp.Controllers;

[Authorize]
public class CoursesController(CategoryService categoryService, CourseService courseService, HttpClient httpClient, DataContext context, UserManager<UserEntity> userManager) : Controller
{
    private readonly CategoryService _categoryService = categoryService;
    private readonly CourseService _courseService = courseService;
    private readonly HttpClient _httpClient = httpClient;
    private readonly DataContext _context = context;
    private readonly UserManager<UserEntity> _userManager = userManager;

    #region GET
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

    [HttpGet("{id}")]
    [Route("/courses/{id}")]
    public async Task<IActionResult> SingleCourse(int id)
    {
        try
        {
            var courseResult = await _courseService.GetCourseAsync(id);
            if (courseResult != null)
            {
                var viewModel = new SingleCourseViewModel
                {
                    Id = courseResult.Id,
                    Title = courseResult.Title,
                    Author = courseResult.Author,
                    OriginalPrice = courseResult.OriginalPrice,
                    DiscountPrice = courseResult.DiscountPrice,
                    Hours = courseResult.Hours,
                    IsBestSeller = courseResult.IsBestSeller,
                    LikesInProcent = courseResult.LikesInProcent,
                    LikesInNumbers = courseResult.LikesInNumbers,
                    ImageUrl = courseResult.ImageUrl,
                    AuthorImageUrl = courseResult.AuthorImageUrl
                };
                return View(viewModel);
            }

        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }

        return RedirectToAction("Error404", "Home");
    }
    #endregion

    [HttpPost]
    [Route("/courses/savecourse/{courseId}")]
    public async Task<IActionResult> SaveCourse(int courseId)
    {
        try
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var existingSavedCourse = await _context.SavedCourses.FirstOrDefaultAsync(x => x.UserId == user.Id && x.CourseId == courseId);

            if (existingSavedCourse != null)
            {
                return Json(new { success = false, message = "Course is already saved." });
            }

            var savedCourse = new SavedCourseEntity
            {
                UserId = user.Id,
                CourseId = courseId
            };

            if (savedCourse != null)
            {
                _context.SavedCourses.Add(savedCourse!);
                await _context.SaveChangesAsync();

                return Json(new { success = true });
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return Problem();
    }

    //[HttpPost]
    //public async Task<IActionResult> SaveCourse([FromBody] SaveCourseDto saveCourseDto)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        try
    //        {
    //            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    //            if (userId != null && saveCourseDto.CourseId != 0)
    //            {
    //                await _courseService.SaveCourseForUserAsync(saveCourseDto.CourseId, userId);

    //                return Json(new { success = true });
    //            }
    //        }
    //        catch (Exception)
    //        {
    //            return Json(new { success = false });
    //        }
    //    }

    //    return Json(new { success = false });
    //}
}
