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
    public partial class Data : PageBase
    {
        private WorkStep ws;

        protected void Page_Load(object sender, EventArgs e)
        {

            foreach (var x in Enum.GetValues(typeof(WorkStep)))
            {
                if ((int)x == Request.QueryString["type"].ToInt32())
                {
                    ws = (WorkStep)x;
                    break;
                }
            }

            if (!IsPostBack)
            {
                this.ddl_mid.Items.AddRange(Serial.GetList(SerialType.MachineID, false));
                this.ddl_type.Items.AddRange(Serial.GetList(SerialType.Type, false));
                this.ddl_shift.Items.AddRange(Serial.GetList(SerialType.Shift, true));
                this.AspNetPager1.PageSize = System.Configuration.ConfigurationManager.AppSettings["PageSize"].ToInt32();
            }
        }



        public override void DataBind()
        {
            var db = DB.Current;
            var cpin = from x in db.sm_biz_Workpiece
                       join s in db.sm_biz_State on x.State equals s.ID
                       where s.Group == (int)ws
                       select x;
            var zongsh = from x in db.sm_biz_Workpiece
                         where x.State == 0 || x.State > ((int)ws) * 10
                         select x;
            var condition = new Query();
            condition.Step = ws;
            condition.TimeFrom = SafeConvert.ToDateTime(this.txt_tf.Text, DateTime.Parse("2000-1-1"));
            condition.TimeTo = SafeConvert.ToDateTime(this.txt_tt.Text, DateTime.Parse("2050-1-1"));
            cpin = cpin.Where(x => x.Date >= condition.TimeFrom && x.Date <= condition.TimeTo);
            zongsh = zongsh.Where(x => x.Date >= condition.TimeFrom && x.Date <= condition.TimeTo);
            if (ddl_mid.SelectedValue != string.Empty)
            {
                condition.MachineID = ddl_mid.SelectedValue.Char();
                cpin = cpin.Where(x => x.MachineID == condition.MachineID);
                zongsh = zongsh.Where(x => x.MachineID == condition.MachineID);
            }
            if (ddl_type.SelectedValue != string.Empty)
            {
                condition.Type = ddl_type.SelectedValue.Char();
                cpin = cpin.Where(x => x.Type == condition.Type);
                zongsh = zongsh.Where(x => x.Type == condition.Type);
            }
            if (ddl_shift.SelectedValue != string.Empty)
            {
                condition.Shift = ddl_shift.SelectedValue.Char();
                cpin = cpin.Where(x => x.Shift == condition.Shift);
                zongsh = zongsh.Where(x => x.Shift == condition.Shift);
            }
            var cpinc = from x in cpin
                        group x by x.Date into g
                        select new Pices { Date = g.Key, Count = g.Count() };
            var zongshc = from x in zongsh
                          group x by x.Date into g
                          select new Pices { Date = g.Key, Count = g.Count() };
            var source = from y in zongshc.ToList()
                         join x in cpinc.ToList() on y.Date equals x.Date into pcs
                         from z in pcs.DefaultIfEmpty(new Pices { Date = DateTime.Now, Count = 0 })
                         select new AClass { Date = y.Date, ZongShu = y.Count, Good = y.Count - z.Count, Bad = z.Count };
            this.AspNetPager1.RecordCount = source.Count();
            var paged = source.Skip(AspNetPager1.StartRecordIndex - 1).Take(AspNetPager1.PageSize);
            this.Repeater1.DataSource = Session["Tmp_data"] = paged;
            this.Repeater1.DataBind();
            Session["TQuery"] = condition;
        }

        internal class AClass
        {
            public DateTime Date { set; get; }
            public int ZongShu { set; get; }
            public int Good { set; get; }
            public int Bad { set; get; }
        }

        internal class Pices
        {
            public DateTime Date { set; get; }
            public int Count { set; get; }
        }

        internal class Query
        {
            public char MachineID { get; set; }
            public char Type { get; set; }
            public char Shift { get; set; }
            public DateTime TimeFrom { get; set; }
            public DateTime TimeTo { get; set; }
            public WorkStep Step { get; set; }
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            DataBind();
        }

        protected void btn_query_Click(object sender, EventArgs e)
        {
            DataBind();
        }

        protected void btn_Click(object sender, EventArgs e)
        {
            var data = Session["Tmp_data"] as IEnumerable<AClass>;
            if (data == null)
            {
                return;
            }
            var filepath = Server.MapPath("~/TempFile/" + Guid.NewGuid().ToString() + ".xls");
            using (IExcel xlc = new WpsExcel())
            {
                int i = 2;
                xlc.SetValue("A1", "日期");
                xlc.SetValue("B1", "生产总数");
                xlc.SetValue("C1", "良品数");
                xlc.SetValue("D1", "不良品数");
                xlc.SetValue("E1", "不良率");
                foreach (var x in data)
                {
                    xlc.SetValue("A" + i.ToString(), x.Date.ToString("MM-dd"));
                    xlc.SetValue("B" + i.ToString(), x.ZongShu);
                    xlc.SetValue("C" + i.ToString(), x.Good);
                    xlc.SetValue("D" + i.ToString(), x.Bad);
                    xlc.SetValue("E" + i.ToString(), (((float)x.Bad * 100 / (float)x.ZongShu)).ToString("f2") + "%");

                    i++;
                }
                xlc.SaveAs(filepath);
            }
            Response.ContentType = "application/vnd.ms-excel;charset=utf-8";
            Response.AppendHeader("Content-Disposition", "attachment;filename=data.xls");
            Response.WriteFile(filepath);
            Response.Flush();
            Response.End();
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                var ier = (sender as Repeater).DataSource as IEnumerable<AClass>;
                if (ier.Count() > 0)
                {
                    (e.Item.FindControl("lb_1") as Label).Text = ier.Sum(x => x.ZongShu).ToString();
                    (e.Item.FindControl("lb_2") as Label).Text = ier.Sum(x => x.Good).ToString();
                    (e.Item.FindControl("lb_3") as Label).Text = ier.Sum(x => x.Bad).ToString();
                    (e.Item.FindControl("lb_4") as Label).Text = (((float)ier.Sum(x => x.Bad) * 100 / (float)ier.Sum(x => x.ZongShu))).ToString("f2") + "%";
                    e.Item.FindControl("hlk_1").Visible = true;
                }
                else
                {
                    
                }

            }
            else if (e.Item.ItemType == ListItemType.Header)
            {
                (e.Item.FindControl("lb_1") as Label).Text = this.ddl_shift.SelectedItem.Text;
            }
        }
    }
}
