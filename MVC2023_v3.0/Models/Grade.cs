using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MVC2023_v3._0.Models
{
    public class Grade

    {
        public int IdCourse { get; set; }
        public string CourseTitle { get; set; }
        public int RegistrationNumber { get; set; }
        public int GradeCourseStudent { get; set; }
        public string CourseSemester { get; set; }
    }
}
