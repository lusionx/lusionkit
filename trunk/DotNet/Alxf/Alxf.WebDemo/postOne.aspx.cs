using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using Alxf.Frame.Page;

namespace Alxf.WebDemo
{
    public partial class postOne : AjaxPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void RequestGET()
        {
            base.RequestGET();
        }

        protected override void RequestPOST()
        {
            string ss = null;
            ss = this.PostValueString(this.txtName);
            ss = this.PostValueString(this.txtPwd);
            ss = this.PostValueString(this.txtRemark);
            ss = this.PostValueString(ddlAge);
            ss = this.PostValueString(hf);
            ss = this.PostValueString(rbl);

            bool bl = false;
            bl = this.PostValueBoolean(cb1);
            bl = this.PostValueBoolean(cb2);
            bl = this.PostValueBoolean(rb1);
            bl = this.PostValueBoolean(rb2);

            IList<string> lstr = null;
            lstr = this.PostValueList(this.CheckBoxList1);
            lstr = this.PostValueList(this.ListBox1);
        }
    }
}
