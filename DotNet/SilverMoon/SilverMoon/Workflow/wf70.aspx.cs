using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SilverMoon.Workflow
{
    public partial class wf70 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.AspNetPager1.RecordCount = 100;
                this.Repeater1.DataSource = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 1, 2, 3, 4, 5, 6, 1, 21, 3, 32, 32, 3 };
                this.Repeater1.DataBind();
            }
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {

        }
    }
}
