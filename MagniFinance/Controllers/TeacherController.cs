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
    public class TeacherController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        // GET: Teacher
        public ActionResult Index()
        {
            var teachers = unitOfWork.TeacherRepository.Get();
            return View(teachers.ToList());
        }

        public ActionResult FindById(int id)
        {
            var teacher = unitOfWork.TeacherRepository.GetByID(id);
            return Json(teacher, JsonRequestBehavior.AllowGet);
        }

        // GET: Teacher/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = unitOfWork.TeacherRepository.GetByID(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // GET: Teacher/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Teacher/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,DateOfBirth,Salary")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.TeacherRepository.Insert(teacher);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(teacher);
        }

        // GET: Teacher/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = unitOfWork.TeacherRepository.GetByID(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: Teacher/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,DateOfBirth,Salary")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.TeacherRepository.Update(teacher);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(teacher);
        }

        // GET: Teacher/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = unitOfWork.TeacherRepository.GetByID(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: Teacher/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Teacher teacher = unitOfWork.TeacherRepository.GetByID(id);
            unitOfWork.TeacherRepository.Delete(teacher);
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
