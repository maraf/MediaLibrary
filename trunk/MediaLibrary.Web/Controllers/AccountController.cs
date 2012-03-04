using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MediaLibrary.Web.Models.Repository;
using MediaLibrary.Web.Models.Domain;
using MediaLibrary.Web.Models.ViewModel;
using MediaLibrary.Web.Core;
using Microsoft.Practices.Unity;

namespace MediaLibrary.Web.Controllers
{
    public class AccountController : MediaLibrary.Web.Mvc.Controller
    {
        [Dependency]
        IRepository<UserAccount> Repository { get; set; }

        [Dependency]
        IAuthProvider AuthProvider { get; set; }

        public ActionResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                bool result = AuthProvider.Authenticate(model.Username, model.Password);
                if (result)
                    return RedirectToAction("index", "home");
            }

            ModelState.AddModelError("", "No such user.");
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Logout()
        {
            AuthProvider.SignOut();
            return RedirectToAction("index", "home");
        }

        public ActionResult Register()
        {
            return View(new RegisterModel());
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            UserAccount account = Repository.GetList().FirstOrDefault(u => u.Username == model.Username);
            if (account != null)
                ModelState.AddModelError("Username", "Username already taken.");

            if (ModelState.IsValid)
            {
                Repository.Add(model.AsUserAccount());
                ShowMessage("User account registered. You can login now.");
            }

            return RedirectToAction("login");
        }

        [Authorize]
        public ActionResult Change()
        {
            return View(new ChangePasswordModel());
        }

        [HttpPost]
        [Authorize]
        public ActionResult Change(ChangePasswordModel model)
        {
            if (model.CurrentPassword != UserAccount.Password)
                ModelState.AddModelError("CurrentPassword", "Current password doesn't match.");

            if (ModelState.IsValid)
            {
                UserAccount.Password = model.NewPassword;
                Repository.Update(UserAccount);
                ShowMessage("Password changed.");
                return View(new ChangePasswordModel());
            }

            return View(model);
        }

        [Authorize]
        [ChildActionOnly]
        public ActionResult UserInfo()
        {
            return PartialView(UserAccount);
        }
    }
}
