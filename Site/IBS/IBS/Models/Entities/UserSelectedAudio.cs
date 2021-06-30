using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class UserSelectedAudio : BaseEntity
    {
        [Display(Name = "نام کاربر")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        [Display(Name = "صدا")]
        public Guid AudioId { get; set; }
        public virtual Audio Audio { get; set; }
        [Display(Name = "شماره هفته")]
        public int WeekNo { get; set; }
    }
}