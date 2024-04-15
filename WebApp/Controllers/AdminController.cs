using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels.Home;

namespace WebApp.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    public IActionResult AdminPortal()
    {
        return View();
    }
}
