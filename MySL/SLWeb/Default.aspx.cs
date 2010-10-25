using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SLWeb
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string str = LangResourceManager.Current.GetString("Welcome");
            //string ss = Response.Cookies.Get("qwewr").Value;
            //CookieHelper.Insert("qwewr", "aaaa");

            Response.Write(str);
        }
    }
}
