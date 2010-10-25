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
    public partial class Report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ddl_mid.Items.AddRange(Serial.GetList(SerialType.MachineID, false));
                WorkStep ws;
                foreach (var x in Enum.GetValues(typeof(WorkStep)))
                {
                    ws = (WorkStep)x;
                    this.ddl_step.Items.Add(new ListItem(ws.ToString(), ((int)ws).ToString()));
                }
                for (int i = 1; i < 13; i++)
                {
                    this.ddl_month.Items.Add(new ListItem(i.ToString()));
                }
            }
        }

        protected void btn_down_Click(object sender, EventArgs e)
        {
            var filepath = XLS(this.ddl_mid.SelectedValue.Char(),
                (WorkStep)ddl_step.SelectedValue.ToInt32(),
                new DateTime(ddl_year.SelectedValue.ToInt32(), ddl_month.SelectedValue.ToInt32(), 1));
            Response.ContentType = "application/vnd.ms-excel;charset=utf-8";
            Response.AppendHeader("Content-Disposition", "attachment;filename=data.xls");
            Response.WriteFile(filepath);
            Response.Flush();
            Response.End();
        }

        protected string XLS(char mid, WorkStep ws, DateTime dt)
        {
            var filepath = Server.MapPath("~/TempFile/" + Guid.NewGuid().ToString() + ".xls");
            var db = BAL.DB.Current;
            var q = from x in db.sm_biz_Workpiece
                    join s in db.sm_biz_State on x.State equals s.ID
                    where x.MachineID == mid && x.Date >= dt && x.Date < dt.AddMonths(1) &&
                    (x.State == 0 || x.State / 10 == (int)ws)
                    select new { Workpiece = x, State = s };
            var data = q.ToList();

            using (IExcel xls = new WpsExcel())
            {
                xls.ChangeCurrentSheet(2);
                int i = 2;
                foreach (var e in data)
                {
                    xls.SetValue("A" + i.ToString(), e.Workpiece.FullSerial);
                    xls.SetValue("B" + i.ToString(), e.State.Name);
                    xls.SetValue("C" + i.ToString(), ((WorkStep)e.State.Group).ToString());
                    xls.SetValue("D" + i.ToString(), e.State.ID);
                    i++;
                }
                xls.ChangeCurrentSheet(1);
                var q1 = (from x in data
                          where x.State.ID != 0
                          group x by x.Workpiece.Date into g
                          select new { Key = g.Key, Count = g.Count() }).ToList();
                xls.SetValue("A1", "时间");
                xls.SetValue("B1", "不良数");

                i = 3;
                var sts = db.sm_biz_State.Where(x => x.Group == (int)ws).ToList();
                foreach (var e in sts)
                {
                    xls.SetValue(1, i, e.Name);
                    i++;
                }

                i = 2;

                int cc = 0;
                foreach (var e in q1)
                {
                    xls.SetValue("A" + i.ToString(), e.Key.ToString("MM月dd日"));
                    xls.SetValue("B" + i.ToString(), e.Count);
                    for (int j = 0; j < sts.Count; j++)
                    {
                        cc = (from x in db.sm_biz_Workpiece
                              where x.Date == e.Key && x.State == sts[j].ID
                              select x).Count();
                        xls.SetValue(i, j + 3, cc);
                    }
                    i++;
                }
                var cht = new GoogleApi.Chart(new GoogleApi.ChartType.Bar(GoogleApi.ChartType.BarType.bvs));
                cht.Size.Height = 200;
                cht.Size.Width = 500;

                //cht.Data.Group.Add(new List<float>(q1.Select(x => (float)x.Key.Day)));
                cht.Data.Group.Add(new List<float>(q1.Select(x => (float)x.Count)));
                //cht.Fix.Items.Add(q1.Select(x => x.Count).Min());
                cht.Fix.Items.Add(0);
                cht.Fix.Items.Add(q1.Select(x => x.Count).Max());
                cht.Axis.AxisX.AddRange(q1.Select(x => x.Key.Day.ToString()));
                for (var j = 0; j <= q1.Select(x => x.Count).Max(); j++)
                {
                    cht.Axis.AxisY.Add(j.ToString());
                }
                //cht.Axis.AxisY.Add(.ToString());
                i += 2;                
                //xls.InsertPicture("A" + i.ToString(), cht.Download(Server.MapPath("~/TempFile/")));
                xls.SaveAs(filepath);
            }
            return filepath;
        }
    }
}
