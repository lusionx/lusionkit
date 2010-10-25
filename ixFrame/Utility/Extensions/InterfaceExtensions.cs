using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility.Extensions
{
    public static class InterfaceExtensions
    {
        public static bool HasEqual(this System.Collections.IEnumerator ie, object oo)
        {
            ie.Reset();
            while (ie.MoveNext())
            {
                if (ie.Current.Equals(oo))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
