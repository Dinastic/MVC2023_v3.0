using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MVC2023_v3._0.Models;

public class ProfessorName
{
    public int Afm { get; set; }
    public string Name { get; set; } = null!;
    public int IdCourse { get; set; }

    public string CourseTitle { get; set; } = null!;

    public string CourseSemester { get; set; } = null!;
}
