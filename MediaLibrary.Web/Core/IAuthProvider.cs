using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaLibrary.Web.Core
{
    public interface IAuthProvider
    {
        bool Authenticate(string username, string password);

        void SignOut();
    }
}
