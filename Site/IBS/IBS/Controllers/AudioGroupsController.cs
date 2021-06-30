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
    public class AudioGroupsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: AudioGroups
        public ActionResult Index()
        {
            var audioGroups = db.AudioGroups.Include(a => a.PatientType).Where(a=>a.IsDeleted==false).OrderByDescending(a=>a.CreationDate);
            return View(audioGroups.ToList());
        }

        // GET: AudioGroups/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AudioGroup audioGroup = db.AudioGroups.Find(id);
            if (audioGroup == null)
            {
                return HttpNotFound();
            }
            return View(audioGroup);
        }

        // GET: AudioGroups/Create
        public ActionResult Create()
        {
            ViewBag.PatientTypeId = new SelectList(db.PatientTypes, "Id", "Title");
            return View();
        }

        // POST: AudioGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Order,Title,PatientTypeId,IsActive,CreationDate,LastModifiedDate,IsDeleted,DeletionDate,Description")] AudioGroup audioGroup)
        {
            if (ModelState.IsValid)
            {
				audioGroup.IsDeleted=false;
				audioGroup.CreationDate= DateTime.Now; 
					
                audioGroup.Id = Guid.NewGuid();
                db.AudioGroups.Add(audioGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PatientTypeId = new SelectList(db.PatientTypes, "Id", "Title", audioGroup.PatientTypeId);
            return View(audioGroup);
        }

        // GET: AudioGroups/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AudioGroup audioGroup = db.AudioGroups.Find(id);
            if (audioGroup == null)
            {
                return HttpNotFound();
            }
            ViewBag.PatientTypeId = new SelectList(db.PatientTypes, "Id", "Title", audioGroup.PatientTypeId);
            return View(audioGroup);
        }

        // POST: AudioGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Order,Title,PatientTypeId,IsActive,CreationDate,LastModifiedDate,IsDeleted,DeletionDate,Description")] AudioGroup audioGroup)
        {
            if (ModelState.IsValid)
            {
				audioGroup.IsDeleted=false;
					audioGroup.LastModifiedDate=DateTime.Now;
                db.Entry(audioGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PatientTypeId = new SelectList(db.PatientTypes, "Id", "Title", audioGroup.PatientTypeId);
            return View(audioGroup);
        }

        // GET: AudioGroups/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AudioGroup audioGroup = db.AudioGroups.Find(id);
            if (audioGroup == null)
            {
                return HttpNotFound();
            }
            return View(audioGroup);
        }

        // POST: AudioGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            AudioGroup audioGroup = db.AudioGroups.Find(id);
			audioGroup.IsDeleted=true;
			audioGroup.DeletionDate=DateTime.Now;
 
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
