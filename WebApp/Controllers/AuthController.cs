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


        [Route("/signin")]
        [HttpGet]
        public IActionResult SignIn()
        {
            var viewModel = new SignInViewModel();
            return View(viewModel);
        }

        [Route("/signin")]
        [HttpPost]
        public IActionResult SignIn(SignInViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            //var result = _authService.SignIn(viewModel.Form);
            //if (result)
            //      return RedirectToAction("Account", "Details");

            viewModel.ErrorMessage = "Incorrect email or password";
            return View(viewModel);
        }
    }
}
