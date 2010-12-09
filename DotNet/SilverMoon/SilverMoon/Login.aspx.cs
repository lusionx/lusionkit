using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Drawing;
using Tool;
using GoogleApi;
using GoogleApi.ChartType;
namespace SilverMoon
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //            Chart ch = new Chart(new Line(LineType.lc));
            //            ch.Size.Height = 250;
            //            ch.Size.Width = 400;
            //            ch.Legend.Items.AddRange(new string[] { "aaa", "bbb" });
            //            ch.Legend.Place = PlaceType.t;
            //            ch.Data.Group.Add(new List<float> { 40, 60, -20, 130, 80, 70, 90, 50 });
            //            ch.Data.Group.Add(new List<float> { 10, 20, 30, 40, 50, 60, 70, 80 });
            //            ch.Fix.Items.AddRange(new float[] { -20, 130 });
            //            ch.Title.Color = Color.Gainsboro;
            //            ch.Title.Text = @"dsadsadsa dsadwdas
            //dsadsa";
            //            ch.Title.FontSize = 15;
            //            ch.Color.Items.AddRange(new Color[] { Color.Red, Color.Green });            
            //this.Literal1.Text = ch.ToString() + "<br/>" + ch.Html(false);
            if (!IsPostBack)
            {
                FormsAuthentication.SignOut();                  
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Membership.ValidateUser(txt_name.Text, txt_pwd.Text))
            {
                //var coo = FormsAuthentication.GetAuthCookie(txt_name.Text, true);
                //coo.Expires = DateTime.Now.AddHours(1);
                //Tool.CookieHelper.Add(coo);                
                FormsAuthentication.RedirectFromLoginPage(txt_name.Text, true);
            }
        }
    }
}
