using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Facilities
{
	public static class IpHelper
	{
		public static long GetIpCode( string ipAddress )
		{
			//return 4278190079;
			if ( ipAddress.Length > 0 )
			{
				string [] ips = ipAddress.Split ( '.' );
				if ( ips.Length == 4 )
				{
					return ( Convert.ToInt64 ( ips [0] ) << 24 ) + ( Convert.ToInt64 ( ips [1] ) << 16 ) + ( Convert.ToInt64 ( ips [2] ) << 8 ) + Convert.ToInt64 ( ips [3] );
				}
			}
			return 0;
		}

		public static string GetIpAddress( long ipCode )
		{
			string dotIP = "";
			int subIP = 0;

			long one = ipCode >> 24;
			subIP = Convert.ToInt32 ( one.ToString ( "f0" ) ) % 256;
			dotIP = subIP.ToString () + ".";
			long two = ipCode >> 16;
			subIP = Convert.ToInt32 ( two.ToString ( "f0" ) ) % 256;
			dotIP += subIP.ToString () + ".";
			long three = ipCode >> 8;
			subIP = Convert.ToInt32 ( three.ToString ( "f0" ) ) % 256;
			dotIP += subIP.ToString () + ".";
			long four = ipCode % 256;
			subIP = Convert.ToInt32 ( four.ToString ( "f0" ) );
			dotIP += subIP.ToString ();
			return dotIP;

		}
	}
}
