using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Alxf.Frame.Tool;
using System.Xml.Linq;

namespace Alxf.DevTool.Uility
{
    public class DevTemplate : XmlCacheBase
    {
        protected override string GetFilePath()
        {
            return HttpContext.Current.Server.MapPath("~/App_Data/DevTemplate.xml");
        }

        public static XElement Root
        {
            get
            {
                var tmp = new DevTemplate();
                return tmp.XmlRoot;
            }
        }
    }
}
