using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Alx.ORM.Core;

namespace Alx.ORM.Code.Oracle
{
    public partial class ProcList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.constr.Text = Request.Params["constr"];
            this.facstr.Text = Request.Params["facstr"];
            ObjectContext db = new ObjectContext(this.constr.Text, this.facstr.Text);
            this.rpt_procs.DataSource = db.Query(@"select object_name from user_objects obj where obj.OBJECT_TYPE='PROCEDURE'", null).Tables[0];
            this.rpt_procs.DataBind();
        }
    }
}