using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class AudioGroup:BaseEntity
    {
        [Display(Name="اولویت")]
        public int Order { get; set; }
        [Display(Name="دسته بندی فایل های صوتی")]
        public string Title { get; set; }
        [Display(Name="نوع بیمار")]
        public Guid PatientTypeId { get; set; }
        public virtual PatientType PatientType { get; set; }

        public virtual ICollection<Audio> Audios { get; set; }
        
    }
}