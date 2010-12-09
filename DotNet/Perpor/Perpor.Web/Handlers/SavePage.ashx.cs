using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Perpor.DAL;
using System.Web.Security;
using System.Configuration;
using Perpor.BLL;

namespace Perpor.Web.Handlers
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class SavePage : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var u = Membership.GetUser();
            if (u.UserName == AppSettings.Get(AppSettings.Keys.AnonymousUserName))
            {
                context.Response.Write(1);
                return;
            }
            var layout = context.Request.Form["json"] ?? string.Empty;
            var db = DB.Current;
            try
            {
                var obj = db.core_UserPages.FirstOrDefault(x => x.UserName == u.UserName);
                obj.Layout = layout;
                db.SubmitChanges();
                context.Response.Write(1);
            }
            catch (Exception)
            {
                context.Response.Write(0);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
