using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Facilities
{
	public sealed class CachingService
	{
		private static bool enableEnterpriseLibraryCaching = Convert.ToBoolean ( System.Configuration.ConfigurationManager.AppSettings ["enableEnterpriseLibraryCaching"] );
		private static ICachingService current = null;
		//private static object lockObject = new object ();

		private CachingService()
		{
			//enableEnterpriseLibraryCaching = Convert.ToBoolean ( System.Configuration.ConfigurationManager.AppSettings ["enableEnterpriseLibraryCaching"] );
			//if ( enableEnterpriseLibraryCaching )
			//{
			//    current = EnterpriseCachingService.Current;
			//}
			//else
			//{
			//    current = AspCachingService.Current;
			//}
		}


		public static ICachingService Current
		{
			get
			{
				//if ( current == null )
				//{
				//    lock ( lockObject )
				//    {
				//        if ( current == null )
				//        {
				//            enableEnterpriseLibraryCaching =Convert.ToBoolean ( System.Configuration.ConfigurationManager.AppSettings ["enableEnterpriseLibraryCaching"] );
				//            if ( enableEnterpriseLibraryCaching )
				//            {
				//                current = EnterpriseCachingService.Current;
				//            }
				//            else
				//            {
				//                current = AspCachingService.Current;
				//            }
				//        }

				//    }
				//}
				if ( current == null )
				{
					System.Threading.Interlocked.CompareExchange ( ref current,
																	AspCachingService.Current as ICachingService, null );
				}
				return current;
			}
		}
	}

	public abstract class OnCacheRemoved
	{
		public virtual void RemoveHandler( object sender, RemoveEventArgs e )
		{

		}
	}

	public sealed class RemoveEventArgs : EventArgs
	{
		private string key = string.Empty;
		private object expiredValue = null;

		public string Key
		{
			get
			{
				return key;
			}
			set
			{
				key = value;
			}
		}

		public object ExpiredValue
		{
			get
			{
				return expiredValue;
			}
			set
			{
				expiredValue = value;
			}
		}


		public RemoveEventArgs( string key, object expiredValue )
		{
			this.key = key;
			this.expiredValue = expiredValue;
		}
	}
}
