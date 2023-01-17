using System;
using System.Collections.Generic;

namespace MVC2023_v3._0.Models;

public partial class Student
{
    public int RegistrationNumber { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Department { get; set; } = null!;

    public string Username { get; set; } = null!;

    public virtual ICollection<CourseHasStudent> CourseHasStudents { get; } = new List<CourseHasStudent>();

    public virtual User UsernameNavigation { get; set; } = null!;
}
