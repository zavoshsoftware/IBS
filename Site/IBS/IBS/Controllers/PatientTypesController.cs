using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models;

namespace IBS.Controllers
{
    public class PatientTypesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: PatientTypes
        public ActionResult Index()
        {
            return View(db.PatientTypes.Where(a=>a.IsDeleted==false).OrderByDescending(a=>a.CreationDate).ToList());
        }

        // GET: PatientTypes/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientType patientType = db.PatientTypes.Find(id);
            if (patientType == null)
            {
                return HttpNotFound();
            }
            return View(patientType);
        }

        // GET: PatientTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PatientTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,IsActive,CreationDate,LastModifiedDate,IsDeleted,DeletionDate,Description")] PatientType patientType)
        {
            if (ModelState.IsValid)
            {
				patientType.IsDeleted=false;
				patientType.CreationDate= DateTime.Now; 
					
                patientType.Id = Guid.NewGuid();
                db.PatientTypes.Add(patientType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(patientType);
        }

        // GET: PatientTypes/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientType patientType = db.PatientTypes.Find(id);
            if (patientType == null)
            {
                return HttpNotFound();
            }
            return View(patientType);
        }

        // POST: PatientTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,IsActive,CreationDate,LastModifiedDate,IsDeleted,DeletionDate,Description")] PatientType patientType)
        {
            if (ModelState.IsValid)
            {
				patientType.IsDeleted=false;
					patientType.LastModifiedDate=DateTime.Now;
                db.Entry(patientType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patientType);
        }

        // GET: PatientTypes/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientType patientType = db.PatientTypes.Find(id);
            if (patientType == null)
            {
                return HttpNotFound();
            }
            return View(patientType);
        }

        // POST: PatientTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            PatientType patientType = db.PatientTypes.Find(id);
			patientType.IsDeleted=true;
			patientType.DeletionDate=DateTime.Now;
 
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
