using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using SLWeb.Code;

namespace SLWeb
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            lock (Application)
            {
                if (Application[EAppKey.UserOnline.ToString()] == null)
                {
                    Application[EAppKey.UserOnline.ToString()] = new List<Code.User>();
                }

                if (Application[EAppKey.Room.ToString()] == null)
                {
                    Application[EAppKey.Room.ToString()] = new List<Code.Room>();
                }
                if (Application[EAppKey.Message.ToString()] == null)
                {
                    Application[EAppKey.Message.ToString()] = new List<Code.Message>();
                }
            }
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

        }
    }
}