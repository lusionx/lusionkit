using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace SLWeb.WS
{
    /// <summary>
    /// Message 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class Message : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public void Invite(string from, string to)
        {
            var mlist = Code.AppHelper.GetMessageList();
            lock (mlist)
            {
                mlist.Add(new SLWeb.Code.Message
                {
                    From = from,
                    To = to,
                    Context = string.Format("{0}邀请你游戏", from),
                    Type = SLWeb.Code.EMsgType.Invite,
                    Enable = true,
                    CreateTime = DateTime.Now,
                    ID = Guid.NewGuid()
                });
            }
        }

        [WebMethod]
        public object[] GetInvite(string to)
        {
            var mlist = Code.AppHelper.GetMessageList();
            var q = from ee in mlist
                    where ee.To == to && ee.Type == SLWeb.Code.EMsgType.Invite && ee.Enable == true
                    select new { Context = ee.Context, ID = ee.ID };
            return q.ToArray();
        }

        [WebMethod]
        public void DropMessang(Guid gg)
        {
            var mlist = Code.AppHelper.GetMessageList();
            lock (mlist)
            {
                var q = from ee in mlist
                        where ee.ID == gg
                        select ee;
                q.Single().Enable = false;
            }
        }
    }
}
