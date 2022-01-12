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
    public class UserQuestionnairesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: UserQuestionnaires
        public ActionResult Index()
        {
            var userQuestionnaires = db.UserQuestionnaires.Include(u => u.PatientType).Where(u=>u.IsDeleted==false).OrderByDescending(u=>u.CreationDate).Include(u => u.User).Where(u=>u.IsDeleted==false).OrderByDescending(u=>u.CreationDate);
            return View(userQuestionnaires.ToList());
        }
  public ActionResult GetChart()
        {
            return View();
        }

        // GET: UserQuestionnaires/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserQuestionnaire userQuestionnaire = db.UserQuestionnaires.Find(id);
            if (userQuestionnaire == null)
            {
                return HttpNotFound();
            }

            UserQuestionnaireDetailViewModel result = new UserQuestionnaireDetailViewModel()
            {
                UserQuestionnaire = userQuestionnaire,
            
                UserQuestionnaireDetails = db.UserQuestionnaireDetails
                    .Include(c=>c.Question) .Where(c => c.UserQuestionnaireId == userQuestionnaire.Id && c.IsDeleted == false).
                    OrderBy(c=>c.Question.QuestionGroupId).ThenBy(c=>c.Question.Order).ToList()
            };

            return View(result);
        }

        // GET: UserQuestionnaires/Create
        public ActionResult Create()
        {
            ViewBag.PatientTypeId = new SelectList(db.PatientTypes, "Id", "Title");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Password");
            return View();
        }

        // POST: UserQuestionnaires/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,StartDate,EndDate,Result,PatientTypeId,IsActive,CreationDate,LastModifiedDate,IsDeleted,DeletionDate,Description")] UserQuestionnaire userQuestionnaire)
        {
            if (ModelState.IsValid)
            {
				userQuestionnaire.IsDeleted=false;
				userQuestionnaire.CreationDate= DateTime.Now; 
					
                userQuestionnaire.Id = Guid.NewGuid();
                db.UserQuestionnaires.Add(userQuestionnaire);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PatientTypeId = new SelectList(db.PatientTypes, "Id", "Title", userQuestionnaire.PatientTypeId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Password", userQuestionnaire.UserId);
            return View(userQuestionnaire);
        }

        // GET: UserQuestionnaires/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserQuestionnaire userQuestionnaire = db.UserQuestionnaires.Find(id);
            if (userQuestionnaire == null)
            {
                return HttpNotFound();
            }
            ViewBag.PatientTypeId = new SelectList(db.PatientTypes, "Id", "Title", userQuestionnaire.PatientTypeId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Password", userQuestionnaire.UserId);
            return View(userQuestionnaire);
        }

        // POST: UserQuestionnaires/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,StartDate,EndDate,Result,PatientTypeId,IsActive,CreationDate,LastModifiedDate,IsDeleted,DeletionDate,Description")] UserQuestionnaire userQuestionnaire)
        {
            if (ModelState.IsValid)
            {
				userQuestionnaire.IsDeleted=false;
					userQuestionnaire.LastModifiedDate=DateTime.Now;
                db.Entry(userQuestionnaire).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PatientTypeId = new SelectList(db.PatientTypes, "Id", "Title", userQuestionnaire.PatientTypeId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Password", userQuestionnaire.UserId);
            return View(userQuestionnaire);
        }

        // GET: UserQuestionnaires/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserQuestionnaire userQuestionnaire = db.UserQuestionnaires.Find(id);
            if (userQuestionnaire == null)
            {
                return HttpNotFound();
            }
            return View(userQuestionnaire);
        }

        // POST: UserQuestionnaires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            UserQuestionnaire userQuestionnaire = db.UserQuestionnaires.Find(id);
			userQuestionnaire.IsDeleted=true;
			userQuestionnaire.DeletionDate=DateTime.Now;
 
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

        [Authorize(Roles = "customer")]
        public ActionResult List()
        {
            var identity = (System.Security.Claims.ClaimsIdentity)User.Identity;

            string id = identity.FindFirst(System.Security.Claims.ClaimTypes.Name).Value;
            Guid userId = new Guid(id);

            var userQuestionnaires = db.UserQuestionnaires.Include(u => u.PatientType).Where(u => u.IsDeleted == false)
                .OrderByDescending(u => u.CreationDate).Include(u => u.User)
                .Where(c=>c.UserId==userId&&c.IsDeleted==false);

            return View(userQuestionnaires.ToList());
        }

        // GET: UserQuestionnaires/Details/5
        public ActionResult UserDetails(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserQuestionnaire userQuestionnaire = db.UserQuestionnaires.Find(id);
            if (userQuestionnaire == null)
            {
                return HttpNotFound();
            }

            UserQuestionnaireDetailViewModel result = new UserQuestionnaireDetailViewModel()
            {
                UserQuestionnaire = userQuestionnaire,

                UserQuestionnaireDetails = db.UserQuestionnaireDetails
                    .Include(c => c.Question).Where(c => c.UserQuestionnaireId == userQuestionnaire.Id && c.IsDeleted == false).
                    OrderBy(c => c.Question.QuestionGroupId).ThenBy(c => c.Question.Order).ToList()
            };

            return View(result);
        }


    }
}
