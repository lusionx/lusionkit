using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using SilverMoon.BAL;

namespace SilverMoon.Handler
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class GetOne : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            string id = context.Request.Form["id"];
            var obj = DB.Current.GetWorkpiece(id);
            context.Response.ContentType = "text/plain";
            if (obj != null)
            {
                var st = obj.sm_biz_State;
                context.Response.Write(st.ID.ToString() + "," + st.Name);
            }
            else
            {
                context.Response.Write(",");
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
