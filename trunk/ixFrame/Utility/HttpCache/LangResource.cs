using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Xml.Linq;
using System.Xml;
using System.Collections;
using System.Collections.Specialized;
using System.Web.Caching;

namespace Utility
{
    /// <summary>
    /// 语言包读取类
    /// </summary>
    public class LangResource : Utility.HttpCache.XmlCacheBase
    {
        protected readonly string DefaultLanguage = "zh-cn";
        private const string LangCookieKey = "xLang";

        protected override string GetFilePath()
        {
            string getlang = CookieHelper.Get(LangCookieKey);
            if (getlang == string.Empty)
            {
                CookieHelper.Add(LangCookieKey, DefaultLanguage);
                getlang = DefaultLanguage;
            }
            string xmlpath = string.Format("~/Lang/{0}.xml", getlang);
            return System.Web.HttpContext.Current.Server.MapPath(xmlpath);
        }

        public string GetString(string key)
        {
            XElement root = base.XmlRoot;
            foreach (XElement xe in root.Elements())
            {
                if (xe.Attribute("key").Value == key)
                {
                    return xe.Value;
                }
            }
            return key;
        }

        private static LangResource _current = null;
        public static LangResource Current
        {
            get
            {
                if (_current == null)
                {
                    _current = new LangResource();
                }
                return _current;
            }
        }
    }


}
