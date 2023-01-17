using System;
using System.Collections.Generic;

namespace MVC2023_v3._0.Models;

public partial class User
{
    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;

    public virtual ICollection<Professor> Professors { get; } = new List<Professor>();

    public virtual ICollection<Secretary> Secretaries { get; } = new List<Secretary>();

    public virtual ICollection<Student> Students { get; } = new List<Student>();
}
