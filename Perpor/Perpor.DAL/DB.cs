using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Perpor.DAL
{
    public sealed class DB
    {
        private static PerporDBDataContext _db;

        public static PerporDBDataContext Current
        {
            get
            {
                if (_db == null)
                {
                    string cn = ConfigurationManager.ConnectionStrings["PerporConnectionString"].ConnectionString;
                    _db = new PerporDBDataContext(cn);
                }
                return _db;
            }
        }

        private DB()
        {

        }
    }
}
