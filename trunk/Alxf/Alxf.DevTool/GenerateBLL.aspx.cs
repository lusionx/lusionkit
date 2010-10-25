using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;

namespace Alxf.DevTool
{
    public partial class GenerateBLL : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            this.btn_submit.Click += new EventHandler(btn_submit_Click);
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            if (this.fileup.HasFile)
            {
                var stream = fileup.PostedFile.InputStream;
                byte[] bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                string type = txt_typpe.Text.Trim();
                var ass = Assembly.Load(bytes);

                var obj = ass.CreateInstance(type);

                var pros = obj.GetType().GetProperties();

                System.Text.StringBuilder sb = new System.Text.StringBuilder("public partial class " +
                    type.Substring(type.LastIndexOf('.') + 1) + "Model:IB2D");
                sb.AppendFormat("&lt;{0}&gt;{{<br/>", type);
                sb.Append("#region Generate<br/>");
                sb.AppendFormat(@"
public {0}Model()<br/>
        {{<br/>
            Alxf.Frame.DataBase.DataModel.SetDefault(this);<br/>
        }}<br/>", type.Substring(type.LastIndexOf('.') + 1));

                var formatstr = "public {0} {1} {{get;set;}}<br/>";

                foreach (var o in pros)
                {
                    var attrs = o.GetCustomAttributes(typeof(System.Data.Linq.Mapping.ColumnAttribute), false);
                    if (true||attrs.Length > 0)
                    {
                        sb.AppendFormat(formatstr, o.PropertyType.FullName, o.Name);
                    }
                }

                sb.AppendFormat(@" public {0} DataModel<br/>
        {{<br/>
            get<br/>
            {{<br/>
                var obj = new {0}();<br/>", type);

                formatstr = "obj.{0} = this.{0};<br/>";
                foreach (var o in pros)
                {
                    var attrs = o.GetCustomAttributes(typeof(System.Data.Linq.Mapping.ColumnAttribute), false);
                    if (true || attrs.Length > 0)
                    {
                        sb.AppendFormat(formatstr, o.Name);
                    }
                }

                sb.Append(@" return obj;<br/>
}<br/>
            set<br/>
            {<br/>");

                formatstr = "this.{0} = value.{0};<br/>";
                foreach (var o in pros)
                {
                    var attrs = o.GetCustomAttributes(typeof(System.Data.Linq.Mapping.ColumnAttribute), false);
                    if (true || attrs.Length > 0)
                    {
                        sb.AppendFormat(formatstr, o.Name);
                    }
                }

                sb.Append("}}<br/>");

                formatstr = "partial void Validate{0}();<br/>";
                foreach (var o in pros)
                {
                    var attrs = o.GetCustomAttributes(typeof(System.Data.Linq.Mapping.ColumnAttribute), false);
                    if (true || attrs.Length > 0)
                    {
                        sb.AppendFormat(formatstr, o.Name);
                    }
                }

                sb.Append("#endregion<br/>}");

                this.lt_cs.Text = sb.ToString();
            }
        }
    }
}