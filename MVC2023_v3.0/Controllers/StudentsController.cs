using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
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
                .Where(x => x.RegistrationNumber == student.RegistrationNumber && x.GradeCourseStudent >= 0)
                .ToList();

            List<Course> courses = _context.Courses.ToList();

            var grades = from x in elements
                         join y in courses on x.IdCourse equals y.IdCourse
                         where x.RegistrationNumber == student.RegistrationNumber
                         select new Grade
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
        public async Task<IActionResult> ShowSingleCourse(string? id)
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
                .Where(x => x.RegistrationNumber == student.RegistrationNumber && x.GradeCourseStudent >= 0)
                .ToList();

            List<Course> courses = _context.Courses.ToList();

            var coursetitles = from x in elements
                               join y in courses on x.IdCourse equals y.IdCourse
                               where x.RegistrationNumber == student.RegistrationNumber
                               select new Grade
                               {
                                   IdCourse= y.IdCourse,
                                   CourseTitle = y.CourseTitle,
                                   RegistrationNumber = student.RegistrationNumber
                               };
            return View(coursetitles);

        }
       
        public async Task<IActionResult> ShowGrade(int? idcourse, int? idregnum)
        {
            
            var student = await _context.Students
                .Include(s => s.UsernameNavigation)
                .FirstOrDefaultAsync(m => m.RegistrationNumber == idregnum);

            if (student == null)
            {
                return NotFound();
            }

            List<CourseHasStudent> elements = _context.CourseHasStudents
                .Where(x => x.RegistrationNumber == student.RegistrationNumber && x.GradeCourseStudent >= 0)
                .ToList();

            List<Course> courses = _context.Courses
                .Where(x => x.IdCourse == idcourse)
                .ToList();

            var grade = from x in elements
                        join y in courses on x.IdCourse equals y.IdCourse
                        where x.RegistrationNumber == student.RegistrationNumber
                        select new Grade
                        {
                             CourseTitle = y.CourseTitle,
                             GradeCourseStudent = x.GradeCourseStudent
                        };

            return View(grade);
        }

        public async Task<IActionResult> ShowSingleSemester(string? id)
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
                .Where(x => x.RegistrationNumber == student.RegistrationNumber && x.GradeCourseStudent >= 0 )
                .ToList();

            List<Course> courses = _context.Courses.ToList();

            var coursesemester = from y in courses
                                 group y by y.CourseSemester into g
                                 join x in elements on g.FirstOrDefault().IdCourse equals x.IdCourse
                                 where x.RegistrationNumber == student.RegistrationNumber
                                 select new Grade
                                 {
                                     RegistrationNumber = student.RegistrationNumber,
                                     CourseSemester = g.FirstOrDefault().CourseSemester,
                                     GradeCourseStudent = x.GradeCourseStudent
                                 };
            return View(coursesemester); 
        }
        public async Task<IActionResult> ShowGrade2(string? idsem, int? idregnum)
        {

            var student = await _context.Students
                .Include(s => s.UsernameNavigation)
                .FirstOrDefaultAsync(m => m.RegistrationNumber == idregnum);

            if (student == null)
            {
                return NotFound();
            }

            List<CourseHasStudent> elements = _context.CourseHasStudents
                .Where(x => x.RegistrationNumber == student.RegistrationNumber && x.GradeCourseStudent >= 0)
                .ToList();

            List<Course> courses = _context.Courses
                .Where(x => x.CourseSemester == idsem)
                .ToList();

            var grade = from x in elements
                        join y in courses on x.IdCourse equals y.IdCourse
                        where x.RegistrationNumber == student.RegistrationNumber
                        select new Grade
                        {
                            CourseTitle = y.CourseTitle,
                            GradeCourseStudent = x.GradeCourseStudent
                        };

            return View(grade);
        }
    }
}
