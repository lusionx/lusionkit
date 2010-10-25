using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace Portal.Facilities
{
	[Serializable]
	public sealed class CacheConfigCollection : CollectionBase
	{
		#region Fields

		Dictionary<string, int> keyIndexs;
		private static object lockObject = new object ();

		#endregion

		#region Properties

		#endregion

		#region Methods

		#region Public Construct Methods

		public CacheConfigCollection ()
		{
		}

		#endregion

		public void CreateKeyIndex()
		{
			//if ( keyIndexs == null )
			//{
			//    lock( lockObject )
			//    {
					if ( keyIndexs == null )
					{
						keyIndexs = new Dictionary<string, int> ( List.Count, StringComparer.InvariantCultureIgnoreCase );
						for ( int i = 0, count = List.Count; i < count; i++ )
						{
							CacheConfigInfo configInfo = List [i] as CacheConfigInfo;
							if ( configInfo != null )
							{
								#if DEBUG
								try
								{
									keyIndexs.Add ( configInfo.Key, i );
								}
								catch( Exception e )
								{
									ExceptionService.Current.Handle( e );
								}
								#else
								if ( !keyIndexs.ContainsKey ( configInfo.Key ) )
								{
									keyIndexs.Add ( configInfo.Key, i );
								}
								#endif
							}
						}
					}
			//    }
			//}
		}
		
		public CacheConfigInfo GetConfig ( string key )
		{
			int index = GetKeyIndex ( key );
			if ( index != -1 )
				return List [index] as CacheConfigInfo;
			return null;
		}

		int GetKeyIndex ( string key )
		{
			if ( keyIndexs == null )
			{
				return -1;
			}
			if ( !keyIndexs.ContainsKey( key ) )
			{
				return -1;
			}
			return keyIndexs[ key ];
			/*
			string index = keyIndexs [key];
			if ( index == null || index.Length == 0 )
				return -1;
			return Convert.ToInt32 ( index );
			*/
		}
		
		public CacheConfigInfo this [int index]
		{
			get
			{
				return ( CacheConfigInfo ) List [index];
			}
			set
			{
				List [index] = value;
			}
		}

		public void Add ( CacheConfigInfo value )
		{
			List.Add ( value );
		}

		public void Add ( CacheConfigInfo [] value )
		{
			for ( int i = 0, count = value.Length; i < count; i++ )
				List.Add ( value );
		}

		public int IndexOf ( CacheConfigInfo value )
		{
			return ( List.IndexOf ( value ) );
		}

		public void Insert ( int index, CacheConfigInfo value )
		{
			List.Insert ( index, value );
		}

		public void Remove ( CacheConfigInfo value )
		{
			List.Remove ( value );
		}

		public bool Contains ( CacheConfigInfo value )
		{
			return ( List.Contains ( value ) );
		}

		#endregion

	}
}
