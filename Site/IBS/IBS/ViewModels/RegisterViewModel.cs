using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace  ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please enter {0}")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public int Age { get; set; }
        public Nullable<bool> Gender { get; set; }

        [Display(Name = "FullName")]
        [Required(ErrorMessage = "Please enter {0}")] 
        public string FullName { get; set; }

        [Required]
        [Range(typeof(bool), "true", "true", ErrorMessage = "accept terms and condition and privacy required.")]
        [Display(Name = "I accept the Terms and Conditions and Privacy policy")]
        public bool Privacy { get; set; }
    }
}