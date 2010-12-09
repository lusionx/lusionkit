using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SilverMoon.BAL;

namespace SilverMoon.Controls
{
    public partial class Menu : System.Web.UI.UserControl
    {
        public string RootMapNodeTile { set; get; }

        protected string AppPath;

        protected string FuncName;
        protected void Page_Load(object sender, EventArgs ee)
        {
            AppPath = Request.ApplicationPath;
            var home = (XElement)BAL.SiteMap.Current.XmlRoot.FirstNode;
            var funcNode = home.Elements().Single(e => e.Attribute("title").Value == RootMapNodeTile);
            var source = from n in funcNode.Elements()
                         select new
                         {
                             title = n.Attribute("description").Value,
                             href = AppPath.TrimEnd('/') + n.Attribute("url").Value.TrimStart('~'),
                             text = n.Attribute("title").Value
                         };
            this.Repeater1.DataSource = source;
            this.Repeater1.DataBind();
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var rpt = (Repeater)e.Item.FindControl("Repeater2");
                var lit = (Literal)e.Item.FindControl("Literal1");
                var home = (XElement)BAL.SiteMap.Current.XmlRoot.FirstNode;
                var l = new List<XElement>();
                ListX(l, home);
                var q = l.Where(x => x.Attribute("title").Value == lit.Text);
                if (q.Count() == 1 && q.Single().HasElements)
                {
                    rpt.DataSource = from x in q.Elements()
                                     select new
                                     {
                                         title = x.Attribute("description").Value,
                                         href = AppPath.TrimEnd('/') + x.Attribute("url").Value.TrimStart('~'),
                                         text = x.Attribute("title").Value
                                     };
                    rpt.DataBind();
                }
            }
        }

        protected void ListX(List<XElement> l, XElement root)
        {
            l.Add(root);
            if (root.HasElements)
            {
                foreach (var x in root.Elements())
                {
                    ListX(l, x);
                }
            }
        }
    }
}