using BookStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            string host = Request.Headers["Host"].ToString();
            string cookie = Request.Headers["Cookie"].ToString();
            HttpContext.Session.SetString("Key", host);
            Response.Cookies.Append("Cookie", cookie);
            return View();
            
        }
        public IActionResult Test()
        {
            ViewBag.Session = HttpContext.Session.GetString("Key");
            ViewBag.Cookie = Request.Cookies["Cookie"];
            return View();
            //return Content("Show Action");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}