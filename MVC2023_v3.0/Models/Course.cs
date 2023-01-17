using System;
using System.Collections.Generic;

namespace MVC2023_v3._0.Models;

public partial class Course
{
    public int IdCourse { get; set; }

    public string CourseTitle { get; set; } = null!;

    public string CourseSemester { get; set; } = null!;

    public int Afm { get; set; }

    public virtual Professor AfmNavigation { get; set; } = null!;

    public virtual ICollection<CourseHasStudent> CourseHasStudents { get; } = new List<CourseHasStudent>();
}
