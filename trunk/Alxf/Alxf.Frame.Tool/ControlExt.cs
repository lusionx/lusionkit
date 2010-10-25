using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Alxf.Frame.Tool
{
    public static class ControlExt
    {
        public static string RegScriptFile(this Control ctr, string serverpath)
        {
            return string.Format("<script type=\"text/javascript\" src=\"{0}\"></script>", ctr.ResolveClientUrl(serverpath));
        }
        public static string RegCssFile(this Control ctr, string serverpath)
        {
            //return string.Format("<link href=\"{0}\" rel=\"stylesheet\" type=\"text/css\" />", ctr.ResolveClientUrl(serverpath));
            return string.Empty;
        }
        
    }
}
