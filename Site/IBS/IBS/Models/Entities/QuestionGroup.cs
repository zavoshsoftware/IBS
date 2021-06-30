using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
	public class QuestionGroup:BaseEntity
	{
	    public QuestionGroup()
	    {
	        Questions=new List<Question>();
	    }

	    public int Order { get; set; }
        [Display(Name="دسته بندی پرسش ها")]
	    public string Title { get; set; }

	    public virtual ICollection<Question> Questions { get; set; }
	}
}