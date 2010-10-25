using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SLWeb.Code
{
    public class Room
    {
        public Guid RoomID { get; set; }

        public List<string> Users { get; set; }

        public const int Max = 2;

        public DateTime? LastActive { get; set; }
    }
}
