using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class AuthController : Controller
    {
        [Route("/signup")]
        public IActionResult SignUp()
        {
            return View();
        }
    }
}
