using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MVC2023_v3._0.Models;

public partial class MvcDbContext : DbContext
{
    public MvcDbContext()
    {
        
    }

    public MvcDbContext(DbContextOptions<MvcDbContext> options)
        : base(options)
    {

    }
    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CourseHasStudent> CourseHasStudents { get; set; }

    public virtual DbSet<Professor> Professors { get; set; }

    public virtual DbSet<Secretary> Secretaries { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=MVC_Db;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.IdCourse);

            entity.ToTable("course");

            entity.Property(e => e.IdCourse)
                .ValueGeneratedNever()
                .HasColumnName("idCOURSE");
            entity.Property(e => e.Afm).HasColumnName("AFM");
            entity.Property(e => e.CourseSemester)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.CourseTitle)
                .HasMaxLength(60)
                .IsUnicode(false);

            entity.HasOne(d => d.AfmNavigation).WithMany(p => p.Courses)
                .HasForeignKey(d => d.Afm)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_course_professors");
        });

        modelBuilder.Entity<CourseHasStudent>(entity =>
        {
            entity.HasKey(e => e.IdPk);

            entity.ToTable("course_has_students");

            entity.Property(e => e.IdPk)
                .ValueGeneratedNever()
                .HasColumnName("idPK");
            entity.Property(e => e.IdCourse).HasColumnName("idCOURSE");

            entity.HasOne(d => d.IdCourseNavigation).WithMany(p => p.CourseHasStudents)
                .HasForeignKey(d => d.IdCourse)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_course_has_students_course");

            entity.HasOne(d => d.RegistrationNumberNavigation).WithMany(p => p.CourseHasStudents)
                .HasForeignKey(d => d.RegistrationNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_course_has_students_students");
        });

        modelBuilder.Entity<Professor>(entity =>
        {
            entity.HasKey(e => e.Afm).HasName("PK_professors_1");

            entity.ToTable("professors");

            entity.Property(e => e.Afm)
                .ValueGeneratedNever()
                .HasColumnName("AFM");
            entity.Property(e => e.Department)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.Surname)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.Professors)
                .HasForeignKey(d => d.Username)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_professors_Users");
        });

        modelBuilder.Entity<Secretary>(entity =>
        {
            entity.HasKey(e => e.Phonenumber);

            entity.ToTable("secretaries");

            entity.Property(e => e.Phonenumber).ValueGeneratedNever();
            entity.Property(e => e.Department)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.Surname)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.Secretaries)
                .HasForeignKey(d => d.Username)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_secretaries_Users");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.RegistrationNumber);

            entity.ToTable("students");

            entity.Property(e => e.RegistrationNumber).ValueGeneratedNever();
            entity.Property(e => e.Department)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.Surname)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.Students)
                .HasForeignKey(d => d.Username)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_students_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("PK__Users__F3DBC57360770B14");

            entity.Property(e => e.Username)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("username");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
