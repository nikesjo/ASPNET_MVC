using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WebApp.ViewModels.Home;
using WebApp.ViewModels.Home.Partials;

namespace WebApp.Controllers
{
    public class HomeController(HttpClient httpClient) : Controller
    {
        private readonly HttpClient _httpClient = httpClient;

        [Route("/")]
        public IActionResult Index()
        {
            var viewModel = new HomeIndexViewModel();
            ViewData["Title"] = viewModel.Title;

            return View(viewModel);
        }

        #region Subscribe
        [HttpPost]
        public async Task<IActionResult> Subscribe(NewsletterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(viewModel), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("https://localhost:7091/api/subscribers", content);
                if (response.IsSuccessStatusCode)
                {
                    return Ok("You are now subscribed");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    return Conflict("You are already subscribed");
                }
            }
            else
            {
                return BadRequest("Invalid email address");
            }

            return RedirectToAction("Home", "Index", "newsletter");
        }

        #endregion

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
