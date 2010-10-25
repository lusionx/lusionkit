using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Xml.Linq;
using System.Xml;
using System.Collections;
using System.Collections.Specialized;
using System.Web.Caching;


namespace Utility.HttpCache
{
    /// <summary>
    /// 通用XML文件缓存策略
    /// </summary>
    public abstract class XmlCacheBase
    {
        private const string PreCacheKey = "PreCacheKey ";

        private string FullCacheKey
        {
            get
            {
                return PreCacheKey + System.IO.Path.GetFileName(this.GetFilePath());
            }
        }

        protected XElement XmlRoot
        {
            get
            {
                object oo = HttpContext.Current.Cache.Get(FullCacheKey);
                if (oo == null)
                {
                    SaveToCache();
                }
                return HttpContext.Current.Cache.Get(FullCacheKey) as XElement;
            }
        }

        protected abstract string GetFilePath();

        private bool CanAddTheKey
        {
            get
            {
                return HttpContext.Current.Cache.Get(FullCacheKey) == null;
            }
        }

        private XElement GetXmlRootFromFile()
        {
            return XDocument.Load(this.GetFilePath()).FirstNode as XElement;
        }

        private void SaveToCache()
        {
            if (CanAddTheKey)
            {
                HttpContext.Current.Cache.Add(FullCacheKey, GetXmlRootFromFile(),
                    new CacheDependency(GetFilePath()), DateTime.MaxValue,
                    Cache.NoSlidingExpiration, CacheItemPriority.High, null);
            }
        }
    }
}
