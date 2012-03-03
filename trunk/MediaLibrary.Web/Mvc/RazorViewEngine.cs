using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediaLibrary.Web.Mvc
{
    public class RazorViewEngine : System.Web.Mvc.RazorViewEngine
    {
        public RazorViewEngine()
        {
            ViewLocationFormats = new string[] {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml"
            };
        }
    }
}