using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
	public class UserQuestionnaireDetail:BaseEntity
	{
	    public Guid UserQuestionnaireId { get; set; }
	    public UserQuestionnaire UserQuestionnaire { get; set; }
	    [Display(Name="سوال")]
	    public Guid QuestionId { get; set; }
	    public Question Question { get; set; }
	    [Display(Name="پاسخ")]
	    public string Answer { get; set; }
    }
}