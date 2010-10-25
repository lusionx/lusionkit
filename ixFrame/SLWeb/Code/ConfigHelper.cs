using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SLWeb.Code
{
    public class ConfigHelper
    {
        public static string GetAppSetting(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key];
        }
    }
}
