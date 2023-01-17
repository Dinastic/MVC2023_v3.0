using System;
using System.Collections.Generic;

namespace MVC2023_v3._0.Models;

public partial class Professor
{
    public int Afm { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Department { get; set; } = null!;

    public string Username { get; set; } = null!;

    public virtual ICollection<Course> Courses { get; } = new List<Course>();

    public virtual User UsernameNavigation { get; set; } = null!;
}
