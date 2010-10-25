using System;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace SLApp
{
    public class ConfigHelper
    {
        private static XElement GetRoot()
        {
            return XDocument.Load("AppSetting.xml").Element("config");
        }

        public static string GetSetting(string name)
        {
            XElement root = GetRoot();
            var q = from ee in root.Elements()
                    where ee.Attribute("name").Value == name
                    select ee.Value;
            return q.Count() == 1 ? q.ToArray()[0] : string.Empty;
        }
    }
}
