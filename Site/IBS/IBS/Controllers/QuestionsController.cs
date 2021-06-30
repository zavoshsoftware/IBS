using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ViewModels;
using Models;

namespace IBS.Controllers
{
    public class QuestionsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Questions
        public ActionResult Index()
        {
            var questions = db.Questions.Include(q => q.AnswerType).Where(q=>q.IsDeleted==false).OrderByDescending(q=>q.CreationDate).Include(q => q.QuestionGroup).Where(q=>q.IsDeleted==false).OrderByDescending(q=>q.CreationDate);
            return View(questions.ToList());
        }

        // GET: Questions/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // GET: Questions/Create
        public ActionResult Create()
        {
            ViewBag.AnswerTypeId = new SelectList(db.AnswerTypes, "Id", "Title");
            ViewBag.QuestionGroupId = new SelectList(db.QuestionGroups, "Id", "Title");
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Order,Title,QuestionGroupId,AnswerTypeId,IsActive,CreationDate,LastModifiedDate,IsDeleted,DeletionDate,Description")] Question question)
        {
            if (ModelState.IsValid)
            {
				question.IsDeleted=false;
				question.CreationDate= DateTime.Now; 
					
                question.Id = Guid.NewGuid();
                db.Questions.Add(question);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AnswerTypeId = new SelectList(db.AnswerTypes, "Id", "Title", question.AnswerTypeId);
            ViewBag.QuestionGroupId = new SelectList(db.QuestionGroups, "Id", "Title", question.QuestionGroupId);
            return View(question);
        }

        // GET: Questions/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            ViewBag.AnswerTypeId = new SelectList(db.AnswerTypes, "Id", "Title", question.AnswerTypeId);
            ViewBag.QuestionGroupId = new SelectList(db.QuestionGroups, "Id", "Title", question.QuestionGroupId);
            return View(question);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Order,Title,QuestionGroupId,AnswerTypeId,IsActive,CreationDate,LastModifiedDate,IsDeleted,DeletionDate,Description")] Question question)
        {
            if (ModelState.IsValid)
            {
				question.IsDeleted=false;
					question.LastModifiedDate=DateTime.Now;
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AnswerTypeId = new SelectList(db.AnswerTypes, "Id", "Title", question.AnswerTypeId);
            ViewBag.QuestionGroupId = new SelectList(db.QuestionGroups, "Id", "Title", question.QuestionGroupId);
            return View(question);
        }

        // GET: Questions/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Question question = db.Questions.Find(id);
			question.IsDeleted=true;
			question.DeletionDate=DateTime.Now;
 
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

        public ActionResult List()
        {
            List<QuestionListViewModel> result = new List<QuestionListViewModel>();

            List<QuestionGroup> questionGroups =
                db.QuestionGroups.Where(c => c.IsDeleted == false && c.IsActive == true).OrderBy(c => c.Order).ToList();

            foreach (QuestionGroup questionGroup in questionGroups)
            {
                result.Add(new QuestionListViewModel()
                {
                    QuestionGroup = questionGroup,

                    Questions = db.Questions.Where(c => c.QuestionGroupId == questionGroup.Id && c.IsDeleted == false)
                        .OrderBy(c => c.Order).ToList()
                });
            }

            return View(result);
        }
    }
}
