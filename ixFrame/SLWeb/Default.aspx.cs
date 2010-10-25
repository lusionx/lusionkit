using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SLWeb.WS;

namespace SLWeb
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WS.UserInf uinf = new UserInf();
            var q = uinf.GetUserList();
            var qq = from ee in q
                     where ee.UserID != Code.CookieHelper.Get("id")
                     select ee;
            this.GridView1.DataSource = qq;
            this.GridView1.DataBind();
        }

        protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            var gv = sender as GridView;
            var lbtn = gv.Rows[e.NewSelectedIndex].Cells[2].FindControl("LinkButton1") as LinkButton;
            string uid = lbtn.CommandArgument;
            Message m = new Message();
            string currUser = Code.CookieHelper.Get("id");
            m.Invite(currUser, uid);

            Room room = new Room();
            
            Response.Redirect("game.aspx");
        }
    }
}
