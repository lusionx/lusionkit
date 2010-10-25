using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Caching;

namespace Portal.Facilities
{
	[Serializable]
	public sealed class CacheConfigInfo
	{
		private string key = string.Empty;
		private CacheDependencyType dependencyType = CacheDependencyType.Time;
		private string dataBaseName = string.Empty;
		private string dependencyTables = string.Empty;
		private int expirationTimeMinute = 3;
		private int threshold = 2;
		
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

		public CacheDependencyType DependencyType
		{
			get
			{
				return dependencyType;
			}
			set
			{
				dependencyType = value;
			}
		}

		public int ExpirationTimeMinute
		{
			get
			{
				return expirationTimeMinute;
			}
			set
			{
				expirationTimeMinute = value;
			}
		}

		public string DataBaseName
		{
			get
			{
				return dataBaseName;
			}
			set
			{
				dataBaseName = value;
			}
		}

		public int Threshold
		{
			get { return threshold; }
			set { threshold = value; }
		}

		public CacheConfigInfo ( string key, string dependencyType, string expirationTimeMinutes, string dependencyDatabasename, string dependencyTables )
		{
			this.key = key;
			this.dependencyType = ( CacheDependencyType ) Convert.ToInt32 ( dependencyType );
			this.expirationTimeMinute = Convert.ToInt32 ( expirationTimeMinutes );
			this.dataBaseName = dependencyDatabasename;
			this.dependencyTables = dependencyTables;
		}

        public AggregateCacheDependency GetDependency()
        {
            AggregateCacheDependency aggregateCacheDependency = null;
            if (this.dependencyType == CacheDependencyType.Database)
            {
                string[] tablenames = dependencyTables.Split(',');
                aggregateCacheDependency = new AggregateCacheDependency();
                for (int i = 0, count = tablenames.Length; i < count; i++)
                {
                    aggregateCacheDependency.Add(new SqlCacheDependency(dataBaseName, tablenames[i]));
                }
            }
            return aggregateCacheDependency;
        }

	}
}
