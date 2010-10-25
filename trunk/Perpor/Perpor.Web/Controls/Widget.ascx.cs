using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Perpor.DAL;
using Perpor.BLL;
using System.Configuration;

namespace Perpor.Web.Controls
{
    public partial class Widget : System.Web.UI.UserControl
    {
        public string H3 { get; set; }

        public string LiClass { get; set; }

        public string ControlName { get; set; }

        protected Control UserControl { get; set; }

        public WidgetData DataSource
        {
            set
            {
                this.LiClass = value.Color;
                this.ControlName = value.ControlName;
                UserControl = LoadUserControl(this.ControlName);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.content.Controls.Add(UserControl);
        }

        private Control LoadUserControl(string ControlName)
        {
            var db = DB.Current;
            var obj = db.core_DevControls.FirstOrDefault(x => x.FileName == ControlName);
            this.H3 = obj.Title;
            var dir = AppSettings.Get(AppSettings.Keys.DevCtrPath);
            return this.LoadControl(dir + obj.FileName);
        }

    }
}