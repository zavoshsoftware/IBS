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
    public class AudiosController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Audios
        public ActionResult Index()
        {
            var audios = db.Audios.Include(a => a.AudioGroup).Where(a=>a.IsDeleted==false).OrderByDescending(a=>a.CreationDate);
            return View(audios.ToList());
        }

        // GET: Audios/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Audio audio = db.Audios.Find(id);
            if (audio == null)
            {
                return HttpNotFound();
            }
            return View(audio);
        }

        // GET: Audios/Create
        public ActionResult Create()
        {
            ViewBag.AudioGroupId = new SelectList(db.AudioGroups, "Id", "Title");
            return View();
        }

        // POST: Audios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Order,Title,AudioGroupId,FileUrl,IsActive,CreationDate,LastModifiedDate,IsDeleted,DeletionDate,Description")] Audio audio)
        {
            if (ModelState.IsValid)
            {
				audio.IsDeleted=false;
				audio.CreationDate= DateTime.Now; 
					
                audio.Id = Guid.NewGuid();
                db.Audios.Add(audio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AudioGroupId = new SelectList(db.AudioGroups, "Id", "Title", audio.AudioGroupId);
            return View(audio);
        }

        // GET: Audios/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Audio audio = db.Audios.Find(id);
            if (audio == null)
            {
                return HttpNotFound();
            }
            ViewBag.AudioGroupId = new SelectList(db.AudioGroups, "Id", "Title", audio.AudioGroupId);
            return View(audio);
        }

        // POST: Audios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Order,Title,AudioGroupId,FileUrl,IsActive,CreationDate,LastModifiedDate,IsDeleted,DeletionDate,Description")] Audio audio)
        {
            if (ModelState.IsValid)
            {
				audio.IsDeleted=false;
					audio.LastModifiedDate=DateTime.Now;
                db.Entry(audio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AudioGroupId = new SelectList(db.AudioGroups, "Id", "Title", audio.AudioGroupId);
            return View(audio);
        }

        // GET: Audios/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Audio audio = db.Audios.Find(id);
            if (audio == null)
            {
                return HttpNotFound();
            }
            return View(audio);
        }

        // POST: Audios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Audio audio = db.Audios.Find(id);
			audio.IsDeleted=true;
			audio.DeletionDate=DateTime.Now;
 
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
