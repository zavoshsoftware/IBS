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
    public class AnswerItemsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public ActionResult Index(Guid id)
        {
            var answerItems = db.AnswerItems.Include(a => a.Question)
                .Where(a => a.QuestionId == id && a.IsDeleted == false).OrderByDescending(a => a.CreationDate);

            return View(answerItems.ToList());
        }

        public ActionResult Create(Guid id)
        {
            ViewBag.QuestionId = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AnswerItem answerItem, Guid id)
        {
            if (ModelState.IsValid)
            {
                answerItem.IsDeleted = false;
                answerItem.CreationDate = DateTime.Now;
                answerItem.QuestionId = id;
                answerItem.Id = Guid.NewGuid();
                db.AnswerItems.Add(answerItem);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = id });
            }

            ViewBag.QuestionId = id;
            return View(answerItem);
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnswerItem answerItem = db.AnswerItems.Find(id);
            if (answerItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionId = answerItem.QuestionId;
            return View(answerItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( AnswerItem answerItem)
        {
            if (ModelState.IsValid)
            {
                answerItem.IsDeleted = false;
                answerItem.LastModifiedDate = DateTime.Now;
                db.Entry(answerItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = answerItem.QuestionId });
            }
            ViewBag.QuestionId = answerItem.QuestionId;
            return View(answerItem);
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnswerItem answerItem = db.AnswerItems.Find(id);
            if (answerItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionId = answerItem.QuestionId;

            return View(answerItem);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            AnswerItem answerItem = db.AnswerItems.Find(id);
            answerItem.IsDeleted = true;
            answerItem.DeletionDate = DateTime.Now;

            db.SaveChanges();
            return RedirectToAction("Index", new { id = answerItem.QuestionId });
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
