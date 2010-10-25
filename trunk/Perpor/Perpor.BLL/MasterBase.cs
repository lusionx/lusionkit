using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Perpor.BLL
{
    public partial class MasterBase : System.Web.UI.MasterPage
    {
        protected override void OnInit(EventArgs e)
        {
            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(),
                "appPath",
                string.Format("var appPath='{0}';", AppSettings.Get(AppSettings.Keys.SiteRoot)),
                true);
            base.OnInit(e);
        }
        
    }
}
