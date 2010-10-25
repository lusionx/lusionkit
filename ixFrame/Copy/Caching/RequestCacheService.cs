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
	public sealed class RequestCacheService
	{
		private const string RequestCacheServiceName = "RequestCacheServiceName";
		private const int CACHE_INIT_COUNT = 200;
		private Hashtable objectCache = null;
		//private static object lockObject = new object ();

		private RequestCacheService()
		{
			//objectCache = new Hashtable( );
		}

		public static RequestCacheService GetCurrent()
		{
            if ( HttpContext.Current == null )
                return new RequestCacheService();
			RequestCacheService current = HttpContext.Current.Items [RequestCacheServiceName] as RequestCacheService;
			if ( current == null )
			{
				current = new RequestCacheService ();
				HttpContext.Current.Items.Add ( RequestCacheServiceName, current );
			}
			return current;
		}

		public int Count
		{
			get
			{
				if ( objectCache == null )
					return 0;
				return objectCache.Count;
			}
		}

		public Hashtable State
		{
			get
			{
				return objectCache;
			}
		}

		public object Get( string key )
		{
			if ( objectCache == null )
				return null;
			if ( objectCache.ContainsKey ( key ) )
			{
				return objectCache [key];
			}
			return null;
		}

		public void Remove( string key )
		{
			if ( objectCache != null && objectCache.ContainsKey ( key ) )
			{
				objectCache.Remove( key );
			}
		}

		public void Add( string key, object value )
		{
			if ( objectCache == null )
				objectCache = new Hashtable ( CACHE_INIT_COUNT );
			if ( objectCache.ContainsKey( key ) )
				objectCache [key] = value;
			else
				objectCache.Add ( key, value );
		}

	}
}
