using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.ViewModels.Auth;

namespace WebApp.Controllers
{
    public class AuthController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager) : Controller
    {
        private readonly UserManager<UserEntity> _userManager = userManager;
        private readonly SignInManager<UserEntity> _signInManager = signInManager;


        #region Individual Account | Sign Up
        
        [HttpGet]
        [Route("/signup")]
        public IActionResult SignUp()
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Details", "Account");

            return View();
        }

        
        [HttpPost]
        [Route("/signup")]
        public async Task<IActionResult> SignUp(SignUpViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var exists = await _userManager.Users.AnyAsync(x => x.Email == viewModel.Form.Email);
                if (exists)
                {
                    ModelState.AddModelError("AlreadyExists", "An account with the same email address already exists");
                    ViewData["StatusMessage"] = "danger|An account with the same email address already exists";
                    return View(viewModel);
                }

                var userEntity = new UserEntity
                {
                    FirstName = viewModel.Form.FirstName,
                    LastName = viewModel.Form.LastName,
                    Email = viewModel.Form.Email,
                    UserName = viewModel.Form.Email
                };

                var result = await _userManager.CreateAsync(userEntity, viewModel.Form.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("SignIn", "Auth");
                }
            }

            return View(viewModel);
        }
        #endregion


        #region Individual Account | Sign In

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
            ViewData["StatusMessage"] = "danger|Incorrect email address or password";
            return View(viewModel);
        }
        #endregion


        #region Individual Account | Sign Out
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
