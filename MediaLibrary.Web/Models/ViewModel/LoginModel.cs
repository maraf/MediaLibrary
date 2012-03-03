using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MediaLibrary.Web.Models.ViewModel
{
    public class LoginModel
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Please, fill username.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Username length must be between 6 and 20 characters.")]
        public string Username { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please, fill password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}