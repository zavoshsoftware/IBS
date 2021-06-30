using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class Audio : BaseEntity
    {
        [Display(Name="اولویت")]
        public int Order { get; set; }
        [Display(Name="عنوان فایل صورتی")]
        public string Title { get; set; }
        [Display(Name="دسته بندی فایل صوتی")]
        public Guid AudioGroupId { get; set; }
        public virtual AudioGroup AudioGroup { get; set; }
        [Display(Name = "فایل")]
        public string FileUrl { get; set; }
    }
}