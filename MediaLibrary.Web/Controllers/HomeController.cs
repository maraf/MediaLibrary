using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MediaLibrary.Web.Models.ViewModel;
using MediaLibrary.Web.Models.Domain;
using MediaLibrary.Web.Models.Repository;

namespace MediaLibrary.Web.Controllers
{
    public class HomeController : MediaLibrary.Web.Mvc.Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
