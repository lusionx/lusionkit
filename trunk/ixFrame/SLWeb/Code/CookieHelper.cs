using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace SLWeb.Code
{
    public static class CookieHelper
    {
        public static void Add(string name, string value)
        {
            Add(name, value, 1);
        }

        public static void Add(string name, string value, int days)
        {
            HttpCookie cookie = new HttpCookie(name, value);
            cookie.Expires = DateTime.Now.AddDays(days);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static void Remove(string name)
        {
            Add(name, null, 0);
        }

        /// <summary>
        /// 读取某个cookie，可能是""
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string Get(string name)
        {
            HttpCookie cook = HttpContext.Current.Request.Cookies.Get(name);
            if (cook == null)
            {
                return string.Empty;
            }
            else
            {
                return cook.Value;
            }

        }
    }
}
