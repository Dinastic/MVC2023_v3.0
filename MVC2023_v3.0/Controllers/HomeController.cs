using Microsoft.AspNetCore.Mvc;
using MVC2023_v3._0.Models;
using System.Diagnostics;

namespace MVC2023_v3._0.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            User user = new User();
            return View(user);
        }
        [HttpPost]
        public IActionResult Index(User user)
        {
            MvcDbContext mvcDbContext = new MvcDbContext();
            var status = mvcDbContext.Users.Where(m => m.Username == user.Username && m.Password == user.Password).FirstOrDefault();
            mvcDbContext.Users.Add(user);
            if (status == null)
            {
                ViewBag.LoginStatus = 0;
            }
            else
            {
                if (user.Role == "Student") {
                    return RedirectToAction("Index", "Students");
                }
                else if (user.Role == "Professor") {
                    return RedirectToAction("Index", "Professors");
                }
                else if (user.Role == "Secretary")
                {
                    return RedirectToAction("Index", "Secretaries");
                }
               
            }
            return View(user);
        }
        public IActionResult SuccessPage()
        {
            return View();
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