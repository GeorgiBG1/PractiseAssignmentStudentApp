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
            using (studentContext = new StudentContext())
            {
                studentContext.Courses.Add(course);
                studentContext.SaveChanges();
            }
        }
        public void AddStudent(Student student)
        {
            using (studentContext = new StudentContext())
            {
                studentContext.Students.Add(student);
                studentContext.SaveChanges();
            }
        }
        public void SubmitHomework(HomeworkSubmission homeworkSubmission)
        {
            using (studentContext = new StudentContext())
            {
                studentContext.HomeworkSubmissions.Add(homeworkSubmission);
                studentContext.SaveChanges();
            }
        }
        public List<Student> GetAllStudentsGroupedByCourses()
        {
            using (studentContext = new StudentContext())
            {
                //StudentCourse studentCourse = new StudentCourse();
                //studentCourse.StudentId = 5;
                //studentCourse.CourseId = 1;
                //studentContext.StudentCourses.Add(studentCourse);
                //studentContext.SaveChanges();
                //System.Threading.Thread.Sleep(1000);
                if (studentContext.Students.FirstOrDefault() != null)
                {
                    var students = studentContext.StudentCourses
                        .OrderBy(sc => sc.CourseId)
                        .ThenBy(sc => sc.StudentId)
                        .Include(sc => sc.Student)
                        .Select(sc => sc.Student)
                        .ToList();
                    students = students.Distinct().ToList();
                    return students;
                }
                return null;
            }
        }
        public Student GetStudentWithAllHomework(int id)
        {
            using (studentContext = new StudentContext())
            {
                Student student = studentContext.Students
                    .Where(s=>s.Id == id)
                    .Include(st => st.HomeworkSubmissions)
                    .FirstOrDefault();
                return student;
            }
        }
        public List<Student> GetAllStudents()
        {
            using (studentContext = new StudentContext())
            {
                if (studentContext.Students.FirstOrDefault() != null)
                {
                    List<Student> students = studentContext.Students
                        .ToList();
                    return students;
                }
                return null;
            }
        }
        public List<Course> GetAllCourses()
        {
            using (studentContext = new StudentContext())
            {
                if (studentContext.Courses.FirstOrDefault() != null)
                {
                    List<Course> courses = studentContext.Courses
                        .ToList();
                    return courses;
                }
                return null;
            }
        }
        public void AddStudentCourse(StudentCourse studentCourse)
        {
            using (studentContext = new StudentContext())
            {
                studentContext.StudentCourses.Add(studentCourse);
                studentContext.SaveChanges();
            }
        }
    }
}
