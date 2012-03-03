using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediaLibrary.Web.Models.Domain
{
    public class UserAccount : BaseObject
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}