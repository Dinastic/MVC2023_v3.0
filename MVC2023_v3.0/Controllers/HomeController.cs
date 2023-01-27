using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC2023_v3._0.Models;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ConstrainedExecution;

namespace MVC2023_v3._0.Controllers
{
    public class HomeController : Controller
    {
        MvcDbContext mvcDbContext = new MvcDbContext();
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
            bool userExists = mvcDbContext.Users.Any(m => m.Username == user.Username && m.Password == user.Password);
            User u = mvcDbContext.Users.FirstOrDefault(m => m.Username == user.Username && m.Password == user.Password);

            if (userExists)
            {
                if (u.Role == "Student" ) 
                {
                    Student s = mvcDbContext.Students.FirstOrDefault(y => y.Username == user.Username);

                    TempData["username"] = s.Username;
                    TempData["name"] = s.Name;
                    TempData["surname"] = s.Surname;
                    TempData["department"] = s.Department;

                    return RedirectToAction("Index", "Students");
                }

                else if (u.Role == "Professor") 
                {
                    Professor p = mvcDbContext.Professors.FirstOrDefault(y => y.Username == user.Username);

                    TempData["username"] = p.Username;
                    TempData["name"] = p.Name;
                    TempData["surname"] = p.Surname;
                    TempData["department"] = p.Department;

                    return RedirectToAction("Index", "Professors");
                }

                else if (u.Role == "Secretary")
                {

                    return RedirectToAction("Index", "Secretaries");
                }
            }

            else
            {
                ViewBag.LoginStatus = 0;
            }
            return View(user);
        }
        public ActionResult SuccessPage()
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