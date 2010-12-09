using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Perpor.DAL;
using Perpor.BLL;
using System.Web.Security;

namespace Perpor.Web.Handlers
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class AddWidget : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var res = context.Response;
            var name = context.Request.Form["name"] ?? string.Empty;
            var db = DB.Current;
            var u = Membership.GetUser();
            var wd = new WidgetData { Color = ColorClasses.Rand, ControlName = name };
            try
            {
                var obj = db.core_UserPages.First(x => x.UserName == u.UserName);
                var page = obj.Page();
                page.AddWidget(wd);
                obj.Layout = page.ToJson();
                db.SubmitChanges();
                res.Write(1);
            }
            catch (Exception)
            {
                res.Write(0);
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
