using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MagniFinance.DAL;
using MagniFinanceExercise.DAL;
using MagniFinanceExercise.Models;

namespace MagniFinanceExercise.Controllers
{
    public class SubjectController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        // GET: Subject
        public ActionResult Index()
        {
            var subjects = unitOfWork.SubjectRepository.Get();
            return View(subjects.ToList());
        }

        // GET: Subject/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = unitOfWork.SubjectRepository.GetByID(id);
            if (subject == null)
            {
                return HttpNotFound();
            }

            return View(subject);
        }

        // GET: Subject/Create
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(unitOfWork.CourseRepository.Get(), "ID", "Name");
            ViewBag.TeacherID = new SelectList(unitOfWork.TeacherRepository.Get(), "ID", "Name");
            return View();
        }

        // POST: Subject/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,CourseID,TeacherID")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.SubjectRepository.Insert(subject);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(unitOfWork.CourseRepository.Get(), "ID", "Name", subject.CourseID);
            ViewBag.TeacherID = new SelectList(unitOfWork.TeacherRepository.Get(), "ID", "Name", subject.TeacherID);
            return View(subject);
        }

        // GET: Subject/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Subject subject = unitOfWork.SubjectRepository.GetByID(id);

            if (subject == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(unitOfWork.CourseRepository.Get(), "ID", "Name", subject.CourseID);
            ViewBag.TeacherID = new SelectList(unitOfWork.TeacherRepository.Get(), "ID", "Name", subject.TeacherID);
            return View(subject);
        }

        // POST: Subject/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,CourseID,TeacherID")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.SubjectRepository.Update(subject);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(unitOfWork.CourseRepository.Get(), "ID", "Name", subject.CourseID);
            ViewBag.TeacherID = new SelectList(unitOfWork.TeacherRepository.Get(), "ID", "Name", subject.TeacherID);
            return View(subject);
        }

        // GET: Subject/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Subject subject = unitOfWork.SubjectRepository.GetByID(id);

            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        // POST: Subject/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Subject subject = unitOfWork.SubjectRepository.GetByID(id);
            unitOfWork.SubjectRepository.Delete(subject);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public ActionResult Statistics()
        {
            var subjects = unitOfWork.SubjectRepository.Get().ToList();
            List<SubjectStatistic> studentsStatistics = new List<SubjectStatistic>();
            subjects.ForEach(s =>
            {
                SubjectStatistic subjectStatistic = new SubjectStatistic();
                subjectStatistic.Name = unitOfWork.SubjectRepository.GetByID(s.ID).Name;
                subjectStatistic.ID = s.ID;
                var enrollements = unitOfWork.EnrollmentRepository
                .Get()
                .Where(e => e.SubjectID == s.ID)
                .ToList();

                enrollements.ForEach(e =>
                {
                    subjectStatistic.Students.Add(new StudentGrade
                    {
                        Name = unitOfWork.StudentRepository.GetByID(e.StudentID).Name,
                        Grade = e.GradeValue 
                    });
                });

                studentsStatistics.Add(subjectStatistic);
            });

            return Json(studentsStatistics, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTeacherOfSubject(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = unitOfWork.SubjectRepository.GetByID(id);
            if (subject == null)
            {
                return HttpNotFound();
            }

            return Json(subject.Teacher, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }

        class SubjectStatistic
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public List<StudentGrade> Students { get; set; } = new List<StudentGrade>();
        }

        class StudentGrade
        {
            public string Name { get; set; }
            public double Grade { get; set; }
        }
    }
}
