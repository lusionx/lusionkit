using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Web;
using System.Web.Caching;

namespace Alxf.Frame.DataBase
{
    public abstract class XmlCache
    {
        private const string PreCacheKey = "PreXmlCacheKey ";

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
        /// 请返回 物理路径
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
