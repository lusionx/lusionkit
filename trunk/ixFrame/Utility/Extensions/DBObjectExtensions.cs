using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Utility.Extensions
{
    //[Obsolete("Please use FieldLoader class in entity constructor method.")]
    public static class DBObjectExtensions
    {
        public static int ToInt(this object obj)
        {
            return ToInt(obj, 0);
        }

        public static int ToInt(this object obj, int def)
        {
            return obj == DBNull.Value ? def : Convert.ToInt32(obj);
        }

        public static bool ToBool(this object obj)
        {
            return ToBool(obj, false);
        }

        public static bool ToBool(this object obj, bool def)
        {
            return obj == DBNull.Value ? def : Convert.ToBoolean(obj);
        }

        public static DateTime ToDateTime(this object obj)
        {
            return obj.ToDateTime(DateTime.Now);
        }

        public static DateTime ToDateTime(this object obj, DateTime def)
        {
            return obj == DBNull.Value ? def : Convert.ToDateTime(obj);
        }

        public static float ToFloat(this object obj)
        {          
            return obj.ToFloat(0);
        }

        public static float ToFloat(this object obj, float def)
        {
            return obj == DBNull.Value ? def : Convert.ToSingle(obj);
        }

        public static double ToDouble(this object obj)
        {           
            return obj.ToDouble(0);
        }

        public static double ToDouble(this object obj, double def)
        {
            return obj == DBNull.Value ? def : Convert.ToDouble(obj);
        }

        public static DateTime TodayMax(this DateTime dt)
        {
            return DateTime.Parse(dt.ToShortDateString() + " 23:59:59.998");
        }
            

    }
}