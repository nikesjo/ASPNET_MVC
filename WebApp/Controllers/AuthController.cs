using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels.Views;

namespace WebApp.Controllers
{
    public class AuthController : Controller
    {
        [Route("/signup")]
        [HttpGet]
        public IActionResult SignUp()
        {
            var viewModel = new SignUpViewModel();
            return View(viewModel);
        }

        [Route("/signup")]
        [HttpPost]
        public IActionResult SignUp(SignUpViewModel viewmodel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewmodel);
            }
            return RedirectToAction("SignIn", "Auth");
        }
    }
}
