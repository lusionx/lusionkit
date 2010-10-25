using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Data;

namespace Utility.Extensions
{
    /// <summary>
    /// 对DropDownList绑定数据的扩展
    /// </summary>
    public static class DataBindExtensions
    {
        public static void LoadData(this DropDownList ddl, DataTable dt, string textfield, string valuefield)
        {
            ddl.DataSource = dt;
            ddl.DataTextField = textfield;
            ddl.DataValueField = valuefield;
            ddl.DataBind();
        }

        public static void LoadData(this DropDownList ddl, DataTable dt, string field)
        {
            ddl.LoadData(dt, field, field);
        }

        public static void LoadData(this DropDownList ddl, string sql, string textfield, string valuefield)
        {
            ddl.LoadData(DB.GetCommandDataSet(sql).Tables[0], textfield, valuefield);
        }

        public static void LoadData(this DropDownList ddl, string sql, string field)
        {
            ddl.LoadData(DB.GetCommandDataSet(sql).Tables[0], field, field);
        }
    }
}
