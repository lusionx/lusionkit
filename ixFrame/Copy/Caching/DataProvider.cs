using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using System.Web.Caching;
using System.Collections.Specialized;

namespace Portal.Facilities
{
	/// <summary>
	/// �����ṩ��
	/// </summary>
	public sealed class DataProvider
	 : IDisposable
	{
		//static BalancedBinaryTree<String> cacheKeyBalancedBinaryTree = new BalancedBinaryTree<string>();
		public static System.Collections.Specialized.OrderedDictionary CachedKeyDictionary = new OrderedDictionary ();
		//Cache Activity log path
		private static string cacheActivityLogPath = System.Configuration.ConfigurationManager.AppSettings ["CacheActivityLogPath"];
		//�Ƿ��¼Cache���־
		private static bool enableLogCacheActivity = Convert.ToBoolean ( System.Configuration.ConfigurationManager.AppSettings ["EnableLogCacheActivity"] );
		/// <summary>
		/// �Ƿ�����Memcached
		/// </summary>
		private static readonly bool ENABLE_MEMCACHED = SafeConvert.ToBoolean ( System.Configuration.ConfigurationManager.AppSettings ["EnableMemcached"] );


		#region Fields

		public const string RemoveByPatternKey = "DAC-RemoveByPatternKey";
		public const string DACPrefix = "DAC";
		public const string Delimiter = "_";
		public const string PARAM_Delimiter = "_PARM{0}_";
		private string configKey = string.Empty;
		private string cacheKey = string.Empty;

		#endregion

		#region Public Methods

		#region Public Construct Methods
		/// <summary>
		/// configKey
		/// </summary>
		/// <param name="configKey">�������ļ�(DatabaseCache.config)��Ӧ��һ��Ϊ�洢������</param>
		/// <param name="parms">��������</param>
		public DataProvider( string configKey, string [] parms )
		{
			this.configKey = configKey;
			StringBuilder cacheKeyBuilder = new StringBuilder ( 2048 );
			cacheKeyBuilder.Append ( DACPrefix );
			cacheKeyBuilder.Append ( Delimiter );
			cacheKeyBuilder.Append ( configKey );
			//cacheKeyBuilder.Append ( Delimiter );
			if ( parms != null )
			{
				for ( int i = 0, count = parms.Length; i < count; i++ )
				{
					cacheKeyBuilder.AppendFormat ( PARAM_Delimiter, i );
					cacheKeyBuilder.Append ( parms [i] );
				}
			}
			cacheKey = cacheKeyBuilder.ToString ();
		}

		#endregion

		/// <summary>
		/// �����ݼ��뵽����
		/// </summary>
		/// <param name="data"></param>
		public void Insert( object data )
		{
			Insert ( data, false );
		}

		public void Insert( object data, bool forceInsert )
		{
			if ( enableLogCacheActivity )
			{
				System.Diagnostics.Trace.WriteLine ( "Insert Entered:" + cacheKey, "Cache" );
				FileHelper.Log ( cacheActivityLogPath, "Insert Entered:" + cacheKey );
			}

			if ( data == null )
				return;
			CacheConfigInfo configInfo = CacheConfigs.Current.GetConfig ( configKey );
			if ( configInfo == null )
			{
				if ( enableLogCacheActivity )
				{
					System.Diagnostics.Trace.WriteLine ( "configInfo == null:" + cacheKey, "Cache" );
					FileHelper.Log ( cacheActivityLogPath, "configInfo == null:" + cacheKey );
				}

				//���û�������û���
				return;
			}
			if ( !forceInsert )
			{
				if ( DataProviderLazeCacheHelper.HasBeenReached ( cacheKey, configInfo.ExpirationTimeMinute, configInfo.Threshold ) )
				{
					DoInsert ( configInfo, data );
				}
			}
			else
			{
				DoInsert ( configInfo, data );
			}
		}

		private void DoInsert( CacheConfigInfo configInfo, object data )
		{
			switch ( configInfo.DependencyType )
			{
				case CacheDependencyType.Time:
					CachingService.Current.Add ( cacheKey, data, configInfo.ExpirationTimeMinute );//, new OnDataCacheRemoved () );
					
					#region Test
					//LJBinsTree binTree = CachedKeyDictionary [configKey] as LJBinsTree;
					//if ( binTree != null )
					//{
					//    CacheBinInfo cacheBinInfo = new CacheBinInfo ( Delimiter, cacheKey );
					//    lock ( binTree )
					//    {
					//        if ( binTree.FindNode ( cacheBinInfo, binTree.GetRoot () ) == null )
					//        {
					//            int index = cacheKey.IndexOf ( Delimiter );
					//            while ( index > 0 )
					//            {
					//                string path = cacheKey.Substring ( 0, index );
					//                CacheBinInfo binInfo = new CacheBinInfo ( Delimiter, path );
					//                if ( binTree.FindNode ( binInfo, binTree.GetRoot () ) == null )
					//                {
					//                    binTree.Insert ( binInfo );
					//                }
					//                index = cacheKey.IndexOf ( Delimiter, index + 1 );
					//            }
					//            binTree.Insert ( cacheBinInfo );
					//        }
					//    }
					//}
					//else
					//{
					//    int index = cacheKey.IndexOf ( Delimiter );
					//    if ( index > 0 )
					//    {
					//        LJBinsTree newBinTree = new LJBinsTree ();
					//        while ( index > 0 )
					//        {
					//            string path = cacheKey.Substring ( 0, index );
					//            CacheBinInfo binInfo = new CacheBinInfo ( Delimiter, path );
					//            newBinTree.Insert ( binInfo );
					//            index = cacheKey.IndexOf ( Delimiter, index + 1 );
					//        }
					//        newBinTree.Insert ( new CacheBinInfo ( Delimiter, cacheKey ) );
					//        CachedKeyDictionary.Add ( configKey, newBinTree );
					//    }
					//} 
					#endregion

					if ( enableLogCacheActivity )
					{
						System.Diagnostics.Trace.WriteLine ( "Cache Added:" + cacheKey, "Cache" );
						FileHelper.Log ( cacheActivityLogPath, "Cache Added:" + cacheKey );
					}

					break;
				case CacheDependencyType.Database:
					//CachingService.Current.Add( cacheKey, data, configInfo.GetDependency () );
					throw new NotImplementedException ();
			}
		}

		/// <summary>
		/// �ӻ�������ȡ����
		/// </summary>
		/// <returns></returns>
		public object Get()
		{
			object cachedObject = CachingService.Current.Get ( cacheKey );
			//object cachedObject = null;

			if ( enableLogCacheActivity )
			{
				StringBuilder strTemp = new StringBuilder ();
				strTemp.Append ( "Cache Retrieved:" + cacheKey );
				if ( cachedObject == null )
					strTemp.Append ( "--NULL" );
				else
					strTemp.Append ( cachedObject.ToString () );

				System.Diagnostics.Trace.WriteLine ( strTemp, "Cache" );

				FileHelper.Log ( cacheActivityLogPath, strTemp.ToString () );
			}

			return cachedObject;

		}

		#region Remove Methods

		/// <summary>
		/// �ӻ������Ƴ�����
		/// </summary>
		public void Remove()
		{
			if ( enableLogCacheActivity )
			{
				FileHelper.Log ( cacheActivityLogPath, "Cache Removed(Remove()):" + cacheKey );
				System.Diagnostics.Trace.WriteLine ( "Cache Removed(Remove()):" + cacheKey, "Cache" );
			}
			CachingService.Current.Remove ( cacheKey );

		}

		/// <summary>
		/// �ӻ�����ͨ��ģʽƥ���Ƴ�����
		/// </summary>
		public void RemoveByPattern()
		{
			if ( enableLogCacheActivity )
			{
				System.Diagnostics.Trace.WriteLine ( "Cache Removed(RemoveByPattern()):" + cacheKey, "Cache" );
				FileHelper.Log ( cacheActivityLogPath, "Cache Removed(RemoveByPattern()):" + cacheKey );
			}

			//LJBinsTree binTree = CachedKeyDictionary[configKey] as LJBinsTree;
			//if (binTree != null)
			//{
			//    lock (binTree)
			//    {
			//        LJTreeNode treeNode = binTree.FindNode(new CacheBinInfo(Delimiter, cacheKey), binTree.GetRoot());
			//        if (treeNode != null)
			//        {
			//            RemoveCacheBinNode(binTree, treeNode);
			//        }
			//    }
			//}
			//else
			//{
			//    //CachingService.Current.RemoveByPattern(cacheKey);
			//    CachingService.Current.Remove(cacheKey);
			//}

			//ͳһ����
			//CachingService.Current.RemoveByPattern(cacheKey);
			object o = RequestCacheService.GetCurrent ().Get ( RemoveByPatternKey );
			if ( o == null )
			{
				RequestCacheService.GetCurrent ().Add ( RemoveByPatternKey, cacheKey );
			}
			else
			{
				o += "|" + cacheKey;
				RequestCacheService.GetCurrent ().Add ( RemoveByPatternKey, o );
			}
		}

		public void RemoveCacheBinNode( LJBinsTree binTree, LJTreeNode binNode )
		{
			if ( binNode.Left != null )
			{
				RemoveCacheBinNode ( binTree, binNode.Left );
			}
			else
			{
				CacheBinInfo binInfo = binNode.data as CacheBinInfo;
				if ( binInfo != null )
				{
					CachingService.Current.Remove ( binInfo.CacheKey );
				}
			}
			if ( binNode.Right != null )
				RemoveCacheBinNode ( binTree, binNode.Right );
			binTree.Delete ( binNode.data );
		}

		public static void Remove( string configKey, string [] parms )
		{
			DataProvider dataProvider = new DataProvider ( configKey, parms );
			dataProvider.Remove ();
		}

		public static void RemoveByPattern( string configKey, string [] parms )
		{
			DataProvider dataProvider = new DataProvider ( configKey, parms );
			dataProvider.RemoveByPattern ();
		}

		#endregion

		#endregion


		#region Dispose Methods

		private bool disposed = false;

		public void Dispose()
		{
			Dispose ( true );
			GC.SuppressFinalize ( this );
		}

		public void Dispose( bool disposing )
		{
			if ( disposed )
				return;
			if ( disposing )
			{

			}
			disposed = true;
		}

		//~DataProvider()
		//{
		//    this.Dispose ( false );
		//}

		#endregion

	}
}

