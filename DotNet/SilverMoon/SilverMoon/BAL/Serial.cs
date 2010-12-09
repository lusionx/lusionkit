using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace SilverMoon.BAL
{
    public class Serial : Tool.XmlCacheBase
    {

        public static ListItem[] GetList(SerialType t, bool addAll)
        {
            if (t == SerialType.Day)
            {
                var _end = new List<ListItem>();
                if (addAll)
                {
                    _end.Add(new ListItem("All", string.Empty));
                }

                for (int i = 1; i < 32; i++)
                {
                    _end.Add(new ListItem(i.ToString("d2")));
                }
                return _end.ToArray();
            }
            var nodes = from node in Current.XmlRoot.Descendants(t.ToString()).Single().Descendants("item")
                        select new ListItem(node.Value, node.Attribute("value").Value);

            if (addAll)
            {
                var q = nodes.ToList();
                q.Insert(0, new ListItem("All", string.Empty));
                return q.ToArray();
            }
            else
            {
                return nodes.ToArray();
            }

        }


        private static Serial _sl;

        private static Serial Current
        {
            get
            {
                if (_sl == null)
                {
                    _sl = new Serial();
                }
                return _sl;
            }
        }

        protected override string GetFilePath()
        {
            return HttpContext.Current.Server.MapPath("~/App_Data/Serial.xml");
        }

        /// <summary>
        /// 从session,保存year
        /// </summary>
        public static int Year
        {
            get
            {
                var sess = HttpContext.Current.Session;
                if (sess["Year"] == null)
                {
                    sess["Year"] = DateTime.Now.Year;
                }
                return Convert.ToInt32(sess["Year"]);
            }
            set
            {
                HttpContext.Current.Session["Year"] = value;
            }
        }

        /// <summary>
        /// 根据配置,绑定年份
        /// </summary>
        /// <param name="dll"></param>
        public static void BindYear(DropDownList dll)
        {
            var q = from a in Current.XmlRoot.Element("Year").Elements()
                    select new ListItem
                    {
                        Text = a.Attribute("value").Value,
                        Selected = a.Attribute("seletct").Value == "1"
                    };
            dll.Items.AddRange(q.ToArray());
        }
    }
}

