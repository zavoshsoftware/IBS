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
    }
}