using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Perpor.DAL;
using Perpor.BLL;

namespace Perpor.Web.Func
{
    public partial class EditWidget : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var db = DB.Current;
                this.dl_wdg.DataSource = db.core_DevControls;
                this.dl_wdg.DataBind();
            }
        }
    }
}
