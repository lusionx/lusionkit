using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Alx.ORM.Core;
using System.Data;


namespace Alx.ORM.Code.Oracle
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.constr.Text = Request.Params["constr"];
                this.facstr.Text = Request.Params["facstr"];
                ObjectContext db = new ObjectContext(this.constr.Text, this.facstr.Text);
                this.rpt_tables.DataSource = db.Query(@"SELECT a.TABLE_NAME, a.COMMENTS 
                FROM USER_TAB_COMMENTS a inner join user_objects b on a.TABLE_NAME = b.object_name
where b.object_type = 'TABLE' order by a.table_name", null).Tables[0];
                this.rpt_tables.DataBind();
            }
        }
    }
}