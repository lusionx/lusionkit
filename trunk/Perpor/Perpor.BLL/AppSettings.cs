using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Perpor.BLL
{
    public class AppSettings
    {
        public enum Keys
        {
            /// <summary>
            /// 匿名用户
            /// </summary>
            AnonymousUserName,
            /// <summary>
            /// 第三方控件的路径目录
            /// </summary>
            DevCtrPath,
            /// <summary>
            /// 错误日志保存目录
            /// </summary>
            LogDir,
            /// <summary>
            /// 网站的根路径
            /// </summary>
            SiteRoot
        }

        public static string Get(Keys k)
        {
            return ConfigurationManager.AppSettings[k.ToString()];
        }
    }
}
