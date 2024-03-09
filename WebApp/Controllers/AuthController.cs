using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels.Views;

namespace WebApp.Controllers
{
    public class AuthController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager) : Controller
    {
        private readonly UserManager<UserEntity> _userManager = userManager;
        private readonly SignInManager<UserEntity> _signInManager = signInManager;


        #region Sign Up
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
        #endregion


        #region Sign In
        
        [HttpGet]
        [Route("/signin")]
        public IActionResult SignIn(string returnUrl)
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Details", "Account");

            ViewData["ReturnUrl"] = returnUrl ?? Url.Content("~/");
            return View();
        }

        
        [HttpPost]
        [Route("/signin")]
        public async Task<IActionResult> SignIn(SignInViewModel viewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(viewModel.Form.Email, viewModel.Form.Password, viewModel.Form.RememberMe, false);
                if (result.Succeeded)
                {
                    if(!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return RedirectToAction(returnUrl);

                    return RedirectToAction("Details", "Account");
                }
            }

            ModelState.AddModelError("IncorrectValues", "Incorrect email address or password");
            viewModel.ErrorMessage = "Incorrect email or password";
            return View(viewModel);
        }
        #endregion


        #region Sign Out
        [HttpGet]
        [Route("/signout")]
        public new async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}
