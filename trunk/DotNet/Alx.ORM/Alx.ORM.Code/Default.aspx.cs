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
                this.rpt_tables.DataSource = db.Query("SELECT TABLE_NAME, COMMENTS FROM USER_TAB_COMMENTS", null).Tables[0];
                this.rpt_tables.DataBind();
            }
        }

        DataTable dt = null;
        protected void btn_g_Click(object sender, EventArgs e)
        {
            var cnnstr = System.Configuration.ConfigurationManager.ConnectionStrings["hrConnection"].ConnectionString;
            ObjectContext db = new ObjectContext(cnnstr);
            dt = db.Query(@"select a.table_name, A.column_name, A.data_type, A.data_length, A.data_precision, A.Data_Scale,A.nullable, A.Data_default, B.comments  
from user_tab_columns A,user_col_comments B where a.COLUMN_NAME=b.column_name and A.Table_Name = B.Table_Name and A.Table_Name in (" + hf.Value + ")", null).Tables[0];

            var dt_comments = db.Query("SELECT TABLE_NAME, COMMENTS FROM USER_TAB_COMMENTS WHERE TABLE_NAME in (" + hf.Value + ")", null).Tables[0];
            this.rpt_class.DataSource = dt_comments;
            this.rpt_class.DataBind();
        }

        protected void rpt_class_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var rpt_pro = e.Item.FindControl("rpt_pro") as Repeater;
            var tname = DataBinder.Eval(e.Item.DataItem, "TABLE_NAME").ToString();
            Func<string, string> f_sysType = s =>
            {
                switch (s)
                {
                    case "NUMBER":
                        return "System.Decimal";
                    case "RAW":
                        return "System.Guid";
                    case "DATE":
                        return "System.DateTime";
                    default://char,varchar2
                        return "System.String";
                }
            };
            Func<string, string> f_dbtype = s =>
                {
                    switch (s)
                    {
                        case "NUMBER":
                            return "DbType.Decimal";
                        case "RAW":
                            return "DbType.Binary";
                        case "DATE":
                            return "DbType.DateTime";
                        default://char,varchar2
                            return "DbType.String";
                    }
                };
            var q = from a in dt.AsEnumerable()
                    where a.Field<string>("TABLE_NAME") == tname
                    select new
                    {
                        systemtype = f_sysType(a.Field<string>("data_type")),
                        dbtype = f_dbtype(a.Field<string>("data_type")),
                        nullable = a.Field<string>("nullable") == "Y" ? "true" : "false",
                        comments = a.Field<string>("comments"),
                        name = a.Field<string>("COLUMN_name").ToLower(),
                        ColumnName = a.Field<string>("COLUMN_name"),
                        proname = a.Field<string>("COLUMN_name"),
                        a = string.Empty
                    };

            rpt_pro.DataSource = q;
            rpt_pro.DataBind();
        }

    }
}