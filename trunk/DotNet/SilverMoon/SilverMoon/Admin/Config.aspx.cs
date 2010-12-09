using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tool;
using SilverMoon.BAL;

namespace SilverMoon.Admin
{
    public partial class Config : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                DataBind();
            }

        }

        public override void DataBind()
        {
            int g = this.ddl_group.SelectedValue.ToInt32();
            var q = DB.Current.sm_biz_State.Where(x => x.Group == g);
            this.Repeater1.DataSource = q;
            this.Repeater1.DataBind();

        }

        protected void ddl_group_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataBind();
        }

        protected void btn_ok_Click(object sender, EventArgs e)
        {
            var db = DB.Current;
            var ss = db.sm_biz_State.Single(x => x.ID == this.txt_id.Text.ToInt32());
            ss.Name = this.txt_newst.Text.Trim();
            db.SubmitChanges();
            DataBind();
        }
    }
}
