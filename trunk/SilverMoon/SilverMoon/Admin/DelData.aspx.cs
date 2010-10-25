using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SilverMoon.BAL;

namespace SilverMoon.Admin
{
    public partial class DelData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ddl_shift.Items.AddRange(Serial.GetList(SerialType.Shift, false));
            }
        }

        protected void btn_de_Click(object sender, EventArgs e)
        {
            var timeFrom = Convert.ToDateTime(this.txt_tf.Text);//2010-4-1 00:00:00
            var timeTo = Convert.ToDateTime(this.txt_tt.Text).AddDays(1).AddSeconds(-1);//2010-4-1 23:59:59
            var shift = ddl_shift.SelectedValue;

            var db = DB.Current;
            var q = from a in db.sm_biz_Workpiece

                    where a.Shift == shift.ToArray()[0] &&

                    a.Date >= timeFrom &&

                    a.Date <= timeTo

                    select a;
            var cc = q.Count();

            if (cc > 0)
            {
                var sql = @"
delete from sm_biz_Workpiece
where shift = '{0}' and date >= '{1}' and date <= '{2}'";
                cc = db.ExecuteCommand(string.Format(sql, shift, timeFrom, timeTo));
                DB.Reset();
            }
        }
    }
}