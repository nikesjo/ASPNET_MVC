using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels.Home;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            var viewModel = new HomeIndexViewModel();
            ViewData["Title"] = viewModel.Title;

            return View(viewModel);
        }

        [HttpGet]
        [Route("/contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [Route("/contact")]
        public IActionResult Contact(ContactFormViewModel viewModel)
        {
            

            return View(viewModel);
        }


        [Route("/error")]
        public IActionResult Error404(int statusCode) => View();

        [Route("/denied")]
        public IActionResult AccessDenied(int statusCode) => View();
    }
}
