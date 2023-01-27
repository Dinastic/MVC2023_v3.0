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
    public class ProfessorsController : Controller
    {
        private readonly MvcDbContext _context;

        public ProfessorsController(MvcDbContext context)
        {
            _context = context;
        }

        // GET: Professors
        public async Task<IActionResult> Index()
        {
            var mvcDbContext = _context.Professors.Include(p => p.UsernameNavigation);
            return View(await mvcDbContext.ToListAsync());
        }

        public async Task<IActionResult> ShowAllGraded(string? id)
        {
            if (id == null || _context.Professors == null)
            {
                return NotFound();
            }
            
            var professor = await _context.Professors
                .Include(p => p.UsernameNavigation)
                .FirstOrDefaultAsync(m => m.Username == id);

            if (professor == null)
            {
                return NotFound();
            }

            List<Course> courses = _context.Courses
                .Where(x => x.Afm == professor.Afm)
                .ToList();

            List<CourseHasStudent> elements = _context.CourseHasStudents
                .Where(x => x.GradeCourseStudent >= 0)
                .ToList();
            


            var grades = from x in courses
                         join y in elements on x.IdCourse equals y.IdCourse 
                         where x.Afm == professor.Afm
                         select new Grade
                         {     
                             RegistrationNumber = y.RegistrationNumber,
                             IdCourse= x.IdCourse,
                             CourseTitle = x.CourseTitle,
                             GradeCourseStudent = y.GradeCourseStudent

                         };
            return View(grades);
            //Debugger
            /*    foreach (var course in courses)
            {
                System.Diagnostics.Debug.WriteLine(course.CourseTitle);
            }*/
        }

        public async Task<IActionResult> EditGrade(string? id)
        {
            if (id == null || _context.Professors == null)
            {
                return NotFound();
            }

            var professor = await _context.Professors
                .Include(p => p.UsernameNavigation)
                .FirstOrDefaultAsync(m => m.Username == id);

            if (professor == null)
            {
                return NotFound();
            }

            List<Course> courses = _context.Courses
                .Where(x => x.Afm == professor.Afm)
                .ToList();

            List<CourseHasStudent> elements = _context.CourseHasStudents
                .DefaultIfEmpty()
                .ToList();



            var grades = from x in courses
                         join y in elements on x.IdCourse equals y.IdCourse
                         where x.Afm == professor.Afm && y.GradeCourseStudent == 0
                         select new Grade
                         {
                             RegistrationNumber = y.RegistrationNumber,
                             IdCourse = x.IdCourse,
                             CourseTitle = x.CourseTitle,
                             GradeCourseStudent = y.GradeCourseStudent

                         };
            return View(grades);
            //Debugger
            /*    foreach (var course in courses)
            {
                System.Diagnostics.Debug.WriteLine(course.CourseTitle);
            }*/
        }

        // GET: Professors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Professors == null)
            {
                return NotFound();
            }

            var professor = await _context.Professors.FindAsync(id);
            if (professor == null)
            {
                return NotFound();
            }
            ViewData["Username"] = new SelectList(_context.Users, "Username", "Username", professor.Username);
            return View(professor);
        }

        // POST: Professors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Afm,Name,Surname,Department,Username")] Professor professor)
        {
            if (id != professor.Afm)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(professor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfessorExists(professor.Afm))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Username"] = new SelectList(_context.Users, "Username", "Username", professor.Username);
            return View(professor);
        }

        

        private bool ProfessorExists(int id)
        {
          return _context.Professors.Any(e => e.Afm == id);
        }
    }
}
