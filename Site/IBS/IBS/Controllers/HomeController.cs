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
            var scoreCount = GetChart("Pain", "3b96726a-8a0d-487c-b992-1a31fa6fd0d7").Length;
            ViewBag.Pain = GetChart("Pain", "3b96726a-8a0d-487c-b992-1a31fa6fd0d7");
            ViewBag.Bloating = GetChart("Bloating", "802374e7-0c5d-4dd8-b88e-e8c9139bb298");
            ViewBag.WellBeing = GetChart("WellBeing", "4ec1ece0-44db-4574-8895-af0086f1ba1a");
            ViewBag.Score = GetWeek(scoreCount);
            return View();
        }

        public object[] GetChart(string type, string id)
        {
            object[] chartItem = new object[]{};
            var UserId = Guid.Parse(User.Identity.Name);
            var userQuestionnaires = db.UserQuestionnaires.Where(w => w.IsDeleted == false && w.UserId == UserId).OrderBy(o => o.CreationDate).ToList();
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
                else if(id == "4ec1ece0-44db-4574-8895-af0086f1ba1a")
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

        public object[] GetWeek(int weekCount)
        {
            object[] chartWeek = new object[] { };
            chartWeek = new object[weekCount];
            chartWeek[0] = "Score";
            for (int j = 1; j < weekCount; j++)
            {
                chartWeek[j] = "Week"+ j.ToString();
            }
            return chartWeek;
        }
        

        }
}