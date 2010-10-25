using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alxf.Frame.WebControl
{
    public class TextBox : System.Web.UI.WebControls.TextBox,IValueable
    {
        public string Value
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }
    }
}
