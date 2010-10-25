using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace SLWeb.Test
{
    /// <summary>
    /// WebService1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }


        [WebMethod]
        public string HelloWorld2(string s1)
        {
            return "Hello World " + s1;
        }

        [WebMethod]
        public string HelloWorld22(string s1, string s2)
        {
            return "Hello World " + s1 + " " + s2;
        }

        [WebMethod]
        public string HelloWorld3(People pp)
        {
            return pp.Name1 + pp.Name2 + pp.Age.ToString("D2");
        }

        [WebMethod]
        public string HelloWorld33(int[] ints)
        {
            string str = string.Empty;
            foreach (int i in ints)
            {
                str += i.ToString("D4");
            }
            return str;
        }

        [WebMethod]
        public People[] GetPs()
        {
            var ss = new List<People>();
            ss.Add(new People { Name1 = "aaa1", Name2 = "bbb1", Age = 11 });
            ss.Add(new People { Name1 = "aaa2", Name2 = "bbb2", Age = 12 });
            ss.Add(new People { Name1 = "aaa3", Name2 = "bbb3", Age = 13 });
            return ss.ToArray();
        }


        [WebMethod]
        public People GetP()
        {
            return new People { Name1 = "aaa33", Name2 = "bbb33", Age = 88 };
        }
    }
}
