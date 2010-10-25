using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SilverMoon.BAL
{
    public class SiteMap : Tool.XmlCacheBase
    {
        protected override string GetFilePath()
        {
            return HttpContext.Current.Server.MapPath("~/Web.sitemap");
        }

        protected SiteMap()
        {
        }

        public override System.Xml.Linq.XElement XmlRoot
        {
            get
            {
                return base.XmlRoot;
            }
        }
        private static SiteMap _sm;

        public static SiteMap Current
        {
            get
            {
                if (_sm == null)
                {
                    _sm = new SiteMap();
                }
                return _sm;
            }
        }
    }
}
