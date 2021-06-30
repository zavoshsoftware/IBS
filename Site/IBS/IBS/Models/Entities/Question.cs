using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
	public class Question:BaseEntity
	{
	    public Question()
	    {
	        AnswerItems=new List<AnswerItem>();
            UserQuestionnaireDetails=new List<UserQuestionnaireDetail>();
	    }
	    [Display(Name="اولویت")]
	    public int Order { get; set; }
	    [Display(Name="عنوان سوال")]
	    public string Title { get; set; }

	    [Display(Name="دسته بندی پرسش")]
	    public Guid QuestionGroupId { get; set; }
	    public virtual QuestionGroup QuestionGroup { get; set; }

	    [Display(Name="نوع پاسخ")]
	    public Guid AnswerTypeId { get; set; }
	    public virtual AnswerType AnswerType { get; set; }

	    public virtual ICollection<AnswerItem> AnswerItems { get; set; }
	    public virtual ICollection<UserQuestionnaireDetail> UserQuestionnaireDetails { get; set; }
	}
}