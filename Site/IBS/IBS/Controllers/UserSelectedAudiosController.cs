using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models;
using ViewModels;

namespace IBS.Controllers
{
    public class UserSelectedAudiosController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: UserSelectedAudios
        public ActionResult Index()
        {
            var userSelectedAudios = db.UserSelectedAudios.Include(u => u.Audio).Where(u => u.IsDeleted == false).OrderByDescending(u => u.CreationDate).Include(u => u.UserQuestionnaire).Where(u => u.IsDeleted == false).OrderByDescending(u => u.CreationDate);
            return View(userSelectedAudios.ToList());
        }
        [Authorize(Roles = "customer")]
        public ActionResult List(Guid id)
        {
            List<UserSelectedAudioViewModel> res = new List<UserSelectedAudioViewModel>();
            ViewBag.Id = id;
            for (int i = 1; i <= 4; i++)
            {
                List<UserSelectedAudio> userSelectedAudios = db.UserSelectedAudios.Include(u => u.Audio)
                    .Where(u => u.IsDeleted == false && u.UserQuestionnaireId == id && u.WeekNo == i)
                    .OrderByDescending(u => u.CreationDate).Include(u => u.UserQuestionnaire)
                    .ToList();

                int weekNo = i;
                string inductionAudio="";
                string deepeningAudio = "";
                string endingAudio = "";
                string TherapyAudio = "";
                bool isChoose = false;

                foreach (var userSelectedAudio in userSelectedAudios)
                {
                    if (userSelectedAudio.Audio.AudioGroup.Title.ToLower() == "induction")
                    {
                        inductionAudio = userSelectedAudio.Audio.FileUrl;
                    }
                    if (userSelectedAudio.Audio.AudioGroup.Title.ToLower() == "deepening")
                    {
                        deepeningAudio = userSelectedAudio.Audio.FileUrl;
                    }

                    if (userSelectedAudio.Audio.AudioGroup.Title.ToLower() == "therapy")
                    {
                        TherapyAudio = userSelectedAudio.Audio.FileUrl;
                    }
                    if (userSelectedAudio.Audio.AudioGroup.Title.ToLower() == "ending")
                    {
                        endingAudio = userSelectedAudio.Audio.FileUrl;
                    }

                    isChoose = true;
                }

                res.Add(new UserSelectedAudioViewModel()
                {
                    DeepeningAudio = deepeningAudio,
                    EndingAudio = endingAudio,
                    InductionAudio = inductionAudio,
                    TherapyAudio = TherapyAudio,
                    WeekNo = weekNo,
                    IsChoose = isChoose
                });
            }

            return View(res);
        }

        // GET: UserSelectedAudios/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserSelectedAudio userSelectedAudio = db.UserSelectedAudios.Find(id);
            if (userSelectedAudio == null)
            {
                return HttpNotFound();
            }
            return View(userSelectedAudio);
        }

        // GET: UserSelectedAudios/Create
        public ActionResult Create()
        {
            ViewBag.AudioId = new SelectList(db.Audios, "Id", "Title");
            ViewBag.UserQuestionnaireId = new SelectList(db.UserQuestionnaires, "Id", "Result");
            return View();
        }

        // POST: UserSelectedAudios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AudioId,WeekNo,UserQuestionnaireId,IsActive,CreationDate,LastModifiedDate,IsDeleted,DeletionDate,Description")] UserSelectedAudio userSelectedAudio)
        {
            if (ModelState.IsValid)
            {
                userSelectedAudio.IsDeleted = false;
                userSelectedAudio.CreationDate = DateTime.Now;

                userSelectedAudio.Id = Guid.NewGuid();
                db.UserSelectedAudios.Add(userSelectedAudio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AudioId = new SelectList(db.Audios, "Id", "Title", userSelectedAudio.AudioId);
            ViewBag.UserQuestionnaireId = new SelectList(db.UserQuestionnaires, "Id", "Result", userSelectedAudio.UserQuestionnaireId);
            return View(userSelectedAudio);
        }

        // GET: UserSelectedAudios/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserSelectedAudio userSelectedAudio = db.UserSelectedAudios.Find(id);
            if (userSelectedAudio == null)
            {
                return HttpNotFound();
            }
            ViewBag.AudioId = new SelectList(db.Audios, "Id", "Title", userSelectedAudio.AudioId);
            ViewBag.UserQuestionnaireId = new SelectList(db.UserQuestionnaires, "Id", "Result", userSelectedAudio.UserQuestionnaireId);
            return View(userSelectedAudio);
        }

        // POST: UserSelectedAudios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AudioId,WeekNo,UserQuestionnaireId,IsActive,CreationDate,LastModifiedDate,IsDeleted,DeletionDate,Description")] UserSelectedAudio userSelectedAudio)
        {
            if (ModelState.IsValid)
            {
                userSelectedAudio.IsDeleted = false;
                userSelectedAudio.LastModifiedDate = DateTime.Now;
                db.Entry(userSelectedAudio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AudioId = new SelectList(db.Audios, "Id", "Title", userSelectedAudio.AudioId);
            ViewBag.UserQuestionnaireId = new SelectList(db.UserQuestionnaires, "Id", "Result", userSelectedAudio.UserQuestionnaireId);
            return View(userSelectedAudio);
        }

        // GET: UserSelectedAudios/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserSelectedAudio userSelectedAudio = db.UserSelectedAudios.Find(id);
            if (userSelectedAudio == null)
            {
                return HttpNotFound();
            }
            return View(userSelectedAudio);
        }

        // POST: UserSelectedAudios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            UserSelectedAudio userSelectedAudio = db.UserSelectedAudios.Find(id);
            userSelectedAudio.IsDeleted = true;
            userSelectedAudio.DeletionDate = DateTime.Now;

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


        public ActionResult SelectNewAudios(Guid id,int weekNo)
        {
       
            return View();
        }

    }
}
