using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediaLibrary.Web.Mvc
{
    public static class WebViewPageHelper
    {
        public static TabList TabList { get; private set; }

        public static void RegisterTabs()
        {
            TabList = new TabList();

            //TabList
            TabItem homeIndex = new TabItem("Home", "home", "index", false) { ActiveOnController = "home", ActiveOnAction = "index" };
            TabItem dbList = new TabItem("Databases", "database", "index") { ActiveOnController = "database", ActiveOnAction = "*" };
            TabItem changeAccount = new TabItem("Change account", "account", "change") { ActiveOnController = "account", ActiveOnAction = "change" };

            //Register
            TabList.Register("home", "*", homeIndex, dbList, changeAccount);
            TabList.Register("account", "change", homeIndex, dbList, changeAccount);
            TabList.Register("database", "*", homeIndex, dbList, changeAccount);
        }
    }
}