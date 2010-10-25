using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;

namespace HR.Web
{
    /// <summary>
    /// 统一处理hander
    /// 配置<add verb="*" path="*.dx" validate="false" type="HR.Web.XHandler,HR.Web"/>
    /// </summary>
    public class XHandler : System.Web.IHttpHandler
    {
        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            var path = context.Request.Url.AbsolutePath;
            var start = path.LastIndexOf("/") + 1;
            var end = path.IndexOf(".dx");
            var classname = path.Substring(start, end - start);

            var hand = Assembly.Load("HR.Web").CreateInstance(classname) as HandlerBase;

            context.Response.Write(hand.DoProcess());
        }
    }

    public abstract class HandlerBase
    {
        public bool IsReusable
        {
            get { return false; }
        }

        public abstract string DoProcess();
    }

    public class TestHandler : HandlerBase
    {
        public override string DoProcess()
        {
            return "TestHandler : HandlerBase";
        }
    }
}