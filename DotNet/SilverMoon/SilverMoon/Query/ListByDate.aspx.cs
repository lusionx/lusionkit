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
    public partial class ListByDate : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBind();
            }
        }

        public override void DataBind()
        {

            var arg = Request.QueryString["time"];
            if (arg == null)
            {
                return;
            }
            else
            {
                var time = arg.ToDateTime();
                var db = BAL.DB.Current;
                this.Repeater1.DataSource = db.sm_biz_Workpiece.Where(x => x.Date == time);
                this.Repeater1.DataBind();
            }
        }
    }
}
