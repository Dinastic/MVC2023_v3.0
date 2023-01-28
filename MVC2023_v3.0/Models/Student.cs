using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MVC2023_v3._0.Models;
[Table("students")]
public partial class Student
{
    [Key]
    public int RegistrationNumber { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Department { get; set; } = null!;

    public string Username { get; set; } = null!;

    public virtual ICollection<CourseHasStudent> CourseHasStudents { get; } = new List<CourseHasStudent>();

    public virtual User UsernameNavigation { get; set; } = null!;
}
