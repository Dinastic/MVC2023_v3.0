using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC2023_v3._0.Models;

namespace MVC2023_v3._0.Controllers
{
    public class StudentsController : Controller
    {
        private readonly MvcDbContext _context;

        public StudentsController(MvcDbContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var mvcDbContext = _context.Students.Include(s => s.UsernameNavigation);
            return View(await mvcDbContext.ToListAsync());
        }


        // GET: Students/ShowAllGrades
        public async Task<IActionResult> ShowAllGrades(string? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.UsernameNavigation)
                .FirstOrDefaultAsync(m => m.Username == id);

            if (student == null)
            {
                return NotFound();
            }

            List<CourseHasStudent> elements = _context.CourseHasStudents
                .Where(x => x.RegistrationNumber == student.RegistrationNumber)
                .ToList();

            List<Course> courses = _context.Courses.ToList();

            var grades = from x in elements
                         join y in courses on x.IdCourse equals y.IdCourse
                         where x.RegistrationNumber == student.RegistrationNumber
                         select new Grades
                         {
                             CourseTitle = y.CourseTitle,
                             GradeCourseStudent = x.GradeCourseStudent
                         };
            return View(grades);
            //Debugger
            /*    foreach (var course in courses)
            {
                System.Diagnostics.Debug.WriteLine(course.CourseTitle);
            }*/
        }

        // GET: Students/ShowSingleCourse/5
        public async Task<IActionResult> ShowSingleCourse(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.UsernameNavigation)
                .FirstOrDefaultAsync(m => m.RegistrationNumber == id);
            if (student == null)
            {
                return NotFound();
            }

            List<CourseHasStudent> elements = _context.CourseHasStudents
                .Where(x => x.RegistrationNumber == student.RegistrationNumber)
                .ToList();

            List<Course> courses = _context.Courses.ToList();

            var coursetitles = from x in elements
                         join y in courses on x.IdCourse equals y.IdCourse
                         where x.RegistrationNumber == student.RegistrationNumber
                         select new Grades
                         {
                             CourseTitle = y.CourseTitle
                         };
            return View(coursetitles);

        }

        private bool StudentExists(int id)
        {
          return _context.Students.Any(e => e.RegistrationNumber == id);
        }
    }
}
