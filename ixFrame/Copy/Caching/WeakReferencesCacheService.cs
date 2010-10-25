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
	public sealed class WeakReferencesCacheService
	{
		private static WeakReferencesCacheService current = null;
		private static Hashtable objectCache = Hashtable.Synchronized ( new Hashtable () );
		private static object lockObject = new object ();

		private WeakReferencesCacheService()
		{
		}

		public static WeakReferencesCacheService Current
		{
			get
			{
				if ( current == null )
				{
					System.Threading.Interlocked.CompareExchange ( ref current, new WeakReferencesCacheService (), null );
				}
				return current;

			}
		}
		
		public int Count
		{
			get
			{
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
			WeakReference objectWR = objectCache[ key ] as WeakReference;
			if ( objectWR != null )
			{
				if ( objectWR.Target != null )
					return objectWR.Target;
				objectCache.Remove( key );
			}
			return null;
		}

		public void Remove( string key )
		{
			if ( objectCache.ContainsKey( key ) )
			{
				WeakReference objectWR = objectCache[ key ] as WeakReference;
				if ( objectWR != null )
				{
					objectWR.Target = null;
				}
				objectCache.Remove( key );
			}
		}

		public void RemoveByPattern( string keyPattern )
		{
			Regex regex = new Regex ( keyPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled );
			IDictionaryEnumerator cacheEnum = objectCache.GetEnumerator ();
			StringCollection cacheKeys = new StringCollection ();
			while ( cacheEnum.MoveNext () )
			{
				string cacheKey = cacheEnum.Key.ToString ();
				if ( regex.IsMatch ( cacheKey ) )
					cacheKeys.Add ( cacheKey );
			}
			for ( int i = 0, count = cacheKeys.Count; i < count; i++ )
			{
				Remove ( cacheKeys[i] );
			}
		}

		public void Clear()
		{
			IDictionaryEnumerator cacheEnum = objectCache.GetEnumerator ();
			StringCollection cacheKeys = new StringCollection ();
			while ( cacheEnum.MoveNext () )
			{
				cacheKeys.Add ( cacheEnum.Key.ToString () );
			}
			for ( int i = 0, count = cacheKeys.Count; i < count; i++ )
			{
				Remove ( cacheKeys [i] );
			}
		}

		public void Add( string key, object value )
		{
			Add( key, value, false );
		}

		public void Add( string key, object value, bool longWeakReference )
		{
			WeakReference objectWR = new WeakReference( value, longWeakReference );
			if ( objectCache.ContainsKey( key ) )
				objectCache[ key ] = objectWR;
			else
				objectCache.Add( key, objectWR );
		}

		//public void MicroInsert( string key, object value, int seconds )
		//{
		//    Add ( key, value, DateTime.Now.AddSeconds ( seconds ) );
		//}
		
		//public void Add( string key, object value, int minutes )
		//{
		//    Add( key, value, DateTime.Now.AddMinutes( minutes ) );
		//}

		//public void Add( string key, object value, int minutes, OnCacheRemoved onCacheRemoved )
		//{
		//    Add ( key, value, DateTime.Now.AddMinutes ( minutes ), onCacheRemoved );
		//}

		//public void Add( string key, object value, DateTime dateTime )
		//{
		//    Add ( key, value, dateTime, null );
		//}

		//public void Add( string key, object value, DateTime dateTime, OnCacheRemoved onCacheRemoved )
		//{
		//    if ( !enableCache )
		//        return;
		//    AspCachingOnRemove aspCachingOnRemove = new AspCachingOnRemove ();
		//    CacheItemRemovedCallback onRemoveCallback = new CacheItemRemovedCallback ( aspCachingOnRemove.RemovedCallback );
		//    if ( onCacheRemoved != null )
		//    {
		//        aspCachingOnRemove.RemoveEvent += new AspCachingOnRemove.RemoveHandler ( onCacheRemoved.RemoveHandler );
		//    }
		//    cache.Insert ( key, value, null, dateTime, TimeSpan.Zero, CacheItemPriority.Default, onRemoveCallback );
		//}


		//public void Add( string key, object value, string filepath )
		//{
		//    Add ( key, value, filepath, null );
		//}

		//public void Add( string key, object value, string filepath, OnCacheRemoved onCacheRemoved )
		//{
		//    if ( !enableCache )
		//        return;
		//    AspCachingOnRemove aspCachingOnRemove = new AspCachingOnRemove ();
		//    CacheItemRemovedCallback onRemoveCallback = new CacheItemRemovedCallback ( aspCachingOnRemove.RemovedCallback );
		//    if ( onCacheRemoved != null )
		//    {
		//        aspCachingOnRemove.RemoveEvent += new AspCachingOnRemove.RemoveHandler ( onCacheRemoved.RemoveHandler );
		//    }
		//    CacheDependency dep = new CacheDependency ( filepath );
		//    cache.Insert ( key, value, dep, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Default, onRemoveCallback );
		//}

	}
}
