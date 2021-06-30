
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
	public class AnswerItem : BaseEntity
	{
	    [Display(Name="اولویت")]
	    public int Order { get; set; }

	    [Display(Name="عنوان پاسخ")]
	    public string Title { get; set; }

		[Display(Name = "نمره")]
		public int Score { get; set; }

		[Display(Name="پرسش")]
	    public Guid QuestionId { get; set; }
	    public virtual Question Question { get; set; }
	}
}