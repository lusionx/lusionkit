using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SilverMoon.BAL;
using Tool;

namespace SilverMoon.Controls
{
    public partial class Query : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ddl_mid.Items.AddRange(Serial.GetList(SerialType.MachineID, false));
                this.ddl_type.Items.AddRange(Serial.GetList(SerialType.Type, false));
                //this.ddl_month.Items.AddRange(Serial.GetList(SerialType.Month, false));
                //this.ddl_date.Items.AddRange(Serial.GetList(SerialType.Day, false));
                this.ddl_shift.Items.AddRange(Serial.GetList(SerialType.Shift, false));
                this.txt_data.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        public IQueryable<DAL.sm_biz_Workpiece> Data
        {
            get
            {
                int nof = Tool.SafeConvert.ToInt32(this.txt_nof.Text, 0) - 1;
                int not = Tool.SafeConvert.ToInt32(this.txt_not.Text, 1000) + 1;
                DateTime dt = this.txt_data.Text.ToDateTime();
                var tmp = DB.Current.CreateWorkpiece(ddl_mid.SelectedValue
                    + ddl_type.SelectedValue
                    + ((char)(dt.Month-1+'A')).ToString()
                    + dt.Day.ToD2()
                    + ddl_shift.SelectedValue + "000");

                var q = DB.Current.sm_biz_Workpiece.Where(x =>
                    x.MachineID == tmp.MachineID &&
                    x.Type == tmp.Type &&
                    //x.Month == tmp.Month &&
                    x.Date == tmp.Date &&
                    x.Shift == tmp.Shift //&&
                    //nof < x.Serial &&
                    //x.Serial < not
                    );
                return q;
            }
        }

        protected void btn_query_Click(object sender, EventArgs e)
        {
            int nof = Tool.SafeConvert.ToInt32(this.txt_nof.Text, 0);
            int not = Tool.SafeConvert.ToInt32(this.txt_not.Text, 1000);
            this.txt_nof.Text = nof.ToString();
            this.txt_not.Text = not.ToString();
            this.Page.DataBind();
        }
    }
}