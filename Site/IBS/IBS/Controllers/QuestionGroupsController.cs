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
    public class QuestionGroupsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: QuestionGroups
        public ActionResult Index()
        {
            return View(db.QuestionGroups.Where(a=>a.IsDeleted==false).OrderByDescending(a=>a.CreationDate).ToList());
        }

        // GET: QuestionGroups/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionGroup questionGroup = db.QuestionGroups.Find(id);
            if (questionGroup == null)
            {
                return HttpNotFound();
            }
            return View(questionGroup);
        }

        // GET: QuestionGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuestionGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Order,Title,IsActive,CreationDate,LastModifiedDate,IsDeleted,DeletionDate,Description")] QuestionGroup questionGroup)
        {
            if (ModelState.IsValid)
            {
				questionGroup.IsDeleted=false;
				questionGroup.CreationDate= DateTime.Now; 
					
                questionGroup.Id = Guid.NewGuid();
                db.QuestionGroups.Add(questionGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(questionGroup);
        }

        // GET: QuestionGroups/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionGroup questionGroup = db.QuestionGroups.Find(id);
            if (questionGroup == null)
            {
                return HttpNotFound();
            }
            return View(questionGroup);
        }

        // POST: QuestionGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Order,Title,IsActive,CreationDate,LastModifiedDate,IsDeleted,DeletionDate,Description")] QuestionGroup questionGroup)
        {
            if (ModelState.IsValid)
            {
				questionGroup.IsDeleted=false;
					questionGroup.LastModifiedDate=DateTime.Now;
                db.Entry(questionGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(questionGroup);
        }

        // GET: QuestionGroups/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionGroup questionGroup = db.QuestionGroups.Find(id);
            if (questionGroup == null)
            {
                return HttpNotFound();
            }
            return View(questionGroup);
        }

        // POST: QuestionGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            QuestionGroup questionGroup = db.QuestionGroups.Find(id);
			questionGroup.IsDeleted=true;
			questionGroup.DeletionDate=DateTime.Now;
 
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
