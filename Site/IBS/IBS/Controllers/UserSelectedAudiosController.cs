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
        public object[] GetWeek(int weekCount)
        {
            object[] chartWeek = new object[] { };
            chartWeek = new object[weekCount];
            chartWeek[0] = "Score";
            for (int j = 1; j < weekCount; j++)
            {
                chartWeek[j] = "Week" + j.ToString();
            }
            return chartWeek;
        }



        public object[] GetChart(string type, string id)
        {
            object[] chartItem = new object[] { };
            var userId = Guid.Parse(User.Identity.Name);
            var userQuestionnaires = db.UserQuestionnaires.Where(w => w.IsDeleted == false && w.UserId == userId).OrderBy(o => o.CreationDate).ToList();
            var answers = db.UserQuestionnaireDetails.Where(w => w.IsDeleted == false);
            List<UserQuestionDetailViewModel> userQuestionDetailViewModelList = new List<UserQuestionDetailViewModel>();
            foreach (var userQuestionId in userQuestionnaires)
            {
                var Id = userQuestionId.Id;
                var questionId = Guid.Parse(id);

                var answer = answers.FirstOrDefault(w => w.UserQuestionnaireId == Id && w.QuestionId == questionId);
                if (id == "3b96726a-8a0d-487c-b992-1a31fa6fd0d7")
                {
                    if (answer != null)
                    {
                        userQuestionDetailViewModelList.Add(new UserQuestionDetailViewModel()
                        {
                            PainAnswer = answer.Answer,
                        });
                    }
                    chartItem = new object[userQuestionDetailViewModelList.Count + 1];
                    chartItem[0] = type;
                    for (int j = 0; j < userQuestionDetailViewModelList.Count; j++)
                    {
                        chartItem[j + 1] = Int32.Parse(userQuestionDetailViewModelList[j].PainAnswer);
                    }

                }
                else if (id == "802374e7-0c5d-4dd8-b88e-e8c9139bb298")
                {
                    if (answer != null)
                    {
                        userQuestionDetailViewModelList.Add(new UserQuestionDetailViewModel()
                        {
                            BloatingAnswer = answer.Answer,
                        });
                    }
                    chartItem = new object[userQuestionDetailViewModelList.Count + 1];
                    chartItem[0] = type;
                    for (int j = 0; j < userQuestionDetailViewModelList.Count; j++)
                    {
                        chartItem[j + 1] = Int32.Parse(userQuestionDetailViewModelList[j].BloatingAnswer);
                    }

                }
                else if (id == "4ec1ece0-44db-4574-8895-af0086f1ba1a")
                {
                    if (answer != null)
                    {
                        userQuestionDetailViewModelList.Add(new UserQuestionDetailViewModel()
                        {
                            WellBeingAnswer = answer.Answer,
                        });
                    }
                    chartItem = new object[userQuestionDetailViewModelList.Count + 1];
                    chartItem[0] = type;
                    for (int j = 0; j < userQuestionDetailViewModelList.Count; j++)
                    {
                        chartItem[j + 1] = Int32.Parse(userQuestionDetailViewModelList[j].WellBeingAnswer);
                    }

                }

            }


            return chartItem;

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
            var userid = Guid.Parse("d066d842-6eab-4f26-a0ac-1219372e35cc");
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


        public ActionResult GetCurrentWeekAudioList()
        {
            var identity = (System.Security.Claims.ClaimsIdentity)User.Identity;

            string id = identity.FindFirst(System.Security.Claims.ClaimTypes.Name).Value;
            var scoreCount = GetChart("Pain", "3b96726a-8a0d-487c-b992-1a31fa6fd0d7").Length; 
            var currentWeek = GetWeek(scoreCount);
            Guid userId = new Guid(id);
            int weekNo = (currentWeek.Count() - 1);
            ViewBag.CurrentWeek = currentWeek.LastOrDefault().ToString();
            List<UserSelectedAudio> userSelectedAudios = db.UserSelectedAudios.Include(u => u.Audio)
                    .Where(u => u.IsDeleted == false && u.UserQuestionnaire.UserId == userId )
                    .OrderByDescending(u => u.CreationDate).Include(u => u.UserQuestionnaire)
                    .ToList();
            string inductionAudio = "";
            string deepeningAudio = "";
            string endingAudio = "";
            string TherapyAudio = "";
            bool isChoose = false;
            bool isAvailable = false;
            Guid audioId1 = Guid.Empty;
            Guid audioId2 = Guid.Empty;
            Guid audioId3 = Guid.Empty;
            Guid audioId4 = Guid.Empty;
            Guid userSelectedId=Guid.Empty;
            var res = new List<UserSelectedAudioViewModel>();
            foreach (var userSelectedAudio in userSelectedAudios)
            {
                if (userSelectedAudio.Audio.AudioGroup.Title.ToLower() == "induction")
                {
                    inductionAudio = userSelectedAudio.Audio.FileUrl;
                    audioId1 = userSelectedAudio.AudioId;
                }
                if (userSelectedAudio.Audio.AudioGroup.Title.ToLower() == "deepening")
                {
                    deepeningAudio = userSelectedAudio.Audio.FileUrl;
                    audioId2 = userSelectedAudio.AudioId;
                }

                if (userSelectedAudio.Audio.AudioGroup.Title.ToLower() == "therapy")
                {
                    TherapyAudio = userSelectedAudio.Audio.FileUrl;
                    audioId3 = userSelectedAudio.AudioId;
                }
                if (userSelectedAudio.Audio.AudioGroup.Title.ToLower() == "ending")
                {
                    endingAudio = userSelectedAudio.Audio.FileUrl;
                    audioId4 = userSelectedAudio.AudioId;
                }
                userSelectedId = userSelectedAudio.Id;
                isChoose = true;
            }

            res.Add(new UserSelectedAudioViewModel()
            {
                Id= userSelectedId,
                DeepeningAudio = deepeningAudio,
                EndingAudio = endingAudio,
                InductionAudio = inductionAudio,
                TherapyAudio = TherapyAudio,
                WeekNo = currentWeek.Count()-1,
                IsChoose = isChoose,
                IsAvailable = isAvailable,
                AudioId1=audioId1, AudioId2 = audioId2,
                AudioId3=audioId3,AudioId4=audioId4
            });

            return View(res.Where(r=>r.WeekNo== weekNo));
        }

        public ActionResult EditSelectedAudio(Guid id, Guid id1, Guid id2,Guid id3,Guid id4, string result, string gender)
        {
            ChooseVoiceViewModel res = new ChooseVoiceViewModel();
            List<ChooseVoiceItemViewModel> audios = new List<ChooseVoiceItemViewModel>();
            
            List<AudioGroup> audioGroups = db.AudioGroups.Where(c => c.IsDeleted == false && c.IsActive)
                .OrderBy(c => c.Order).ToList();

            var identity = (System.Security.Claims.ClaimsIdentity)User.Identity;
             
            var scoreCount = GetChart("Pain", "3b96726a-8a0d-487c-b992-1a31fa6fd0d7").Length;
            var currentWeek = GetWeek(scoreCount);
            Guid userId = new Guid(identity.FindFirst(System.Security.Claims.ClaimTypes.Name).Value);
            int weekNo = (currentWeek.Count() - 1);
            ViewBag.CurrentWeek = currentWeek.LastOrDefault().ToString();
            Guid audioId1 = Guid.Empty;
            Guid audioId2 = Guid.Empty;
            Guid audioId3 = Guid.Empty;
            Guid audioId4 = Guid.Empty;
            if (gender == null)
                gender = "1";

            bool genderBool = true;

            if (gender == "0")
                genderBool = false;

            //if (!string.IsNullOrEmpty(result))
            //{
                res.NeedTherapy = true;
            //}
            //else
            //{
            //    audioGroups = audioGroups.Where(c => c.Title != "Therapy").ToList();
            //}

            foreach (AudioGroup audioGroup in audioGroups)
            {
                audios.Add(new ChooseVoiceItemViewModel()
                {
                    AudioGroup = audioGroup,
                    Audios = db.Audios.Where(c =>
                            c.AudioGroupId == audioGroup.Id && c.IsDeleted == false && c.Gender == genderBool &&
                            c.IsActive == true)
                        .OrderBy(c => c.Order).ToList(),
                    AudioId1=audioId1,
                    AudioId2=audioId2,
                    AudioId3=audioId3,
                    AudioId4=audioId4
                });
            }

            res.Audios = audios;

          
            ViewBag.result = result;
            
            ViewBag.gender = gender;
            ViewBag.qId = id;
            
            return View(res);
        }
    }
}
