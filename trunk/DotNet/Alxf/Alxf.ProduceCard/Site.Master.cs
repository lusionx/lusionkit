using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace Alxf.ProduceCard
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            foreach (SiteMapNode node in SiteMap.RootNode.ChildNodes)
            {
                var mu = new MenuItem();
                mu.NavigateUrl = node.Url;
                mu.Text = node.Title;
                mu.ToolTip = node.Description;
                this.NavigationMenu.Items.Add(mu);
                if (node.HasChildNodes)
                {
                    AddMenu(node, mu);
                }
            }

            //AddMenu(SiteMap.RootNode,this.NavigationMenu.Items[0]
        }

        protected void AddMenu(SiteMapNode smnode, MenuItem mi)
        {
            foreach (SiteMapNode node in smnode.ChildNodes)
            {
                var mu = new MenuItem();
                //node.Roles根据角色权限来
                //Membership.GetUser()
                //Roles.GetAllRoles();
                mu.NavigateUrl = node.Url;
                mu.Text = node.Title;
                mu.ToolTip = node.Description;
                mi.ChildItems.Add(mu);
                if (node.HasChildNodes)
                {
                    AddMenu(smnode, mu);
                }
            }
        }
    }
}
