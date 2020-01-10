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
    public class CourseController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        // GET: Course
        public ActionResult Index()
        {
            var courses = unitOfWork.CourseRepository.Get();
            return View(courses.ToList());
        }

        // GET: Course/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Course course = unitOfWork.CourseRepository.GetByID(id);

            if (course == null)
            {
                return HttpNotFound();
            }

            return View(course);
        }

        // GET: Course/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Course/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] Course course)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.CourseRepository.Insert(course);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(course);
        }

        // GET: Course/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Course course = unitOfWork.CourseRepository.GetByID(id);

            if (course == null)
            {
                return HttpNotFound();
            }

            return View(course);
        }

        // POST: Course/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] Course course)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.CourseRepository.Update(course);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(course);
        }

        // GET: Course/Delete/5
        public ActionResult Delete(int? id)
        {
            Course course = unitOfWork.CourseRepository.GetByID(id);
            return View(course);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = unitOfWork.CourseRepository.GetByID(id);
            unitOfWork.CourseRepository.Delete(id);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Statistics()
        {
            List<CourseStatistics> courseStatistics = new List<CourseStatistics>();
            unitOfWork.CourseRepository.Get()
                .ToList()
                .ForEach(c =>
                {
                    var subjects = unitOfWork.SubjectRepository.Get().Where(s => s.CourseID == c.ID);
                    var subjectsIDs = subjects.Select(s => s.ID);
                    int subjectsCount = subjects.Where(s => s.CourseID == c.ID).Count();
                    int teachersCount = subjects.Select(t => t.TeacherID).Distinct().Count();
                    var enrollments = unitOfWork.EnrollmentRepository.Get().Where(e => subjectsIDs.Contains(e.SubjectID));
                    double avg = 0;
                    if (enrollments.Count() > 0) avg = enrollments.Average(e => e.GradeValue);

                    courseStatistics.Add(new CourseStatistics
                    {
                        Name = c.Name,
                        TeachersCount = teachersCount,
                        SubjectsCount = subjectsCount,
                        Average = avg
                    });
                });

            return Json(courseStatistics, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }

        class CourseStatistics
        { 
            public string Name { get; set; }
            public int TeachersCount { get; set; }
            public int SubjectsCount { get; set; }
            public double Average { get; set; }
        }
    }
}
