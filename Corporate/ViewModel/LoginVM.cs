using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Corporate.ViewModel
{
    public class LoginVM
    {
        public string UserName { get; set; }
       
        public string Password { get; set; }
    }
    public class ChangePasswordVM
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]

        public string Newpassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Old password")]
        public string OldPassword { get; set; }
    }
}
