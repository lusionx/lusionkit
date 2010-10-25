using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility.Extensions;

namespace SLWeb.Test
{
    public partial class gv : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var pp = new List<People>();
            pp.Add(new People { Age = 11, Name1 = "asa", Name2 = "fff" });
            pp.Add(new People { Age = 15, Name1 = "asa", Name2 = "fff" });
            this.GridView1.DataSource = pp;
            this.GridView1.DataBind();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //e.Row.Localize(
            }
        }
    }
}
