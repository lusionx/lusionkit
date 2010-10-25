using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace SLWeb
{
    /// <summary>
    /// WebService1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }


        [WebMethod]
        public string HelloMan(string name)
        {
            return "Hello " + name;
        }

        [WebMethod]
        public int[] GetInt()
        {
            return new int[] { 1, 2, 3, 4, 5, 6 };
        }

        [WebMethod]
        public List<Code.People> GetPList()
        {
            List<Code.People> l = new List<Code.People>();
            l.Add(new Code.People { Age = 10, Name = "aaa" });
            l.Add(new Code.People { Age = 20, Name = "bbb" });
            return l;
        }
    }
}
