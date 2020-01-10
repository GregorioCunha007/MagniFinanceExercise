using MagniFinanceExercise.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MagniFinanceExercise.DAL
{
    public class Collegeinitializer : DropCreateDatabaseAlways<CollegeContext>
    {
        protected override void Seed(CollegeContext context)
        {
            var students = new List<Student>
            {
                new Student{ID=1, Name="Harry Potter",DateOfBirth=DateTime.Parse("1984-07-30")},
                new Student{ID=2,Name="Hermione Granger",DateOfBirth=DateTime.Parse("1984-03-21")},
                new Student{ID=3,Name="Ronald Weasley",DateOfBirth=DateTime.Parse("1984-06-01")},
                new Student{ID=4,Name="Luna Lovegood",DateOfBirth=DateTime.Parse("1984-09-11")},
                new Student{ID=5,Name="Neville Longbottom",DateOfBirth=DateTime.Parse("1984-07-31")}
            };

            students.ForEach(s => context.Students.Add(s));
            context.SaveChanges();

            var courses = new List<Course>
            {
                new Course{ID=100,Name="Medicine"},
                new Course{ID=200,Name="Applied mathematics"},
                new Course{ID=300,Name="Software engineering"}
            };
            courses.ForEach(s => context.Courses.Add(s));
            context.SaveChanges();

            var teachers = new List<Teacher>
            {
                new Teacher{ID=101,Name="Minerva Mcgonagall", DateOfBirth=DateTime.Parse("1935-07-01"), Salary="20.000€"},
                new Teacher{ID=444,Name="Horace Slughorn", DateOfBirth=DateTime.Parse("1940-04-23"), Salary="15.000€"},
                new Teacher{ID=33,Name="Albus Dumbledore", DateOfBirth=DateTime.Parse("1920-01-31"), Salary="50.000€"},
                new Teacher{ID=123,Name="Pomona Sprout", DateOfBirth=DateTime.Parse("1941-03-07"), Salary="18.000€"},
                new Teacher{ID=643,Name="Severus Snape", DateOfBirth=DateTime.Parse("1975-11-13"), Salary="15.000€"},
                new Teacher{ID=432,Name="Sybill Trelawney", DateOfBirth=DateTime.Parse("1962-03-09"), Salary="17.000€"},
                new Teacher{ID=354,Name="Remus Lupin", DateOfBirth=DateTime.Parse("1975-08-10"), Salary="11.000€"},
                new Teacher{ID=321,Name="Arthur Flitwick", DateOfBirth=DateTime.Parse("1960-02-10"), Salary="20.000€"},
            };
            teachers.ForEach(s => context.Teachers.Add(s));
            context.SaveChanges();

            var subjects = new List<Subject>
            {
                new Subject{ID=1001,CourseID=100,Name="Neurology", TeacherID=444},
                new Subject{ID=1002,CourseID=100,Name="Biology", TeacherID=123},
                new Subject{ID=1003,CourseID=100,Name="Potions", TeacherID=643},
                new Subject{ID=2001,CourseID=200,Name="Statistics", TeacherID=321},
                new Subject{ID=2002,CourseID=200,Name="Trigonometry", TeacherID=33},
                new Subject{ID=3001, CourseID=300,Name="Ambientes virtuais em execução", TeacherID=33},
                new Subject{ID=3002, CourseID=300,Name="Sistemas operativos", TeacherID=354},
                new Subject{ID=3003, CourseID=300,Name="Progamação em dispositivos móveis", TeacherID=123},
                new Subject{ID=3004, CourseID=300,Name="POO", TeacherID=101},
            };
            subjects.ForEach(s => context.Subjects.Add(s));
            context.SaveChanges();

            var grades = new List<Grade>
            {
                new Grade { Value = 1 },
                new Grade { Value = 2 },
                new Grade { Value = 3 },
                new Grade { Value = 4 },
                new Grade { Value = 5 },
                new Grade { Value = 6 },
                new Grade { Value = 7 },
                new Grade { Value = 8 },
                new Grade { Value = 9 },
                new Grade { Value = 10 },
                new Grade { Value = 11 },
                new Grade { Value = 12 },
                new Grade { Value = 13 },
                new Grade { Value = 14 },
                new Grade { Value = 15 },
                new Grade { Value = 16 },
                new Grade { Value = 17 },
                new Grade { Value = 18 },
                new Grade { Value = 19 },
                new Grade { Value = 20 },
            };
            grades.ForEach(s => context.Grades.Add(s));
            context.SaveChanges();

            var enrollments = new List<Enrollment>
            {
                new Enrollment { StudentID=1, SubjectID=1001, GradeValue=12, Year = 1997 },
                new Enrollment { StudentID=1, SubjectID=1002, GradeValue=14, Year = 1997 },
                new Enrollment { StudentID=1, SubjectID=1003, GradeValue=16, Year = 1997 },
                new Enrollment { StudentID=2, SubjectID=1001, GradeValue=19, Year = 1997 },
                new Enrollment { StudentID=2, SubjectID=1002, GradeValue=19, Year = 1997 },
                new Enrollment { StudentID=2, SubjectID=1003, GradeValue=17, Year = 1997 }
            };
            enrollments.ForEach(s => context.Enrollments.Add(s));
            context.SaveChanges();

            base.Seed(context);
        }
    }
}