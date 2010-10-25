using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Utility.Extensions;

namespace Utility
{
    public static class CookieHelper
    {
        public static void Add(string name, string value)
        {
            Add(name, value, 1);
        }

        public static void Add(string name, string value, int hours)
        {
            HttpCookie cookie = new HttpCookie(name, value);
            cookie.Expires = DateTime.Now.AddHours(hours);
            System.Collections.IEnumerator ie = HttpContext.Current.Response.Cookies.Keys.GetEnumerator();
            if (ie.HasEqual(name))
            {
                HttpContext.Current.Response.Cookies.Set(cookie);
            }
            else
            {
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
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
