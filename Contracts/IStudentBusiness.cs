﻿using StudentApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp
{
    public interface IStudentBusiness
    {
        void AddCourse(Course course);
        void AddStudent(Student student);
        List<Student> GetAllStudentsGroupedByCourses();
        Student GetStudentWithAllHomework(int id);
        void SubmitHomework(HomeworkSubmission homeworkSubmission);
    }
}
