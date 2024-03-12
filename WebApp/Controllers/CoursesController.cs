using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[Authorize]
public class CoursesController : Controller
{
    [HttpGet]
    [Route("/courses")]
    public IActionResult Courses()
    {
        return View();
    }

    [HttpGet]
    [Route("/courses/singlecourse")]
    public IActionResult SingleCourse()
    {
        return View();
    }
}
