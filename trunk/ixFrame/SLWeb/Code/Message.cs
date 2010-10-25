using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SLWeb.Code
{
    public class Message
    {
        public string From { get; set; }

        public string To { set; get; }

        public EMsgType Type { set; get; }

        public string Context { set; get; }

        public bool Enable { set; get; }

        public DateTime? CreateTime { set; get; }

        public Guid ID { get; set; }
    }
}
