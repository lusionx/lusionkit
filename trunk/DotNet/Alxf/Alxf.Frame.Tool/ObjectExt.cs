using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alxf.Frame.Tool
{
    public static class ObjectExt
    {
        public static string ToJson(this Object obj)
        {
            System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            return jss.Serialize(obj);
        }
    }
}
