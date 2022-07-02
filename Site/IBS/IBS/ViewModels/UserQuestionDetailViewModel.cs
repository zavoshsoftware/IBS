using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class UserQuestionDetailViewModel
    {
        public string ScoreAnswer { get; set; }

        public string PainAnswer { get; set; }
        public string BloatingAnswer { get; set; }
        public string WellBeingAnswer { get; set; }

        public ProfileViewModel Profile { get; set; }
    }


    public class DashBoardViewModel
    {
        public string CurrentWeek { get; set; }
        public ProfileViewModel Profile { get; set; }
    }

    public class ProfileViewModel
    {
        public string  FullName { get; set; }
        public string  Email { get; set; }
        public string  Age { get; set; }
    }
}