using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SLWeb.WS;

namespace SLWeb
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btn_login_Click(object sender, EventArgs e)
        {
            //UserInf uinf = new UserInf();
            //if (!uinf.IsOnline(this.TextBox1.Text.Trim()))
            //{
            //    Code.CookieHelper.Add("id", TextBox1.Text.Trim());
            //    Response.Redirect("Default.aspx");
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('此用户已经在线');", true);
            //    return;
            //}
            Code.CookieHelper.Add("id", TextBox1.Text.Trim());
            Response.Redirect("Default.aspx");
        }
    }
}
