using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SilverMoon.BAL;
using Tool;
using SilverMoon.DAL;

namespace SilverMoon.Query
{
    public partial class QueryAll : PageBase
    {
        private WorkStep ws;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ddl_mid.Items.AddRange(Serial.GetList(SerialType.MachineID, true));
                this.ddl_type.Items.AddRange(Serial.GetList(SerialType.Type, true));
                this.ddl_shift.Items.AddRange(Serial.GetList(SerialType.Shift, true));
                this.AspNetPager1.PageSize = System.Configuration.ConfigurationManager.AppSettings["PageSize"].ToInt32();
                foreach (var x in Enum.GetValues(typeof(WorkStep)))
                {
                    ws = (WorkStep)x;
                    this.ddl_step.Items.Add(new ListItem(ws.ToString(), ((int)ws).ToString()));
                }
                this.ddl_step.Items.Insert(0, new ListItem("All", "9999"));
                ddl_step_SelectedIndexChanged(null, null);
            }
        }

        public override void DataBind()
        {
            ws = (WorkStep)this.ddl_step.SelectedValue.ToInt32();
            var db = DB.Current;
            int st = (int)ws;
            IQueryable<sm_biz_Workpiece> q;
            if (this.ddl_step.SelectedValue == "9999")
            {
                q = db.sm_biz_Workpiece.Where(x => x.State != 0);
            }
            else
            {
                if (this.ddl_st.SelectedValue == "9999")
                {
                    q = DB.Current.sm_biz_Workpiece.Where(x => x.State > st * 10 && x.State < st * 10 + 100);
                }
                else
                {
                    q = DB.Current.sm_biz_Workpiece.Where(x => x.State == this.ddl_st.SelectedValue.ToInt32());
                }
            }
            if (this.ddl_mid.SelectedValue != string.Empty)
            {
                q = q.Where(aa => aa.MachineID == this.ddl_mid.SelectedValue.Char());
            }
            if (this.ddl_type.SelectedValue != string.Empty)
            {
                q = q.Where(aa => aa.Type == this.ddl_type.SelectedValue.Char());
            }
            if (this.ddl_shift.SelectedValue != string.Empty)
            {
                q = q.Where(aa => aa.Shift == this.ddl_shift.SelectedValue.Char());
            }
            if (this.txt_tf.Text.Trim() != string.Empty)
            {
                q = q.Where(aa => aa.Date >= Convert.ToDateTime(this.txt_tf.Text));
            }
            if (this.txt_tt.Text.Trim() != string.Empty)
            {
                q = q.Where(aa => aa.Date <= Convert.ToDateTime(this.txt_tt.Text));
            }
            this.AspNetPager1.RecordCount = q.Count();
            this.lb_badt.Text = this.AspNetPager1.RecordCount.ToString();
            this.Repeater1.DataSource = q.Skip(AspNetPager1.StartRecordIndex - 1).Take(AspNetPager1.PageSize);
            this.Repeater1.DataBind();
        }



        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            DataBind();
        }

        protected void btn_query_Click(object sender, EventArgs e)
        {
            DataBind();
        }

        protected void ddl_step_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ws = this.ddl_step.SelectedValue.ToInt32();
            this.ddl_st.Items.Clear();
            if (ws == 9999)
            {

            }
            else
            {
                this.ddl_st.Items.Clear();
                this.ddl_st.Items.AddRange((
                    from x in DB.Current.sm_biz_State
                    where x.Group == ws
                    select new ListItem(x.Name, x.ID.ToString())).ToArray());
            }
            this.ddl_st.Items.Insert(0, new ListItem("All", "9999"));

        }
    }
}
