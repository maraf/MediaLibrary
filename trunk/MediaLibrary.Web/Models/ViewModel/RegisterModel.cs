using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using MediaLibrary.Web.Models.Validators;
using MediaLibrary.Web.Models.Domain;

namespace MediaLibrary.Web.Models.ViewModel
{
    [PropertiesMustMatch("Password", "ConfirmPassword", ErrorMessage = "The password and confirmation password must match.")]
    public class RegisterModel
    {
        [Display(Name="Username")]
        [Required(ErrorMessage="Please, fill username.")]
        [StringLength(20, MinimumLength=3, ErrorMessage="Username length must be between 6 and 20 characters.")]
        public string Username { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage="Please, fill password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Please, refill password.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public UserAccount AsUserAccount()
        {
            return new UserAccount { 
                Username = Username,
                Password = Password
            };
        }
    }
}