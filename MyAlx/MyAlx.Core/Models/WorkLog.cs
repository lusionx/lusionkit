using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAlx.Core.Entity;

namespace MyAlx.Core.Models
{
    public class WorkLog : IModel<worklog>
    {
        public void InitFromEntityObject(worklog dModel)
        {
            Log = dModel.Log;
        }

        public string Log { get; set; }
    }
}
