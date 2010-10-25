using System;
using System.Collections;
using System.Reflection;

namespace Portal.Facilities
{
	public static class AssemblyCache
	{
		private static Hashtable assemblyCache = Hashtable.Synchronized ( new Hashtable () );

		public static Assembly Load( string assemblyName )
		{
			Assembly assembly = ( Assembly ) assemblyCache [assemblyName];
			if ( assembly == null )
			{
				assembly = Assembly.Load ( assemblyName );
				assemblyCache [assemblyName] = assembly;
			}
			return assembly;
		}
	}
}
