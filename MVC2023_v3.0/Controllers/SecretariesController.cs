using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC2023_v3._0.Models;

namespace MVC2023_v3._0.Controllers
{
    public class SecretariesController : Controller
    {
        private readonly MvcDbContext _context;

        public SecretariesController(MvcDbContext context)
        {
            _context = context;
        }

        // GET: Secretaries
        public async Task<IActionResult> Index()
        {
            var mvcDbContext = _context.Secretaries.Include(s => s.UsernameNavigation);
            return View(await mvcDbContext.ToListAsync());
        }

        public async Task<IActionResult> ShowAllCourses(string? id)
        {
            List<Professor> professors = _context.Professors.ToList();
            List<Course> courses = _context.Courses.ToList();

            var grade = from x in professors
                        join y in courses on x.Afm equals y.Afm
                        select new ProfessorName
                        {
                            CourseTitle = y.CourseTitle,
                            CourseSemester = y.CourseSemester,
                            Name = x.Name
                        };
            return View(grade);
        }

        // GET: Secretaries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Secretaries == null)
            {
                return NotFound();
            }

            var secretary = await _context.Secretaries
                .Include(s => s.UsernameNavigation)
                .FirstOrDefaultAsync(m => m.Phonenumber == id);
            if (secretary == null)
            {
                return NotFound();
            }

            return View(secretary);
        }

        // GET: Secretaries/CreateStudent
        public IActionResult CreateStudent()
        {
            ViewData["Username"] = new SelectList(_context.Users, "Username", "Username");
            return View();
        }

        // GET: Secretaries/CreateProfessor
        public IActionResult CreateProfessor()
        {
            ViewData["Username"] = new SelectList(_context.Users, "Username", "Username");
            return View();
        }

        // GET: Secretaries/CreateCourse
        public IActionResult CreateCourse()
        {
            ViewData["Username"] = new SelectList(_context.Users, "Username", "Username");
            return View();
        }

        // POST: Secretaries/CreateNewStudent
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNewStudent([Bind("RegistrationNumber,Name,Surname,Department,Username")] Student student,string pwd)
        {
                User user = new User();
                user.Username = student.Username;
                user.Role = "Student";
                user.Password = pwd;
                _context.Users.Add(user);
                _context.Students.Add(student);
                _context.SaveChanges();

            return View(student);
        }

        // POST: Secretaries/CreateNewProfessor
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNewProfessor([Bind("Afm,Name,Surname,Department,Username")] Professor professor, string pwd)
        {
            User user = new User();
            user.Username = professor.Username;
            user.Role = "Professor";
            user.Password = pwd;
            _context.Users.Add(user);
            _context.Professors.Add(professor);
            _context.SaveChanges();

            return View(professor);
        }

        // POST: Secretaries/CreateNewCourse
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNewCourse([Bind("IdCourse,CourseTitle,CourseSemester,Afm")] Course course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();
            return View(course);
        }
    }
}
