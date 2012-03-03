using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using MediaLibrary.Web.Models.Domain;
using MediaLibrary.Web.Models.Repository;

namespace MediaLibrary.Web.Mvc
{
    public abstract class WebViewPage<TModel> : System.Web.Mvc.WebViewPage<TModel>
    {
        public UIHelper<TModel> UI { get; protected set; }

        public HtmlMessage Message
        {
            get { return ViewContext.TempData["Message"] as HtmlMessage; }
            set { ViewContext.TempData["Message"] = value; }
        }

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
                    IRepository<UserAccount> repository = new UserAccountRepository();
                    userAccount = repository.GetList().FirstOrDefault(u => u.Username == User.Identity.Name);
                }

                return userAccount;
            }
        }

        #endregion

        public WebViewPage()
            : base()
        {
            UI = new UIHelper<TModel>(this);
        }
    }
}