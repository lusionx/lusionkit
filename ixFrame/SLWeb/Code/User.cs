using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SLWeb.Code
{
    public class User
    {
        public string UserID { set; get; }

        public DateTime? LastActive { set; get; }

        public bool Leisure { set; get; }
    }
}
