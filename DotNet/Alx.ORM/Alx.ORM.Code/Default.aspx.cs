using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Alx.ORM.Core;
using System.Data;


namespace Alx.ORM.Code
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var cnnstr = System.Configuration.ConfigurationManager.ConnectionStrings["hrConnection"].ConnectionString;
                ObjectContext db = new ObjectContext(cnnstr);
                this.rpt_tables.DataSource = db.Query("select table_name from user_tables", null).Tables[0];
                this.rpt_tables.DataBind();
            }
        }

        DataTable dt = null;
        protected void btn_g_Click(object sender, EventArgs e)
        {
            var cnnstr = System.Configuration.ConfigurationManager.ConnectionStrings["hrConnection"].ConnectionString;
            ObjectContext db = new ObjectContext(cnnstr);
            dt = db.Query("SELECT COLUMN_name,data_type,TABLE_NAME FROM user_tab_columns WHERE TABLE_NAME in (" + hf.Value + ")", null).Tables[0];
            this.rpt_class.DataSource = dt.AsEnumerable().Select(a => a.Field<string>("TABLE_NAME")).Distinct();
            this.rpt_class.DataBind();

        }

        protected void rpt_class_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var rpt_pro = e.Item.FindControl("rpt_pro") as Repeater;
            var tname = e.Item.DataItem as string;
            var q = from a in dt.AsEnumerable()
                    where a.Field<string>("TABLE_NAME") == tname
                    select new
                    {
                        systemtype = "System.String",
                        name = a.Field<string>("COLUMN_name").ToLower(),
                        ColumnName = a.Field<string>("COLUMN_name"),
                        proname = a.Field<string>("COLUMN_name").Substring(0, 1).ToUpper() + a.Field<string>("COLUMN_name").Substring(1).ToLower(),
                        a =string.Empty
                    };

            rpt_pro.DataSource = q;
            rpt_pro.DataBind();
        }

    }
}