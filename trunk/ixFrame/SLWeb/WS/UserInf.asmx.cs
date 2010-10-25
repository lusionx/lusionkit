using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.Services;
using SLWeb.Code;

namespace SLWeb.WS
{
    /// <summary>
    /// UserInf 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class UserInf : System.Web.Services.WebService
    {
        public readonly int ActiveMinutes = Convert.ToInt32(ConfigHelper.GetAppSetting("minute"));

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public bool UpdateUser(string userid)
        {
            var users = Code.AppHelper.GetUserList();
            lock (users)
            {
                var uu = from u in users
                         where u.UserID == userid
                         select u;
                if (uu.Count() == 1)
                {
                    uu.Single().LastActive = DateTime.Now;
                }
                else
                {
                    users.Add(new User { UserID = userid, LastActive = DateTime.Now, Leisure = true });
                }
            }
            return true;
        }

        [WebMethod]
        public bool IsOnline(string id)
        {
            var users = Code.AppHelper.GetUserList();
            var uu = from u in users
                     where u.UserID == id
                     select u;
            if (uu.Count() == 1)
            {
                return uu.Single().LastActive > DateTime.Now.AddMinutes(0 - this.ActiveMinutes);
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<Code.User> GetUserList()
        {
            var l = Code.AppHelper.GetUserList();
            var q = from ee in l
                    where ee.UserID != string.Empty &&
                    ee.LastActive > DateTime.Now.AddMinutes(0 - this.ActiveMinutes)
                    select ee;
            return q;
        }
    }
}
