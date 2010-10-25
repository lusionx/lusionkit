using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alxf.Frame.WebControl
{
    public class DropDownList : System.Web.UI.WebControls.DropDownList, IValueable
    {

        #region IValueable Members

        public string Value
        {
            get
            {
                return base.SelectedValue;
            }
            set
            {
                base.SelectedValue = value;
            }
        }

        #endregion
    }
}
