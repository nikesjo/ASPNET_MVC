using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
