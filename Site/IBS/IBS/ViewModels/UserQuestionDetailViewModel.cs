using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class UserQuestionDetailViewModel
    {
        public Guid UserQuestionnaireId { get; set; }

        public string QuestionId { get; set; }/* = { "3b96726a-8a0d-487c-b992-1a31fa6fd0d7", "802374e7-0c5d-4dd8-b88e-e8c9139bb298", "4ec1ece0-44db-4574-8895-af0086f1ba1a" };*/

        public string Answer { get; set; }

        public bool IsDeleted { get; set; }

        public string WeekName { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreationDate { get; set; }
    }
}