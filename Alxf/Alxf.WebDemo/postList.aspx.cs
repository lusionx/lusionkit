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
using System.Reflection;
using Alxf.Frame.Page;

namespace Alxf.WebDemo
{
    public partial class postList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var obj = new Tmodel
                {
                    Name = "lxing",
                    Age = 22,
                    Xx = "asdsadsa"
                };
                this.BindModel(obj);
            }

        }

        public class Tmodel
        {
            public string Name { get; set; }
            public string Xx { get; set; }
            public int Age { get; set; }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var mm = this.GetModel<Tmodel>();

            System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();

            Response.Write(jss.Serialize(mm));
        }
    }
}
