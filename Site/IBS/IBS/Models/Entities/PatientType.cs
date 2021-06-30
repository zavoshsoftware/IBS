using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class PatientType : BaseEntity
    {
        public PatientType()
        {
            UserQuestionnaires = new List<UserQuestionnaire>();
            AudioGroups = new List<AudioGroup>();
        }
        [Display(Name="نوع بیمار")]
        public string Title { get; set; }

        public virtual ICollection<UserQuestionnaire> UserQuestionnaires { get; set; }
        public virtual ICollection<AudioGroup> AudioGroups { get; set; }
    }
}