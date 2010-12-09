using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tool;
using SilverMoon.BAL;

namespace SilverMoon.Test
{
    public partial class wsp : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //using (Tool.MsExcel xls = new MsExcel())
            //{
            //    xls.SetValue("B2", 123);
            //    xls.ChangeCurrentSheet(2);
            //    xls.SheetName = "xxx2";
            //    xls.SetValue("B2", 2123);
            //    xls.SaveAs(Server.MapPath("aa.xls"));
            //}
            //this.Label1.Text = DateTime.Now.ToString();
            //int i = BAL.WorkStep.DC.ToInt32
            var db = DB.Current;
            var q = from a in db.sm_biz_Workpiece
                    join b in db.sm_biz_State on a.State equals b.ID
                    select new { a.FullSerial, a.Remark, b.Name };

            this.CreateXls(q, "xxx.xls");
        }
    }
}
