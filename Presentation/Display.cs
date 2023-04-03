using StudentApp.Business;
using StudentApp.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Presentation
{
    public class Display
    {


        private readonly int closeOperationId = 6;
        private IStudentBusiness studentBusiness = new StudentBusiness();
        public Display()
        {
            Input();
        }
        private static void ShowMenu()
        {
            Console.WriteLine(new string('-', 40));
            Console.WriteLine(new string('-', 18) + "MENU" + new string('-', 18));
            Console.WriteLine(new string('-', 40));
            Console.WriteLine("1. Add new course");
            Console.WriteLine("2. Add new student");
            Console.WriteLine("3. Submit homework");
            Console.WriteLine("4. Get all students grouped by courses");
            Console.WriteLine("5. Get student with all homework");
            Console.WriteLine("6. Exit");
        }
        private void Input()
        {
            var operation = -1;
            do
            {
                ShowMenu();
                operation = int.Parse(Console.ReadLine()!);
                switch (operation)
                {
                    case 1:
                        AddCourse();
                        break;
                    case 2:
                        AddStudent();
                        break;
                    case 3:
                        SubmitHomework();
                        break;
                    case 4:
                        GetAllStudentsGroupedByCourses();
                        break;
                    case 5:
                        GetStudentWithAllHomework();
                        break;
                    default:
                        break;
                }
            } while (operation != closeOperationId);
        }
        private void AddCourse()
        {
            Course course = new Course();
            Console.Write("Enter name: ");
            course.Name = Console.ReadLine()!;
            Console.Write("Enter description: ");
            course.Description = Console.ReadLine()!;
            Console.Write("Enter price: ");
            course.Price = decimal.Parse(Console.ReadLine()!);
            Console.Write("Enter duration of the course:\nEnter days: ");
            int days = int.Parse(Console.ReadLine()!);
            var startDate = DateTime.Now;
            course.StartDate = startDate;
            startDate = startDate.AddDays(days);
            course.EndDate = startDate;
            studentBusiness.AddCourse(course);
        }
        private void AddStudent()
        {
            Student student = new Student();
            Console.Write("Enter name: ");
            student.Name = Console.ReadLine()!;
            Console.Write("Enter phone number: ");
            student.PhoneNumber = int.Parse(Console.ReadLine()!);
            Console.Write("Enter birthday date:\nDay: ");
            int day = int.Parse(Console.ReadLine()!);
            Console.Write("Month: ");
            int month = int.Parse(Console.ReadLine()!);
            Console.Write("Year: ");
            int year = int.Parse(Console.ReadLine()!);
            student.Birthday = new DateTime(year, month, day);
            Console.Write("Enter register date:\nDay: ");
            day = int.Parse(Console.ReadLine()!);
            Console.Write("Month: ");
            month = int.Parse(Console.ReadLine()!);
            Console.Write("Year: ");
            year = int.Parse(Console.ReadLine()!);
            student.RegisteredOn = new DateTime(year, month, day);
            studentBusiness.AddStudent(student);
        }
        private void SubmitHomework()
        {
            List<Student> students = studentBusiness.GetAllStudents();
            List<Course> courses = studentBusiness.GetAllCourses();
            if (students != null && courses != null)
            {
                Console.WriteLine("All students:");
                foreach (var student in students)
                {
                    Console.WriteLine($"ID - {student.Id}. Name - {student.Name}");
                }
                Console.Write("Select student ID: ");
                int studentId = int.Parse(Console.ReadLine()!);
                Console.WriteLine("All courses:");
                foreach (var course in courses)
                {
                    Console.WriteLine($"ID - {course.Id}. Name - {course.Name}");
                }
                Console.Write("Select course ID: ");
                int courseId = int.Parse(Console.ReadLine()!);
                Console.Write("Enter content (string): ");
                string content = Console.ReadLine()!;
                Console.Write("Enter content type (string): ");
                string contentType = Console.ReadLine()!;
                Console.Write("Enter submission time:\n" +
                    "Day: ");
                int day = int.Parse(Console.ReadLine()!);
                Console.Write("Month: ");
                int month = int.Parse(Console.ReadLine()!);
                Console.Write("Year: ");
                int year = int.Parse(Console.ReadLine()!);
                HomeworkSubmission homeworkSubmission = new HomeworkSubmission();
                homeworkSubmission.StudentId = studentId;
                homeworkSubmission.CourseId = courseId;
                homeworkSubmission.Content = content;
                homeworkSubmission.ContentType = contentType;
                homeworkSubmission.SubmissionTime = new DateTime(year, month, day);
                studentBusiness.SubmitHomework(homeworkSubmission);
                StudentCourse studentCourse = new StudentCourse();
                studentCourse.StudentId = studentId;
                studentCourse.CourseId = courseId;
                studentBusiness.AddStudentCourse(studentCourse);
            }
            else if (students != null && courses == null)
            {
                Console.WriteLine("To submit the homework, you have to add course!");
            }
            else if (courses != null && students == null)
            {
                Console.WriteLine("To submit the homework, you have to add student!");
            }
            else 
            { 
                Console.WriteLine("To submit the homerok, " +
                    "you have to add student and course!"); 
            }
        }
        private void GetAllStudentsGroupedByCourses()
        {
            Console.WriteLine("Students:");
            var students = studentBusiness.GetAllStudentsGroupedByCourses();
            if (students != null)
            {
                foreach (var student in students)
                {
                    Console.WriteLine($"ID - {student.Id}. Name - {student.Name}\n" +
                        $"Additional information:\nPhone number: {student.PhoneNumber}\n" +
                        $"Birthday on {student.Birthday}\n" +
                        $"Registered on {student.RegisteredOn}\n");
                }
            }
            else { Console.WriteLine("There aren't any students yet!"); }
        }
        private void GetStudentWithAllHomework()
        {
            var allStudents = studentBusiness.GetAllStudents();
            if (allStudents != null)
            {
                Console.WriteLine("All students:");
                foreach (var st in allStudents)
                {
                    Console.WriteLine($"ID - {st.Id}. Name - {st.Name}");
                }
                Console.Write("\nEnter id for student: ");
                int studentId = int.Parse(Console.ReadLine()!);
                var student = studentBusiness.GetStudentWithAllHomework(studentId);
                Console.WriteLine($"ID - {student.Id}. Name - {student.Name}\n" +
                    $"Additional information:\nPhone number: {student.PhoneNumber}\n" +
                    $"Birthday on {student.Birthday}\n" +
                    $"Registered on {student.RegisteredOn}\n" +
                    $"Homework: ");
                int homeworkCount = student.HomeworkSubmissions.Count();
                if (student.HomeworkSubmissions.Any())
                {
                    foreach (var homework in student.HomeworkSubmissions)
                    {
                        Console.WriteLine($"ID - {homework.Id}.\nContent - {homework.Content}\n" +
                            $"Submission on {homework.SubmissionTime.ToShortTimeString()}\n");
                    }
                }
                else if(homeworkCount == 0)
                { Console.WriteLine("This student hasn't got any homework yet!"); }
            }
            else { Console.WriteLine("There aren't any students yet!"); }
        }
    }
}
