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
    public partial class GenCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObjectContext db = new ObjectContext(Request.Params["constr"], Request.Params["facstr"]);
                dt = db.Query(@"select a.table_name, A.column_name, A.data_type, A.data_length, A.data_precision, A.Data_Scale,A.nullable, A.Data_default, B.comments  
from user_tab_columns A,user_col_comments B where a.COLUMN_NAME=b.column_name and A.Table_Name = B.Table_Name and A.Table_Name in (" + Request.Params["tabs"] + ") order by a.column_id", null).Tables[0];
                var dt_comments = db.Query("SELECT TABLE_NAME, COMMENTS FROM USER_TAB_COMMENTS WHERE TABLE_NAME in (" + Request.Params["tabs"] + ")", null).Tables[0];
                this.rpt_class.DataSource = dt_comments;
                this.rpt_class.DataBind();
                this.Title = Request.Params["tabs"];
            }
        }

        DataTable dt = null;
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
            ObjectContext db = new ObjectContext(Request.Params["constr"], Request.Params["facstr"]);
            var pkCol = db.Query(@"select col.column_name from user_constraints con,  user_cons_columns col 
where con.constraint_name = col.constraint_name and con.constraint_type='P' and col.table_name = ?", new object[] { tname }).Tables[0].Rows[0].Field<string>(0);
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
                        dbnull = a.Field<string>("nullable") == "Y" && a.Field<string>("data_type") == "DATE" ? "?" : "",
                        pk = a.Field<string>("COLUMN_name") == pkCol ? ", IsPrimary = true" : ""
                    };

            rpt_pro.DataSource = q;
            rpt_pro.DataBind();
        }
    }
}