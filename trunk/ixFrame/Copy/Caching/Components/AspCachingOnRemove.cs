using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Caching;

namespace Portal.Facilities
{
	public class AspCachingOnRemove
	{
		public delegate void RemoveHandler( object sender, RemoveEventArgs e );
		public event RemoveHandler RemoveEvent;
		
		public void RemovedCallback( String key, Object expiredValue, CacheItemRemovedReason reason )
		{
			if ( RemoveEvent != null )
			{
				RemoveEvent ( this, new RemoveEventArgs ( key, expiredValue ) );
			}
//#if DEBUG
//            if ( reason == CacheItemRemovedReason.Underused )
//            {
//                ExceptionService.Current.Handle ( new Exception ( "FreeMemory:" + key ) );
//            }
//#endif
			IDisposable disposableObject = expiredValue as IDisposable;
			if ( disposableObject != null )
			{
				disposableObject.Dispose ();
			}

		}
	}
}
