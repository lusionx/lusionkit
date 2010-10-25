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
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Year : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {            
            context.Response.ContentType = "text/plain";
            try
            {
                Serial.Year = Convert.ToInt32(context.Request.QueryString[0] ?? DateTime.Now.Year.ToString());
                context.Response.Write("yes");
            }
            catch
            {
                context.Response.Write("no");
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
