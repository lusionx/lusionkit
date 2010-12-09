using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tool;
using SilverMoon.BAL;

namespace SilverMoon.Workflow
{
    public partial class wf10 : PageBase
    {
        protected BAL.WorkStep ws = WorkStep.DC;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Serial.BindYear(ddl_year);
                this.AspNetPager1.PageSize = System.Configuration.ConfigurationManager.AppSettings["PageSize"].ToInt32();
                this.ddl_state.Items.AddRange(
                    (from x in DB.Current.sm_biz_State
                     where x.Group == (int)WorkStep.DC || x.ID == 0
                     select new ListItem(x.Name, x.ID.ToString())).ToArray());

            }
        }

        public override void DataBind()
        {
            var q = this.Query1.Data;
            this.AspNetPager1.RecordCount = q.Count();
            this.Repeater1.DataSource = this.Query1.Data.Skip(AspNetPager1.StartRecordIndex - 1).Take(AspNetPager1.PageSize);
            this.Repeater1.DataBind();
            base.DataBind();
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            DataBind();
        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            var db = DB.Current;
            var p = db.GetWorkpiece(this.txt_sno.Text);
            p.State = ddl_state.SelectedValue.ToInt32();
            p.Remark = this.txt_remark.Text;
            db.SubmitChanges();
            DataBind();
            JQueryReady("window.scroll(0, $(window).height());");
        }

        protected bool CanUpdate(object state)
        {

            int stat = (int)state;
            if (stat == 0)
            {
                return true;
            }

            int grp = (int)ws;
            return stat > grp * 10 && stat < grp * 10 + 100;
        }
    }
}
