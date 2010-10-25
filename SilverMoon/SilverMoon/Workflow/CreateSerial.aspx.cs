using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SilverMoon.BAL;
using Tool;

namespace SilverMoon.Workflow
{
    public partial class CreateSerial : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.btn_del.Visible = false;
            if (!IsPostBack)
            {
                this.AspNetPager1.PageSize = System.Configuration.ConfigurationManager.AppSettings["PageSize"].ToInt32();

                this.ddl_mid.Items.AddRange(Serial.GetList(SerialType.MachineID, false));
                this.ddl_type.Items.AddRange(Serial.GetList(SerialType.Type, false));
                this.ddl_month.Items.AddRange(Serial.GetList(SerialType.Month, false));
                this.ddl_month.SelectedValue = DateTime.Now.Month.ToMonth();
                //int i = 3;
                //this.ddl_month.SelectedValue = i.ToMonth();
                this.dd_date.Items.AddRange(Serial.GetList(SerialType.Day, false));
                this.dd_date.SelectedValue = DateTime.Now.Day.ToD2();
                this.ddl_shift.Items.AddRange(Serial.GetList(SerialType.Shift, false));

                Serial.BindYear(ddl_year);
            }
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            ShowData();
        }

        protected bool CheckNoF(string str)
        {
            int i;
            if (int.TryParse(str, out i) && i > -1)
            {
                return true;
            }
            return false;
        }
        protected bool CheckNoT(string str)
        {
            int i;
            if (int.TryParse(str, out i) && i < 1000 && i > -1)
            {
                return true;
            }
            return false;
        }

        protected void btn_create_Click(object sender, EventArgs e)
        {
            var piece = new List<DAL.sm_biz_Workpiece>();

            int nof;// = Tool.SafeConvert.ToInt32(this.txt_nof.Text, 1);
            int not;// = Tool.SafeConvert.ToInt32(this.txt_not.Text, 1000) + 1;
            if (CheckNoT(this.txt_nof.Text))
            {
                nof = int.Parse(this.txt_nof.Text);
            }
            else
            {
                Alert("最小值为0");
                return;
            }

            if (CheckNoT(this.txt_not.Text))
            {
                not = int.Parse(this.txt_not.Text);
            }
            else
            {
                Alert("最大值为999");
                return;
            }
            DAL.sm_biz_Workpiece p;
            string pre = ddl_mid.SelectedValue
                + ddl_type.SelectedValue
                + ddl_month.SelectedValue
                + dd_date.SelectedValue
                + ddl_shift.SelectedValue;
            for (int i = nof; i <= not; i++)
            {
                try
                {
                    p = DB.Current.CreateWorkpiece(pre + i.ToString("d3"));
                    p.State = 1000;
                    p.Remark = string.Empty;
                    piece.Add(p);
                }
                catch (ArgumentOutOfRangeException)
                {
                    Alert("时间取值非法");
                    return;
                }
            }
            this.lt_sum.Text = string.Format("共生成{0}个", not - nof + 1);
            Session["tmp_create"] = piece;
            this.AspNetPager1.RecordCount = piece.Count;
            ShowData();
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            var data = (List<DAL.sm_biz_Workpiece>)Session["tmp_create"];
            if (data != null)
            {
                var db = DB.Current;
                foreach (var x in data)
                {
                    if (db.GetWorkpiece(x.FullSerial) == null)
                    {
                        x.State = 0;
                        db.sm_biz_Workpiece.InsertOnSubmit(x);
                    }
                    else
                    {
                        x.State = 1001;
                    }
                }
                db.SubmitChanges();
                Session["tmp_create"] = data;
                this.btn_del.Visible = true;
                ShowData();
            }
            else
            {
                JQueryReady("alert('请先生成再保存');");
            }
        }

        protected void btn_del_Click(object sender, EventArgs e)
        {
            var data = (List<DAL.sm_biz_Workpiece>)Session["tmp_create"];
            DAL.sm_biz_Workpiece obj;
            var db = DB.Current;
            foreach (var x in data)
            {
                obj = db.GetWorkpiece(x.FullSerial);
                if (obj != null)
                {
                    db.sm_biz_Workpiece.DeleteOnSubmit(obj);
                    x.State = 1000;
                }
            }
            db.SubmitChanges();

            Session["tmp_create"] = data;
            ShowData();
        }


        protected void ShowData()
        {
            var data = (List<DAL.sm_biz_Workpiece>)Session["tmp_create"];
            if (data != null)
            {
                this.Repeater1.DataSource = data.Skip(AspNetPager1.StartRecordIndex - 1).Take(AspNetPager1.PageSize);
                this.Repeater1.DataBind();
            }
        }

    }
}
