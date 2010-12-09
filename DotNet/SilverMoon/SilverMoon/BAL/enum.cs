using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SilverMoon.BAL
{
    public enum SerialType
    {
        MachineID = 10,
        Type = 20,
        Month = 30,
        Day = 40,
        Shift = 50
    }

    public enum WorkStep
    {
        DC = 10,
        FI浇口 = 20,
        FI外观 = 30,
        检查系 = 40,
        TFTE = 50,        
    }
}
