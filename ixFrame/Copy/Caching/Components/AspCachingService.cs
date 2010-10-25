using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;

namespace Portal.Facilities
{
	public sealed class AspCachingService : ICachingService
	{
		private static Cache cache;
		private static AspCachingService current = null;
		//private static object lockObject = new object ();
        //Commented out by Robert,061226

		private const int DEFAULT_MINUTES = 3;
		private static bool enableCache = true;
        private static StringCollection removeByPatternStrs = new StringCollection();
        private static int removeByPatternCounter = 0;
        private static object synObject = new object();
        private static DateTime lastRemoveTime = DateTime.Now;

       
		private AspCachingService()
		{
			HttpContext context = HttpContext.Current;
			if ( context != null )
			{
				cache = context.Cache;
			}
			else
			{
				cache = HttpRuntime.Cache;
			}
			enableCache = Convert.ToBoolean ( System.Configuration.ConfigurationManager.AppSettings ["enableCache"] );
		}
		
		public static AspCachingService Current
		{
			get
			{
                //Commented out by Robert,061226
                //We don't need synchronization as we use it in CachingService.cs
				//lock ( lockObject )
				//{
					if ( current == null )
					{
						current = new AspCachingService ();
					}
					return current;
				//}
			}
		}

		public string Name
		{
			get
			{
				return "AspCachingService";
			}
		}
		
		public int Count
		{
			get
			{
				return cache.Count;
			}
		}

		public Hashtable State
		{
			get
			{
				Hashtable cacheHashtable = new Hashtable ();
				IDictionaryEnumerator cacheEnum = cache.GetEnumerator ();
				while ( cacheEnum.MoveNext () )
				{
					cacheHashtable.Add ( cacheEnum.Key, cacheEnum.Value );
				}
				return cacheHashtable;
			}
		}

		public object Get( string key )
		{
			if ( ! enableCache )
				return null;
			return cache [key];
		}

		public void Remove( string key )
		{
			cache.Remove ( key );
		}

		public List<string> RemoveByPattern( string keyPattern )
		{
			List<string> matchKeys = new List<string>( );
			Regex regex = new Regex ( keyPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled );
			IDictionaryEnumerator cacheEnum = cache.GetEnumerator ();
			while ( cacheEnum.MoveNext () )
			{
				if ( regex.IsMatch ( cacheEnum.Key.ToString () ) )
				{
					string key = cacheEnum.Key.ToString();
					cache.Remove ( key );
					matchKeys.Add ( key );
				}
			}
			return matchKeys;
			//removeByPatternStrs.Add(keyPattern);
			//if (lastRemoveTime.AddSeconds(2) > DateTime.Now)
			//{
			//    lock (synObject)
			//    {
			//        //if (removeByPatternStrs.Count > 100)
			//        {
			//            //Regex regex = new Regex(keyPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
			//            IDictionaryEnumerator cacheEnum = cache.GetEnumerator();
			//            //StringCollection cacheKeys = new StringCollection();
			//            bool bExist = cacheEnum.MoveNext();
			//            bool bMoved = false;
			//            while (bExist)
			//            {
			//                bMoved = false;
			//                string cacheKey = cacheEnum.Key.ToString();
			//                for (int count = 0; count < removeByPatternStrs.Count; count++)
			//                {
			//                    if (cacheKey.Contains(removeByPatternStrs[count]))
			//                    {
			//                        bExist = cacheEnum.MoveNext();
			//                        bMoved = true;
			//                        Remove(cacheKey);
			//                        //removeByPatternStrs.Remove(removeByPatternStrs[count]);
			//                        break;
			//                    }
			//                }
			//                if (!bMoved)
			//                    bExist = cacheEnum.MoveNext();
			//            }
			//            removeByPatternStrs.Clear();
			//        }
			//        lastRemoveTime = DateTime.Now;
                
			//    }
        
            //}


            /*
			Regex regex = new Regex ( keyPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled );
			IDictionaryEnumerator cacheEnum = cache.GetEnumerator ();
			StringCollection cacheKeys = new StringCollection ();
			while ( cacheEnum.MoveNext () )
			{
				string cacheKey = cacheEnum.Key.ToString ();
				if ( regex.IsMatch ( cacheKey ) )
					cacheKeys.Add ( cacheKey );
			}
			for ( int i = 0, count = cacheKeys.Count; i < count; i++ )
			{
				Remove ( cacheKeys [i] );
			}
             */

		}

		public void Clear()
		{
			IDictionaryEnumerator cacheEnum = cache.GetEnumerator ();
			StringCollection cacheKeys = new StringCollection ();
			while ( cacheEnum.MoveNext () )
			{
				cacheKeys.Add ( cacheEnum.Key.ToString() );
			}
			for ( int i = 0, count = cacheKeys.Count; i < count; i++ )
			{
				Remove ( cacheKeys[i] );
			}
		}

		public void Add( string key, object value )
		{
			Add ( key, value, DEFAULT_MINUTES );
		}

		public void MicroInsert( string key, object value, int seconds )
		{
			Add ( key, value, DateTime.Now.AddSeconds ( seconds ) );
		}
		
		public void Add( string key, object value, int minutes )
		{
			Add( key, value, DateTime.Now.AddMinutes( minutes ) );
		}

		public void Add( string key, object value, int minutes, OnCacheRemoved onCacheRemoved )
		{
			Add ( key, value, DateTime.Now.AddMinutes ( minutes ), onCacheRemoved );
		}

		public void Add( string key, object value, DateTime dateTime )
		{
			Add ( key, value, dateTime, null );
		}

		public void Add( string key, object value, DateTime dateTime, OnCacheRemoved onCacheRemoved )
		{
			if ( !enableCache )
				return;
			CacheItemRemovedCallback onRemoveCallback = null;
			if ( onCacheRemoved != null )
			{
				AspCachingOnRemove aspCachingOnRemove = new AspCachingOnRemove ();
				onRemoveCallback = new CacheItemRemovedCallback ( aspCachingOnRemove.RemovedCallback );
				aspCachingOnRemove.RemoveEvent += new AspCachingOnRemove.RemoveHandler ( onCacheRemoved.RemoveHandler );
			}
			cache.Insert ( key, value, null, dateTime, TimeSpan.Zero, CacheItemPriority.NotRemovable, onRemoveCallback );
		}


		public void Add( string key, object value, string filepath )
		{
			Add ( key, value, filepath, null );
		}

		public void Add( string key, object value, string filepath, OnCacheRemoved onCacheRemoved )
		{
			if ( !enableCache )
				return;
			CacheItemRemovedCallback onRemoveCallback = null;
			if ( onCacheRemoved != null )
			{
				AspCachingOnRemove aspCachingOnRemove = new AspCachingOnRemove ();
				onRemoveCallback = new CacheItemRemovedCallback ( aspCachingOnRemove.RemovedCallback );
				aspCachingOnRemove.RemoveEvent += new AspCachingOnRemove.RemoveHandler ( onCacheRemoved.RemoveHandler );
			}
			CacheDependency dep = new CacheDependency ( filepath );
			cache.Insert ( key, value, dep, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, onRemoveCallback );
		}

	}
}
