using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Web;
using System.Xml;
using System.Web.Caching;

namespace Portal.Facilities
{
	[Serializable]
	public sealed class CacheConfigs
	{
		#region Member variables & constructor

		string xmlFile = "/Config/DatabaseCache.config";
		private static CacheConfigCollection configs = null;
		private static CacheConfigs current = null;

		public static CacheConfigs Current
		{
			get
			{
				if ( current == null )
				{
					current = new CacheConfigs ();
				}
				return current;
			}
		}
		
		private CacheConfigs()
		{
			GetConfigs();
		}

		#endregion

		#region properties

		public CacheConfigInfo GetConfig ( string key )
		{
			if ( configs == null )
			{
#if DEBUG
				//ExceptionService.Current.Handle( new Exception( "数据库缓存配置文件不存在！"));
				Logger.Current.Log ( "DataBaseCache", "数据库缓存配置文件不存在！" );
#endif
				
				return null;
			}
			CacheConfigInfo cacheConfigInfo = configs.GetConfig( key );
			if ( cacheConfigInfo != null )
			{
				return cacheConfigInfo;
			}
#if DEBUG
			//ExceptionService.Current.Handle ( new Exception ( "数据库缓存配置键不存在:" + key ) );
			Logger.Current.Log ( "DataBaseCache", "数据库缓存配置键不存在:" + key );
#endif
			return null;
		}

		private CacheConfigCollection GetConfigs ()
		{
			/*
			string cacheKey = APPLICATION_NAME;
			CacheConfigCollection configs = CachingService.Current.Get ( cacheKey ) as CacheConfigCollection;
			if ( configs == null )
			{
				lock( lockObject )
				{
					configs = CachingService.Current.Get ( cacheKey ) as CacheConfigCollection;
					if ( configs == null )
					{
						string file = null;
						HttpContext context = HttpContext.Current;
						if ( context != null )
							file = context.Server.MapPath ( "~/" + xmlFile );
						else
							file = Path.Combine ( AppDomain.CurrentDomain.BaseDirectory, xmlFile );

						//string file = HttpContext.Current.Server.MapPath ( xmlFile );
						if ( !System.IO.File.Exists ( file ) )
							return null;
						configs = new CacheConfigCollection ();
						XmlDocument doc = new XmlDocument ();
						doc.Load ( file );
						XmlNode root = doc.SelectSingleNode ( "Caches" );
						foreach ( XmlNode n in root.ChildNodes )
						{
							if ( n.NodeType != XmlNodeType.Comment )
							{
								CacheConfigInfo configInfo = new CacheConfigInfo(
									n.Attributes[ "key" ].Value,
									n.Attributes[ "DependencyType" ].Value,
									n.Attributes[ "ExpirationTimeMinutes" ].Value,
									n.Attributes[ "DependencyDatabasename" ].Value,
									n.Attributes[ "DependencyTables" ].Value );
								XmlAttribute thresholdXmlAttribute = n.Attributes[ "Threshold" ];
								if ( thresholdXmlAttribute != null )
									configInfo.Threshold = Convert.ToInt32( thresholdXmlAttribute.Value );
								configs.Add ( configInfo );
							}
						}
						configs.CreateKeyIndex ();
						CachingService.Current.Add ( cacheKey, configs, file );
					}
				}
			}
			 * */
			if ( configs == null )
			{
				string file = null;
				HttpContext context = HttpContext.Current;
				if ( context != null )
					file = context.Server.MapPath ( "~/" + xmlFile );
				else
					file = Path.Combine ( AppDomain.CurrentDomain.BaseDirectory, xmlFile );

				//string file = HttpContext.Current.Server.MapPath ( xmlFile );
				if ( !System.IO.File.Exists ( file ) )
					return null;
				configs = new CacheConfigCollection ();
				XmlDocument doc = new XmlDocument ();
				doc.Load ( file );
				XmlNode root = doc.SelectSingleNode ( "Caches" );
				int index = 0;
				foreach ( XmlNode n in root.ChildNodes )
				{
					if ( n.NodeType != XmlNodeType.Comment )
					{
						index++;
						try
						{
							CacheConfigInfo configInfo = new CacheConfigInfo (
								n.Attributes ["key"].Value.ToLower (),
								n.Attributes ["DependencyType"].Value,
								n.Attributes ["ExpirationTimeMinutes"].Value,
								n.Attributes ["DependencyDatabasename"].Value,
								n.Attributes ["DependencyTables"].Value );
							XmlAttribute thresholdXmlAttribute = n.Attributes ["Threshold"];
							if ( thresholdXmlAttribute != null )
							{
								configInfo.Threshold = Convert.ToInt32 ( thresholdXmlAttribute.Value );
							}
							configs.Add ( configInfo );
						}
						catch( Exception e )
						{
							//ExceptionService.Current.Handle( e );
							ExceptionService.Current.Handle ( new Exception ( String.Format ( "DataBaseCache.config: {0};/r/n{1}", n.InnerXml, e.Message ) ) );
						}
					}
				}
				configs.CreateKeyIndex ();
			}			
			return configs;
		}

		#endregion


	}
}
