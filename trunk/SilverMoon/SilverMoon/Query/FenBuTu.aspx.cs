using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SilverMoon.BAL;
using Tool;

namespace SilverMoon.Query
{
    public partial class FenBuTu : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var date = Request.QueryString["time"];
            Query.Data.Query q = HttpContext.Current.Session["TQuery"] as Query.Data.Query;
            if (date != null && q != null)
            {
                this.rpt_d.DataSource = DB.Current.sm_biz_Workpiece
                    .Where(x => x.MachineID == q.MachineID
                        && x.Type == q.Type
                        && x.Date == date.ToDateTime()
                        && x.Shift == 'D');
                this.rpt_n.DataSource = DB.Current.sm_biz_Workpiece
                    .Where(x => x.MachineID == q.MachineID
                        && x.Type == q.Type
                        && x.Date == date.ToDateTime()
                        && x.Shift == 'N');
                this.rpt_n.DataBind();
                this.rpt_d.DataBind();
            }
            else
            {
                this.Label1.Visible = true;
            }
        }

        protected string GetStateCss(object obj)
        {
            int st = Convert.ToInt32(obj);
           return "st"+ DB.Current.sm_biz_State.SingleOrDefault(x => x.ID == st).Group.ToString();           
        }
    }
}
