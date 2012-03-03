using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using MediaLibrary.Web.Models.Domain;
using MediaLibrary.Web.Models.Repository;

namespace MediaLibrary.Web.Core
{
    public class FormsAuthProvider : IAuthProvider
    {
        public bool Authenticate(string username, string password)
        {
            IRepository<UserAccount> repository = new UserAccountRepository();

            if (!String.IsNullOrEmpty(password))
            {
                UserAccount user = repository.GetList().FirstOrDefault(u => u.Username == username && u.Password == password);
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(username, false);
                    return true;
                }
            }

            return false;
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}