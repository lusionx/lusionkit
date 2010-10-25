using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tool;
using System.Data.Linq;
using SilverMoon.BAL;

namespace SilverMoon.DAL
{
    public partial class SMDataDataContext
    {
        public sm_biz_Workpiece CreateWorkpiece(string fullno)
        {
            int year = Serial.Year;
            var _o = new sm_biz_Workpiece();
            _o.MachineID = fullno.ToCharArray(0, 1)[0];
            _o.Type = fullno.ToCharArray(1, 1)[0];
            _o.Date = new DateTime(year, fullno.ToCharArray(2, 1)[0] - '@', fullno.Substring(3, 2).ToInt32());
            _o.Shift = fullno.ToCharArray(5, 1)[0];
            _o.Serial = SafeConvert.ToInt16(fullno.Substring(6, 3));
            _o.State = 0;
            return _o;
        }

        public override void SubmitChanges(System.Data.Linq.ConflictMode failureMode)
        {
            try
            {
                base.SubmitChanges(System.Data.Linq.ConflictMode.ContinueOnConflict);
            }
            catch (ChangeConflictException)
            {
                foreach (var x in this.ChangeConflicts)
                {
                    x.Resolve(System.Data.Linq.RefreshMode.KeepCurrentValues);
                }
            }
        }

        public sm_biz_Workpiece GetWorkpiece(string fullno)
        {
            var o = CreateWorkpiece(fullno);
            var q = this.sm_biz_Workpiece.Where(x =>
                x.MachineID == o.MachineID &&
                x.Type == o.Type &&
                x.Date == o.Date &&
                x.Shift == o.Shift &&
                x.Serial == o.Serial);
            return q.FirstOrDefault();
        }

        public string ShowState(object o)
        {
            return this.sm_biz_State.Single(x => x.ID == o.ToString().ToInt32()).Name;
        }
    }
}
