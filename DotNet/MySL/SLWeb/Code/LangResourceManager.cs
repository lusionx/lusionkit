using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Xml.Linq;
using System.Xml;
using System.Collections;
using System.Collections.Specialized;
using System.Web.Caching;

namespace SLWeb
{

    [Serializable]
    public class LangResourceManager : BaseResourceManager
    {

        private static LangResourceManager current = null;

        public static LangResourceManager Current
        {
            get
            {
                if (current == null)
                    current = new LangResourceManager();
                return current;
            }
        }

        protected override string GetFilePath(string cookieValue)
        {
            return HttpContext.Current.Server.MapPath("~/Lang/zh-cn.xml");
        }

    }
}
