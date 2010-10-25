using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Perpor.DAL;
using System.Web.Security;
using Perpor.BLL;
using Perpor.Web.Controls;

namespace Perpor.Web
{
    public partial class Default : PageBase
    {
        protected PPage pp = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            var db = DB.Current;
            var u = Membership.GetUser();
            pp = db.core_UserPages.First(x => x.UserName == u.UserName).Page();
            #region 注释
            //pp = new PPage();
            //pp.Column1.Add(new WidgetData
            //{
            //    ControlName = "UC0.ascx",
            //    Color = "widget " + BLL.ColorClasses.Blue
            //});
            //pp.Column1.Add(new WidgetData
            //{
            //    ControlName = "UC1.ascx",
            //    Color = "widget " + BLL.ColorClasses.Green
            //});
            //pp.Column2.Add(new WidgetData
            //{
            //    ControlName = "UC2.ascx",
            //    Color = "widget " + BLL.ColorClasses.Orange

            //});
            //pp.Column2.Add(new WidgetData
            //{
            //    ControlName = "UC3.ascx",
            //    Color = "widget " + BLL.ColorClasses.Red

            //});
            //pp.Column0.Add(new WidgetData
            //{
            //    ControlName = "UC4.ascx",
            //    Color = "widget " + BLL.ColorClasses.White

            //});
            //pp.Column0.Add(new WidgetData
            //{
            //    ControlName = "UC5.ascx",
            //    Color = "widget " + BLL.ColorClasses.Yellow
            //});
            #endregion
            this.rptcln.DataSource = new int[] { 0, 1, 2 };
            this.rptcln.DataBind();

        }

        protected void rptcln_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            int i = Convert.ToInt32(e.Item.DataItem);
            var rpt = e.Item.FindControl("rptwidget") as Repeater;
            rpt.DataSource = pp[i];
            rpt.DataBind();
        }
        protected void rptwidget_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var data = e.Item.DataItem as WidgetData;
            var ctr = e.Item.FindControl("Widget1") as Widget;
            ctr.DataSource = data;
        }
    }
}
