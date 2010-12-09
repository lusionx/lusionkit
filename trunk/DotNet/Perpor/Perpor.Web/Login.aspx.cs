using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Configuration;
using Perpor.BLL;

namespace Perpor.Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FormsAuthentication.SignOut();
            }
        }

        protected void btn_go_Click(object sender, EventArgs e)
        {
            if (Membership.ValidateUser(txt_name.Text, txt_pwd.Text))
            {
                FormsAuthentication.RedirectFromLoginPage(txt_name.Text, true);
            }
        }

        protected void btn_any_Click(object sender, EventArgs e)
        {
            var name = AppSettings.Get(AppSettings.Keys.AnonymousUserName);
            FormsAuthentication.RedirectFromLoginPage(name, true);
        }

        protected void btn_reg_Click(object sender, EventArgs e)
        {
            List<string> dw = new List<string> { "lxingsys@163.com", "liuxing@wisdomin.com" };
            //Message.SendMail(dw, "tt11", "xx11");
        }


    }
}
