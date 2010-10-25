using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace SLWeb.WS
{
    /// <summary>
    /// Room 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class Room : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public bool Create()
        {
            var rooms = Code.AppHelper.GetRoomList();
            lock (rooms)
            {
                rooms.Add(new SLWeb.Code.Room
                {
                    RoomID = Guid.NewGuid(),
                    Users = new List<string>(),
                    LastActive = DateTime.Now
                });
            }
            return true;
        }

        [WebMethod]
        public bool AddUser(Guid roomid, string uid)
        {
            var rooms = Code.AppHelper.GetRoomList();
            lock (rooms)
            {
                var q = from ee in rooms
                        where ee.RoomID == roomid
                        select ee;
                var players = q.Single().Users;
                if (players.Contains(uid))
                {
                    return false;
                }
                else
                {
                    players.Add(uid);
                    return true;
                }
            }

        }
    }
}
