using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LffBlog.test
{
    public partial class updiv : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //this.Label1.Text = "111";
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //this.Label1.Text = this.TextBox1.Text;
        }
    }
}
