using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;

namespace Alxf.Frame.DataBase
{
    /// <summary>
    /// 缓存DataContext，并提供锁机制？，
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public abstract class ContextBase<TContext> : IContextBase<TContext> where TContext : System.Data.Linq.DataContext
    {
        private static object _lockflag = new object();

        private const string CacheKey = "ADB";

        /// <summary>
        /// 优先取缓存的DB,//需要锁机制吗？
        /// </summary>
        public TContext Current
        {
            get
            {
                return New;
                ////需要锁机制吗？
                //var cache = System.Web.HttpContext.Current.Cache;

                //var db = cache[CacheKey] as TContext;

                //if (db == null)
                //{
                //    var one = this.New;
                //    cache.Insert(CacheKey, one);
                //    return one;
                //}
                //else
                //{
                //    return db;
                //}
            }
        }

        public abstract TContext New
        {
            get;
        }

        public void Reset()
        {
            var cache = System.Web.HttpContext.Current.Cache;
            var db = cache[CacheKey];
            if (db != null)
            {
                cache.Remove(CacheKey);
            }
        }
    }
}
