using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tool;
using System.Data.Linq.Mapping;

namespace SilverMoon.DAL
{
    public partial class sm_biz_Workpiece
    {
        public string FullSerial
        {
            get
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(this.MachineID);
                sb.Append(this.Type);
                sb.Append((char)(this.Date.Month + '@'));
                sb.Append(this.Date.Day.ToString("d2"));
                sb.Append(this.Shift);
                sb.Append(this.Serial.ToString("d3"));
                return sb.ToString();
            }
        }

        partial void OnStateChanging(int value)
        {
            this.Time = System.DateTime.Now;
        }       
    }
}
