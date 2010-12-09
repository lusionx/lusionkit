using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tool;
using System.Text;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace SilverMoon.BAL
{
    public class PageBase : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            if (ValidateUser())
            {
                base.OnInit(e);
            }
            else
            {
                Response.Redirect("~/NoRight.htm");
            }
        }

        private bool ValidateUser()
        {
            var xe = System.Web.SiteMap.CurrentNode;
            if (xe == null)
            {
                return true;
            }
            var pageroles = System.Web.SiteMap.CurrentNode.Roles.Cast<string>();
            if (pageroles.Count() == 0)
            {
                return true;
            }
            var useroles = Roles.GetRolesForUser(Membership.GetUser().UserName);
            if (useroles.Contains(System.Configuration.ConfigurationManager.AppSettings["AdminRole"]))
            {
                return true;
            }
            return pageroles.Intersect(useroles).Count() > 0;
        }

        public void JQueryReady(string jsstr)
        {
            ClientScript.RegisterStartupScript(this.GetType(),
                "ready", "$(function(){" + jsstr + "});", true);
        }

        public void Alert(string msg)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "alert('{0}');".FormatWith(msg), true);
        }

        protected string ShowState(object o)
        {
            return DB.Current.ShowState(o);
        }

        public override void DataBind()
        {
            base.DataBind();
        }

        public void CreateXls(object DataSource, string fileName)
        {
            var response = this.Response;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.Buffer = true;
            response.BufferOutput = true;
            response.ContentType = "application/octet-stream";
            response.Charset = "utf-8";
            response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName));
            response.ContentEncoding = Encoding.GetEncoding("utf-8");
            response.ContentType = "application/ms-excel";
            CultureInfo formatProvider = new CultureInfo("ZH-CN", true);
            StringWriter writer = new StringWriter(formatProvider);
            HtmlTextWriter writer2 = new HtmlTextWriter(writer);
            var gv = new GridView();
            //gv.RowDataBound += new EventHandler(gv_RowDataBound);
            gv.DataSource = DataSource;
            gv.DataBind();
            gv.RenderControl(writer2);
            response.Write(writer.ToString());
            response.End();

        }

        private void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            /*
1）  文本：vnd.ms-excel.numberformat:@
2）  日期：vnd.ms-excel.numberformat:yyyy/mm/dd
3）  数字：vnd.ms-excel.numberformat:#,##0.00
4）  货币：vnd.ms-excel.numberformat:￥#,##0.00
5）  百分比：vnd.ms-excel.numberformat: #0.00%
             List<PropertyInfo> pro_names = new List<PropertyInfo>();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.DataItem.GetType().GetProperties();
                    if (1 == 1)
                    {
                        e.Row.Cells[i].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
                    }
                }
            } */

        }
    }
}
