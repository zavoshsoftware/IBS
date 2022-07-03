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
            UserQuestionnaire userQuestionnaire = db.UserQuestionnaires.Find(id);
            for (int i = 1; i <= 6; i++)
            {
                List<UserSelectedAudio> userSelectedAudios = db.UserSelectedAudios.Include(u => u.Audio)
                    .Where(u => u.IsDeleted == false && u.UserQuestionnaireId == id && u.WeekNo == i)
                    .OrderByDescending(u => u.CreationDate).Include(u => u.UserQuestionnaire)
                    .ToList();

                int weekNo = i;
                string inductionAudio = "";
                string deepeningAudio = "";
                string endingAudio = "";
                string TherapyAudio = "";
                bool isChoose = false;
                bool isAvailable = false;

                if (DateTime.Today.Date >= userQuestionnaire.CreationDate.AddDays(i * 7))
                    isAvailable = true;

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
                    IsChoose = isChoose,
                    IsAvailable = isAvailable
                });
            }

            return View(res);
        }

     [Authorize(Roles = "customer")]
        public ActionResult List2()
        {
            var identity = (System.Security.Claims.ClaimsIdentity)User.Identity;

            string id = identity.FindFirst(System.Security.Claims.ClaimTypes.Name).Value;

            Guid userId = new Guid(id);

            List<UserSelectedAudioViewModel> res = new List<UserSelectedAudioViewModel>();
            ViewBag.Id = id;
         //   UserQuestionnaire userQuestionnaire = db.UserQuestionnaires.Find(id);
            for (int i = 1; i <= 6; i++)
            {
                List<UserSelectedAudio> userSelectedAudios = db.UserSelectedAudios.Include(u => u.Audio)
                    .Where(u => u.IsDeleted == false && u.UserQuestionnaire.UserId==userId)
                    .OrderByDescending(u => u.CreationDate).Include(u => u.UserQuestionnaire)
                    .ToList();

                int weekNo = i;
                string inductionAudio = "";
                string deepeningAudio = "";
                string endingAudio = "";
                string TherapyAudio = "";
                bool isChoose = false;
                bool isAvailable = false;

                //if (DateTime.Today.Date >= userQuestionnaire.CreationDate.AddDays(i * 7))
                //    isAvailable = true;

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
                    IsChoose = isChoose,
                    IsAvailable = isAvailable
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


        public ActionResult SelectNewAudios(Guid id, int weekNo, string gender)
        {
            ChooseVoiceViewModel res = new ChooseVoiceViewModel();
            List<ChooseVoiceItemViewModel> audios = new List<ChooseVoiceItemViewModel>();

            List<AudioGroup> audioGroups = db.AudioGroups.Where(c => c.IsDeleted == false && c.IsActive)
                .OrderBy(c => c.Order).ToList();
            var userid = Guid.Parse("3CB0F7A9-8683-493F-B4E5-E1EBC9545547");
            var userq = db.UserQuestionnaires.FirstOrDefault(c =>
                c.Id == userid);
            var user = db.Users.FirstOrDefault(u => u.Id == userq.UserId);
            if (gender == null)
                gender = "1";

            bool genderBool = true;

            if (gender == "0")
                genderBool = false;


            //audioGroups = audioGroups.Where(c => c.Title != "Therapy").ToList();


            foreach (AudioGroup audioGroup in audioGroups)
            {
                audios.Add(new ChooseVoiceItemViewModel()
                {
                    AudioGroup = audioGroup,
                    Audios = db.Audios.Where(c =>
                            c.AudioGroupId == audioGroup.Id && c.IsDeleted == false && c.Gender == genderBool &&
                            c.IsActive == true)
                        .OrderBy(c => c.Order).ToList()
                });
            }

            res.Audios = audios;


            //ViewBag.result = result;

            ViewBag.gender = gender;
            ViewBag.qId = id;
            ViewBag.weekNo = weekNo;

            return View(res);
        }

    }
}
