using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Utility.Extensions
{

    public static class DataRowExtensions
    {
        #region By number
        public static string GetString(this IDataRecord rec, int fldnum)
        {
            if (rec.IsDBNull(fldnum))
                return string.Empty;
            return rec.GetString(fldnum);
        }

        public static decimal GetDecimal(this IDataRecord rec, int fldnum)
        {
            if (rec.IsDBNull(fldnum))
                return 0;
            return rec.GetDecimal(fldnum);
        }

        public static bool GetBoolean(this IDataRecord rec, int fldnum)
        {
            if (rec.IsDBNull(fldnum))
                return false;
            return rec.GetBoolean(fldnum);
        }

        public static byte GetByte(this IDataRecord rec, int fldnum)
        {
            if (rec.IsDBNull(fldnum))
                return 0;
            return rec.GetByte(fldnum);
        }

        public static DateTime GetDateTime(this IDataRecord rec, int fldnum)
        {
            if (rec.IsDBNull(fldnum))
                return DateTime.MinValue;
            return rec.GetDateTime(fldnum);
        }

        public static double GetDouble(this IDataRecord rec, int fldnum)
        {
            if (rec.IsDBNull(fldnum))
                return 0;
            return rec.GetDouble(fldnum);
        }

        public static float GetFloat(this IDataRecord rec, int fldnum)
        {
            if (rec.IsDBNull(fldnum))
                return 0;
            return rec.GetFloat(fldnum);
        }

        public static Guid GetGuid(this IDataRecord rec, int fldnum)
        {
            if (rec.IsDBNull(fldnum))
                return Guid.Empty;
            return rec.GetGuid(fldnum);
        }

        public static Int32 GetInt32(this IDataRecord rec, int fldnum)
        {
            if (rec.IsDBNull(fldnum))
                return 0;
            return rec.GetInt32(fldnum);
        }


        public static Int16 GetInt16(this IDataRecord rec, int fldnum)
        {
            if (rec.IsDBNull(fldnum))
                return 0;
            return rec.GetInt16(fldnum);
        }


        public static Int64 GetInt64(this IDataRecord rec, int fldnum)
        {
            if (rec.IsDBNull(fldnum))
                return 0;
            return rec.GetInt64(fldnum);
        }
        #endregion

        #region  By Name
        public static string GetString(this IDataRecord rec, string fldname)
        {
            return rec.GetString(rec.GetOrdinal(fldname));
        }

        
        public static decimal GetDecimal(this IDataRecord rec, string fldname)
        {
            return rec.GetDecimal(rec.GetOrdinal(fldname));
        }

        public static bool GetBoolean(this IDataRecord rec, string fldname)
        {
            return rec.GetBoolean(rec.GetOrdinal(fldname));
        }



        public static byte GetByte(this IDataRecord rec, string fldname)
        {
            return rec.GetByte(rec.GetOrdinal(fldname));
        }



        public static DateTime GetDateTime(this IDataRecord rec, string fldname)
        {
            return rec.GetDateTime(rec.GetOrdinal(fldname));
        }

        
        public static double GetDouble(this IDataRecord rec, string fldname)
        {
            return rec.GetDouble(rec.GetOrdinal(fldname));
        }

        
        public static float GetFloat(this IDataRecord rec, string fldname)
        {
            return rec.GetFloat(rec.GetOrdinal(fldname));
        }


        public static Guid GetGuid(this IDataRecord rec, string fldname)
        {
            return rec.GetGuid(rec.GetOrdinal(fldname));
        }

        
        public static Int32 GetInt32(this IDataRecord rec, string fldname)
        {
            return rec.GetInt32(rec.GetOrdinal(fldname));
        }

        
        public static Int16 GetInt16(this IDataRecord rec, string fldname)
        {
            return rec.GetInt16(rec.GetOrdinal(fldname));
        }


        public static Int64 GetInt64(this IDataRecord rec, string fldname)
        {
            return rec.GetInt64(rec.GetOrdinal(fldname));
        }

        #endregion
    }
}