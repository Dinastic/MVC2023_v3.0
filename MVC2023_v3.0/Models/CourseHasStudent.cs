using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MVC2023_v3._0.Models;

public partial class CourseHasStudent
{

    public int IdCourse { get; set; }

    public int RegistrationNumber { get; set; }

    public int GradeCourseStudent { get; set; }

    public int IdPk { get; set; }

    public virtual Course IdCourseNavigation { get; set; } = null!;

    public virtual Student RegistrationNumberNavigation { get; set; } = null!;
}
