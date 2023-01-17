using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MVC2023_v3._0.Models;

public partial class Professor
{
    [Key]
    public int Afm { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Department { get; set; } = null!;

    public string Username { get; set; } = null!;

    public virtual ICollection<Course> Courses { get; } = new List<Course>();

    public virtual User UsernameNavigation { get; set; } = null!;
}
