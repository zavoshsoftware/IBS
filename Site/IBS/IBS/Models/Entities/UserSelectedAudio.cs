using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class UserSelectedAudio : BaseEntity
    {
    

        [Display(Name = "صدا")]
        public Guid AudioId { get; set; }
        public virtual Audio Audio { get; set; }
        [Display(Name = "شماره هفته")]
        public int WeekNo { get; set; }

        public Guid? UserQuestionnaireId { get; set; }
        public UserQuestionnaire UserQuestionnaire { get; set; }
    }
}