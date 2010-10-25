using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tool;

namespace SilverMoon.BAL
{
    public static class BAlExtend
    {
        //由A-L的字符
        public static string ShowMonth(this object chr)
        {
            int i = chr.ToString().ToCharArray()[0] - '@';
            return i.ToString("d2");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i">1-12</param>
        /// <returns></returns>
        public static string ToMonth(this int i)
        {
            int chr = i - 1 + 'A';

            return Convert.ToChar(chr).ToString();
        }

        public static string ToD2(this object chr)
        {
            return chr.ToString().ToInt32().ToString("d2");
        }
    }
}
