using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using SilverMoon.BAL;
using System.Data.SqlClient;
using Tool;

namespace SilverMoon.Admin
{
    public partial class App : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_clr_Click(object sender, EventArgs e)
        {
            var dir = new System.IO.DirectoryInfo(Server.MapPath("~/TempFile/"));
            foreach (var f in dir.GetFiles())
            {
                f.Delete();
            }
        }

        protected void btn_bak_Click(object sender, EventArgs e)
        {
            string sql = "BACKUP DATABASE [{0}] TO DISK=N'{1}{0}{2}.bak' WITH INIT".FormatWith("SilverMoon",
                System.Configuration.ConfigurationManager.AppSettings["BakDir"],
                DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss"));

            try
            {
                DB.Current.ExecuteCommand(sql);
                Alert("备份成功");
            }
            catch (System.Exception)
            {
            }
        }
    }
}
