using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SilverMoon.DAL;
using System.Configuration;

namespace SilverMoon.BAL
{
    public sealed class DB
    {
        private static SMDataDataContext _db;

        public static SMDataDataContext Current
        {
            get
            {
                if (_db == null)
                {
                    string cn = ConfigurationManager.ConnectionStrings["SilverMoon"].ConnectionString;
                    _db = new SMDataDataContext(cn);
                }
                return _db;
            }
        }

        [Obsolete("次方法直接new一个实例,而不是单利模式", false)]
        public static SMDataDataContext xCurrent
        {
            get
            {
                string cn = ConfigurationManager.ConnectionStrings["SilverMoon"].ConnectionString;
                return new SMDataDataContext(cn);
            }
        }

        /// <summary>
        /// 置空,DB,以便下次重新new
        /// </summary>
        public static void Reset()
        {
            _db = null;
        }

        private DB()
        {

        }
    }
}
