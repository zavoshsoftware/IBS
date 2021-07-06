using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class UserQuestionnaireDetailViewModel
    {
        public UserQuestionnaire UserQuestionnaire { get; set; }
        public List<UserQuestionnaireDetail> UserQuestionnaireDetails { get; set; }
    }
}