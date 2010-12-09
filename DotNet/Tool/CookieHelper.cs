using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Tool
{
    public static class CookieHelper
    {
        public static void Add(string name, string value)
        {
            Add(name, value, 1);
        }

        public static void Add(HttpCookie coo)
        {
            HttpContext.Current.Response.Cookies.Add(coo);
        }

        public static void Add(string name, string value, int hours)
        {
            HttpCookie cookie = new HttpCookie(name, value);
            cookie.Expires = DateTime.Now.AddHours(hours);
            HttpContext.Current.Response.Cookies.Add(cookie);

        }

        public static void Remove(string name)
        {
            HttpContext.Current.Response.Cookies.Remove(name);
        }

        /// <summary>
        /// 读取某个cookie，可能是null
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string Get(string name)
        {
            var coo = HttpContext.Current.Request.Cookies.Get(name);
            if (coo == null)
            {
                return string.Empty;
            }
            else
            {
                return coo.Value;
            }
        }
    }
}
