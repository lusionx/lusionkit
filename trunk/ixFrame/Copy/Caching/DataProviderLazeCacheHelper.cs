using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Facilities
{
	public sealed class DataProviderLazeCacheRequestToken
	{
		private int requestCount = 1;
		public int RequestCount
		{
			get
			{
				return this.requestCount;
			}
		}

		public void Increment()
		{
			this.requestCount++;
		}
	}
	
	public sealed class DataProviderLazeCacheHelper
	{
		public static bool HasBeenReached( string cacheKey )
		{
			return HasBeenReached ( cacheKey, 10 );
		}

		public static bool HasBeenReached( string cacheKey, int cacheDurationInMinutes )
		{
			return HasBeenReached ( cacheKey, cacheDurationInMinutes, 2 );
		}
		
		public static bool HasBeenReached( string cacheKey, int cacheDurationInMinutes, int requestCountThreshold )
		{
			string cacheRequestTokenKey = cacheKey + "_token";
			DataProviderLazeCacheRequestToken cacheRequestToken = CachingService.Current.Get ( cacheRequestTokenKey ) as DataProviderLazeCacheRequestToken;
			if ( cacheRequestToken == null )
			{
				cacheRequestToken = new DataProviderLazeCacheRequestToken ();
				CachingService.Current.Add ( cacheRequestTokenKey, cacheRequestToken, cacheDurationInMinutes );
			}
			else
			{
				cacheRequestToken.Increment ();
			}
			if ( cacheRequestToken.RequestCount >= requestCountThreshold )
			{
				return true;
			}
			else
			{
				return false;
			}
		}

	}


}
