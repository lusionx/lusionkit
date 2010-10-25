using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alxf.Frame.Tool
{
    /// <summary>
    /// string的扩展方法
    /// </summary>
    public static class StringExt
    {
        public static Int32 ToInt32(this string str)
        {
            return Convert.ToInt32(str);
        }

        public static DateTime ToDateTime(this string str)
        {
            return Convert.ToDateTime(str);
        }

        public static Guid ToGuid(this string str)
        {
            return new Guid(str);
        }
    }
}
