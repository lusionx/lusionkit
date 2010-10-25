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
	public sealed class StaticCacheService
	{
		private const int CACHE_INIT_COUNT = 20000;
		private Hashtable objectCache = null;
		private static StaticCacheService current = null;

		private StaticCacheService()
		{
			objectCache = new Hashtable ( CACHE_INIT_COUNT );
		}

		public static StaticCacheService Current
		{
			get
			{
				if ( current == null )
				{
					System.Threading.Interlocked.CompareExchange ( ref current, new StaticCacheService (), null );
				}
				return current;
			}
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
			if ( objectCache.ContainsKey ( key ) )
			{
				return objectCache[ key ];
			}
			return null;
		}

		public void Remove( string key )
		{
			if ( objectCache.ContainsKey ( key ) )
			{
				objectCache.Remove ( key );
			}
		}

		public void Add( string key, object value )
		{
			if ( objectCache.ContainsKey ( key ) )
			{
				objectCache [key] = value;
			}
			else
			{
				objectCache.Add ( key, value );
			}
		}

	}
}
