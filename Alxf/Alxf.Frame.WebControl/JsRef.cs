using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Alxf.Frame.WebControl
{
    [DefaultProperty("Src")]
    [ToolboxData("<{0}:JsRef runat=server></{0}:JsRef>")]
    public class JsRef : System.Web.UI.WebControls.WebControl
    {
        [Bindable(false)]
        [DefaultValue("")]
        public string Src { get; set; }

        [Bindable(false)]
        [DefaultValue(1)]
        public int Order { get; set; }

        protected override void RenderContents(HtmlTextWriter output)
        {
            output.Write(string.Empty);
        }

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            //<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.3/jquery.min.js"></script>
            writer.AddAttribute("type", "text/javascript");

            string src = base.ResolveClientUrl(this.Src);
            writer.AddAttribute("src", src);


        }

        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Script;
            }
        }
    }
}
