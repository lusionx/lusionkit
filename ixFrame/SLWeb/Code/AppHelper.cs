using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SLWeb.Code
{
    public class AppHelper
    {
        public static List<Code.User> GetUserList()
        {
            HttpApplicationState app = HttpContext.Current.Application;
            return app[EAppKey.UserOnline.ToString()] as List<Code.User>;
        }

        public static List<Code.Room> GetRoomList()
        {
            HttpApplicationState app = HttpContext.Current.Application;
            return app[EAppKey.Room.ToString()] as List<Code.Room>;
        }

        public static List<Code.Message> GetMessageList()
        {
            HttpApplicationState app = HttpContext.Current.Application;
            return app[EAppKey.Message.ToString()] as List<Code.Message>;
        }

    }
}
