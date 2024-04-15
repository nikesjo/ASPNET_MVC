using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    public IActionResult AdminPortal()
    {
        return View();
    }
}
