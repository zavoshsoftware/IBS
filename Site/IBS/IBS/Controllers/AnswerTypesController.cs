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
    public class AnswerTypesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: AnswerTypes
        public ActionResult Index()
        {
            return View(db.AnswerTypes.Where(a=>a.IsDeleted==false).OrderByDescending(a=>a.CreationDate).ToList());
        }

        // GET: AnswerTypes/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnswerType answerType = db.AnswerTypes.Find(id);
            if (answerType == null)
            {
                return HttpNotFound();
            }
            return View(answerType);
        }

        // GET: AnswerTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AnswerTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,IsActive,CreationDate,LastModifiedDate,IsDeleted,DeletionDate,Description")] AnswerType answerType)
        {
            if (ModelState.IsValid)
            {
				answerType.IsDeleted=false;
				answerType.CreationDate= DateTime.Now; 
					
                answerType.Id = Guid.NewGuid();
                db.AnswerTypes.Add(answerType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(answerType);
        }

        // GET: AnswerTypes/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnswerType answerType = db.AnswerTypes.Find(id);
            if (answerType == null)
            {
                return HttpNotFound();
            }
            return View(answerType);
        }

        // POST: AnswerTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,IsActive,CreationDate,LastModifiedDate,IsDeleted,DeletionDate,Description")] AnswerType answerType)
        {
            if (ModelState.IsValid)
            {
				answerType.IsDeleted=false;
					answerType.LastModifiedDate=DateTime.Now;
                db.Entry(answerType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(answerType);
        }

        // GET: AnswerTypes/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnswerType answerType = db.AnswerTypes.Find(id);
            if (answerType == null)
            {
                return HttpNotFound();
            }
            return View(answerType);
        }

        // POST: AnswerTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            AnswerType answerType = db.AnswerTypes.Find(id);
			answerType.IsDeleted=true;
			answerType.DeletionDate=DateTime.Now;
 
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
