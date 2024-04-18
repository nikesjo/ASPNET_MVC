using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using WebApp.ViewModels.Home;
using WebApp.ViewModels.Home.Partials;

namespace WebApp.Controllers
{
    public class HomeController(HttpClient httpClient, IConfiguration configuration) : Controller
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly IConfiguration _configuration = configuration;

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
                var response = await _httpClient.PostAsync($"https://localhost:7091/api/subscribers?key={_configuration["ApiKey"]}", content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["StatusMessage"] = "success|Your message was sent!";
                    return Ok();
                    //return Ok("You are now subscribed");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    return Conflict("You are already subscribed");
                }
            }
            else
            {
                //return BadRequest("Invalid email address");
                ModelState.AddModelError("IncorrectValues", "Incorrect email address or password");
                ViewData["StatusMessage"] = "danger|Incorrect email address or password";
            }

            return RedirectToAction("Index", "Home", "newsletter");
        }

        #endregion

        #region Contact
        [HttpGet]
        [Route("/contact")]
        public IActionResult Contact()
        {
            var viewModel = new ContactFormViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [Route("/contact")]
        public async Task<IActionResult> Contact(ContactFormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var content = new StringContent(JsonConvert.SerializeObject(viewModel), Encoding.UTF8, "application/json");
                    var response = await _httpClient.PostAsync($"https://localhost:7091/api/contact?key={_configuration["ApiKey"]}", content);
                    if (response.IsSuccessStatusCode)
                    {
                        ViewData["StatusMessage"] = "success|Your message was sent!";
                    }
                }
                catch
                {
                    ViewData["StatusMessage"] = "danger|Something went wrong.";
                }
            }
            else
            {
                ModelState.AddModelError("IncorrectValues", "Incorrect email address or password");
                ViewData["StatusMessage"] = "danger|Your message was not sent. Please enter fields properly.";
            }

            return View();
        }
        #endregion

        [Route("/error")]
        public IActionResult Error404(int statusCode) => View();

        [Route("/denied")]
        public IActionResult AccessDenied(int statusCode) => View();
    }
}
