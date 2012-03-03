using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MediaLibrary.Web.Models.Domain;
using MediaLibrary.Web.Models.Repository;
using System.Reflection;
using Microsoft.Practices.Unity;

namespace MediaLibrary.Web.Mvc
{
    public class Controller : System.Web.Mvc.Controller
    {
        #region Přihlášený uživatel

        private UserAccount userAccount;

        /// <summary>
        /// Vrací instanci aktuálně přihlášeného uživatele.
        /// Pokud není přihlášen, vrací null!
        /// </summary>
        public UserAccount UserAccount
        {
            get
            {
                if (userAccount == null)
                {
                    IRepository<UserAccount> repository = DependencyResolver.Current.GetService<IRepository<UserAccount>>();
                    userAccount = repository.GetList().FirstOrDefault(u => u.Username == User.Identity.Name);
                }

                return userAccount;
            }
        }

        #endregion

        #region Podpora pro DI

        protected T LocateService<T>()
        {
            return DependencyResolver.Current.GetService<T>();
        }

        #endregion

        public Controller()
        {
            PropertyInfo[] properties = DependencyAttributes.GetProperties(GetType());
            foreach (PropertyInfo prop in properties)
            {
                prop.SetValue(this, DependencyResolver.Current.GetService(prop.PropertyType), null);
            }
        }

        public void ShowMessage(string content, HtmlMessageType type = HtmlMessageType.Success)
        {
            TempData["Message"] = HtmlMessage.Create(content, type);
        }
    }
}