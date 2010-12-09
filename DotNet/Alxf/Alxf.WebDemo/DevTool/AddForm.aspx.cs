using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using Alxf.BLL.Demo;
using Alxf.Frame.Tool;
using Alxf.Frame.ModelBase;

namespace Alxf.WebDemo.DevTool
{
    public partial class AddForm : System.Web.UI.Page
    {
        private const string XName = "AddForm";

        protected void Page_Load(object sender, EventArgs e)
        {
            this.btn_submit.Click += new EventHandler(btn_submit_Click);
            this.btn_set.Click += new EventHandler(btn_set_Click);
            if (!IsPostBack)
            {

            }
        }

        void btn_set_Click(object sender, EventArgs e)
        {
            var filepath = Server.MapPath("~/TmpFile/") + Guid.NewGuid().ToString() + ".dll";
            fileup.SaveAs(filepath);

            var ass = Assembly.LoadFile(filepath);
            string[] type = txt_typpe.Text.Split(',');
            var obj = ass.CreateInstance(type[0]);
            Session.Add("type", obj);
            var q = from a in obj.GetType().GetProperties()
                    select new { a.Name, a.PropertyType };
            this.rpt_pname.DataSource = q;
            this.rpt_pname.DataBind();
        }

        void btn_submit_Click(object sender, EventArgs e)
        {
            DataBind();
        }

        public override void DataBind()
        {
            #region 收集信息
            var obj = Session["type"];
            var q1 = from a in obj.GetType().GetProperties()
                     select new
                     {
                         a.Name,
                         a.PropertyType
                     };

            var q2 = from a in this.rpt_pname.Items.Cast<RepeaterItem>()
                     select new
                     {
                         name = (a.FindControl("hf_name") as HiddenField).Value,
                         ctype = (a.FindControl("rbl_ctype") as RadioButtonList).SelectedValue
                     };

            var q = from a in q1
                    join b in q2 on a.Name equals b.name
                    select new
                    {
                        a.Name,
                        a.PropertyType,
                        b.ctype
                    };

            #endregion

            var myroot = DevTemplate.Root.Descendants(XName).Single();

            #region 生成aspx片段

            System.Text.StringBuilder sb = new System.Text.StringBuilder(myroot.Descendants("head").Single().Value);
            var item = myroot.Descendants("item").Single().Value;
            var dic = ValidateJs(obj);

            string js = string.Empty;
            foreach (var o in q)
            {
                if (dic.ContainsKey(o.Name))
                {
                    js = "alxjs=\"" + dic[o.Name] + "\"";
                }
                else
                {
                    js = string.Empty;
                }
                sb.AppendFormat(item, o.Name, o.ctype, js);
            }
            sb.Append(myroot.Descendants("foot").Single().Value);
            this.lt_aspx.Text = Server.HtmlEncode(sb.ToString());

            #endregion

            string _cModel = @"
public {0} Model<br/>
        {{<br/>
            get<br/>
            {{<br/>
                var obj = new {0}();<br/>";

            sb = new System.Text.StringBuilder();
            sb.AppendFormat(_cModel, obj.GetType().ToString());

            #region getmodel

            item = @"<br/>
obj.{0} = this.af_{0}.Value.ToString()";

            foreach (var o in q)
            {
                sb.AppendFormat(item, o.Name);
                if (o.PropertyType == typeof(Int32))
                {
                    sb.Append(".ToInt32();");
                }
                else if (o.PropertyType == typeof(DateTime))
                {
                    sb.Append(".ToDateTime();");
                }
                else if (o.PropertyType == typeof(Guid))
                {
                    sb.Append(".ToGuid();");
                }
                else
                {
                    sb.Append(";");
                }

            }
            sb.Append(@"<br/>
return obj;}");

            #endregion

            #region setmodel
            sb.Append(@"<br/>set<br/>
{");
            item = "<br/>this.af_{0}.Value = value.{0}.ToString();";

            foreach (var o in q)
            {
                sb.AppendFormat(item, o.Name);
            }

            sb.Append(@"<br/>
}}");

            #endregion

            this.lt_cs.Text = sb.ToString();

        }


        protected Dictionary<string, string> ValidateJs(object obj)
        {
            var attrs = from a in obj.GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                        where a.Name.StartsWith("On") && a.Name.EndsWith("Changed")
                        select new
                        {
                            Name = a.Name,
                            Attr = a.GetCustomAttributes(true)
                        };

            Dictionary<string, string> dic = new Dictionary<string, string>();
            string proname = string.Empty;
            foreach (var a in attrs)
            {
                ValidateAttribute b = a.Attr.FirstOrDefault(c => c is ValidateAttribute) as ValidateAttribute;
                if (b != null)
                {
                    proname = a.Name.Substring(2).Replace("Changed", "");
                    //var jsobj = ;
                    dic.Add(proname, (new { b.Default, b.ErrorMessage, b.IsRequired, b.Function }).ToJson().Replace("\"", "'"));
                }
            }
            return dic;
        }

    }
}
