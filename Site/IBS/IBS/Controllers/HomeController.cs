using Microsoft.AspNet.Identity;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ViewModels;

namespace IBS.Controllers
{
    public class HomeController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Home
        public ActionResult Index()
        {
            return RedirectToAction("login", "Account");
        }
        public ActionResult Play()
        {
            return View();
        }
        public ActionResult TestRange()
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            var i = 1;
            var UserId = Guid.Parse(User.Identity.Name);
            var userQuestionnairesIds = db.UserQuestionnaires.Where(w => w.IsDeleted == false && w.UserId == UserId).OrderBy(o => o.CreationDate).ToList();
            var answers = db.UserQuestionnaireDetails.Where(w => w.IsDeleted == false);
            List<UserQuestionDetailViewModel> userQuestionDetailViewModelList = new List<UserQuestionDetailViewModel>();
            List<string> questionIds = new List<string>(new string[] { "3b96726a-8a0d-487c-b992-1a31fa6fd0d7", "802374e7-0c5d-4dd8-b88e-e8c9139bb298", "4ec1ece0-44db-4574-8895-af0086f1ba1a" });

            foreach (var questionId in questionIds)
            {
                var Id =Guid.Parse(questionId);
                foreach (var item in userQuestionnairesIds)
                {
                    var answer = answers.FirstOrDefault(w => w.UserQuestionnaireId == item.Id && w.QuestionId == Id);
                    userQuestionDetailViewModelList.Add(new UserQuestionDetailViewModel()
                    {
                        UserQuestionnaireId = answer.UserQuestionnaireId,
                        QuestionId = questionId,
                        WeekName = $"week{i++}",
                        Answer = answer.Answer,
                        IsDeleted = answer.IsDeleted,
                        IsActive = answer.IsActive,
                        CreationDate = answer.CreationDate
                    });
                }
            }
            //ViewBag.pain = string.Join(",", userQuestionDetailViewModelList.Where(s => s.QuestionId == "3b96726a-8a0d-487c-b992-1a31fa6fd0d7").Select(m => m.Answer));
            //ViewBag.Bloating = string.Join(",", userQuestionDetailViewModelList.Where(s => s.QuestionId == "3b96726a-8a0d-487c-b992-1a31fa6fd0d7").Select(m => m.Answer));
            //ViewBag.WellBeing = string.Join(",", userQuestionDetailViewModelList.Where(s => s.QuestionId == "3b96726a-8a0d-487c-b992-1a31fa6fd0d7").Select(m => m.Answer));

            ViewBag.Json = Newtonsoft.Json.JsonConvert.SerializeObject(userQuestionDetailViewModelList);
            return View(userQuestionDetailViewModelList);
        }

        [HttpGet]
        public string GetChart()
        {
            var i = 1;
            var UserId = Guid.Parse(User.Identity.Name);
            var userQuestionnairesIds = db.UserQuestionnaires.Where(w => w.IsDeleted == false && w.UserId == UserId).OrderBy(o => o.CreationDate).ToList();
            var answers = db.UserQuestionnaireDetails.Where(w => w.IsDeleted == false);
            List<UserQuestionDetailViewModel> userQuestionDetailViewModelList = new List<UserQuestionDetailViewModel>();
            List<string> questionIds = new List<string>(new string[] { "3b96726a-8a0d-487c-b992-1a31fa6fd0d7", "802374e7-0c5d-4dd8-b88e-e8c9139bb298", "4ec1ece0-44db-4574-8895-af0086f1ba1a" });

            foreach (var questionId in questionIds)
            {
                var Id = Guid.Parse(questionId);
                foreach (var item in userQuestionnairesIds)
                {
                    var answer = answers.FirstOrDefault(w => w.UserQuestionnaireId == item.Id && w.QuestionId == Id);
                    userQuestionDetailViewModelList.Add(new UserQuestionDetailViewModel()
                    {
                        UserQuestionnaireId = answer.UserQuestionnaireId,
                        QuestionId = questionId,
                        WeekName = $"week{i++}",
                        Answer = answer.Answer,
                        IsDeleted = answer.IsDeleted,
                        IsActive = answer.IsActive,
                        CreationDate = answer.CreationDate
                    });
                }
            }

            var res = Newtonsoft.Json.JsonConvert.SerializeObject(userQuestionDetailViewModelList);
            return res;

        }

    }
}