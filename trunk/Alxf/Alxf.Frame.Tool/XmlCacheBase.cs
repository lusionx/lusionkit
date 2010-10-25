using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Xml.Linq;
using System.Xml;
using System.Collections;
using System.Collections.Specialized;
using System.Web.Caching;


namespace Alxf.Frame.Tool
{
    /// <summary>
    /// 通用XML文件缓存策略
    /// </summary>
    public abstract class XmlCacheBase
    {
        private const string PreCacheKey = "PreXmlCacheKey_";

        private string FullCacheKey
        {
            get
            {
                return PreCacheKey + System.IO.Path.GetFileName(this.GetFilePath());
            }
        }

        public virtual XElement XmlRoot
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

        /// <summary>
        /// 请返回物理路径
        /// </summary>
        /// <returns></returns>
        protected abstract string GetFilePath();

        private bool CanAddTheKey
        {
            get
            {
                return HttpContext.Current.Cache.Get(FullCacheKey) == null;
            }
        }

        private XNode GetXmlRootFromFile()
        {
            return XDocument.Load(this.GetFilePath()).FirstNode;
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
