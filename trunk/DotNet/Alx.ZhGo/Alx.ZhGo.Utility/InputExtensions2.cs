using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Web.Mvc.Html
{
    public static class InputExtensions2
    {
        public static MvcHtmlString Submit(this HtmlHelper htmlHelper, string value)
        {
            return MvcHtmlString.Create("<input type=\"submit\" value=\"" + value + "\" />");
        }
    }
}
