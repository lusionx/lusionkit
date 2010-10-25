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
    public abstract class BaseResourceManager
    {

        protected const string APPLICATION_NAME = "Community-Resource";
        protected const string DEFAULT_LANGUAGE = "zh-cn";
        private static string LangCookieKey = "Lang";

        protected abstract string GetFilePath(string cookieValue);

        public string GetString(string key)
        {
            return GetString(key, CurrentLanguage);
        }

        public string GetString(string key, string language)
        {
            Dictionary<string, string> dic = LoadFromCashe(language);

            if (dic.ContainsKey(key))
            {
                return dic[key];
            }
            else
            {
                return key;
            }
        }

        private Dictionary<string, string> LoadFromCashe(string lang)
        {
            string cachekey = "Lang " + lang;
            object cashe = HttpContext.Current.Cache[cachekey];
            if (cashe != null)
            {
                return cashe as Dictionary<string, string>;
            }
            else
            {
                Dictionary<string, string> res = LoadResourceXml(this.GetFilePath(CurrentLanguage));
                HttpContext.Current.Cache.Add(cachekey, res, new CacheDependency(GetFilePath(lang)),
                    DateTime.MaxValue, Cache.NoSlidingExpiration, CacheItemPriority.High, null);
                return res;
            }

        }

        public string CurrentLanguage
        {
            get
            {
                object lang = CookieHelper.Get(LangCookieKey);
                if (lang != null)
                {
                    return lang.ToString();
                }
                else
                {
                    CookieHelper.Add(LangCookieKey, DEFAULT_LANGUAGE);
                }
                return DEFAULT_LANGUAGE;
            }

            set
            {
                CookieHelper.Add(LangCookieKey, value);
            }
        }

        private Dictionary<string, string> LoadResourceXml(string xmlpath)
        {
            Dictionary<string, string> res = new Dictionary<string, string>();

            XElement root = this.GetRoot(xmlpath);
            foreach (XElement ex in root.Elements())
            {
                res.Add(ex.Attribute("key").Value, ex.Value);
            }
            return res;
        }

        private XElement GetRoot(string xmlpath)
        {
            return XDocument.Load(xmlpath).Element("root");
        }
    }
}

