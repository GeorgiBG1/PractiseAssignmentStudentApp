using Microsoft.EntityFrameworkCore;
using StudentApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Data
{
    public class StudentContext : DbContext
    {
        public StudentContext()
        {
            //this.Database.Migrate(); 
        }
        protected override void OnConfiguring(DbContextOptionsBuilder
            optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=StudentDB;" +
                    "Trusted_Connection=true;");
            }
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<HomeworkSubmission> HomeworkSubmissions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourse>()
              .HasKey(s => new { s.StudentId, s.CourseId });
            base.OnModelCreating(modelBuilder);
        }
    }
}
