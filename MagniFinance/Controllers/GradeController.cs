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
    public class GradeController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        // GET: Grade
        public ActionResult Index()
        {
            var grades = unitOfWork.GradeRepository.Get();
            return View(grades.ToList());
        }

        // GET: Grade/Details/5
        public ActionResult Details(double id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Grade grade = unitOfWork.GradeRepository.GetByID(id);

            if (grade == null)
            {
                return HttpNotFound();
            }
            return View(grade);
        }

        // GET: Grade/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Grade/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Value")] Grade grade)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.GradeRepository.Insert(grade);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(grade);
        }

        // GET: Grade/Edit/5
        public ActionResult Edit(double id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Grade grade = unitOfWork.GradeRepository.GetByID(id);

            if (grade == null)
            {
                return HttpNotFound();
            }
            return View(grade);
        }

        // POST: Grade/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Value")] Grade grade)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.GradeRepository.Update(grade);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(grade);
        }

        // GET: Grade/Delete/5
        public ActionResult Delete(double id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Grade grade = unitOfWork.GradeRepository.GetByID(id);

            if (grade == null)
            {
                return HttpNotFound();
            }
            return View(grade);
        }

        // POST: Grade/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Grade grade = unitOfWork.GradeRepository.GetByID(id);
            unitOfWork.GradeRepository.Delete(grade);
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
