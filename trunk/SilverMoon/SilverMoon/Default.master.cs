using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SilverMoon
{
    public partial class Default1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //向页面注入一个全局变量表示网站根路径,如:'','/vdir'
            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(),
                "AppPath",
                string.Format("var AppPath='{0}';", Request.ApplicationPath),
                true);
            if (!IsPostBack)
            {
                var us = Membership.GetUser();
                if (us != null)
                {
                    this.lb_uName.Text = us.UserName;
                }
                else
                {
                    FormsAuthentication.SignOut();
                }
                //增加标题
                this.Page.Title = this.lt_fcname.Text = SiteMap.CurrentNode.Title;
            }
        }
    }
}
