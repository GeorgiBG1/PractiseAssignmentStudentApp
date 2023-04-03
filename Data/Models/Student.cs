using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace StudentApp.Data.Models
{
    public class Student
    {
        public int Id { get; set; }
        public DateTime Birthday { get; set; }
        public string Name { get; set; }
        public int PhoneNumber { get; set; }
        public DateTime RegisteredOn { get; set; }
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
        public virtual ICollection<HomeworkSubmission> HomeworkSubmissions { get; set; }
    }
}
