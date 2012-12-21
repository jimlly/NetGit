using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Configuration;
using Yuantel.Cache;

namespace WebApp
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            //启动日志log4net
            log4net.Config.XmlConfigurator.Configure();

            //#if Membase
            //    Yuantel.Cache.YtMembase.AppInit();
            //#else
            //    string server = ConfigurationManager.AppSettings["CacheServer"];
            //    if (server != null)
            //    {
            //        String[] serverlist = { server };
            //        ytCache.Init(serverlist);
            //    }
            //#endif
            //ytCache.
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
            #if Membase
                Yuantel.Cache.YtMembase.Close();
            #else
                ytCache.Close();
            #endif
        }
    }
}