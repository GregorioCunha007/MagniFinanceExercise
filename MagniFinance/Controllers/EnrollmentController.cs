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
    public class EnrollmentController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        // GET: Enrollment
        public ActionResult Index()
        {
            var enrollments = unitOfWork.EnrollmentRepository.Get(); //.Include(e => e.Grade).Include(e => e.Student).Include(e => e.Subject);
            return View(enrollments.ToList());
        }

        // GET: Enrollment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = unitOfWork.EnrollmentRepository.GetByID(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // GET: Enrollment/Create
        public ActionResult Create()
        {
            ViewBag.GradeValue = new SelectList(unitOfWork.GradeRepository.Get(), "Value", "Value");
            ViewBag.StudentID = new SelectList(unitOfWork.StudentRepository.Get(), "ID", "Name");
            ViewBag.SubjectID = new SelectList(unitOfWork.SubjectRepository.Get(), "ID", "Name");
            return View();
        }

        // POST: Enrollment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SubjectID,StudentID,Date,GradeValue")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.EnrollmentRepository.Insert(enrollment);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            ViewBag.GradeValue = new SelectList(unitOfWork.GradeRepository.Get(), "Value", "Value");
            ViewBag.StudentID = new SelectList(unitOfWork.StudentRepository.Get(), "ID", "Name");
            ViewBag.SubjectID = new SelectList(unitOfWork.SubjectRepository.Get(), "ID", "Name");
            return View(enrollment);
        }

        // GET: Enrollment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = unitOfWork.EnrollmentRepository.GetByID(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            ViewBag.GradeValue = new SelectList(unitOfWork.GradeRepository.Get(), "Value", "Value");
            ViewBag.StudentID = new SelectList(unitOfWork.StudentRepository.Get(), "ID", "Name");
            ViewBag.SubjectID = new SelectList(unitOfWork.SubjectRepository.Get(), "ID", "Name");
            return View(enrollment);
        }

        // POST: Enrollment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SubjectID,StudentID,Date,GradeValue")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.EnrollmentRepository.Update(enrollment);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            ViewBag.GradeValue = new SelectList(unitOfWork.GradeRepository.Get(), "Value", "Value");
            ViewBag.StudentID = new SelectList(unitOfWork.StudentRepository.Get(), "ID", "Name");
            ViewBag.SubjectID = new SelectList(unitOfWork.SubjectRepository.Get(), "ID", "Name");
            return View(enrollment);
        }

        // GET: Enrollment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Enrollment enrollment = unitOfWork.EnrollmentRepository.GetByID(id);

            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // POST: Enrollment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Enrollment enrollment = unitOfWork.EnrollmentRepository.GetByID(id);
            unitOfWork.EnrollmentRepository.Delete(enrollment);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
