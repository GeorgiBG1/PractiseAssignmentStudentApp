using Microsoft.EntityFrameworkCore;
using StudentApp.Data;
using StudentApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Business
{
    public class StudentBusiness : IStudentBusiness
    {
        private StudentContext studentContext;
        public void AddCourse(Course course)
        {
            using (studentContext)
            {
                studentContext.Add(course);
                studentContext.SaveChanges();
            }
        }
        public void AddStudent(Student student)
        {
            using (studentContext)
            {
                studentContext.Add(student);
                studentContext.SaveChanges();
            }
        }
        public void SubmitHomework(HomeworkSubmission homeworkSubmission)
        {
            using (studentContext)
            {
                studentContext.Add(homeworkSubmission);
                studentContext.SaveChanges();
            }
        }
        public List<Student> GetAllStudentsGroupedByCourses()
        {
            using(studentContext)
            {
                List<Student> students = studentContext.Students
                    .OrderBy(s => s.StudentCourses.Select(sc => sc.CourseId))
                    .ToList();
                return students;
            }
        }
        public Student GetStudentWithAllHomework(int id)
        {
            using (studentContext)
            {
                Student student = studentContext.Students
                    .Include(st => st.HomeworkSubmissions.Where(hs => hs.StudentId == id))
                    .FirstOrDefault();
                return student; 
            }
        }
    }
}
