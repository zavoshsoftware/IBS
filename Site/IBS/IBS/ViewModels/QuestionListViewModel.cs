using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class QuestionListViewModel
    {
        public QuestionGroup QuestionGroup { get; set; }
        public List<Question> Questions { get; set; }
    }

   
}