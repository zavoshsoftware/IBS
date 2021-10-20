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
        public TextItem HeaderTextFirstPage { get; set; }
        public TextItem HeaderTextSecondPage { get; set; }
        public List<Question> Questions { get; set; }
        public List<TextItem> TextItems { get; set; }
    }

   
}