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
            var questions = db.Questions.Include(q => q.AnswerType).Where(q => q.IsDeleted == false).OrderByDescending(q => q.CreationDate).Include(q => q.QuestionGroup).Where(q => q.IsDeleted == false).OrderByDescending(q => q.CreationDate);
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
                question.IsDeleted = false;
                question.CreationDate = DateTime.Now;

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
                question.IsDeleted = false;
                question.LastModifiedDate = DateTime.Now;
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
            question.IsDeleted = true;
            question.DeletionDate = DateTime.Now;

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

        public ActionResult SubmitQuestion(QuestionSubmitViewModel input)
        {
            try
            {

                var identity = (System.Security.Claims.ClaimsIdentity)User.Identity;

                string id = identity.FindFirst(System.Security.Claims.ClaimTypes.Name).Value;

                User user = db.Users.Find(ToGuid(id));

                string result = "";

                if (input.Q1.ToLower() == "yes")
                    result += "pain-";

                if (input.Q4.ToLower() == "yes")
                    result += "bloating-";

                UserQuestionnaire userQuestionnaire = new UserQuestionnaire()
                {
                    Id = Guid.NewGuid(),
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    UserId = user.Id,
                    Result = result
                };
                db.UserQuestionnaires.Add(userQuestionnaire);

                InsertToUserQuestionarieDetail(ToGuid("3592deef-ce88-45fc-bf3d-4fef9f767aaf"), null, input.Q1,
                    userQuestionnaire.Id);

                InsertToUserQuestionarieDetail(ToGuid("3b96726a-8a0d-487c-b992-1a31fa6fd0d7"), null, input.Q2,
                    userQuestionnaire.Id);

                InsertToUserQuestionarieDetail(ToGuid("fb85df1d-e3f7-46db-a1dc-b5f70e23438e"), null, input.Q3,
                    userQuestionnaire.Id);

                InsertToUserQuestionarieDetail(ToGuid("18a28495-9433-4493-a1ae-e751eb1faf5d"), null, input.Q4,
                    userQuestionnaire.Id);

                InsertToUserQuestionarieDetail(ToGuid("802374e7-0c5d-4dd8-b88e-e8c9139bb298"), null, input.Q5,
                    userQuestionnaire.Id);

                InsertToUserQuestionarieDetail(ToGuid("44edc10a-4271-4a60-a34f-6bbf258f046c"), null, input.Q6,
                    userQuestionnaire.Id);

                InsertToUserQuestionarieDetail(ToGuid("4ec1ece0-44db-4574-8895-af0086f1ba1a"), null, input.Q7,
                    userQuestionnaire.Id);

                int score = 0;

                score += InsertToUserQuestionarieDetail(null, 1, input.R1, userQuestionnaire.Id).Value;
                score += InsertToUserQuestionarieDetail(null, 2, input.R2, userQuestionnaire.Id).Value;
                score += InsertToUserQuestionarieDetail(null, 3, input.R3, userQuestionnaire.Id).Value;
                score += InsertToUserQuestionarieDetail(null, 4, input.R4, userQuestionnaire.Id).Value;
                score += InsertToUserQuestionarieDetail(null, 5, input.R5, userQuestionnaire.Id).Value;
                score += InsertToUserQuestionarieDetail(null, 6, input.R6, userQuestionnaire.Id).Value;
                score += InsertToUserQuestionarieDetail(null, 7, input.R7, userQuestionnaire.Id).Value;
                score += InsertToUserQuestionarieDetail(null, 8, input.R8, userQuestionnaire.Id).Value;
                score += InsertToUserQuestionarieDetail(null, 9, input.R9, userQuestionnaire.Id).Value;
                score += InsertToUserQuestionarieDetail(null, 10, input.R10, userQuestionnaire.Id).Value;
                score += InsertToUserQuestionarieDetail(null, 11, input.R11, userQuestionnaire.Id).Value;
                score += InsertToUserQuestionarieDetail(null, 12, input.R12, userQuestionnaire.Id).Value;
                score += InsertToUserQuestionarieDetail(null, 13, input.R13, userQuestionnaire.Id).Value;
                score += InsertToUserQuestionarieDetail(null, 14, input.R14, userQuestionnaire.Id).Value;

                if (score > 11)
                {
                    userQuestionnaire.Result += "Anxiety-score:" + score;
                }
                db.SaveChanges();

                return Json(userQuestionnaire.Id + "|" + userQuestionnaire.Result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }

        public Guid ToGuid(string id)
        {
            return new Guid(id);
        }

        public int? InsertToUserQuestionarieDetail(Guid? questionId, int? radioId, string answer,
            Guid userQuestionnaireId)
        {
            if (questionId != null)
            {
                UserQuestionnaireDetail userQuestionnaireDetail = new UserQuestionnaireDetail()
                {
                    Id = Guid.NewGuid(),
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    UserQuestionnaireId = userQuestionnaireId,
                    Answer = answer,
                    QuestionId = questionId.Value,

                };

                db.UserQuestionnaireDetails.Add(userQuestionnaireDetail);
            }
            else if (radioId != null)
            {
                Guid hadScoreGroupId = new Guid("AC610C67-ECC2-47FB-BE02-09A41E031D16");
                Question question = db.Questions.FirstOrDefault(v => v.Order == radioId && v.QuestionGroupId == hadScoreGroupId && v.IsDeleted == false);
                Guid answerId = ToGuid(answer);
                AnswerItem answerItem = db.AnswerItems.FirstOrDefault(c => c.Id == answerId);

                if (question != null && answerItem != null)
                {
                    UserQuestionnaireDetail userQuestionnaireDetail = new UserQuestionnaireDetail()
                    {
                        Id = Guid.NewGuid(),
                        CreationDate = DateTime.Now,
                        IsActive = true,
                        IsDeleted = false,
                        UserQuestionnaireId = userQuestionnaireId,
                        Answer = answerItem.Title,
                        QuestionId = question.Id,

                    };

                    db.UserQuestionnaireDetails.Add(userQuestionnaireDetail);

                    return answerItem.Score;
                }
            }

            return null;

        }
    }
}
