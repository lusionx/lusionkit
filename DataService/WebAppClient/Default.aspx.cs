using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Services.Client;
using System.Linq;
using System.Net;

namespace WebAppClient
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Uri uri = new Uri("http://localhost:1098/WebDataT.svc/");
            var db = new Service.KjptDB(uri);
            db.Credentials = new NetworkCredential("hack", "1234");
            this.GridView1.DataSource = db.Persons.Where(a => a.Name.StartsWith("郭") && a.Hospital.Contains("市")).Take(20);
            //this.GridView1.DataSource=db.com_persons.
            this.GridView1.DataBind();
        }
    }
}
