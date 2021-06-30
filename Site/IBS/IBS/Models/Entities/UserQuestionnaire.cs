using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
	public class UserQuestionnaire:BaseEntity
	{
	    [Display(Name="کاربر")]
	    public Guid UserId { get; set; }
	    public virtual User User { get; set; }

	    [Display(Name="زمان شروع")]
	    public DateTime StartDate { get; set; }
        [Display(Name="زمان پایان")]
	    public DateTime EndDate { get; set; }

	    [Display(Name="نتیجه")]
	    public string Result { get; set; }

	    [Display(Name="نوع بیماری")]
	    public Guid PatientTypeId { get; set; }
        public virtual PatientType PatientType { get; set; }
	}
}