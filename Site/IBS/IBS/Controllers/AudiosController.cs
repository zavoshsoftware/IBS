using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models;
using ViewModels;

namespace IBS.Controllers
{
    public class AudiosController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Audios
        public ActionResult Index()
        {
            var audios = db.Audios.Include(a => a.AudioGroup).Where(a => a.IsDeleted == false).OrderByDescending(a => a.CreationDate);
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
        public ActionResult Create(Audio audio, HttpPostedFileBase fileupload)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed
                string newFilenameUrl = string.Empty;
                if (fileupload != null)
                {
                    string filename = Path.GetFileName(fileupload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    newFilenameUrl = "/Uploads/audio/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);
                    fileupload.SaveAs(physicalFilename);
                    audio.FileUrl = newFilenameUrl;
                }


                #endregion
                audio.IsDeleted = false;
                audio.CreationDate = DateTime.Now;

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
        public ActionResult Edit(Audio audio, HttpPostedFileBase fileupload)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed
                string newFilenameUrl = string.Empty;
                if (fileupload != null)
                {
                    string filename = Path.GetFileName(fileupload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    newFilenameUrl = "/Uploads/audio/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);
                    fileupload.SaveAs(physicalFilename);
                    audio.FileUrl = newFilenameUrl;
                }


                #endregion
                audio.IsDeleted = false;
                audio.LastModifiedDate = DateTime.Now;
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
            audio.IsDeleted = true;
            audio.DeletionDate = DateTime.Now;

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

        public void DataEnter()
        {
            for (int i = 1; i < 4; i++)
            {

                Audio audio = new Audio()
                {
                    Id = Guid.NewGuid(),
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    Title = "audio women" + i,
                    AudioGroupId = new Guid("8F1E5363-6EB6-4E15-BF5A-A4ED82D853F5"),
                    FileUrl = "/Uploads/audio/6c7d5ba9d2e6420daa40ba9e69563b47.mp3",
                    Gender = false,
                    Order = i
                };
                db.Audios.Add(audio);

                Audio audio2 = new Audio()
                {
                    Id = Guid.NewGuid(),
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    Title = "audio men" + i,
                    AudioGroupId = new Guid("8F1E5363-6EB6-4E15-BF5A-A4ED82D853F5"),
                    FileUrl = "/Uploads/audio/6c7d5ba9d2e6420daa40ba9e69563b47.mp3",
                    Gender = true,
                    Order = i
                };
                db.Audios.Add(audio2);
            }

            for (int i = 1; i < 5; i++)
            {

                Audio audio = new Audio()
                {
                    Id = Guid.NewGuid(),
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    Title = "audio women" + i,
                    AudioGroupId = new Guid("1E5656C8-D07D-4F33-94CB-2EEFF052CC42"),
                    FileUrl = "/Uploads/audio/6c7d5ba9d2e6420daa40ba9e69563b47.mp3",
                    Gender = false,
                    Order = i
                };
                db.Audios.Add(audio);

                Audio audio2 = new Audio()
                {
                    Id = Guid.NewGuid(),
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    Title = "audio men" + i,
                    AudioGroupId = new Guid("1E5656C8-D07D-4F33-94CB-2EEFF052CC42"),
                    FileUrl = "/Uploads/audio/6c7d5ba9d2e6420daa40ba9e69563b47.mp3",
                    Gender = true,
                    Order = i
                };
                db.Audios.Add(audio2);
            }

            for (int i = 1; i < 5; i++)
            {

                Audio audio = new Audio()
                {
                    Id = Guid.NewGuid(),
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    Title = "audio women" + i,
                    AudioGroupId = new Guid("7B678AB5-0838-4D4A-95D0-8952F74A99D9"),
                    FileUrl = "/Uploads/audio/6c7d5ba9d2e6420daa40ba9e69563b47.mp3",
                    Gender = false,
                    Order = i
                };
                db.Audios.Add(audio);

                Audio audio2 = new Audio()
                {
                    Id = Guid.NewGuid(),
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    Title = "audio men" + i,
                    AudioGroupId = new Guid("7B678AB5-0838-4D4A-95D0-8952F74A99D9"),
                    FileUrl = "/Uploads/audio/6c7d5ba9d2e6420daa40ba9e69563b47.mp3",
                    Gender = true,
                    Order = i
                };
                db.Audios.Add(audio2);
            }

            for (int i = 1; i < 5; i++)
            {

                Audio audio = new Audio()
                {
                    Id = Guid.NewGuid(),
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    Title = "audio women" + i,
                    AudioGroupId = new Guid("92231FC8-8BC0-4F3D-9677-09E3D4B64FCA"),
                    FileUrl = "/Uploads/audio/6c7d5ba9d2e6420daa40ba9e69563b47.mp3",
                    Gender = false,
                    Order = i
                };
                db.Audios.Add(audio);

                Audio audio2 = new Audio()
                {
                    Id = Guid.NewGuid(),
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    Title = "audio men" + i,
                    AudioGroupId = new Guid("92231FC8-8BC0-4F3D-9677-09E3D4B64FCA"),
                    FileUrl = "/Uploads/audio/6c7d5ba9d2e6420daa40ba9e69563b47.mp3",
                    Gender = true,
                    Order = i
                };
                db.Audios.Add(audio2);
            }

            db.SaveChanges();
        }

        public ActionResult ChooseVoice(Guid qId,string result, string gender)
        {
            ChooseVoiceViewModel res = new ChooseVoiceViewModel();
            List<ChooseVoiceItemViewModel> audios = new List<ChooseVoiceItemViewModel>();
            
            List<AudioGroup> audioGroups = db.AudioGroups.Where(c => c.IsDeleted == false && c.IsActive)
                .OrderBy(c => c.Order).ToList();

            if (gender == null)
                gender = "1";

            bool genderBool = true;

            if (gender == "0")
                genderBool = false;

            if (!string.IsNullOrEmpty(result))
            {
                res.NeedTherapy = true;
            }
            else
            {
                audioGroups = audioGroups.Where(c => c.Title != "Therapy").ToList();
            }

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

          
            ViewBag.result = result;
            
            ViewBag.gender = gender;
            ViewBag.qId = qId;
            
            return View(res);
        }


        public ActionResult SubmitaudioCollection(AudioSelectionViewModel input)
        {
            try
            {
                string audio1 = input.Audiomen1;
                string audio2 = input.Audiomen2;
                string audio3 = input.Audiomen3;
                string audio4 = input.Audiomen4;
                if (input.gender == "0")
                {
                    audio1 = input.Audiowomen1;
                    audio2 = input.Audiowomen2;
                    audio3 = input.Audiowomen3;
                    audio4 = input.Audiowomen4;
                }

                UserSelectedAudio userSelectedAudio = new UserSelectedAudio()
                {
                    Id = Guid.NewGuid(),
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    UserQuestionnaireId = new Guid(input.QuestionnarieId),
                    AudioId = new Guid(audio1),
                    WeekNo = input.WeekNo,
                };

                db.UserSelectedAudios.Add(userSelectedAudio);

                userSelectedAudio = new UserSelectedAudio()
                {
                    Id = Guid.NewGuid(),
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    UserQuestionnaireId = new Guid(input.QuestionnarieId),
                    AudioId = new Guid(audio2),
                    WeekNo = input.WeekNo,
                };

                db.UserSelectedAudios.Add(userSelectedAudio);

                userSelectedAudio = new UserSelectedAudio()
                {
                    Id = Guid.NewGuid(),
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    UserQuestionnaireId = new Guid(input.QuestionnarieId),
                    AudioId = new Guid(audio3),
                    WeekNo = input.WeekNo,
                };

                db.UserSelectedAudios.Add(userSelectedAudio);

                userSelectedAudio = new UserSelectedAudio()
                {
                    Id = Guid.NewGuid(),
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    UserQuestionnaireId = new Guid(input.QuestionnarieId),
                    AudioId = new Guid(audio4),
                    WeekNo = input.WeekNo,
                };

                db.UserSelectedAudios.Add(userSelectedAudio);

                db.SaveChanges();
                return Json("true", JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }
         

        public ActionResult SubmitEditaudioCollection(AudioSelectionViewModel input)
        {
            try
            {
                string audio1 = input.Audiomen1;
                string audio2 = input.Audiomen2;
                string audio3 = input.Audiomen3;
                string audio4 = input.Audiomen4;
                if (input.gender == "0")
                {
                    audio1 = input.Audiowomen1;
                    audio2 = input.Audiowomen2;
                    audio3 = input.Audiowomen3;
                    audio4 = input.Audiowomen4;
                }

                UserSelectedAudio userSelectedAudio = new UserSelectedAudio()
                {
                    Id = Guid.NewGuid(),
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    UserQuestionnaireId = new Guid(input.QuestionnarieId),
                    AudioId = new Guid(audio1),
                    WeekNo = input.WeekNo,
                };

                db.UserSelectedAudios.Add(userSelectedAudio);

                userSelectedAudio = new UserSelectedAudio()
                {
                    Id = Guid.NewGuid(),
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    UserQuestionnaireId = new Guid(input.QuestionnarieId),
                    AudioId = new Guid(audio2),
                    WeekNo = input.WeekNo,
                };

                db.UserSelectedAudios.Add(userSelectedAudio);

                userSelectedAudio = new UserSelectedAudio()
                {
                    Id = Guid.NewGuid(),
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    UserQuestionnaireId = new Guid(input.QuestionnarieId),
                    AudioId = new Guid(audio3),
                    WeekNo = input.WeekNo,
                };

                db.UserSelectedAudios.Add(userSelectedAudio);

                userSelectedAudio = new UserSelectedAudio()
                {
                    Id = Guid.NewGuid(),
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    UserQuestionnaireId = new Guid(input.QuestionnarieId),
                    AudioId = new Guid(audio4),
                    WeekNo = input.WeekNo,
                };

                db.UserSelectedAudios.Add(userSelectedAudio);

                db.SaveChanges();
                return Json("true", JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }

    }
}
