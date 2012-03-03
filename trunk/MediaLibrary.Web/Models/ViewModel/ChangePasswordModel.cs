using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using MediaLibrary.Web.Models.Validators;

namespace MediaLibrary.Web.Models.ViewModel
{
    [PropertiesMustMatch("NewPassword", "ConfirmPassword", ErrorMessage = "New password and confirmation password must match.")]
    public class ChangePasswordModel
    {
        [Display(Name = "Current password")]
        [Required(ErrorMessage = "Please, fill current password.")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Display(Name = "New password")]
        [Required(ErrorMessage = "Please, fill new password.")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name = "Confirm new password")]
        [Required(ErrorMessage = "Please, refill new password.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}