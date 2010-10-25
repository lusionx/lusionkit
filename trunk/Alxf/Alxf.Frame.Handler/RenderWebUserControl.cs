using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Web.UI;

namespace Alxf.Frame.Handler
{
    /// <summary>
    /// 直接访问用户控件,其基本作用就是一个有模板的handler,所以没有postback的概念
    /// 需要配置Web.config httpHandlers节点
    /// </summary>
    class RenderWebUserControl : IHttpHandler
    {
        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            StringWriter output = new StringWriter();
            var m_pageHolder = new Page();
            var control = m_pageHolder.LoadControl(context.Request.Url.AbsolutePath);
            m_pageHolder.Controls.Add(control);
            HttpContext.Current.Server.Execute(m_pageHolder, output, false);
            context.Response.Write(output.ToString());
        }
    }
}
