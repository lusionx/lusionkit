using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Perpor.DAL;
using Newtonsoft.Json;

namespace Perpor.BLL
{
    public static class DALExt
    {
        public static PPage Page(this core_UserPage up)
        {
            return JsonConvert.DeserializeObject<PPage>(up.Layout);
        }
    }
}
