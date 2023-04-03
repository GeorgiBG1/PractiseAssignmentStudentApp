using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable enable
namespace StudentApp.Data.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public DateTime EndDate { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
