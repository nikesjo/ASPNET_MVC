using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
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

    //[HttpPost]
    //public async Task<IActionResult> AddCourse(SavedCourseModel model)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        try
    //        {
    //            var course = new SavedCourseEntity
    //            {
    //                UserId = model.UserId!,
    //                CourseId = model.CourseId
    //            };
    //            _context.SavedCourses.Add(course);
    //            await _context.SaveChangesAsync();
    //            return RedirectToAction("Courses", "Courses");
    //        }
    //        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }

    //    }

    //    return RedirectToAction("Courses", "Courses");
    //}

    [HttpPost]
    public async Task<IActionResult> AddCourse(int courseId)
    {
        // Hämta den inloggade användaren
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Unauthorized(); // Användaren är inte inloggad
        }

        // Skapa ett nytt SavedCourseEntity-objekt för att spara bokmärket
        var savedCourse = new SavedCourseEntity
        {
            UserId = user.Id,
            CourseId = courseId
        };

        // Spara bokmärket i databasen
        _context.SavedCourses.Add(savedCourse);
        await _context.SaveChangesAsync();

        return Ok();
    }

    //[HttpPost("{id}")]
    //public async Task<IActionResult> AddCourse(int id)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        try
    //        {
    //            var userId = HttpContext.User.Identities.FirstOrDefault();
    //            var savedCourse = new SavedCourseModel
    //            {
    //                CourseId = id,
    //                UserId = userId?.ToString(),
    //            };

    //            if (savedCourse != null)
    //            {
    //                await _context.SavedCourses.AddAsync(savedCourse);
    //                await _context.SaveChangesAsync();
    //            }

    //            return View();
    //        }
    //        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }

    //    }

    //    return View();
    //}
    //[HttpPost]
    //public async Task<IActionResult> AddCourse(CourseDto dto)
    //{
    //    var claims = HttpContext.User.Identities.FirstOrDefault();
    //    var course = new SavedCourseModel
    //    {
    //        CourseId = dto.Id,
    //        UserId = HttpContext.User.Id,
    //    };
    //    var userId = (HttpContext.Current.User.Identity as ClaimsIdentity).Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)
    //    //var userId = HttpContext.User.Identity;
    //    return View();
    //}
}
