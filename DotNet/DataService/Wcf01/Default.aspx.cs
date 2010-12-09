using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Wcf01.BLL;

namespace Wcf01
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var db = new KjptDB();

            this.GridView1.DataSource = db.Persons.Take(10);
            this.GridView1.DataBind();
        }
    }
}
