using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
	public class AnswerType:BaseEntity
	{
	    public AnswerType()
	    {
	        Questions = new List<Question>();
	    }

        [Display(Name="نوع پاسخ")]
        public string Title { get; set; }
	    public ICollection<Question> Questions { get; set; }
	}
}